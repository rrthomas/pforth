\ Mit assembler for pForth
\
\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Mit assembler )


VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS
: BITS   S" ADDRESS-UNIT-BITS" ENVIRONMENT?
   INVERT ABORT" ADDRESS-UNIT-BITS query not supported" ;
BITS  CONSTANT BITS/

VARIABLE PC
VARIABLE I-ADDR
VARIABLE I-SHIFT

ALSO FORTH DEFINITIONS
INCLUDE" code.fs"
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

$06 $00 0OPS  MNEXT MJUMP   MJUMPZ   MCALL       MPOP    MDUP    MSWAP
$0F $08 0OPS  MLOAD MSTORE  MLIT     MLIT_PC_REL MLIT_0  MLIT_1  MLIT_2   MLIT_3
$17 $11 0OPS        MLT     MULT     MNEGATE     MADD    MMUL    MDIVMOD  MUDIVMOD
$1F $18 0OPS  MNOT  MAND    MOR      MXOR        MLSHIFT MRSHIFT MARSHIFT MSIGN_EXTEND

\ FIXME: Do this properly!
: MHALT   NOPALIGN $00000103 , ;

1 CONSTANT LIB_MIT
2 CONSTANT LIB_C

PREVIOUS DEFINITIONS
