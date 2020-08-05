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


INCLUDE" assembler.fs"
INCLUDE" save.fs"


\ Meta-compiler utilities

ALSO ASSEMBLER
64 1024 * CONSTANT DICTIONARY-SIZE


\ STUB FOO creates an empty word.
\ This is used to POSTPONE target words that may not exist on the host.
: STUB   BL WORD  HEADER ;

\ Create stubs for words that may not exist on host
STUB IP
STUB DOCOL
STUB LINK
STUB UNLINK
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

: ADD-RESOLVE   DUP @  LAST CELL+  TUCK  !  SWAP ! ;
: (DOES>)   >DOES> ADD-RESOLVE ;
: DOES-LINK,   0 , ;
: DOES>   LAST POSTPONE RELATIVE-LITERAL  POSTPONE (DOES>)  UNLINK,  ALIGN
   HERE LAST TUCK - CELL/  SWAP >INFO
   DUP @ $FFFF0000 AND ROT OR  SWAP !  DOES-LINK, ; IMMEDIATE COMPILING


VOCABULARY META  ALSO META DEFINITIONS
FOREIGN  ' NON-META?  ' 'SELECTOR >BODY REL! \ build meta-compiler using native compiler
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
: (POSTPONE)   >NAME  ?FIND CURRENT-COMPILE, ;

\ POSTPONE itself must be defined in FORTH, so that it can be run during the
\ compilation of the rest of META, which is FOREIGN while it is being built.
ALSO FORTH DEFINITIONS

: POSTPONE   BL WORD FIND ?DUP 0= IF UNDEFINED THEN 0> IF >COMPILE REL@
   CURRENT-COMPILE, ELSE [ ' CURRENT-RELATIVE-LITERAL CURRENT-COMPILE, ] C" (POSTPONE)" ?FIND CURRENT-COMPILE, THEN ;
IMMEDIATE COMPILING

META DEFINITIONS  PREVIOUS  \ use META POSTPONE and LINK,
INCLUDE" compiler-postpone.fs"
ALSO META  FOREIGN  PREVIOUS


INCLUDE" code.fs"
INCLUDE" util.fs"
INCLUDE" control2.fs"
INCLUDE" control3.fs"
INCLUDE" strings2b.fs"
INCLUDE" compiler2.fs"
INCLUDE" interpreter3.fs"
INCLUDE" compiler4.fs"
INCLUDE" compiler5.fs"
\ FIXME: wrap this definition rather than copy-and-modify
: [']   ALIGN '  POSTPONE RELATIVE-LITERAL ; IMMEDIATE COMPILING
INCLUDE" defer-fetch-store.fs"
INCLUDE" defining.fs"
\ FIXME: wrap this definition rather than copy-and-modify
: TO   ' >BODY ! ;
   :NONAME   ' >BODY  ALIGN POSTPONE RELATIVE-LITERAL  POSTPONE ! ;IMMEDIATE
INCLUDE" vocabulary.fs"
INCLUDE" resolver-branch.fs"

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

NATIVE  ' LOCAL?  ' 'SELECTOR >BODY REL! \ now meta-compiler is built, allow it to run

ALSO FORTH   \ use FORTH's VOCABULARY
VOCABULARY NEW-FORTH   \ define the new root vocabulary
PREVIOUS

SIZE DICTIONARY CROSS  \ define a new dictionary
' CURRENT-COMPILE, >BODY @   \ save compiler
' CURRENT-LITERAL >BODY @
' CURRENT-RELATIVE-LITERAL >BODY @
' COMPILE, ' CURRENT-COMPILE, >BODY REL!   \ use target compiler
' LITERAL ' CURRENT-LITERAL >BODY REL!
' RELATIVE-LITERAL ' CURRENT-RELATIVE-LITERAL >BODY REL!
'FORTH   \ save value of 'FORTH
' CROSS >BODY @  INCLUDE" init-space.fs" CELLS -  TO 'FORTH
   \ make 'FORTH point to the start of it minus the initial branch

ALSO CROSS NEW-FORTH DEFINITIONS FOREIGN
INCLUDE" primitives.fs"
INCLUDE" system-params.fs"
[UNDEFINED] MINIMAL-PRIMITIVES [IF]
   INCLUDE" extra-primitives.fs"
[THEN]

INCLUDE" highlevel.fs"
INCLUDE" initialize.fs"

' NEW-FORTH >BODY REL@ REL@  ' FORTH >BODY REL@  REL!   \ patch root wordlist
' FORTH >BODY REL@ CELL+  ' CHAIN >BODY  REL!   \ patch CHAIN
' FORTH >NAME CELL-  0 OVER !  CELL-  0 SWAP !   \ zero FORTH wordlist's info and link fields
' VALUE >DOES>  ALSO META  RESOLVES VALUE  PREVIOUS \ resolve run-times
' DEFER >DOES>  ALSO META  RESOLVES DEFER  PREVIOUS
' VOCABULARY >DOES>  ALSO META  RESOLVES VOCABULARY  PREVIOUS
' ABORT ' SCAN-TEST >BODY REL!
' ABORT ' VISIBLE? >BODY REL!
' NEW-FORTH >BODY REL@ REL@  PREVIOUS   \ leave initial branch target on the stack

ALIGN HERE 'FORTH -   \ ( length ) of binary image
ROOT HERE OVER ALLOT   \ make space for binary image ( length start )
TUCK   \ ( start length start )
'FORTH  INCLUDE" init-space.fs" CELLS   \ ( s l s 'FORTH nCELLS )
TUCK + -ROT +   \ ( s l 'FORTH+nCELLS s+nCELLS )
2 PICK MOVE   \ copy dictionary ( s l )

OVER INCLUDE" init-space.fs" CELLS ERASE   \ zero initial branch space
OVER SWAP 2SWAP 'FORTH ROT  NATIVE-CALL   \ patch in initial branch

S" pforth-new" SAVE-OBJECT   \ write system image

PREVIOUS PREVIOUS DEFINITIONS   \ restore original order
TO 'FORTH   \ restore 'FORTH
TO CURRENT-RELATIVE-LITERAL   \ restore original compiler
TO CURRENT-LITERAL
TO CURRENT-COMPILE,

ALSO META
??STACK   \ check stack is balanced
PREVIOUS
