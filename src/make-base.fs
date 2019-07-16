#!/usr/bin/env pforth
\ Metacompile pForth base image
\ Reuben Thomas   started 15/4/96

MARKER DISPOSE

DEPTH VALUE INITIAL-DEPTH   \ Note initial stack depth
INCLUDE" platform.fs"
CR .( Metacompiling pForth for ) "PLATFORM TYPE .( : )


INCLUDE" target-util.fs"
INCLUDE" assembler.fs"


\ Meta-compiler utilities

ALSO ASSEMBLER
INCLUDE" util.fs"


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


\ Machinery for compiling forward references to defining words' DOES> code

\ >DOES>, given the xt of a defining word, returns the address of the DOES>
\ code.
: >DOES>   ( xt -- 'does )   DUP >INFO @ $FFFF AND CELLS  + ;

: ADD-RESOLVE   DUP @  LAST CELL+  TUCK  !  SWAP ! ;
: (DOES>)   R> R>ADDRESS CELL+  ADD-RESOLVE ;
: DOES-LINK,   0 , ;
INCLUDE" does.fs"


VOCABULARY META  ALSO META DEFINITIONS
FOREIGN  ' NON-META? TO 'SELECTOR \ build meta-compiler using native compiler
DECIMAL

\ Relocate new dictionary
: >ADDRESS<   DUP @  ?DUP IF  <'FORTH SWAP !  ELSE DROP  THEN ;
: RELOCATE   ( a-addr -- )
   CURRENT-VOLUME @ @  #THREADS CELLS  OVER + SWAP DO
      I @  ?DUP IF  <'FORTH I !  THEN
   CELL +LOOP
   BEGIN
      >LINK
      DUP @ ?DUP WHILE
      OVER 3 CELLS +   \ hack to go from link field to code field
      >COMPILE >ADDRESS<
      OVER CELL+   \ hack to go from link field to thread field
      >ADDRESS<
      DUP <'FORTH ROT !
   REPEAT
   DROP ;

\ Check stack is balanced
: ??STACK
   DEPTH INITIAL-DEPTH <> IF
      .S ." stack not balanced" CR  ABORT
   THEN ;


INCLUDE" compiler.fs"
INCLUDE" native-branch.fs"
INCLUDE" compiler1.fs"
INCLUDE" save.fs"


\ Special definition of POSTPONE, to cope with FOREIGN vocabularies

: FIND-AND-COMPILE,   ( c-addr -- )
   FIND  0= IF  UNDEFINED  THEN  CURRENT-COMPILE, ;
: (POSTPONE)   R> R>ADDRESS ALIGNED DUP CELL+ >R @ >NAME  FIND-AND-COMPILE, ;

\ POSTPONE itself must be defined in FORTH, so that it can be run during the
\ compilation of the rest of META, which is FOREIGN while it is being built.
ALSO FORTH DEFINITIONS
: POSTPONE   BL WORD FIND ?DUP 0= IF UNDEFINED THEN 0> IF >COMPILE @
   CURRENT-COMPILE, ELSE C" (POSTPONE)" FIND-AND-COMPILE, ALIGN <'FORTH , THEN ;
IMMEDIATE COMPILING

META DEFINITIONS  PREVIOUS  \ use META POSTPONE and LINK,
INCLUDE" bracket-does.fs"
INCLUDE" compiler-postpone.fs"
ALSO META  FOREIGN  PREVIOUS


INCLUDE" control2.fs"
INCLUDE" control3.fs"
INCLUDE" compiler2.fs"
INCLUDE" interpreter3.fs"
INCLUDE" compiler4.fs"
INCLUDE" compiler5.fs"
INCLUDE" defer-fetch-store.fs"
INCLUDE" defining.fs"
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

64 1024 * CONSTANT SIZE
INCLUDE" branch-cells.fs" CONSTANT #TARGET-BRANCH-CELLS

NATIVE  ' LOCAL? TO 'SELECTOR \ now meta-compiler is built, allow it to run

VOLUME NEW   \ define a new hash table
NEW   \ make the new dictionary the current volume
ALSO FORTH   \ use FORTH's VOCABULARY
VOCABULARY NEW-FORTH   \ define the new root vocabulary
PREVIOUS

SIZE DICTIONARY CROSS  \ define a new dictionary
' CURRENT-COMPILE, >BODY @   \ save compiler
' CURRENT-LITERAL >BODY @
' COMPILE, TO CURRENT-COMPILE,   \ use target compiler
' LITERAL TO CURRENT-LITERAL   \ use target compiler
TARGET-'FORTH   \ save value of TARGET-'FORTH
'FORTH   \ save value of 'FORTH
' CROSS >BODY @  #THREADS INCLUDE" init-space.fs" + CELLS -  TO 'FORTH
   \ make 'FORTH point to the start of it minus the threads table and
   \ initial branch
INCLUDE" target-forth.fs" TO TARGET-'FORTH

ALSO CROSS NEW-FORTH DEFINITIONS FOREIGN
'FORTH <'FORTH   \ 'FORTH of new system
INCLUDE" primitives.fs"
INCLUDE" inner-interpreter.fs"
INCLUDE" system-params.fs"
[UNDEFINED] MINIMAL-PRIMITIVES [IF]
   INCLUDE" extra-primitives.fs"
[THEN]

INCLUDE" highlevel.fs"
INCLUDE" initialize.fs"

HERE <'FORTH  ' ROOTDP >BODY !   \ patch ROOTDP
' NEW-FORTH >BODY @ @ <'FORTH  ' FORTH >BODY @ >'FORTH  !
   \ patch root wordlist
' FORTH >BODY @ CELL+  ' CHAIN >BODY  !   \ patch CHAIN
' FORTH >NAME 8 -  'FORTH <'FORTH INCLUDE" init-space.fs" CELLS + OVER !  4 -  0 OVER !  4 -  0 SWAP !
   \ patch FORTH wordlist
1  ' KERNEL >NAME 8 -  !   \ patch #WORDLISTS
' VALUE >DOES>  ALSO META  RESOLVES VALUE  PREVIOUS \ resolve run-times
' DEFER >DOES>  ALSO META  RESOLVES DEFER  PREVIOUS
' VOCABULARY >DOES>  ALSO META  RESOLVES VOCABULARY  PREVIOUS
' ABORT <'FORTH TO SCAN-TEST
' ABORT <'FORTH TO VISIBLE?
' NEW-FORTH >BODY @ @  PREVIOUS  DUP RELOCATE   \ relocate the new dictionary
   \ leave initial branch target on the stack

ALIGN HERE 'FORTH -   \ ( length ) of binary image
ROOT HERE OVER ALLOT   \ make space for binary image ( length start )
TUCK   \ ( start length start )
'FORTH  #THREADS INCLUDE" init-space.fs" + CELLS   \ ( s l s 'FORTH (#THREADS+n)CELLS )
TUCK + -ROT +   \ ( s l 'FORTH+(#T+n)CELLS H+(#T+n)CELLS )
2 PICK MOVE   \ copy dictionary ( s l )
OVER INCLUDE" init-space.fs" CELLS + CURRENT-VOLUME @ @  SWAP   \ ( s l 'THREADS s+(n CELLS) )
#THREADS CELLS MOVE   \ copy threads ( s l )

OVER INCLUDE" init-space.fs" CELLS ERASE   \ zero initial branch space
OVER SWAP 2SWAP 'FORTH ROT  NATIVE-BRANCH   \ patch in initial branch

S" pforth-new" SAVE-OBJECT   \ write system image

KERNEL PREVIOUS PREVIOUS DEFINITIONS   \ restore original order
TO 'FORTH   \ restore 'FORTH
TO TARGET-'FORTH   \ restore TARGET-'FORTH
TO CURRENT-LITERAL   \ restore original compiler
TO CURRENT-COMPILE,

ALSO META
??STACK   \ check stack is balanced
PREVIOUS
