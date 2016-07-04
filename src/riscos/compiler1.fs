\ Compiler

: (POSTPONE)   R> R>ADDRESS  DUP 4 + >R  @ CURRENT-COMPILE, ; COMPILING


\ Data structures

: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr )   4 + ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS @  CALL ;