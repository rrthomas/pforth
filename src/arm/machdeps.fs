\ Machine-dependent words (ARM)
\ Reuben Thomas


\ System variables

0 VALUE LIMIT \ FIXME: make it a constant


\ Compiler #1

HEX
: MASK-FLAGS   03FFFFFF AND ;
: (C")   R>  MASK-FLAGS  DUP C@ 1+ CHARS OVER + ALIGNED  >R ;
: (S")   R>  MASK-FLAGS  DUP C@  TUCK 1+ CHARS OVER + ALIGNED  >R
   CHAR+ SWAP ;
DECIMAL


\ Writing code to memory

: SYNCHRONIZE   ( from to -- )   SWAP 1  [ 3 0 ]
   OS" XOS_SynchroniseCodeAreas" ;
: CODE!   ( x adr -- )   TUCK !  DUP SYNCHRONIZE ;
: CODE,   ( x -- )   ALIGN  ,  HERE CELL- DUP SYNCHRONIZE ;


\ Branches

: NOPALIGN   ALIGN ; COMPILING

HEX
: AHEAD   HERE  EA000000 CODE, ; IMMEDIATE
: IF   E35B0000 CODE, E49CB004 CODE,  HERE  0A000000 CODE, ; IMMEDIATE

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


\ Compiler #2

HEX
: (POSTPONE)   R>  MASK-FLAGS  DUP 4 + >R  @ COMPILE, ; COMPILING
DECIMAL

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: UNLOOP,   POSTPONE UNLOOP ; COMPILING


\ Data structures

: LITERAL   POSTPONE (LITERAL) ALIGN  , ; IMMEDIATE

: CREATE,   LINK,  POSTPONE (CREATE) ;
: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr )   4 + ;
HEX
: (DOES>)   LAST >DOES  DUP  R> MASK-FLAGS @  CALL ;
DECIMAL


\ OS access #2

HEX
: OS   ( #regs-in #regs-out swi -- )
   -ROT 2DUP + >R ROT
   R@ IF  E52CB004 CODE,  THEN
   ROT ?DUP IF
      1 SWAP LSHIFT 1- E8BC0000 OR CODE,
   THEN
   EF000000 OR CODE,
   ?DUP IF
      1 SWAP LSHIFT 1- E92C0000 OR CODE,
   THEN
   R> IF  E49CB004 CODE,  THEN ;
IMMEDIATE COMPILING
DECIMAL
: CLI   ( c-addr -- )   C0END  [ 1 0 ] OS" OS_CLI" ;


\ Terminal input/output

32 CONSTANT BL
: CR   [ 0 0 ] OS" OS_NewLine" ;
: EMIT   [ 1 0 ] OS" OS_WriteC" ;
: DEL   127 EMIT ;
: PAGE   12 EMIT ;

CODE KEY
SWI," OS_ReadC"
TOP SP PUSH,
TOP R0 MOV,
CC RET,
R0 126 # MOV,
SWI," XOS_Byte"
END-SUB

: DEL?   DUP 127 =  SWAP 8 =  OR ;
: CR?   13 = ;
: EOL   (S")  [ 1 C, 10 C, ALIGN ] ;

\ GET-LINE behaves like ACCEPT except that the line terminator is appended to
\ the string, so that +n1-1 is the maximum possible length of the string
: GET-LINE   ( c-addr +n1 -- +n2 )   >R >R  255 32 R> R> SWAP
   [ 4 2 ] OS" OS_ReadLine"  DROP ;

: AT-XY   31 EMIT  SWAP EMIT EMIT ;

77 CONSTANT WIDTH   \ width of display

: "COPYRIGHT   S" ©" ;
: "ENVIRONMENT   S" RISC OS" ;


\ OS-specific file access

\ Set the type of the file given by c-addr u2 to u1
: TYPE-FILE   ( u1 c-addr u2 -- )   C0END  18  [ 3 0 8 ] OS ;