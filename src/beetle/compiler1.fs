\ (c) Reuben Thomas 1995-2021
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

: DO,   $4B C, ;
: LOOP,   $4C ADR, ;
: +LOOP,   $4E ADR, ;
: END-LOOP, ;

ALSO ASSEMBLER
: EXECUTE   STATE @ IF  $46 C,  NOPALIGN  ELSE [ $46 C,  NOPALIGN ]
   THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  $47 C,  NOPALIGN  ELSE [ $47 C,  NOPALIGN ]
   THEN ; IMMEDIATE
PREVIOUS


\ Data structures

: LITERAL   DUP HERE 1+ FITS IF  $53 C, FIT,  ELSE $52 C,  NOPALIGN  ,  THEN ;
IMMEDIATE COMPILING
: RELATIVE-LITERAL   NOPALIGN $1E5652 , ( FIXME: BLITERAL BEP@ B+ )
   HERE -  CELL-  , ; IMMEDIATE COMPILING

\ Leave UNLINK, in next cell where it can be patched by DOES>
: CREATE,   LINK,  $56 C,  $23 C,  NOPALIGN  UNLINK,  NOPALIGN ;
: >BODY   2 CELLS + ;
\ >DOES>, given the xt of a defining word, returns the address of the DOES>
\ code.
: >DOES>   ( xt -- 'does )   DUP >INFO @ $FFFF AND CELLS  + ;
: (DOES>)   >DOES>  LAST CELL+  DUP ROT BRANCH ;