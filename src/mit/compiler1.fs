\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;

: EXECUTE   STATE @ IF  $03 ,  ELSE  [ $03 , ]  THEN ; IMMEDIATE
\ FIXME: 2 constant!
: @EXECUTE   STATE @ IF  $030810 , 2 ,  ELSE  [ $030810 , 2 , ]  THEN ; IMMEDIATE


\ Data structures

: LITERAL   LITERAL, ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
: (DOES>)   LAST  DUP  R> R>ADDRESS ALIGNED @  CALL ;