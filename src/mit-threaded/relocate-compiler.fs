\ Machine-dependent words (Threaded Mit)
\ Reuben Thomas


\ Core compiler

: NOPALIGN   ALIGN ;

: OFFSET   ( from to -- offset )   >-< CELL- ;
: OFFSET,   ( to -- )   HERE CELL- SWAP OFFSET , ;

: @BRANCH   ( from -- to )   @ >'FORTH ;
: JOIN   ( from to -- )   <'FORTH  SWAP ! ;
: COMPILE,   ( to -- )   <'FORTH , ;
: LEAVE, ;
