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

: FITS   ( x addr -- flag )   DUP ALIGNED >-<  DUP IF  BITS/ * 1-
   1 SWAP LSHIFT  SWAP DUP 0< IF  INVERT  THEN  U>  ELSE NIP  THEN ;
: FIT,   ( x -- )   HERE DUP ALIGNED >-<  0 ?DO  DUP C,
   BITS/ RSHIFT  LOOP  DROP ;
: NOPALIGN   0 FIT, ;

: OPLESS   CREATE C,  DOES> C@ C, ;
: OPFUL   CREATE C,  DOES> C@  OVER HERE 1+ FITS IF  1+ C, FIT,
   ELSE C,  NOPALIGN  ,  THEN ;
: OPADR   CREATE C,  DOES> C@  OVER HERE 1+ ALIGNED - CELL/
   DUP HERE 1+ FITS  IF  SWAP 1+ C, FIT, DROP  ELSE DROP C,
   NOPALIGN  'FORTH - ,  THEN ;

: 0OPS   SWAP 1+ SWAP DO  I OPLESS  LOOP ;
: BOPS   SWAP 1+ SWAP DO  I OPADR  2 +LOOP ;

$07 $00 0OPS  BNEXT00 BDUP    BDROP   BSWAP   BOVER   BROT    B-ROT   BTUCK
$0A $08 0OPS  BNIP    BPICK   BROLL
$0F $0C 0OPS  B>R     BR>     BR@     B<
$11 $10 0OPS  B>      B=
$18 $17 0OPS  BU<     BU>
$1E     OPLESS B+
$27 $25 0OPS  B*      B/      BMOD
$2A $28 0OPS  B/MOD   BU/MOD  BS/REM
$2E     OPLESS BNEGATE
$36 $31 0OPS          BINVERT BAND    BOR     BXOR    BLSHIFT BRSHIFT
$3C $39 0OPS  B@      B!      BC@     BC!
$41 $3E 0OPS  BSP@    BSP!    BRP@    BRP!
$47 $42 0OPS  BEP@    BS0@    B#S     BR0@    B#R     B'THROW@
$4B $48 0OPS  B'THROW! BMEMORY@ B'BAD@ B-ADDRESS@
$4E $4C BOPS  BBRANCH  B?BRANCH
$50     OPLESS BEXECUTE
$52     OPADR BCALL
$54     OPLESS BEXIT
$58 $56 BOPS  B(LOOP) B(+LOOP)
$5B $5A 0OPS  BUNLOOP BJ
$5C     OPFUL B(LITERAL)
$60 $5E 0OPS  BTHROW  BHALT   BLINK

$86 $80 0OPS  BARGC  BARG   BSTDIN BSTDOUT BSTDERR BOPEN-FILE BCLOSE-FILE
$8A $87 0OPS  BREAD-FILE BWRITE-FILE BFILE-POSITION BREPOSITION-FILE
$8C $88 0OPS  BFLUSH-FILE BRENAME-FILE BDELETE-FILE BFILE-SIZE BRESIZE-FILE
    $8D OPLESS BFILE-STATUS

PREVIOUS DEFINITIONS
