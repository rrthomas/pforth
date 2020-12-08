\ (c) Reuben Thomas 2019-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Data structures

: LITERAL
   DUP
   DUP CELLS CELL/  OVER = IF  .PUSHI  ELSE  .PUSH  THEN
   PUSH, ; IMMEDIATE COMPILING
: RELATIVE-LITERAL   DUP .PUSHRELI  PUSHREL, ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
\ >DOES>, given the xt of a defining word, returns the address of the DOES>
\ code.
: >DOES>   ( xt -- 'does )   DUP >INFO @ $FFFF AND CELLS  + ;
: (DOES>)   DUP >NAME CREATED !  >DOES>  LAST CELL+  DUP ROT CALL ;