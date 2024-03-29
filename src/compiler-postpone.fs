\ (c) Reuben Thomas 2019-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler words that need special treatment during meta-compilation owing
\ to their use of POSTPONE

\ Compiler

: AGAIN   DUP BACKWARD .BRANCH  HERE BRANCH, SWAP !BRANCH ; IMMEDIATE COMPILING
: UNTIL   DUP BACKWARD .IF  HERE IF, SWAP !BRANCH ; IMMEDIATE COMPILING

: DOES-LINK,   POSTPONE R> ;

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE UNTIL ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE UNTIL ; COMPILING
: END-LOOP,   POSTPONE UNLOOP ; COMPILING

: CREATE,   .NOP  NOP,  RAW-POSTPONE (CREATE)
   LAST >NAME .CREATED-CODE
   ['] (CREATE) >NAME  CREATED ! ;
