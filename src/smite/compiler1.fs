\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;

: EXECUTE   STATE @ IF  $9B C,  ELSE [ $9B C, ]  THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  $95 C,  $9B C,  ELSE [ $95 C,  $9B C, ]
   THEN ; IMMEDIATE


\ Data structures

: LITERAL   LITERAL, ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr )   1+ ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS ALIGNED @  BRANCH ;