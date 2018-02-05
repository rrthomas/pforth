\ Metacompile pForth
\ Reuben Thomas   started 15/4/96

MARKER DISPOSE
DEPTH VALUE INITIAL-DEPTH   \ Note initial stack depth
INCLUDE" platform.fs"
CR .( Metacompiling pForth for ) "PLATFORM TYPE .( : )


INCLUDE" target-util.fs"
INCLUDE" assembler.fs"


\ Meta-compiler utilities

\ ARM-specific meta-compiler utilities (FIXME)
: '?   BL WORD FIND  0= IF  DROP 0  THEN ;

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
      .S ." stack not balanced" CR
   THEN ;


ALSO ASSEMBLER

INCLUDE" save.fs"
INCLUDE" util.fs"

INCLUDE" compiler.fs"
INCLUDE" compiler1.fs"


\ Special definition of POSTPONE, to cope with FOREIGN vocabularies

: FIND-AND-COMPILE,   ( c-addr -- )
   FIND  0= IF  UNDEFINED  THEN  CURRENT-COMPILE, ;
: (POSTPONE)   R> R>ADDRESS DUP CELL+ >R @ >NAME  FIND-AND-COMPILE, ;

\ POSTPONE itself must be defined in FORTH, so that it can be run during the
\ compilation of the rest of META, which is FOREIGN while it is being built.
NATIVE ALSO FORTH DEFINITIONS
: POSTPONE   BL WORD FIND ?DUP 0= IF UNDEFINED THEN 0> IF >COMPILE @
   CURRENT-COMPILE, ELSE C" (POSTPONE)" FIND-AND-COMPILE, ALIGN <'FORTH , THEN ;
IMMEDIATE COMPILING
PREVIOUS   \ use META POSTPONE and LINK,
INCLUDE" compiler-postpone.fs"
INCLUDE" (does).fs"
INCLUDE" does.fs"
INCLUDE" does-resolver.fs"
ALSO FORTH  META DEFINITIONS FOREIGN  PREVIOUS


INCLUDE" control1.fs"
INCLUDE" control2.fs"
INCLUDE" compiler2.fs"
INCLUDE" interpreter4.fs"
INCLUDE" compiler4.fs"
INCLUDE" compiler5.fs"
INCLUDE" defining.fs"
INCLUDE" vocabulary.fs"
INCLUDE" resolver.fs"

DOES>-RESOLVER (VALUE) WILL-DO VALUE
DOES>-RESOLVER (VECTOR) WILL-DO VECTOR
DOES>-RESOLVER (VOCABULARY) WILL-DO VOCABULARY
3 REDEFINER >COMPILERS<

PREVIOUS


\ Constants

32 1024 * CONSTANT SIZE
INCLUDE" throw-contents.fs"


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
' CROSS >BODY @  #THREADS 1+ CELLS -  TO 'FORTH
   \ make 'FORTH point to the start of it minus the threads table and
   \ initial branch
INCLUDE" target-forth.fs" TO TARGET-'FORTH

ALSO CROSS NEW-FORTH DEFINITIONS FOREIGN
>COMPILERS<
'FORTH <'FORTH   \ 'FORTH of new system
INCLUDE" primitives.fs"

INCLUDE" highlevel.fs"
INCLUDE" initialize.fs"

HERE <'FORTH  ' ROOTDP >BODY !   \ patch ROOTDP
HEX
' NEW-FORTH >BODY @ @ <'FORTH  ' FORTH >BODY @ >'FORTH  !
   \ patch root wordlist
' FORTH >BODY @ CELL+  ' CHAIN >BODY  !   \ patch CHAIN
' FORTH >NAME 8 -  'FORTH <'FORTH CELL+ OVER !  4 -  0 OVER !  4 -  0 SWAP !
   \ patch FORTH wordlist
1  ' KERNEL >NAME 8 -  !   \ patch #WORDLISTS
DECIMAL
' VALUE >DOES> RESOLVES (VALUE)   \ resolve run-times
' VECTOR >DOES> RESOLVES (VECTOR)
' VOCABULARY >DOES> RESOLVES (VOCABULARY)
' NEW-FORTH >BODY @ @  PREVIOUS  DUP RELOCATE   \ relocate the new dictionary
   \ leave initial branch target on the stack
>COMPILERS<

ALIGN HERE 'FORTH -   \ ( length ) of binary image
ROOT HERE OVER ALLOT   \ make space for binary image ( length start )
TUCK   \ ( start length start )
'FORTH  #THREADS 1+ CELLS   \ ( s l s 'FORTH (#THREADS+1)CELLS )
TUCK + -ROT +   \ ( s l 'FORTH+(#T+1)CELLS H+(#T+1)CELLS )
2 PICK MOVE   \ copy dictionary ( s l )
OVER CELL+ CURRENT-VOLUME @ @  SWAP   \ ( s l 'THREADS s+CELL )
#THREADS CELLS MOVE   \ copy threads ( s l )

OVER TUCK DUP 2ROT  + 'FORTH -  >COMPILERS< BRANCH >COMPILERS<   \ patch in initial branch

S" pforth" SAVE   \ write system image

KERNEL PREVIOUS DEFINITIONS   \ restore original order
TO 'FORTH   \ restore 'FORTH
TO TARGET-'FORTH   \ restore TARGET-'FORTH
TO CURRENT-LITERAL   \ restore original compiler
TO CURRENT-COMPILE,

ALSO META
??STACK   \ check stack is balanced
PREVIOUS
