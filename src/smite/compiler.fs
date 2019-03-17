\ Machine-dependent words (SMite)
\ FIXME: Use assembler
\ Reuben Thomas


\ Core compiler

\ FIXME: Distinguish the case where we must align to a cell boundary for
\ code-fiddling reasons (e.g. REDEFINER) from that where we need to align to
\ a branch target (AHEAD); the latter is a null operation on SMite
: NOPALIGN   $1F CALIGN ; \ FIXME: NOP

: @LITERAL   ( addr -- n )
   0 SWAP                               \ result
   CELL OVER + CHAR- DO
      8 LSHIFT
      I C@  OR
   -CHAR +LOOP ;
: !LITERAL   ( n addr -- )
   CELL OVER + SWAP DO
      DUP  $FF AND  I C!
      8 RSHIFT
   LOOP  DROP ;
: LITERAL,   ( n -- )   $04 C,  HERE  CELL ALLOT  !LITERAL ;

: AHEAD   HERE  0 LITERAL,  $19 C, ; IMMEDIATE COMPILING
: IF   HERE  0 LITERAL,  $1A C, ; IMMEDIATE COMPILING

: @BRANCH   ( from -- to )   1+ @LITERAL >'FORTH ;
: !BRANCH   ( at from to opcode -- )   HERE >R  >R  ROT DP !
   <'FORTH LITERAL,  DROP  R> C,  R> DP ! ;

: BRANCH   ( at from to -- )   $19 !BRANCH ;
: CALL   ( at from to -- )   $1B !BRANCH ;

: JOIN   ( from to -- )   <'FORTH  SWAP 1+ !LITERAL ;

: CALL,   ( to -- )   <'FORTH LITERAL,  $1B C, ;
\ FIXME: 6 + below is a hack to skip over the primitive's prologue
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP 6 + C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;

: LEAVE, ;