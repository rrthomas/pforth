\ (c) Reuben Thomas 2019-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

ALSO ASSEMBLER
: NATIVE-CALL   ( at from to -- )   $0C HERE >R  >R  >-< CELL-  SWAP DP !
   R> INSTRUCTION-BIT LSHIFT $44 OR ,  ,  R> DP ! ;
PREVIOUS