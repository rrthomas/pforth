\ Control structures #1
\
\ (c) Reuben Thomas 2016-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

PROVIDE: J   R> R> R> R>  DUP  -ROT >R >R  -ROT >R >R ; [THEN]
PROVIDE: (LOOP)   R>  R> 1+  DUP R@ =  SWAP >R  SWAP >R ; [THEN]
PROVIDE: (+LOOP)   R> SWAP  R>  R@ OVER SWAP -  -ROT +  R@ OVER SWAP -
   SWAP >R  XOR 0<  SWAP >R ; [THEN]
PROVIDE: UNLOOP   R>  R> DROP R> DROP  >R ; [THEN]