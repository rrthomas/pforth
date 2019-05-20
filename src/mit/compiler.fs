\ Machine-dependent words (Mit)
\ FIXME: Use assembler
\ Reuben Thomas


\ Core compiler

\ FIXME: Distinguish the case where we must align to a cell boundary for
\ code-fiddling reasons (e.g. REDEFINER) from that where we need to align to
\ a branch target (AHEAD); the latter is a null operation on Mit
: NOPALIGN   ALIGN ;

: @LITERAL   ALIGNED @ ;
: !LITERAL   ALIGNED ! ;
: LITERAL,   ( n -- )
   DUP 4 U< IF \ Use a LIT_n instruction if possible
      $0C + C,  ALIGN \ FIXME: ALIGN shouldn't be needed
   ELSE
      $0A C,  ALIGN ,
   THEN ;

: AHEAD   HERE  $010A ,  0 , ; IMMEDIATE COMPILING
: IF   HERE  $020A ,  0 , ; IMMEDIATE COMPILING

: @BRANCH   ( from -- to )   1+ @LITERAL >'FORTH ;
: !BRANCH   ( at from to opcode -- )   HERE >R  >R  ROT DP !
   R> 8 ( FIXME: INSTRUCTION-BIT ) LSHIFT $0A OR ,  <'FORTH ,  DROP  R> DP ! ;

: BRANCH   ( at from to -- )   $01 !BRANCH ;
: CALL   ( at from to -- )   $03 !BRANCH ;

: JOIN   ( from to -- )   <'FORTH  SWAP 1+ !LITERAL ;

: CALL,   ( to -- )   ALIGN  $030A ,  <'FORTH , ;
\ FIXME: name the phrase ">INFO 2 + C@" INLINE-SIZE
\ FIXME: 12 + below is a hack to skip over the primitive's prologue
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP 12 + C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;

: LEAVE, ;