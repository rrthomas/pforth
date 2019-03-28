\ Compiler words that need special treatment during meta-compilation owing
\ to their use of POSTPONE

\ Compiler

: LINK,   POSTPONE >R ;
: UNLINK,   POSTPONE R>  $19 , ;

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: UNLOOP,   POSTPONE UNLOOP ; COMPILING


\ Data structures

: CREATE,   POSTPONE (CREATE) ;