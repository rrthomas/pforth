\ Machine-dependent words (ARM)
\
\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Branches

: NOPALIGN   ALIGN ; COMPILING

: AHEAD   HERE  $EA000000 CODE, ; IMMEDIATE COMPILING
: IF   $E35B0000 CODE, $E49CB004 CODE,  HERE  $0A000000 CODE, ;
IMMEDIATE COMPILING

: B>OFFSET   ( instruction -- offset )   $00FFFFFF AND ;
: >BRANCH   ( from to -- offset )   >-<  2 ARSHIFT 2 -
   DUP  24 ARSHIFT  NEGATE  1 RSHIFT  IF  -1 THROW  THEN ( FIXME: ABORT" offset out of range" )
   B>OFFSET ;
: BRANCH>   ( from offset -- to )   2 +  8 LSHIFT 6 ARSHIFT  + ;
: @BRANCH   ( from -- to )   DUP @  B>OFFSET  BRANCH> ;
: !BRANCH   ( at from to op-mask -- )   -ROT  >BRANCH  OR  SWAP CODE! ;

: BRANCH   ( at from to -- )   $EA000000 !BRANCH ;
: CALL   ( at from to -- )   $EB000000 !BRANCH ;

: JOIN   ( from to -- )   OVER TUCK @  $FF000000 AND  !BRANCH ;

: CALL,   ( to -- )   HERE  4 ALLOT  DUP ROT CALL ;
: COMPILE,   CALL, ;

: LINK,   $E52DE004 CODE, ;
: UNLINK,   $E49DF004 CODE, ;
: LEAVE, ;