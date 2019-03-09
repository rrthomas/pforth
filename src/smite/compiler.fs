\ Machine-dependent words (SMite)
\ FIXME: Use assembler
\ Reuben Thomas


\ Core compiler

\ FIXME: Distinguish the case where we must align to a cell boundary for
\ code-fiddling reasons (e.g. REDEFINER) from that where we need to align to
\ a branch target (AHEAD); the latter is a null operation on SMite
: NOPALIGN   $1F CALIGN ; \ FIXME: NOP

\ Find most-significant bit set in a CELL
\ After https://stackoverflow.com/questions/2589096/find-most-significant-bit-left-most-that-is-set-in-a-bit-array
\ FIXME: Code is hard-wired for 32 bits
CREATE MSB-TABLE
   0 , 9 , 1 , 10 , 13 , 21 , 2 , 29 , 11 , 14 , 16 , 18 , 22 , 25 , 3 , 30 ,
   8 , 12 , 20 , 28 , 15 , 17 , 24 , 7 , 19 , 27 , 23 , 6 , 26 , 5 , 4 , 31 ,
: MSB   ( x -- +n )
   DUP 0< IF  NEGATE  THEN              \ normalize to +ve
   \ first round up to one less than a power of 2
   DUP  1 ARSHIFT  OR
   DUP  2 ARSHIFT  OR
   DUP  4 ARSHIFT  OR
   DUP  8 ARSHIFT  OR
   DUP 16 ARSHIFT  OR

   $07C4ACDD *  27 RSHIFT  CELLS
   MSB-TABLE + @ ;

\ FIXME: Constants copied from assembler.fs
6 CONSTANT LITERAL-CHUNK-BIT
1 LITERAL-CHUNK-BIT LSHIFT  1-  CONSTANT LITERAL-CHUNK-MASK
$40 CONSTANT LITERAL-CONTINUATION
$80 CONSTANT LITERAL-FINAL
: ALL-BITS-SAME   ( n -- f )   DUP 0=  SWAP INVERT 0=  OR ;
: @LITERAL   ( addr -- n )
   0                                    \ result
   0                                    \ number of bits loaded
   BEGIN                                \ continuation bytes
      2 PICK C@                         \ next byte
      DUP  LITERAL-CHUNK-MASK INVERT  AND  LITERAL-CONTINUATION =  WHILE
      LITERAL-CHUNK-MASK AND  OVER LSHIFT
      ROT OR                            \ add to result
      ROT 1+  SWAP                      \ point to next byte
      ROT LITERAL-CHUNK-BIT +           \ update shift
   REPEAT                               \ FIXME: check for missing end byte
   OVER LSHIFT  ROT OR                  \ add last byte to result
   SWAP ( FIXME: BITS) 8 1- +           \ add BITS-1 to number of bits
   ( FIXME: BITS) 8 CELLS >-<           \ sign-extend
   TUCK LSHIFT  SWAP ARSHIFT
   NIP ;                                \ drop address
: LITERAL,   ( n -- )
   DUP MSB 1+                           \ calculate MSB
   BEGIN                                \ compile continuation bytes
      LITERAL-CHUNK-BIT OVER < WHILE
      OVER  LITERAL-CHUNK-MASK AND  LITERAL-CONTINUATION OR  C,
      SWAP  LITERAL-CHUNK-BIT ARSHIFT  SWAP
      LITERAL-CHUNK-BIT -
   REPEAT
   DROP                                 \ drop number of bits
   LITERAL-FINAL OR  C, ;               \ compile last byte
: !LITERAL   ( n addr -- )   HERE >R  DP !  LITERAL,  R> DP ! ;

\ FIXME: When we have assembler, "6" below becomes: CELL BITS/ *  LITERAL-CHUNK-BIT /  1+
\ FIXME: Rather than compiling NOPs, compile a 6-byte (possibly non-normal) number
: ADDRESS-SPACE   6 0 DO  $1F C,  LOOP ;

: AHEAD   HERE  ADDRESS-SPACE  $19 C, ; IMMEDIATE COMPILING
: IF   HERE  ADDRESS-SPACE  $1A C, ; IMMEDIATE COMPILING

: @BRANCH   ( from -- to )   @LITERAL >'FORTH ;
: !BRANCH   ( at from to opcode -- )   HERE >R  >R  ROT DP !
   <'FORTH LITERAL,  DROP  R> C,  R> DP ! ;

: BRANCH   ( at from to -- )   $19 !BRANCH ;
: CALL   ( at from to -- )   $1B !BRANCH ;

: JOIN   ( from to -- )   <'FORTH  SWAP !LITERAL ;

: CALL,   ( to -- )   <'FORTH LITERAL,  $1B C, ;
\ FIXME: Second 2 + below is a hack to skip over the primitive's prologue
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP 2 + C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;

: LEAVE, ;