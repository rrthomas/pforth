\ (c) Reuben Thomas 2018-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Extra primitives )

ALSO ASSEMBLER


\ Stack primitives

1 2 PRIMITIVE DUP
MLIT MDUP NOPALIGN
0 ,
END-PRIMITIVE

2 3 PRIMITIVE OVER
MLIT MDUP NOPALIGN
1 ,
END-PRIMITIVE

3 3 PRIMITIVE ROT
MLIT MSWAP MLIT MSWAP
0 , 1 ,
END-PRIMITIVE

3 3 PRIMITIVE -ROT
MLIT MSWAP MLIT MSWAP
1 , 0 ,
END-PRIMITIVE


\ Arithmetic and logical primitives

2 1 PRIMITIVE -
MNEGATE MADD
END-PRIMITIVE

1 1 PRIMITIVE 1+
MNOT
MNEGATE
END-PRIMITIVE

1 1 PRIMITIVE 1-
MNEGATE
MNOT
END-PRIMITIVE


PREVIOUS
