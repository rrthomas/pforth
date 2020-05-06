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

: @LITERAL   ALIGNED @ ;
: !LITERAL   ALIGNED ! ;
: LITERAL,   ( n -- )
   DUP 32 +  64 U< IF \ Use a PUSHI instruction if possible
      2 LSHIFT  $2 OR  C,  NOPALIGN \ FIXME: ALIGN shouldn't be needed
   ELSE
      $40 C,  NOPALIGN ,
   THEN ;

: OFFSET   ( from to -- offset )   >-< CELL- ;
: OFFSET,   ( to -- )   HERE CELL- SWAP OFFSET , ;
: JOIN   ( from to -- )   OVER CELL- SWAP OFFSET SWAP ! ;

: AHEAD   $0444 ,  HERE  0 , ; IMMEDIATE COMPILING
: IF   $0844 ,  HERE  0 , ; IMMEDIATE COMPILING

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