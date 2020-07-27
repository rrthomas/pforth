\ Machine-dependent words (Threaded Mit)
\
\ (c) Reuben Thomas 2019-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Core compiler

: NOPALIGN   ALIGN ;

: CALL   ( at from to -- )   >-< SWAP ! ;

: @BRANCH   ( from -- to )   DUP @ 2 ARSHIFT + $FFFFFFFC AND ;
: !BRANCH   ( from to -- )   OVER - 2 LSHIFT  OVER @ $F AND  OR  SWAP ! ;
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP @ ,  CELL+  LOOP  DROP
   ELSE HERE - ,  THEN ;

: BEGIN   HERE ; IMMEDIATE COMPILING
: AHEAD   HERE  3 ,  ( FIXME: OP2_JUMPI 2 LSHIFT OP_LEVEL2 OR ) ; IMMEDIATE COMPILING
: IF   HERE  7 ,   ( FIXME: OP2_JUMPZI 2 LSHIFT OP_LEVEL2 OR) ; IMMEDIATE COMPILING

: THEN   HERE !BRANCH ; IMMEDIATE COMPILING

: LINK, ;
: UNLINK,   $FF , ( FIXME: BRET ) ; COMPILING
: LEAVE, ;
