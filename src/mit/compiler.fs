\ Machine-dependent words (Mit)
\ FIXME: Use assembler
\ Reuben Thomas


\ Core compiler

: NOPALIGN   0 CALIGN ;

: @LITERAL   ALIGNED @ ;
: !LITERAL   ALIGNED ! ;
: LITERAL,   ( n -- )
   DUP 4 U< IF \ Use a LIT_n instruction if possible
      $0C + C,  NOPALIGN \ FIXME: ALIGN shouldn't be needed
   ELSE
      $0A C,  NOPALIGN ,
   THEN ;

: OFFSET   ( from to -- offset )   >-< CELL- ;
: OFFSET,   ( to -- )   HERE CELL- SWAP OFFSET , ;
: JOIN   ( from to -- )   OVER CELL- SWAP OFFSET SWAP ! ;

: AHEAD   $010B ,  HERE  0 , ; IMMEDIATE COMPILING
: IF   $020B ,  HERE  0 , ; IMMEDIATE COMPILING

: @BRANCH   ( from -- to )   DUP @ + ;
: !BRANCH   ( at from to opcode -- )   HERE >R  >R  OFFSET  SWAP DP !
   R> 8 ( FIXME: INSTRUCTION-BIT ) LSHIFT $0B OR ,  ,  R> DP ! ;

: BRANCH   ( at from to -- )   $01 !BRANCH ;
: CALL   ( at from to -- )   $03 !BRANCH ;

: CALL,   ( to -- )   NOPALIGN  $030B ,  HERE - , ;
\ FIXME: name the phrase ">INFO 2 + C@" INLINE-SIZE
\ FIXME: 8 + below is a hack to skip over the primitive's prologue
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP 8 + C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;

: LEAVE, ;