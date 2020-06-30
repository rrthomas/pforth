\ (c) Reuben Thomas 1995-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

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
: RELATIVE-LITERAL   NOPALIGN $1E425C , ( FIXME: BLITERAL BEP@ B+ )
   HERE -  CELL-  , ; IMMEDIATE COMPILING

\ Leave UNLINK, in next cell where it can be patched by DOES>
: CREATE,   LINK,  $42 C,  $23 C,  NOPALIGN  UNLINK,  NOPALIGN ;
: >BODY   2 CELLS + ;
\ >DOES>, given the xt of a defining word, returns the address of the DOES>
\ code.
: >DOES>   ( xt -- 'does )   DUP >INFO @ $FFFF AND CELLS  + ;
: (DOES>)   >DOES>  LAST CELL+  DUP ROT BRANCH ;