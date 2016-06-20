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


\ Compiler

HEX
\ IMMEDIATE both for run-time speed and R: compatibility with Beetle version
\ FIXME: It should not be necessary for the latter reason
\ FIXME: Inline POSTPONE LITERAL to avoid loop with definition of (POSTPONE)
: R>ADDRESS   ['] (LITERAL) COMPILE, 03FFFFFF ,  ['] AND COMPILE, ;
IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS  DUP 4 + >R  @ COMPILE, ; COMPILING
DECIMAL

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: UNLOOP,   POSTPONE UNLOOP ; COMPILING


\ Data structures

: LITERAL   POSTPONE (LITERAL) ALIGN  , ; IMMEDIATE COMPILING

: CREATE,   LINK,  POSTPONE (CREATE) ;
: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr )   4 + ;
HEX
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS @  CALL ;
DECIMAL