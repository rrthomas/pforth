\ SMite assembler for pForth
\ Reuben Thomas   started 1/95

\ FIXME: Add an IP which allows advantage to be taken of packing opcodes
\ into the current word after an instruction with an operand is assembled

CR .( SMite assembler )


VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS
: BITS   S" ADDRESS-UNIT-BITS" ENVIRONMENT?
   INVERT ABORT" ADDRESS-UNIT-BITS query not supported" ;
BITS  CONSTANT BITS/

FORTH DEFINITIONS
: CODE   BL WORD HEADER  ALSO ASSEMBLER ;
: END-CODE   ALIGN  PREVIOUS ;
ASSEMBLER DEFINITIONS

: INLINE   ( char -- )   LAST >INFO 2 + C! ;

: OPLESS   CREATE C,  DOES> C@ C, ;
: 0OPS   SWAP 1+ SWAP DO  I OPLESS  LOOP ;

\ FIXME: Use different prefix, not B!
$87 $80 0OPS  BNOP    BPOP    BPUSH   BSWAP   BRPUSH  BPOP2R  BRPOP   BLT
$8F $88 0OPS  BEQ     BULT    BADD    BNEGATE BMUL    BUDIVMOD BDIVMOD BINVERT
$97 $90 0OPS  BAND    BOR     BXOR    BLSHIFT BRSHIFT BLOAD   BSTORE  BLOADB
$9F $98 0OPS  BSTOREB BBRANCH BBRANCHZ BCALL  BRET    BTHROW  BHALT   BCALL_NATIVE
$A7 $A0 0OPS  BEXTRA BPUSH_WORD_SIZE BPUSH_NATIVE_POINTER_SIZE BPUSH_SP BSTORE_SP BPUSH_RP BSTORE_RP BPUSH_PC
$AE $A8 0OPS  BPUSH_SSIZE BPUSH_RSIZE BPUSH_HANDLER BSTORE_HANDLER BPUSH_MEMORY BPUSH_BADPC BPUSH_INVALID

6 CONSTANT LITERAL-CHUNK-BIT
1 LITERAL-CHUNK-BIT LSHIFT  1-  CONSTANT LITERAL-CHUNK-MASK
$40 CONSTANT LITERAL-CONTINUATION

PREVIOUS DEFINITIONS
