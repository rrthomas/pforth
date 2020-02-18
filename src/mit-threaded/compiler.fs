\ Machine-dependent words (Threaded Mit)
\
\ (c) Reuben Thomas 2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Core compiler

: NOPALIGN   ALIGN ;

: @BRANCH   ( from -- to )   @ ;
: JOIN   ( from to -- )   SWAP ADDRESS! ;
: COMPILE,   ( to -- )   ADDRESS, ;
: LEAVE, ;
