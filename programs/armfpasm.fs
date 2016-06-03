( Floating Point Assembler )
ASSEMBLER DEFINITIONS   ALSO FORTH
DECIMAL

VARIABLE FRound                VARIABLE FPrecision
: SPREC   0 FPrecision ! ;     : DPREC   1 FPrecision ! ;
: EPREC   2 FPrecision ! ;     : PPREC   3 FPrecision ! ;
: NEAREST   0 FRound ! ;       : +INF   1 FRound ! ;
: -INF   2 FRound ! ;          : ZERO   3 FRound ! ;

: !ROUND   FRound @ 5 <<
  FPrecision @ DUP 1 AND 7 <<
  SWAP 2 AND 18 << OR OR   COND OR! ;
SPREC NEAREST

: FCONST   8 0 DO   I CONSTANT   I 8 OR CONSTANT   LOOP ;
  FCONST
        F0 #0.0   F1 #1.0   F2 #2.0   F3 #3.0
        F4 #4.0   F5 #5.0   F6 #0.5   F7 #10.0

: FPCR   ( addr -- offset )
  HERE ALIGN 8 + -
  DUP 0< IF @- ELSE @+ THEN
  ABS DUP 1023 > ABORT" FP Address Range" PC SWAP ;

HEX
: STF  ( Fn Rn n -- )    2 >>> SWAP 10 <<  OR
  FPrecision @ 1 AND 0F << OR
  FPrecision @ 2 AND 15 << OR  C000000 OR  100 OR
  COND @ OR    SWAP  C << OR    , RESET ;
: LDF   100000 COND OR! STF ;
\ Need to add STFM, LDFM or whatever

: FLT   0C <<  0E000110 OR SWAP 10 << OR
        !ROUND COND @ OR , RESET ;
: WFS   0 SWAP 00200000 COND OR! FLT ;
: RFS   100000 COND OR! WFS ;
: FIX   SWAP  00100000 COND OR! FLT ;

: FOP   CREATE  , DOES>  @ DUP 1 AND
  IF 0E008100 ELSE ROT 10 << 0E000100 OR THEN !ROUND SWAP
  1 BIC 13 << OR  OR SWAP 0C << OR COND @ OR , RESET ;
: FOPS  1C 0 DO   I FOP   LOOP ;

FOPS   ADF MVF MUF MNF SUF ABS RSF RND
       DVF SQT RDF LOG POW LGN RPW EXP
       RMF SIN FML COS FDV TAN FRD ASN
       POL ACS ??? ATN

: CMF   SWAP 10 << OR  0E90F110 OR COND @ OR , RESET ;
: CNF   00300000 OR CMF ;
: CMFE   00400000 OR CMF ;
: CNFE   00600000 OR CMF ;
