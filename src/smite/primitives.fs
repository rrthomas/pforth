CR .( Required primitives )

ALSO ASSEMBLER \ For INLINE

\ Stack primitives

1 0 PRIMITIVE DROP
BPOP
END-PRIMITIVE

1 1 PRIMITIVE PICK
BDUP
END-PRIMITIVE

2 2 PRIMITIVE SWAP
] 0 [ BSWAP
END-PRIMITIVE

0 1 PRIMITIVE CELL
] 4 [
END-PRIMITIVE

0 1 PRIMITIVE -CELL
] -4 [
END-PRIMITIVE


\ Memory primitives

1 1 PRIMITIVE @
BLOAD
END-PRIMITIVE

2 0 PRIMITIVE !
BSTORE
END-PRIMITIVE

1 1 PRIMITIVE C@
BLOADB
END-PRIMITIVE

2 0 PRIMITIVE C!
BSTOREB
END-PRIMITIVE


\ Arithmetic and logical primitives

2 1 PRIMITIVE +
BADD
END-PRIMITIVE

2 1 PRIMITIVE -
BNEGATE BADD
END-PRIMITIVE

1 1 PRIMITIVE NEGATE
BNEGATE
END-PRIMITIVE

2 1 PRIMITIVE *
BMUL
END-PRIMITIVE

\ FIXME: check for division by zero
2 2 PRIMITIVE U/MOD
BUDIVMOD
] 0 [ BSWAP
END-PRIMITIVE

2 2 PRIMITIVE S/REM
BDIVMOD
] 0 [ BSWAP
END-PRIMITIVE

2 1 PRIMITIVE =
BEQ
BNEGATE
END-PRIMITIVE

2 1 PRIMITIVE <
BLT
BNEGATE
END-PRIMITIVE

2 1 PRIMITIVE U<
BULT
BNEGATE
END-PRIMITIVE

2 1 PRIMITIVE AND
BAND
END-PRIMITIVE

2 1 PRIMITIVE OR
BOR
END-PRIMITIVE

2 1 PRIMITIVE XOR
BXOR
END-PRIMITIVE

1 1 PRIMITIVE INVERT
BNOT
END-PRIMITIVE

2 1 PRIMITIVE LSHIFT
BLSHIFT
END-PRIMITIVE

2 1 PRIMITIVE RSHIFT
BRSHIFT
END-PRIMITIVE


\ System primitives

1 0 $2 LIBC-PRIMITIVE HALT


\ Control primitives

\ Must NOT be inline, as it needs caller's PC!
\ FIXME: use LIT_PC_REL
CODE (CREATE)
0 LITERAL, BSWAP
BBRANCH
END-CODE

INCLUDE" bracket-does.fs"


\ Stack management

\ FIXME: Make this a small constant!
VARIABLE PRIMITIVE-RP

\ FIXME: Make this a small constant!
VARIABLE RP
\ FIXME: >R and R> must be defined as CODE words, because they are needed by
\ LINK, and UNLINK,
1 0 PRIMITIVE >R
' RP >BODY <'FORTH LITERAL,
0 LITERAL, BDUP
BLOAD
-4 LITERAL, BADD \ FIXME: target -CELL, not -4
0 LITERAL, BDUP
1 LITERAL, BSWAP
BSTORE
BSTORE
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the start of each word!

0 1 PRIMITIVE R>
' RP >BODY <'FORTH LITERAL,
0 LITERAL, BDUP
BLOAD
0 LITERAL, BDUP
4 LITERAL, BADD \ FIXME: target CELL, not 4
0 LITERAL, BSWAP
1 LITERAL, BSWAP
BSTORE
BLOAD
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the start of each word!

0 1 PRIMITIVE R@
' RP >BODY <'FORTH LITERAL,
BLOAD
BLOAD
END-PRIMITIVE

0 1 PRIMITIVE RP@
' RP >BODY <'FORTH LITERAL,
BLOAD
END-PRIMITIVE

\ FIXME: -9 THROW if RP is out of range
\ Must be a primitive as it would mess up its own return
1 0 PRIMITIVE RP!
' RP >BODY <'FORTH LITERAL,
BSTORE
END-PRIMITIVE


\ Stack management primitives

0 1 PRIMITIVE SP@
BGET_STACK_DEPTH
CELL LITERAL,
BMUL
END-PRIMITIVE

0 0 PRIMITIVE SP! \ Lie about arguments and results!
CELL LITERAL,
BUDIVMOD
BPOP
BSET_STACK_DEPTH
END-PRIMITIVE


\ FIXME: Put in better order; must be defined after bracket-create is included because of use of VALUE
1024 1024 * VALUE MEMORY-SIZE \ FIXME: command-line parameter

\ FIXME: Make these optional in pForth (highlevel.fs does not need them)
4096 CONSTANT STACK-CELLS
4096 CONSTANT RETURN-STACK-CELLS

0 CONSTANT S0
0 VALUE R0


PREVIOUS
