\ Machine-dependent words (Mit)
\ FIXME: Use assembler
\
\ (c) Reuben Thomas 2018-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Core compiler

: NOPALIGN   0 CALIGN ;

: @LITERAL   ALIGNED @ ;
: !LITERAL   ALIGNED ! ;
: LITERAL,   ( n -- )   $10 C,  NOPALIGN , ;

: OFFSET   ( from to -- offset )   >-< CELL- ;
: OFFSET,   ( to -- )   HERE CELL- SWAP OFFSET , ;
: JOIN   ( from to -- )   OVER CELL- SWAP OFFSET SWAP ! ;

: AHEAD   $0111 ,  HERE  0 , ; IMMEDIATE COMPILING
: IF   $0211 ,  HERE  0 , ; IMMEDIATE COMPILING

: @BRANCH   ( from -- to )   DUP @ + ;
: !BRANCH   ( at from to opcode -- )   HERE >R  >R  OFFSET  SWAP DP !
   R> 8 ( FIXME: INSTRUCTION-BIT ) LSHIFT $11 OR ,  ,  R> DP ! ;

: BRANCH   ( at from to -- )   $01 !BRANCH ;
: CALL   ( at from to -- )   $03 !BRANCH ;

: CALL,   ( to -- )   NOPALIGN  $0311 ,  HERE - , ;
\ FIXME: name the phrase ">INFO 2 + C@" INLINE-SIZE
\ FIXME: 12 + below is a hack to skip over the primitive's prologue
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP 12 + C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;

: LEAVE, ;