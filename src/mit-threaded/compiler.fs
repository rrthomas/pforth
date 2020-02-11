\ Machine-dependent words (Threaded Mit)
\ Reuben Thomas


\ Core compiler

: NOPALIGN   ALIGN ;

: @BRANCH   ( from -- to )   @ ;
: JOIN   ( from to -- )   SWAP ! ;
: COMPILE,   ( to -- )   , ;
: LEAVE, ;
