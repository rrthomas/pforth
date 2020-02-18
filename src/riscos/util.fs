\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Define a primitive which takes x arguments and returns y results.
\
\ Usage:
\
\ x y PRIMITIVE FOO
\ ( arg2 arg1 -> TOP=arg1, R0=arg2 )
\ ( assembly code )
\ ( TOP=result1, R0=result2 -> result2 result1 )
\ END-PRIMITIVE \ or END-PRIMITIVE-CODE, if explicit flow control is needed

: PRIMITIVE   ( args results -- results args )
   CODE                       \ make a code word
   SWAP                       \ ( results args )
   DUP 3 < INVERT ABORT" PRIMITIVE needs < 3 arguments"
   OVER  3 < INVERT ABORT" PRIMITIVE needs < 3 results"
   2DUP  0=  SWAP 0<>  AND IF \ push cached stack item if args=0 & results<>0
      TOP SP PUSH,
   THEN
   DUP 1 > IF                 \ pop second argument if args>1
      R0 SP POP,
   THEN ;

: END-PRIMITIVE-CODE   ( results args -- )
   OVER  2 = IF               \ push R0 if results=2
      R0 SP PUSH,
   THEN
   2DUP 0>  SWAP 0=  AND IF   \ pop TOP if args>0 & results=0
      TOP SP POP,
   THEN
   2DROP ;

: END-PRIMITIVE
   END-PRIMITIVE-CODE
   END-SUB ;
