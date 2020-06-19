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
: LITERAL,   75 , ; \ FIXME: B(LITERAL)
: LITERAL   LITERAL, , ; IMMEDIATE COMPILING

: BRANCH   ( at from to -- )   HERE >R
   ROT DP !  77 ,  ,  79 , ( FIXME: BBRANCH )  DROP  R> DP ! ;

: AHEAD   77 ,  HERE  0 ,  79 , ( FIXME: BBRANCH ) ; IMMEDIATE COMPILING
: IF   77 ,  HERE  0 ,  81 , ( FIXME: BBRANCH ) ; IMMEDIATE COMPILING

: DOES-LINK,   POSTPONE R> POSTPONE DROP ;
: LINK, ;
: UNLINK,   69 , ( FIXME: BEXIT ) ; COMPILING

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: END-LOOP,   POSTPONE UNLOOP ; COMPILING

: CREATE,   POSTPONE (CREATE) UNLINK, ;