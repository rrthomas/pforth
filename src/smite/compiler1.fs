\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;

: EXECUTE   STATE @ IF  $9D C,  -1 LITERAL, $87 C,  1 LITERAL,  $83 C,  $AA C,  ELSE
   [ $9D C,  -1 C, $87 C,  1 C, ( FIXME: HACK! )  $83 C,  $AA C, ]  THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  $92 C,  $9D C,  -1 LITERAL, $87 C,  1 LITERAL,  $83 C,  $AA C,
   ELSE  [ $92 C,  $9D C,  -1 C, $87 C,  1 C, ( FIXME: HACK! )  $83 C,  $AA C, ]  THEN ; IMMEDIATE


\ Data structures

: LITERAL   LITERAL, ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr )   1+ ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS ALIGNED @  BRANCH ;