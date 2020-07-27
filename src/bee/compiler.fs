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

: @BRANCH   ( from -- to )   DUP @ 2 ARSHIFT + $FFFFFFFC AND ;
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP @ ,  CELL+  LOOP  DROP
   ELSE HERE - ,  THEN ;

: AHEAD   HERE  3 ,  ( FIXME: OP2_JUMPI 2 LSHIFT OP_LEVEL2 OR ) ; IMMEDIATE COMPILING
: IF   HERE  7 ,   ( FIXME: OP2_JUMPZI 2 LSHIFT OP_LEVEL2 OR) ; IMMEDIATE COMPILING
: JOIN   ( from to -- )   OVER - 2 LSHIFT  OVER @ $F AND  OR  SWAP ! ;

: LINK, ;
: UNLINK,   $FF , ( FIXME: BRET ) ; COMPILING
: LEAVE, ;

: OFFSET   ( from to -- offset )   >-< ;
: CALL   ( at from to -- )   OFFSET SWAP ! ;
