\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS  DUP CELL+ >R  @ CURRENT-COMPILE, ;

HEX
: DO,   4B C, ;
: LOOP,   4C ADR, ;
: +LOOP,   4E ADR, ;
: UNLOOP, ;

ALSO ASSEMBLER
: EXECUTE   STATE @ IF  46 C,  NOPALIGN  ELSE [ 46 C,  NOPALIGN ]
   THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  47 C,  NOPALIGN  ELSE [ 47 C,  NOPALIGN ]
   THEN ; IMMEDIATE
PREVIOUS
DECIMAL


\ Data structures

HEX
: LITERAL   DUP HERE 1+ FITS IF  53 C, FIT,  ELSE 52 C,  NOPALIGN  ,  THEN ;
IMMEDIATE COMPILING

\ Leave UNLINK, in next cell where it can be patched by DOES>
: CREATE,   LINK,  56 C,  23 C,  NOPALIGN  UNLINK,  NOPALIGN ;
DECIMAL
: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr ) CELL+ ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS @  BRANCH ;