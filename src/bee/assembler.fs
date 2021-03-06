\ Bee assembler for pForth
\
\ (c) Reuben Thomas 2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Bee assembler )


VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS

: INLINE   ( char -- )   LAST >INFO 2 + C! ;

FORTH DEFINITIONS
INCLUDE" code.fs"
: END-CODE   ALIGN  PREVIOUS ;
ASSEMBLER DEFINITIONS

: OFFSET   ( from to -- offset )   >-< ;
: OFFSET,   ( to -- )   HERE SWAP OFFSET , ;

0 CONSTANT OP_CALLI     1 CONSTANT OP_PUSHI
2 CONSTANT OP_PUSHRELI  3 CONSTANT OP_LEVEL2
3 CONSTANT OP_MASK

0 CONSTANT OP2_JUMPI  1 CONSTANT OP2_JUMPZI
2 CONSTANT OP2_TRAP   3 CONSTANT OP2_INSN
3 CONSTANT OP2_MASK

: >OPCODE   ( operand type -- )   SWAP  2 LSHIFT  OR ;
: >OPCODE2   ( operand type -- )   SWAP  2 LSHIFT  OR  2 LSHIFT   OP_LEVEL2 OR ;
: TRAP   CREATE OP2_TRAP >OPCODE2 ,  DOES> @ , ;
: INST   CREATE OP2_INSN >OPCODE2 ,  DOES> @ , ;
: INSTS   SWAP 1+ SWAP DO  I INST  LOOP ;

: BCALLI  OP_CALLI >OPCODE , ;
: BPUSHI   OP_PUSHI >OPCODE , ;
: BPUSHRELI   HERE SWAP OFFSET 2 ARSHIFT  OP_PUSHRELI >OPCODE , ;

 7  0 INSTS BNOP     BNOT     BAND     BOR      BXOR     BLSHIFT  BRSHIFT  BARSHIFT
15  8 INSTS BPOP     BDUP     BSET     BSWAP    BJUMP    BJUMPZ   BCALL    BRET
23 16 INSTS BLOAD    BSTORE   BLOAD1   BSTORE1  BLOAD2   BSTORE2  BLOAD4   BSTORE4
31 24 INSTS BNEG     BADD     BMUL     BDIVMOD  BUDIVMOD BEQ      BLT      BULT
39 32 INSTS BPUSHR   BPOPR    BDUPR    BCATCH   BTHROW   BBREAK   BWORD_BYTES BGET_M0
47 40 INSTS BGET_MSIZE BGET_SSIZE BGET_SP BSET_SP BGET_DSIZE BGET_DP BSET_DP BGET_HANDLER_SP

0 TRAP LIBC

PREVIOUS DEFINITIONS
