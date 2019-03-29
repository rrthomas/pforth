\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;

: EXECUTE   STATE @ IF  $1B ,  ELSE  [ $1B , ]  THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  $6D5 ,  ELSE  [ $6D5 C, ]  THEN ; IMMEDIATE


\ Data structures

: LITERAL   LITERAL, ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr ) ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS ALIGNED @  CALL ;