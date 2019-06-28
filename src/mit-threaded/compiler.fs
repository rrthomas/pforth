\ Machine-dependent words (Threaded Mit)
\ Reuben Thomas


\ Core compiler

: NOPALIGN   ALIGN ;

: @BRANCH   ( from -- to )   @ >'FORTH ;
: CALL   ( at from to -- )   <'FORTH  ROT !  DROP ;
: JOIN   ( from to -- )   <'FORTH  SWAP ! ;
: COMPILE,   ( to -- )   <'FORTH , ;
: LEAVE, ;
