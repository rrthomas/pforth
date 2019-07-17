\ Compiler

: (POSTPONE)   R> R>ADDRESS  DUP 4 + >R  @ CURRENT-COMPILE, ; COMPILING


\ Data structures

: >BODY   2 CELLS + ;
: (DOES>)   LAST 4 +  DUP  R> R>ADDRESS @  CALL ;