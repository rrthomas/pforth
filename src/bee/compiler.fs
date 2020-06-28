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

: @BRANCH   ( from -- to )   DUP @ + 2 XOR ;
: JOIN   ( from to -- )   OVER -  2 OR  SWAP ! ;
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP @ ,  CELL+  LOOP  DROP
   ELSE HERE - ,  THEN ;
: LEAVE, ;
: OFFSET   ( from to -- offset )   >-< ;
: CALL   ( at from to -- )   OFFSET SWAP ! ;
