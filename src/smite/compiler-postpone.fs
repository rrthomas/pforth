\ Compiler words that need special treatment during meta-compilation owing
\ to their use of POSTPONE

\ Compiler

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: UNLOOP,   POSTPONE UNLOOP ; COMPILING


\ Data structures

: CREATE,   LINK,  NOPALIGN  POSTPONE (CREATE)  UNLINK,  NOPALIGN ;