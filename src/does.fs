\ (c) Reuben Thomas 2016-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: DOES>   LAST POSTPONE RELATIVE-LITERAL  POSTPONE (DOES>)  UNLINK,  NOPALIGN
   HERE LAST TUCK - CELL/  SWAP >INFO
   DUP @ $FFFF0000 AND ROT OR  SWAP !  DOES-LINK, ; IMMEDIATE COMPILING