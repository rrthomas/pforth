\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;

: EXECUTE   STATE @ IF  $1A C,  ELSE  [ $1A C, ]  THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  $14 C,  $1A C,  ELSE  [ $14 C,  $1A C, ]  THEN ; IMMEDIATE


\ Data structures

: LITERAL   LITERAL, ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr ) ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS ALIGNED @  CALL ;