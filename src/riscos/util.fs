\ Define a primitive which takes x arguments and returns y results.
\
\ Usage:
\
\ x y PRIMITIVE FOO
\ ( arg2 arg1 -> TOP=arg1, R0=arg2 )
\ ( assembly code )
\ ( TOP=result1, R0=result2 -> result2 result1 )
\ END-SUB

: PRIMITIVE   ( args results -- args results )
   CODE                       \ make a code word
   SWAP                       \ ( results args )
   DUP 3 < INVERT ABORT" PRIMITIVE needs < 3 arguments"
   2DUP  OVER 2 < INVERT  -ROT >  AND ABORT" PRIMITIVE needs args >= results or results < 2"
   2DUP  0=  SWAP 0<>  AND IF \ push cached stack item if args=0 & results<>0
      TOP SP PUSH,
   THEN
   DUP 1 > IF                 \ pop second argument if args>1
      R0 SP POP,
   THEN ;

: END-PRIMITIVE   ( args results -- )
   2DUP 0>  SWAP 0=  AND IF   \ pop TOP if args>0 & results=0
      TOP SP POP,
   THEN
   2DROP
   END-SUB ;
