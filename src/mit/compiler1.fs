\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

: EXECUTE   STATE @ IF  S>, $0C ,  ELSE  [ 1 COMPILE-S> $0C , ]  THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  S>, $0C20 ,  ELSE  [ 1 COMPILE-S> $0C20 , ]  THEN ; IMMEDIATE


\ Data structures

: >BODY   2 CELLS + ;
: (DOES>)   LAST  DUP ROT CALL ;