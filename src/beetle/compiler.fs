\ Machine-dependent words (Beetle)
\ FIXME: Use assembler
\
\ (c) Reuben Thomas 1995-2021
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Packing opcodes into cells

\ FIXME: once assembler "built-in", remove the following
: FITS   ( x addr -- flag )   DUP ALIGNED >-<  DUP IF  8 * 1-  1 SWAP LSHIFT
   SWAP DUP 0< IF  INVERT  THEN  U>  ELSE NIP  THEN ;
\ FIXME: FIT, should abort on failure
: FIT,   ( x -- )   HERE DUP ALIGNED >-<  0 ?DO  DUP C,  8 RSHIFT  LOOP
   DROP ;
: FIT!   ( x addr -- )   HERE  SWAP DP !  1 ALLOT  SWAP FIT,  DP ! ;
: NOPALIGN   0 FIT, ;


\ Core compiler

: OFFSET   ( from to -- offset )   >-<  CELL/ 1-
   DUP  24 ARSHIFT  NEGATE  1 RSHIFT  IF  -1 THROW  THEN ( FIXME: ABORT" offset out of range" )
   $00FFFFFF AND ;
: @BRANCH   ( from -- to )   DUP 1+ CELL- ALIGNED @  8 ARSHIFT  1+ CELLS + ;
: !BRANCH   ( at from to -- )   OFFSET SWAP FIT! ;

\ FIXME: allow arbitrary branches; at the moment we're effectively
\ restricted to 64Mb.
: BRANCH   ( at from to -- )   $43 3 PICK C!  !BRANCH ;
: CALL   ( at from to -- )   $49 3 PICK C!  !BRANCH ;

: ADR,   ( to opcode -- )   HERE ROT OFFSET  TUCK HERE 1+ FITS
   INVERT IF  NOPALIGN  THEN  1+ C, FIT, ;
: CALL,   ( to -- )   $48 ADR, ;
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;

: AHEAD   NOPALIGN  HERE DUP $42 ADR, ; IMMEDIATE COMPILING
: IF   NOPALIGN  HERE DUP $44 ADR, ; IMMEDIATE COMPILING
: JOIN   ( from to -- )   OVER SWAP !BRANCH ;

: BEGIN   NOPALIGN HERE ; IMMEDIATE COMPILING
: THEN   NOPALIGN HERE JOIN ; IMMEDIATE COMPILING

: LINK, ;
: UNLINK,   $4A C, ;
: LEAVE,   $50 C, ;