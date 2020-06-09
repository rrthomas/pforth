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
: BRANCH   ( at from to -- )   HERE >R
   ROT DP !  POSTPONE (BRANCH)  ,  DROP  R> DP ! ;

: AHEAD   POSTPONE (BRANCH)  HERE  0 , ; IMMEDIATE COMPILING
: IF   POSTPONE (?BRANCH)  HERE  0 , ; IMMEDIATE COMPILING

: DOES-LINK,   $0C441802 ,  POSTPONE DOCOL ;
: LINK,   $0C44 ,  POSTPONE DOCOL ;
: UNLINK,   POSTPONE EXIT ; COMPILING

: LITERAL,   ( n -- )   POSTPONE (LITERAL) ;
: LITERAL   LITERAL, , ; IMMEDIATE COMPILING

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: END-LOOP,   POSTPONE UNLOOP ; COMPILING