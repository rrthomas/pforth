\ Beetle assembler for pForth
\
\ (c) Reuben Thomas 1995-2021
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ FIXME: Add an IP which allows advantage to be taken of packing opcodes
\ into the current word after an instruction with an operand is assembled

CR .( Beetle assembler )


VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS
: BITS   S" ADDRESS-UNIT-BITS" ENVIRONMENT?
   INVERT ABORT" ADDRESS-UNIT-BITS query not supported" ;
BITS  CONSTANT BITS/

FORTH DEFINITIONS
INCLUDE" code.fs"
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
$0F $08 0OPS  BNIP    BPICK   BROLL   B?DUP   B>R     BR>     BR@     B<
$17 $10 0OPS  B>      B=      B<>     B0<     B0>     B0=     B0<>    BU<
$1F $18 0OPS  BU>     B0      B1      B-1     BCELL   B-CELL  B+      B-
$27 $20 0OPS  B>-<    B1+     B1-     BCELL+  BCELL-  B*      B/      BMOD
$2F $28 0OPS  B/MOD   BU/MOD  BS/REM  B2/     BCELLS  BABS    BNEGATE BMAX
$37 $30 0OPS  BMIN    BINVERT BAND    BOR     BXOR    BLSHIFT BRSHIFT B1LSHIFT
$3F $38 0OPS  B1RSHIFT B@     B!      BC@     BC!     B+!     BSP@    BSP!
$41 $40 0OPS  BRP@    BRP!
$44 $42 BOPS  BBRANCH  B?BRANCH
$47 $46 0OPS  BEXECUTE B@EXECUTE
$48     OPADR BCALL
$4B $4A 0OPS  BEXIT   B(DO)
$4E $4C BOPS  B(LOOP) B(+LOOP)
$51 $50 0OPS  BUNLOOP BJ
$52     OPFUL B(LITERAL)
$57 $54 0OPS  BTHROW  BHALT   BEP@    BLIB
$5F $59 0OPS  BLINK   BS0@    BS0!    BR0@    BR0!    B'THROW@ B'THROW!
$62 $60 0OPS  BMEMORY@ B'BAD@ B-ADDRESS@

PREVIOUS DEFINITIONS
