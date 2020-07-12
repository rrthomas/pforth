\ (c) Reuben Thomas 1995-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: INITIALIZE
   \ Assume that we were called by a call instruction at 'FORTH, and
   \ use our return address to calculate the new value of 'FORTH.
   R> CELL-  TO 'FORTH
   MEMORY@ M0@ +
   [ HERE 0 ,  DUP ] \ value of HERE
   START ;
ALIGN  HERE >-<  OP_OFFSET OR  SWAP ! \ FIXME: add !OFFSET
