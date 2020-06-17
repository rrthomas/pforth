\ Machine-dependent words (Mit)
\ FIXME: Use assembler
\
\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Core compiler

: NOPALIGN   0 CALIGN ;

: OFFSET   ( from to -- offset )   >-< CELL- ;
: OFFSET,   ( to -- )   HERE CELL- SWAP OFFSET , ;
: JOIN   ( from to -- )   OVER CELL- SWAP OFFSET SWAP ! ;

: AHEAD   $0444 ,  HERE  0 , ; IMMEDIATE COMPILING
: S>,   ( FIXME: 1 COMPILE-S> )
   $06201402 , \ 0 MPUSHI MDUP MLOAD 1 MPUSHI
   $06701214 , \ MDUP 4 MPUSHI MADD 1 MPUSHI; FIXME: target CELL, not 4
   $100618 , ; COMPILING \ MSWAP 1 MPUSHI MPOP
: >S,   ( FIXME: 1 COMPILE->S )
   $70F21406 , \ 1 MPUSHI MDUP -4 MPUSHI MADD \ FIXME: target CELL, not 4
   $70F21806 , \ 1 MPUSHI MSWAP -4 MPUSHI MADD
   $24 , ; COMPILING \ MSTORE
: IF
   S>,  $0844 ,  HERE  0 , ; IMMEDIATE COMPILING

: @BRANCH   ( from -- to )   DUP @ + ;
: !BRANCH   ( at from to opcode -- )   HERE >R  >R  OFFSET  SWAP DP !
   R> 8 ( FIXME: INSTRUCTION-BIT ) LSHIFT $44 OR ,  ,  R> DP ! ;

: BRANCH   ( at from to -- )   $04 !BRANCH ;
: CALL   ( at from to -- )   $0C !BRANCH ;

: CALL,   ( to -- )   NOPALIGN  $0C44 ,  HERE - , ;
\ FIXME: name the phrase ">INFO 2 + C@" INLINE-SIZE
\ FIXME: 8 + below is a hack to skip over the primitive's prologue
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP 8 + C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;

: LEAVE, ;