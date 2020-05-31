\ (c) Reuben Thomas 2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;
: (RELATIVE-POSTPONE)   R> R>ADDRESS ALIGNED DUP CELL+ >R  @ HERE - , ;


\ Data structures

: >BODY   2 CELLS + ;
: CREATE,  $01060C0B , CELL , ; \ LIT_PC_REL ( CELL ) LIT_0 SWAP JUMP
: (DOES>)   $030B  LAST  TUCK !  CELL+  OVER -  SWAP ! ;