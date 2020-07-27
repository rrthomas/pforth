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

: AGAIN   POSTPONE AHEAD  SWAP JOIN ; IMMEDIATE COMPILING
: UNTIL   POSTPONE IF  SWAP JOIN ; IMMEDIATE COMPILING

: DOES-LINK,   POSTPONE R> POSTPONE DROP ;

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE UNTIL ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE UNTIL ; COMPILING
: END-LOOP,   POSTPONE UNLOOP ; COMPILING

: CREATE,   POSTPONE (CREATE) UNLINK, ;