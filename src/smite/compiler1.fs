\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS  DUP CELL+ >R  @ CURRENT-COMPILE, ;

ALSO ASSEMBLER
: EXECUTE   STATE @ IF  $29 C,  NOPALIGN  ELSE [ $29 C,  NOPALIGN ]
   THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  $15 C,  $29 C,  NOPALIGN  ELSE [ $15 C,  $29 C,  NOPALIGN ]
   THEN ; IMMEDIATE
PREVIOUS


\ Data structures

: LITERAL   $2E C,  NOPALIGN  , ;
IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr )   1+ ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS @  BRANCH ;