\ (c) Reuben Thomas 2019-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Data structures

: LITERAL
   DUP 2 LSHIFT  2 ARSHIFT  OVER = IF
      2 LSHIFT  1 OR , ( FIXME: BPUSHI )
   ELSE
      $8 ,  ,  $21F , $10F , ( FIXME: HERE 2 CELLS + BCALLI BRFROM BFETCH )
   THEN ; IMMEDIATE COMPILING
\ FIXME: BPUSHRELI
: RELATIVE-LITERAL   ALIGN HERE -  2 OR , ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
\ >DOES>, given the xt of a defining word, returns the address of the DOES>
\ code.
: >DOES>   ( xt -- 'does )   DUP >INFO @ $FFFF AND CELLS  + ;
: (DOES>)   >DOES>  LAST CELL+  DUP ROT CALL ;