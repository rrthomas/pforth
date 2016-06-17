\ Terminal input/output

: BL   0 LIB ;
: CR   1 LIB ;
: EMIT   2 LIB ;
: DEL   8 EMIT  BL EMIT  8 EMIT ;
: KEY   3 LIB ;

: DEL?   DUP 127 =  SWAP 8 =  OR ;
: CR?   DUP 13 =  SWAP 10 =  OR ;
: EOL   (S")  [ 1 C, 10 C, ALIGN ] ;

77 CONSTANT WIDTH   \ width of display