\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;

: EXECUTE   STATE @ IF  $03 ,  ELSE  [ $03 , ]  THEN ; IMMEDIATE
\ FIXME: 2 constant!
: @EXECUTE   STATE @ IF  $03080A , 2 ,  ELSE  [ $03080A , 2 , ]  THEN ; IMMEDIATE


\ Data structures

: LITERAL   LITERAL, ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr ) ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS ALIGNED @  CALL ;