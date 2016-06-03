: TRIAD   ( n -- )   \ display "triad" of blocks containing n
   CR  3 / 3 *
   DUP 3 + SWAP DO
      I LIST
   LOOP
   PAGE ;

: EMITS   ( x1 ... xu u -- )   0 DO EMIT LOOP ;

: CURSOR   [ 0 0 55 ] OS ;
: -CURSOR   [ 0 0 54 ] OS ;

: OS_Byte   ( outs -- )   3 6 ROT  POSTPONE OS ; IMMEDIATE COMPILING

: WAIT   ( wait for next vertical blank )   0 0 19 [ 0 ] OS_Byte ;
: KEY?   ( -- f )   0 0 122 [ 2 ] OS_Byte  DROP 255 <> ;

VARIABLE FLIPPER
: FLIP   ( swap shadow screens )
   FLIPPER @ DUP 3 XOR DUP FLIPPER !  WAIT 0 113 [ 0 ] OS_Byte
   0 112 [ 0 ] OS_Byte  PAGE ;
: SHADOW   ( set up shadow screens )
   PAGE  2 FLIPPER !  FLIP ;
: SINGLE   ( restore single screen )
   1 0 112 [ 0 ] OS_Byte  WAIT 1 0 113 [ 0 ] OS_Byte  PAGE ;

: RGB   ( r g b n -- ) ( Sets colour n to r g b )
   >R SWAP ROT  16 R> 19  6 EMITS ;   ( b g r 16 n 19 -- )
: BORDER   ( r g b -- ) ( Sets border colour to r g b )
   SWAP ROT  24 0 19  6 EMITS ;   ( b g r 24 0 19 -- )

\ READ/DATA constructs (numbers only)
\ link data screens with -->
: NUMBER?   ( addr -- f )   BEGIN  1+ DUP C@  DUP ASCII : =
   SWAP DUP ASCII 0 ASCII 9 1+ WITHIN ROT OR  SWAP ASCII ,
   ASCII / 1+ WITHIN OR  NOT UNTIL  C@ BL = ;
: <BLOCK   ( scr# -- )   BLK ! 0 >IN ! ;
: <LINE   ( line# -- )   64 * >IN ! ;
: <TIB   0 <BLOCK  0 #TIB ! ;
: READ   BEGIN  BL WORD DUP NUMBER? NOT WHILE  FIND IF EXECUTE
   ELSE DROP THEN  REPEAT  NUMBER ;

VARIABLE SEED  HEX
: RANDOM   SEED @  DUP  1 <<  SWAP 0< IF 1D827B41 XOR THEN  DUP
   SEED ! ;  DECIMAL