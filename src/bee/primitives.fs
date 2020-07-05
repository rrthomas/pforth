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
CODE SP@   BGET_SP 2 BPUSH BLSHIFT BRET END-CODE \ FIXME: constant!
CODE SP!   2 BPUSH BRSHIFT BSET_SP BRET END-CODE \ FIXME: constant!
CODE RP@   BGET_RP BRET END-CODE 1 INLINE
CODE RP!   BSET_RP BRET END-CODE 1 INLINE
CODE MEMORY@   BGET_MEMORY BRET END-CODE 1 INLINE
CODE S0   0 BPUSH BRET END-CODE
CODE R0   0 BPUSH BRET END-CODE

\ Memory primitives
CODE @   BLOAD BRET END-CODE 1 INLINE
CODE !   BSTORE BRET END-CODE 1 INLINE
CODE C@   BLOAD1 BRET END-CODE 1 INLINE
CODE C!   BSTORE1 BRET END-CODE 1 INLINE

\ Arithmetic and logical primitives
CODE +   BADD BRET END-CODE 1 INLINE
CODE NEGATE   BNEGATE BRET END-CODE 1 INLINE
CODE *   BMUL BRET END-CODE 1 INLINE
CODE (U/MOD)   BUDIVMOD 0 BPUSH BSWAP BRET END-CODE 3 INLINE
CODE (S/REM)   BDIVMOD 0 BPUSH BSWAP BRET END-CODE 3 INLINE
CODE =   BEQ BNEGATE BRET END-CODE 2 INLINE
CODE <   BLT BNEGATE BRET END-CODE 2 INLINE
CODE U<   BULT BNEGATE BRET END-CODE 2 INLINE
CODE INVERT   BNOT BRET END-CODE 1 INLINE
CODE AND   BAND BRET END-CODE 1 INLINE
CODE OR   BOR BRET END-CODE 1 INLINE
CODE XOR   BXOR BRET END-CODE 1 INLINE
CODE LSHIFT   BLSHIFT BRET END-CODE 1 INLINE
CODE RSHIFT   BRSHIFT BRET END-CODE 1 INLINE

\ Control primitives
INCLUDE" bracket-does.fs"
CODE EXIT   BRET END-CODE 1 INLINE \ FIXME: Should be EXECUTEable
CODE EXECUTE   BCALL BRET END-CODE 1 INLINE
CODE @EXECUTE   BLOAD BCALL BRET END-CODE

\ System primitives
CODE HALT   BTHROW END-CODE 1 INLINE
CODE ARGLEN   BARGLEN BRET END-CODE 1 INLINE
CODE ARGCOPY   BARGCOPY BRET END-CODE 1 INLINE
CODE TOTAL-ARGS   BARGC BRET END-CODE

CODE STDIN   BSTDIN BRET END-CODE 1 INLINE
CODE STDOUT   BSTDOUT BRET END-CODE 1 INLINE
CODE STDERR   BSTDERR BRET END-CODE 1 INLINE
CODE OPEN-FILE   BOPEN-FILE BRET END-CODE 1 INLINE
CODE CLOSE-FILE   BCLOSE-FILE BRET END-CODE 1 INLINE
CODE READ-FILE   BREAD-FILE BRET END-CODE 1 INLINE
CODE WRITE-FILE   BWRITE-FILE BRET END-CODE 1 INLINE
CODE FILE-POSITION   BFILE-POSITION BRET END-CODE 1 INLINE
CODE REPOSITION-FILE   BREPOSITION-FILE BRET END-CODE 1 INLINE
CODE (CREATE)   BDUPR BWORD_BYTES BADD BRET END-CODE
