\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS  DUP CELL+ >R  @ CURRENT-COMPILE, ;

ALSO ASSEMBLER
: EXECUTE   STATE @ IF  $50 C,  NOPALIGN  ELSE [ $50 C,  NOPALIGN ]
   THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  $39 C,  $50 C,  NOPALIGN  ELSE [ $39 C,  $50 C,  NOPALIGN ]
   THEN ; IMMEDIATE
PREVIOUS


\ Data structures

: LITERAL   DUP HERE 1+ FITS IF  $5D C, FIT,  ELSE $5C C,  NOPALIGN  ,  THEN ;
IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr )   1+ ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS @  BRANCH ;