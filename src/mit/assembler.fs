\ Mit assembler for pForth
\ Reuben Thomas

CR .( Mit assembler )


VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS
: BITS   S" ADDRESS-UNIT-BITS" ENVIRONMENT?
   INVERT ABORT" ADDRESS-UNIT-BITS query not supported" ;
BITS  CONSTANT BITS/

VARIABLE PC
VARIABLE I-ADDR
VARIABLE I-SHIFT

ALSO FORTH DEFINITIONS
: CODE   BL WORD HEADER  ALSO ASSEMBLER ;
: END-CODE   ALIGN  0 I-ADDR !  PREVIOUS ;
PREVIOUS DEFINITIONS

: INLINE   ( char -- )   LAST >INFO 2 + C! ;

8 CONSTANT INSTRUCTION-BIT
1  INSTRUCTION-BIT LSHIFT  1- CONSTANT INSTRUCTION-MASK

: _FETCH   ( -- )   HERE I-ADDR !  0 ,  HERE PC !  0 I-SHIFT ! ;

: INSTRUCTION   ( opcode -- )
   DUP INSTRUCTION-MASK U> ABORT" invalid opcode"
   HERE PC @ <> IF  0 I-ADDR !  THEN    \ invalidate I-ADDR if memory
                                        \ ALLOTted other than by WORD,
   I-ADDR @ 0= IF  _FETCH  THEN         \ start of a new word
   I-ADDR @ @                           \ ( opcode cur-word )
   OVER  I-SHIFT @ LSHIFT  OR           \ ( opcode new-word )
   2DUP I-SHIFT @ RSHIFT <> IF          \ if we ran out of space,
      _FETCH DROP                       \ advance a word
   ELSE
      NIP                               \ otherwise we're fine
   THEN
   I-ADDR @ !                           \ store new-word
   INSTRUCTION-BIT I-SHIFT +! ;

: OPLESS   CREATE C,  DOES> C@ INSTRUCTION ;
: 0OPS   SWAP 1+ SWAP DO  I OPLESS  LOOP ;

\ FIXME: Use different prefix, not B!
$07 $00 0OPS  BNEXT BBRANCH BBRANCHZ BCALL       BPOP    BDUP    BSWAP    BPUSH_STACK_DEPTH
$0F $08 0OPS  BLOAD BSTORE  BLIT     BLIT_PC_REL BLIT_0  BLIT_1  BLIT_2   BLIT_3
$17 $10 0OPS  BEQ   BLT     BULT     BNEGATE     BADD    BMUL    BDIVMOD  BUDIVMOD
$1F $18 0OPS  BNOT  BAND    BOR      BXOR        BLSHIFT BRSHIFT BARSHIFT BSIGN_EXTEND

1 CONSTANT LIB_MIT
2 CONSTANT LIB_C

2 CONSTANT NATIVE-POINTER-CELLS

PREVIOUS DEFINITIONS