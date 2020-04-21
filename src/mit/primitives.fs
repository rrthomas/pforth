\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Required primitives )

INCLUDE" call-cells.fs" CELLS >'FORTH TO PRIMITIVE-RP


\ Stack primitives

1 0 PRIMITIVE DROP
MLIT MPOP NOPALIGN
1 ,
END-PRIMITIVE

1 1 PRIMITIVE PICK
MDUP
END-PRIMITIVE

2 2 PRIMITIVE SWAP
MLIT MSWAP NOPALIGN
0 ,
END-PRIMITIVE

0 1 PRIMITIVE CELL
MLIT NOPALIGN 4 ,
END-PRIMITIVE

0 1 PRIMITIVE -CELL
MLIT NOPALIGN -4 ,
END-PRIMITIVE


\ Memory primitives

1 1 PRIMITIVE @
MLOAD
END-PRIMITIVE

2 0 PRIMITIVE !
MSTORE
END-PRIMITIVE

1 1 PRIMITIVE C@
MLOAD1
END-PRIMITIVE

2 0 PRIMITIVE C!
MSTORE1
END-PRIMITIVE


\ Arithmetic and logical primitives

2 1 PRIMITIVE +
MADD
END-PRIMITIVE

1 1 PRIMITIVE NEGATE
MNEGATE
END-PRIMITIVE

2 1 PRIMITIVE *
MMUL
END-PRIMITIVE

2 2 PRIMITIVE U/MOD
MUDIVMOD MLIT MSWAP NOPALIGN
0 ,
END-PRIMITIVE

2 2 PRIMITIVE S/REM
MDIVMOD MLIT MSWAP NOPALIGN
0 ,
END-PRIMITIVE

2 1 PRIMITIVE =
MXOR MLIT MULT MNEGATE
1 ,
END-PRIMITIVE

2 1 PRIMITIVE <
MLT
MNEGATE
END-PRIMITIVE

2 1 PRIMITIVE U<
MULT
MNEGATE
END-PRIMITIVE

2 1 PRIMITIVE AND
MAND
END-PRIMITIVE

2 1 PRIMITIVE OR
MOR
END-PRIMITIVE

2 1 PRIMITIVE XOR
MXOR
END-PRIMITIVE

1 1 PRIMITIVE INVERT
MNOT
END-PRIMITIVE

2 1 PRIMITIVE LSHIFT
MLSHIFT
END-PRIMITIVE

2 1 PRIMITIVE RSHIFT
MRSHIFT
END-PRIMITIVE


\ System primitives

1 0 PRIMITIVE HALT
MHALT
END-PRIMITIVE


\ Control primitives

\ Must NOT be inline, as it needs caller's PC!
\ FIXME: inline it somehow?
CODE (CREATE)
MLIT MSWAP MJUMP NOPALIGN
0 ,
END-CODE

INCLUDE" primitive-bracket-does.fs"


\ Stack management

VARIABLE RP
\ FIXME: >R and R> must be defined as CODE words, because they are needed by
\ LINK, and UNLINK,
1 0 PRIMITIVE >R
MLIT_PC_REL MLIT MDUP MLOAD
' RP >BODY OFFSET,
0 ,
MLIT MADD MLIT MDUP
-4 , \ FIXME: target -CELL, not -4
0 ,
MLIT MSWAP MSTORE MSTORE
1 ,
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the start of each word!

0 1 PRIMITIVE R>
MLIT_PC_REL MLIT MDUP MLOAD
' RP >BODY OFFSET,
0 ,
MLIT MDUP MLIT MADD
0 ,
4 , \ FIXME: target CELL, not 4
MLIT MSWAP MLIT MSWAP
0 ,  1 ,
MSTORE MLOAD
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the start of each word!

0 1 PRIMITIVE R@
MLIT_PC_REL MLOAD MLOAD
' RP >BODY OFFSET,
END-PRIMITIVE
0 INLINE \ Prevent inlining because of relative offset to RP

0 1 PRIMITIVE RP@
MLIT_PC_REL MLOAD
' RP >BODY OFFSET,
END-PRIMITIVE
0 INLINE \ Prevent inlining because of relative offset to RP

\ FIXME: -9 THROW if RP is out of range
\ Must be a primitive as it would mess up its own return
1 0 PRIMITIVE RP!
MLIT_PC_REL MSTORE NOPALIGN
' RP >BODY OFFSET,
END-PRIMITIVE
0 INLINE \ Prevent inlining because of relative offset to RP

0 LIBMITFEATURES-PRIMITIVE TOTAL-ARGS
1 LIBMITFEATURES-PRIMITIVE MIT_ARGV
2 LIBMITFEATURES-PRIMITIVE MIT_EXTRA_INSTRUCTION


\ Stack management primitives

0 0 PRIMITIVE SP@
MTHIS_STATE MGET_STACK_DEPTH
MLIT MLSHIFT NOPALIGN
2 , \ FIXME constant!
END-PRIMITIVE

\ FIXME: -9 THROW if out of range
1 0 PRIMITIVE SP!
MLIT MRSHIFT NOPALIGN
2 , \ FIXME constant!
MTHIS_STATE MSET_STACK_DEPTH
END-PRIMITIVE
