\ Terminal input/output

32 CONSTANT BL
: CR   [ 0 0 ] OS" OS_NewLine" ;
: EMIT   [ 1 0 ] OS" OS_WriteC" ;
: DEL   127 EMIT ;
: PAGE   12 EMIT ;

CODE KEY
SWI," OS_ReadC"
TOP SP PUSH,
TOP R0 MOV,
CC RET,
R0 126 # MOV,
SWI," XOS_Byte"
END-SUB

: DEL?   DUP 127 =  SWAP 8 =  OR ;
: CR?   13 = ;
: EOL   (S")  [ 1 C, 10 C, ALIGN ] ;

\ GET-LINE behaves like ACCEPT except that the line terminator is appended to
\ the string, so that +n1-1 is the maximum possible length of the string
: GET-LINE   ( c-addr +n1 -- +n2 )   >R >R  255 32 R> R> SWAP
   [ 4 2 ] OS" OS_ReadLine"  DROP ;

: AT-XY   31 EMIT  SWAP EMIT EMIT ;

77 CONSTANT WIDTH   \ width of display