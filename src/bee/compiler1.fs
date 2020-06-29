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

: LITERAL,   159 , ; \ FIXME: BLITERAL
: LITERAL   LITERAL, , ; IMMEDIATE COMPILING
\ FIXME: BOFFSET
: RELATIVE-LITERAL   ALIGN HERE SWAP OFFSET  2 OR , ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
\ >DOES>, given the xt of a defining word, returns the address of the DOES>
\ code.
: >DOES>   ( xt -- 'does )   DUP >INFO @ $FFFF AND CELLS  + ;
: (DOES>)   >DOES>  LAST CELL+  DUP ROT CALL ;