\ Compiler words that need special treatment during meta-compilation owing
\ to their use of POSTPONE

\ Compiler
: BRANCH   ( at from to -- )   HERE >R
   ROT DP !  POSTPONE (BRANCH)  ,  DROP  R> DP ! ;

: AHEAD   POSTPONE (BRANCH)  HERE  0 , ; IMMEDIATE COMPILING
: IF   POSTPONE (?BRANCH)  HERE  0 , ; IMMEDIATE COMPILING

: DOES-LINK,   $030B060C ,  RELATIVE-POSTPONE DOCOL ;
: LINK,   $030B ,  RELATIVE-POSTPONE DOCOL ;
: UNLINK,   POSTPONE EXIT ; COMPILING

: LITERAL,   ( n -- )   POSTPONE (LITERAL)  , ;
: LITERAL   LITERAL, ; IMMEDIATE COMPILING

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: END-LOOP,   POSTPONE UNLOOP ; COMPILING
