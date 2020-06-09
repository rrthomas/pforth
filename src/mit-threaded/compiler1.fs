\ (c) Reuben Thomas 2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING


\ Data structures

: >BODY   2 CELLS + ;
: CREATE,   $04180203 , 0 , ; \ ( 1 MPUSHRELI  0 MPUSHI  MSWAP MJUMP )
: (DOES>)   $0C44  LAST  TUCK !  TUCK - CELL-  SWAP CELL+ ! ;
