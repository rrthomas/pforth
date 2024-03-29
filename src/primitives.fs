\ (c) Reuben Thomas 1995-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Required primitives )

\ Stack primitives
CODE DROP   BPOP BRET END-CODE 1 INLINE
CODE PICK   BDUP BRET END-CODE 1 INLINE
CODE >R   BPUSHR BRET END-CODE 1 INLINE
CODE R>   BPOPR BRET END-CODE 1 INLINE
CODE R@   BDUPR BRET END-CODE 1 INLINE
CODE CELL   BWORD_BYTES BRET END-CODE 1 INLINE

\ Stack management primitives
CODE SP@   BGET_DP BWORD_BYTES BMUL BRET END-CODE
CODE SP!   BWORD_BYTES BUDIVMOD BPOP BSET_DP BRET END-CODE
CODE RP@   BGET_SP BRET END-CODE 1 INLINE
CODE RP!   BSET_SP BRET END-CODE 1 INLINE
CODE MEMORY@   BGET_MSIZE BRET END-CODE 1 INLINE
CODE M0@   BGET_M0 BRET END-CODE 1 INLINE
CODE S0   0 BPUSHI BRET END-CODE
CODE R0   0 BPUSHI BRET END-CODE

\ Memory primitives
CODE @   BLOAD BRET END-CODE 1 INLINE
CODE !   BSTORE BRET END-CODE 1 INLINE
CODE C@   BLOAD1 BRET END-CODE 1 INLINE
CODE C!   BSTORE1 BRET END-CODE 1 INLINE

\ Arithmetic and logical primitives
CODE +   BADD BRET END-CODE 1 INLINE
CODE NEGATE   BNEG BRET END-CODE 1 INLINE
CODE *   BMUL BRET END-CODE 1 INLINE
CODE (U/MOD)   BUDIVMOD 0 BPUSHI BSWAP BRET END-CODE 3 INLINE
CODE (S/REM)   BDIVMOD 0 BPUSHI BSWAP BRET END-CODE 3 INLINE
CODE =   BEQ BNEG BRET END-CODE 2 INLINE
CODE <   BLT BNEG BRET END-CODE 2 INLINE
CODE U<   BULT BNEG BRET END-CODE 2 INLINE
CODE INVERT   BNOT BRET END-CODE 1 INLINE
CODE AND   BAND BRET END-CODE 1 INLINE
CODE OR   BOR BRET END-CODE 1 INLINE
CODE XOR   BXOR BRET END-CODE 1 INLINE
CODE LSHIFT   BLSHIFT BRET END-CODE 1 INLINE
CODE RSHIFT   BRSHIFT BRET END-CODE 1 INLINE

\ Control primitives
CODE EXIT   BRET END-CODE 1 INLINE \ FIXME: Should be EXECUTEable
CODE EXECUTE   BCALL BRET END-CODE 1 INLINE
CODE @EXECUTE   BLOAD BCALL BRET END-CODE

\ System primitives
CODE HALT   BTHROW END-CODE 1 INLINE
\ (CREATE) must not be inlined
CODE (CREATE)   LAST >NAME ' .DOES-LABEL TO-ASMOUT  BPOPR BRET END-CODE
