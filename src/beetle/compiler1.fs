\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS  DUP CELL+ >R  @ CURRENT-COMPILE, ;

: DO,   $55 C, ;
: LOOP,   $56 ADR, ;
: +LOOP,   $58 ADR, ;
: END-LOOP, ;

ALSO ASSEMBLER
: EXECUTE   STATE @ IF  $50 C,  NOPALIGN  ELSE [ $50 C,  NOPALIGN ]
   THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  $51 C,  NOPALIGN  ELSE [ $51 C,  NOPALIGN ]
   THEN ; IMMEDIATE
PREVIOUS


\ Data structures

: LITERAL   DUP HERE 1+ FITS IF  $5D C, FIT,  ELSE $5C C,  NOPALIGN  ,  THEN ;
IMMEDIATE COMPILING

\ Leave UNLINK, in next cell where it can be patched by DOES>
: CREATE,   LINK,  $42 C,  $23 C,  NOPALIGN  UNLINK,  NOPALIGN ;
: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr )   CELL+ ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS @  BRANCH ;