\ Machine-dependent words (ARM)
\ Reuben Thomas


\ System variables

0 VALUE LIMIT \ FIXME: make it a constant


\ Writing code to memory

: SYNCHRONIZE   ( from to -- )   SWAP 1  [ 3 0 ]
   OS" XOS_SynchroniseCodeAreas" ;
: CODE!   ( x adr -- )   TUCK !  DUP SYNCHRONIZE ;
: CODE,   ( x -- )   ALIGN  ,  HERE CELL- DUP SYNCHRONIZE ;


\ Branches

: NOPALIGN   ALIGN ; COMPILING

HEX
: AHEAD   HERE  EA000000 CODE, ; IMMEDIATE COMPILING
: IF   E35B0000 CODE, E49CB004 CODE,  HERE  0A000000 CODE, ;
IMMEDIATE COMPILING

: >BRANCH   ( from to -- offset )   >-<  2 RSHIFT 2 -  00FFFFFF AND ;
: BRANCH>   ( offset from -- to )   2 +  8 LSHIFT 6 ARSHIFT  + ;
: !BRANCH   ( at from to op-mask -- )   -ROT  >BRANCH  OR  SWAP CODE! ;

: BRANCH   ( at from to -- )   EA000000 !BRANCH ;
: CALL   ( at from to -- )   EB000000 !BRANCH ;

: JOIN   ( from to -- )   OVER TUCK @ !BRANCH ;

: CALL,   ( to -- )   HERE  4 ALLOT  DUP ROT CALL ;
: COMPILE,   CALL, ;

: LINK,   E52DE004 CODE, ;
: UNLINK,   E49DF004 CODE, ;
: LEAVE,   E51FF004 CODE, ;
DECIMAL