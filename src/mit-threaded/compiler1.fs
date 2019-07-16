\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;


\ Data structures

: >BODY   2 CELLS + ;
: CREATE,  $01060C0B , CELL , ; \ LIT_PC_REL ( CELL ) LIT_0 SWAP JUMP
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr ) ;
: (DOES>)   $030A  LAST >DOES  TUCK !  CELL+ DUP  R> R>ADDRESS @  CALL ;