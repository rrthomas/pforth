CR .( Assembler words )

ALSO ASSEMBLER

12 PRIMITIVES DUP DROP SWAP OVER ROT -ROT TUCK NIP PICK ROLL ?DUP >R
 7 PRIMITIVES R> R@ < > = U< U>
 5 PRIMITIVES 0 1 -1 CELL -CELL
CODE TRUE   B-1 BEXIT END-CODE  1 INLINE
CODE FALSE   B0 BEXIT END-CODE  1 INLINE
CODE 2*   B1LSHIFT BEXIT END-CODE  1 INLINE
 2 PRIMITIVES 2/ CELLS
CODE CELL/   B2/ B2/ BEXIT END-CODE  2 INLINE
 6 PRIMITIVES 1LSHIFT 1RSHIFT + - >-< *
 8 PRIMITIVES THROW U/MOD /MOD S/REM / MOD ABS NEGATE
11 PRIMITIVES AND OR XOR INVERT LSHIFT RSHIFT @ ! C@ C! +!
CODE BYE   B0 BHALT END-CODE  2 INLINE
 3 PRIMITIVES J EXIT UNLOOP
INCLUDE" bracket-does.fs"
 6 PRIMITIVES SP@ SP! RP@ RP! 'THROW! MEMORY@
CODE LIMIT   BMEMORY@ BEXIT END-CODE  1 INLINE
CODE S0   BS0@ BEXIT END-CODE  1 INLINE
CODE R0   BR0@ BEXIT END-CODE  1 INLINE
CODE STACK-CELLS   B#S BEXIT END-CODE  1 INLINE
CODE RETURN-STACK-CELLS   B#R BEXIT END-CODE  1 INLINE
 2 PRIMITIVES HALT LINK
CODE ARSHIFT   B1 BSWAP BLSHIFT  B/  BEXIT END-CODE  4 INLINE
CODE TOTAL-ARGS   BARGC BEXIT END-CODE  1 INLINE
CODE ABSOLUTE-ARG   BARG BEXIT END-CODE  1 INLINE
 7 PRIMITIVES STDIN STDOUT STDERR OPEN-FILE CLOSE-FILE READ-FILE WRITE-FILE
 4 PRIMITIVES FILE-POSITION REPOSITION-FILE FLUSH-FILE RENAME-FILE
 4 PRIMITIVES DELETE-FILE FILE-SIZE RESIZE-FILE FILE-STATUS

PREVIOUS