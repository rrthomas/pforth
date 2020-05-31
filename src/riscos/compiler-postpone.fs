\ Compiler words that need special treatment during meta-compilation owing
\ to their use of POSTPONE
\
\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

\ IMMEDIATE for run-time speed
: R>ADDRESS   POSTPONE (LITERAL) $03FFFFFF ,  POSTPONE AND ;
IMMEDIATE COMPILING

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: END-LOOP,   POSTPONE UNLOOP ; COMPILING


\ Data structures

\ Compile code that will push the cell immediately after it
: LITERAL,   POSTPONE (LITERAL) ;
: LITERAL   LITERAL,  , ; IMMEDIATE COMPILING

: CREATE,   LINK,  POSTPONE (CREATE) ;
: DOES-LINK,   LINK,  POSTPONE (DOES) ;
