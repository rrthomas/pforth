CR .( Assembler words )

ALSO ASSEMBLER

12 PRIMITIVES DUP DROP SWAP OVER ROT -ROT TUCK NIP PICK ROLL ?DUP >R
12 PRIMITIVES R> R@ < > = <> 0< 0> 0= 0<> U< U>
 5 PRIMITIVES 0 1 -1 CELL -CELL
CODE TRUE   B-1 BEXIT END-CODE  1 INLINE
CODE FALSE   B0 BEXIT END-CODE  1 INLINE
 4 PRIMITIVES 1+ 1- CELL+ CELL-
CODE 2*   B1LSHIFT BEXIT END-CODE  1 INLINE
 2 PRIMITIVES 2/ CELLS
CODE CELL/   B2/ B2/ BEXIT END-CODE  2 INLINE
 6 PRIMITIVES 1LSHIFT 1RSHIFT + - >-< *
10 PRIMITIVES THROW U/MOD /MOD S/REM / MOD MAX MIN ABS NEGATE
11 PRIMITIVES AND OR XOR INVERT LSHIFT RSHIFT @ ! C@ C! +!
CODE BYE   B0 BHALT END-CODE  2 INLINE
 3 PRIMITIVES J EXIT UNLOOP
INCLUDE" bracket-does.fs"
 4 PRIMITIVES SP@ SP! RP@ RP!
 3 PRIMITIVES HALT LIB LINK
CODE ARSHIFT   B1 BSWAP BLSHIFT  B/  BEXIT END-CODE  4 INLINE

PREVIOUS