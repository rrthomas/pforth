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

: OFFSET   ( from to -- offset )   >-< CELL- ;
: OFFSET,   ( to -- )   HERE CELL- SWAP OFFSET , ;

INCLUDE" compiler.fs"