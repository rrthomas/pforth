\ Integer Mandelbrot set
\ From Silicon Vision's RiscForth

\ Integer complex arithmetic
DECIMAL
: COMPLEX+   ( complex1 complex2 -- sum )   UNDER+ UNDER+ ;
: COMPLEX^2  ( complex -- complex^2 )   2DUP * 10000 / 2*  >R  ( 2 AB )
   DUP * SWAP DUP * >-< 10000 / R> ;

: DIVERGENT?   ( complex -- flag )
   DUP * SWAP DUP *  + 400000000 > ;

: ASSIGN   ( value -- colour )
   DUP 32 < IF 63 >-<  ELSE  64 > IF 0 ELSE 63 THEN  THEN ;


\ Mandelbrot Integer
: ITERATE   ( complex -- times )           0 0
    65 0 DO
    2OVER 2SWAP     COMPLEX^2  COMPLEX+
    2DUP   DIVERGENT?
           IF 2DROP 2DROP I LEAP THEN    LOOP
    2DROP 2DROP 65 ;

VARIABLE x1 VARIABLE y1 VARIABLE x2 VARIABLE y2
VARIABLE ScaleX VARIABLE ScaleY
VARIABLE Sh VARIABLE Sv
VARIABLE x  VARIABLE y
800 ScaleX ! 800 ScaleY !
: INIT -20000 DUP x1 ! y1 !   20000 DUP x2 ! y2 ! ;
INIT

: SetH
 x2 @ x1 @ - ScaleX @
             / 2  * Sh !  x1 @ x ! ;
: SetV
 y2 @ y1 @ - ScaleY @
      / 4 * Sv !  y1 @ y ! ;
: HLOOP ( ypos -- ) SetH ScaleX @ 0 DO
  x @ y @ ITERATE ASSIGN  0 GCOL
      DUP I SWAP POINT  PAUSE  Sh @ x +! 2 +LOOP DROP ;
: (MANDEL)   SetV ScaleY @ 0 DO
  I HLOOP Sv @ y @ + y ! 4 +LOOP ;

15 MODE    0  0  0  4  0  19    6 VDUS
29 VDU 0 2EMIT 200 2EMIT
0 31 79 26 TWINDOW
: scale   0 MAX ScaleX @ MIN ;
VARIABLE MarkX VARIABLE MarkY
: (SETUP)  LIT" POINTER 1 " CLI$ BEGIN 3 3 GCOL
  @MOUSEB IF @MOUSEXY scale MarkY !
                      scale MarkX ! LIT"  FX15 0 " CLI$
  80000 0 DO LOOP
  BEGIN @MOUSEB IF @MOUSEXY EXIT THEN
  MarkX @ MarkY @ @MOUSEXY  scale MarkY @ - >R
  scale  MarkX @ - R>  2OVER 2OVER BOX BOX AGAIN
  THEN AGAIN ;
: SETUP   (SETUP) scale MarkY @ 2DUP MIN MarkY ! MAX y !
                  scale MarkX @ 2DUP MIN MarkX ! MAX x ! ;
