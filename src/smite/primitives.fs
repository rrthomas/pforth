CR .( Assembler words )

ALSO ASSEMBLER

11 PRIMITIVES DUP DROP SWAP OVER ROT -ROT TUCK NIP PICK ROLL >R
 7 PRIMITIVES R> R@ < > = U< U>
: CELL   4 ;
: -CELL   -4 ;
 2 PRIMITIVES + *
CODE -   BNEGATE B+ BEXIT END-CODE  2 INLINE
 7 PRIMITIVES THROW U/MOD /MOD S/REM / MOD NEGATE
10 PRIMITIVES AND OR XOR INVERT LSHIFT RSHIFT @ ! C@ C!
 3 PRIMITIVES J EXIT UNLOOP
INCLUDE" bracket-create.fs"
INCLUDE" bracket-does.fs"
 6 PRIMITIVES SP@ SP! RP@ RP! 'THROW! MEMORY@
CODE LIMIT   BMEMORY@ BEXIT END-CODE  1 INLINE
CODE S0   BS0@ BEXIT END-CODE  1 INLINE
CODE R0   BR0@ BEXIT END-CODE  1 INLINE
CODE STACK-CELLS   B#S BEXIT END-CODE  1 INLINE
CODE RETURN-STACK-CELLS   B#R BEXIT END-CODE  1 INLINE
 2 PRIMITIVES HALT LINK
CODE TOTAL-ARGS   BARGC BEXIT END-CODE  1 INLINE
CODE ABSOLUTE-ARG   BARG BEXIT END-CODE  1 INLINE
 7 PRIMITIVES STDIN STDOUT STDERR OPEN-FILE CLOSE-FILE READ-FILE WRITE-FILE
 4 PRIMITIVES FILE-POSITION REPOSITION-FILE FLUSH-FILE RENAME-FILE
 4 PRIMITIVES DELETE-FILE FILE-SIZE RESIZE-FILE FILE-STATUS

PREVIOUS