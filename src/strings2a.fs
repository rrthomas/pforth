\ (c) Reuben Thomas 1991-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: (C")   R>  R>ADDRESS  DUP C@ 1+ CHARS OVER + ALIGNED  >R ;
: (S")   R>  R>ADDRESS  DUP C@  TUCK 1+ CHARS OVER + ALIGNED  >R
   CHAR+ SWAP ;
