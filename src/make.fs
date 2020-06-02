\ Metacompile pForth base image
\
\ (c) Reuben Thomas 1996-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

MARKER DISPOSE

\ Halt immediately on exception, for easier debugging
\ FIXME: Only halt on memory exceptions (or use core dump facility)
\ : HALT-HANDLER   HALT ;
\ ' HALT-HANDLER 'THROW!

DEPTH VALUE INITIAL-DEPTH   \ Note initial stack depth
INCLUDE" platform.fs"
CR .( Metacompiling pForth for ) "PLATFORM TYPE .( : )


INCLUDE" target-util.fs"
INCLUDE" relocate.fs"   \ Need target's version of RELOCATE for call below
INCLUDE" assembler.fs"
INCLUDE" save.fs"


\ Meta-compiler utilities

ALSO ASSEMBLER
'FORTH VALUE TARGET-'FORTH   \ While building meta-compiler, don't relocate
64 1024 * CONSTANT DICTIONARY-SIZE
CREATE RELOCATION-TABLE  HERE 0 ,  HERE 0 ,  HERE DICTIONARY-SIZE CELL/ ALLOT
VALUE RELOCATIONS  VALUE #RELOCATIONS
: (ADD-RELOCATION)   ( a-addr type -- )
   OVER CELL 1- AND ABORT" Relocation address must be aligned!"
   OVER RELOCATION-TABLE < ABORT" Invalid relocation!"
   DUP CELL < INVERT ABORT" Relocation type must be < CELL!"
   OR  #RELOCATIONS @ CELLS RELOCATIONS + !
   1 #RELOCATIONS +! ;
: ADD-RELOCATION   ( a-addr -- )
   0 (ADD-RELOCATION) ;
: ADDRESS!   DUP ADD-RELOCATION  ! ;
: ADDRESS,   HERE ADD-RELOCATION  , ;

: <'FORTH   'FORTH -  TARGET-'FORTH + ;
: >'FORTH   'FORTH +  TARGET-'FORTH - ;


\ STUB FOO creates an empty word.
\ This is used to POSTPONE target words that may not exist on the host.
: STUB   BL WORD  HEADER ;

\ Create stubs for words that may not exist on host
STUB IP
STUB DOCOL
STUB (LITERAL)
STUB (BRANCH)
STUB (?BRANCH)
STUB (LOOP)
STUB (+LOOP)
STUB UNLOOP
STUB (CREATE)
STUB (C")
STUB (S")


\ Machinery for compiling forward references to defining words' DOES> code

\ >DOES>, given the xt of a defining word, returns the address of the DOES>
\ code.
: >DOES>   ( xt -- 'does )   DUP >INFO @ $FFFF AND CELLS  + ;

: ADD-RESOLVE   DUP @  LAST CELL+  TUCK  !  SWAP ! ;
: (DOES>)   ADD-RESOLVE ;
: DOES-LINK,   0 , ;
\ FIXME: Why is this needed here as well as below (under INCLUDE" defining.fs")?
: DOES>   LITERAL, HERE 0 ,  POSTPONE (DOES>)  UNLINK,
   NOPALIGN  HERE TUCK  LAST TUCK - CELL/  SWAP >INFO
   DUP @ ROT OR  SWAP !  ADDRESS!  DOES-LINK, ; IMMEDIATE COMPILING


VOCABULARY META  ALSO META DEFINITIONS
FOREIGN  ' NON-META? TO 'SELECTOR \ build meta-compiler using native compiler
DECIMAL

\ Check stack is balanced
: ??STACK
   DEPTH INITIAL-DEPTH <> IF
      .S ." stack not balanced" CR  ABORT
   THEN ;


INCLUDE" relocate-compiler.fs"
INCLUDE" native-call.fs"
INCLUDE" compiler1.fs"


\ Special definition of POSTPONE, to cope with FOREIGN vocabularies

: ?FIND   ( c-addr -- xt )   FIND  0= IF  UNDEFINED  THEN ;
: (POSTPONE)   R> R>ADDRESS ALIGNED DUP CELL+ >R @ >NAME  ?FIND CURRENT-COMPILE, ;
: (RELATIVE-POSTPONE)   R> R>ADDRESS ALIGNED DUP CELL+ >R @ >NAME  ?FIND HERE - , ;

\ POSTPONE itself must be defined in FORTH, so that it can be run during the
\ compilation of the rest of META, which is FOREIGN while it is being built.
ALSO FORTH DEFINITIONS
: POSTPONE   BL WORD FIND ?DUP 0= IF UNDEFINED THEN 0> IF >COMPILE @
   CURRENT-COMPILE, ELSE C" (POSTPONE)" ?FIND CURRENT-COMPILE, ALIGN ADDRESS, THEN ;
IMMEDIATE COMPILING
\ FIXME: RELATIVE-POSTPONE exists only for mit-threaded, and only in make.fs
: RELATIVE-POSTPONE   BL WORD FIND ?DUP 0= IF UNDEFINED THEN 0> IF >COMPILE @
   CURRENT-COMPILE, ELSE C" (RELATIVE-POSTPONE)" ?FIND CURRENT-COMPILE, ALIGN ADDRESS, THEN ;
IMMEDIATE COMPILING

META DEFINITIONS  PREVIOUS  \ use META POSTPONE and LINK,
INCLUDE" bracket-does.fs"
INCLUDE" compiler-postpone.fs"
ALSO META  FOREIGN  PREVIOUS


\ ADDRESS- variants are not to be compiled, so no compilation method needed
: ADDRESS-TO   ' >BODY  DUP ADD-RELOCATION  ! ;
: HEADER   ( c-addr -- )
   HEADER
   LAST >LINK  DUP @ IF  ADD-RELOCATION  ELSE DROP  THEN ;
INCLUDE" code.fs"
INCLUDE" util.fs"
: ADDRESS-LITERAL   CURRENT-LITERAL,  HERE ADD-RELOCATION  , ; IMMEDIATE
INCLUDE" control2.fs"
INCLUDE" control3.fs"
INCLUDE" strings2b.fs"
INCLUDE" compiler2.fs"
INCLUDE" interpreter3.fs"
INCLUDE" compiler4.fs"
: ;IMMEDIATE   POSTPONE ;  IMMEDIATE  LAST >COMPILE ADDRESS! ; IMMEDIATE COMPILING
INCLUDE" compiler5.fs"
\ FIXME: wrap this definition rather than copy-and-modify
: [']   ALIGN '  POSTPONE ADDRESS-LITERAL ; IMMEDIATE COMPILING
INCLUDE" defer-fetch-store.fs"
INCLUDE" defining.fs"
\ FIXME: wrap these definitions rather than copy-and-modify
: IS   ' >BODY  DUP ADD-RELOCATION  !  ; \ FIXME: really DEFER!
   :NONAME   ALIGN '  POSTPONE ADDRESS-LITERAL  POSTPONE DEFER! ;IMMEDIATE
: TO   ' >BODY ! ;
   :NONAME   ' >BODY  ALIGN POSTPONE ADDRESS-LITERAL  POSTPONE ! ;IMMEDIATE
: DOES>   LITERAL, HERE 0 ,  POSTPONE (DOES>)  UNLINK,
   NOPALIGN  HERE TUCK  LAST TUCK - CELL/  SWAP >INFO
   DUP @ ROT OR  SWAP !  ADDRESS!  DOES-LINK, ; IMMEDIATE COMPILING
INCLUDE" vocabulary.fs"
INCLUDE" resolver-branch.fs"
: IMMEDIATE   IMMEDIATE  LAST >COMPILE ADD-RELOCATION ;

: RESOLVES   ( name )   ( a-addr -- )
   '
   >DOES> @                          \ get first address in branch list
   BEGIN  ?DUP WHILE                 \ chain down list until null marker
      DUP  @                         \ get next address in list
      -ROT 2DUP SWAP RESOLVER-BRANCH \ compile the call or branch
      SWAP
   REPEAT
   DROP ;                            \ drop a-addr


\ Constants

DICTIONARY-SIZE CONSTANT SIZE
INCLUDE" call-cells.fs" CONSTANT #TARGET-CALL-CELLS

NATIVE  ' LOCAL? TO 'SELECTOR \ now meta-compiler is built, allow it to run

ALSO FORTH   \ use FORTH's VOCABULARY
VOCABULARY NEW-FORTH   \ define the new root vocabulary
PREVIOUS

SIZE DICTIONARY CROSS  \ define a new dictionary
' CURRENT-COMPILE, >BODY @   \ save compiler
' CURRENT-LITERAL >BODY @
' CURRENT-LITERAL, >BODY @
' COMPILE, TO CURRENT-COMPILE,   \ use target compiler
' LITERAL TO CURRENT-LITERAL
' LITERAL, TO CURRENT-LITERAL,
'FORTH   \ save value of 'FORTH
' CROSS >BODY @  INCLUDE" init-space.fs" CELLS -  TO 'FORTH
   \ make 'FORTH point to the start of it minus the initial branch
'FORTH 5 ROLL !   \ store 'FORTH in RELOCATION-TABLE
0 #RELOCATIONS !   \ FIXME: incorrect relocations will have been added by
\ {,RELATIVE-}POSTPONE compiling the meta-compiler
INCLUDE" target-forth.fs" TO TARGET-'FORTH \ Set value for relocation

ALSO CROSS NEW-FORTH DEFINITIONS FOREIGN
TARGET-'FORTH   \ 'FORTH of new system
INCLUDE" primitives.fs"
INCLUDE" inner-interpreter.fs"
INCLUDE" system-params.fs"
[UNDEFINED] MINIMAL-PRIMITIVES [IF]
   INCLUDE" extra-primitives.fs"
[THEN]

INCLUDE" highlevel.fs"
INCLUDE" initialize.fs"

' NEW-FORTH >BODY @ @  ' FORTH >BODY @  ADDRESS!   \ patch root wordlist
' FORTH >BODY @ CELL+  ' CHAIN >BODY  ADDRESS!   \ patch CHAIN
' FORTH >BODY DUP @  SWAP ADDRESS!   \ relocate FORTH
' FORTH >NAME 4 -  0 OVER !  4 -  0 SWAP !   \ patch FORTH wordlist
' VALUE >DOES>  ALSO META  RESOLVES VALUE  PREVIOUS \ resolve run-times
' DEFER >DOES>  ALSO META  RESOLVES DEFER  PREVIOUS
' VOCABULARY >DOES>  ALSO META  RESOLVES VOCABULARY  PREVIOUS
' ABORT ADDRESS-TO SCAN-TEST
' ABORT ADDRESS-TO VISIBLE?
\ patch ROOTDP: HERE plus relocations table plus one cell for relocation
\ generated by ADDRESS!
HERE  #RELOCATIONS @ 3 +  CELLS +  ' ROOTDP >BODY ADDRESS!
' NEW-FORTH >BODY @ @  PREVIOUS   \ leave initial branch target on the stack

\ Normalize relocations relative to TARGET-'FORTH, to make the built image
\ reproducible.
'FORTH TARGET-'FORTH RELOCATION-TABLE RELOCATE

ALIGN HERE 'FORTH -   \ ( length ) of binary image
ROOT HERE OVER ALLOT   \ make space for binary image ( length start )
TUCK   \ ( start length start )
'FORTH  INCLUDE" init-space.fs" CELLS   \ ( s l s 'FORTH nCELLS )
TUCK + -ROT +   \ ( s l 'FORTH+nCELLS s+nCELLS )
2 PICK MOVE   \ copy dictionary ( s l )
RELOCATION-TABLE #RELOCATIONS @ 2 + CELLS   \ ( s l 'relocation-table relocation-table-length )
TUCK HERE  OVER ALLOT  SWAP MOVE   \ copy relocation table ( s l relocation-table-length )
+   \ ( s l' )

OVER INCLUDE" init-space.fs" CELLS ERASE   \ zero initial branch space
OVER SWAP 2SWAP 'FORTH ROT  NATIVE-CALL   \ patch in initial branch

S" pforth-new" SAVE-OBJECT   \ write system image

PREVIOUS PREVIOUS DEFINITIONS   \ restore original order
TO 'FORTH   \ restore 'FORTH
TO CURRENT-LITERAL,   \ restore original compiler
TO CURRENT-LITERAL
TO CURRENT-COMPILE,

ALSO META
??STACK   \ check stack is balanced
PREVIOUS
