\ Terminal input/output

\ I/O streams
: STDIN   2 LIB ;
: STDOUT   3 LIB ;
: STDERR   4 LIB ;


CREATE IO-BUFFER  CELL ALLOT

: EMIT   IO-BUFFER  TUCK C!  1  STDOUT WRITE-FILE  DROP ;
: KEY   IO-BUFFER  DUP 1  STDIN READ-FILE  2DROP  C@ ;

: BL   32 ;
: CR   13 EMIT  10 EMIT ;
: DEL   8 EMIT  BL EMIT  8 EMIT ;

: DEL?   DUP 127 =  SWAP 8 =  OR ;
: CR?   DUP 13 =  SWAP 10 =  OR ;
: EOL   (S")  [ 1 C, 10 C, ALIGN ] ;

77 CONSTANT WIDTH   \ width of display