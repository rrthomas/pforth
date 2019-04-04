\ Machine-dependent words (SMite)
\ FIXME: Use assembler
\ Reuben Thomas


\ Core compiler

\ FIXME: Distinguish the case where we must align to a cell boundary for
\ code-fiddling reasons (e.g. REDEFINER) from that where we need to align to
\ a branch target (AHEAD); the latter is a null operation on SMite
: NOPALIGN   ALIGN ;

: @LITERAL   ALIGNED @ ;
: !LITERAL   ALIGNED ! ;
: LITERAL,   ( n -- )   $07 C,  ALIGN , ;

: AHEAD   HERE  $0107 ,  0 , ; IMMEDIATE COMPILING
: IF   HERE  $0207 ,  0 , ; IMMEDIATE COMPILING

: @BRANCH   ( from -- to )   1+ @LITERAL >'FORTH ;
: !BRANCH   ( at from to opcode -- )   HERE >R  >R  ROT DP !
   R> 8 ( FIXME: INSTRUCTION-BIT ) LSHIFT $07 OR ,  <'FORTH ,  DROP  R> DP ! ;

: BRANCH   ( at from to -- )   $01 !BRANCH ;
: CALL   ( at from to -- )   $03 !BRANCH ;

: JOIN   ( from to -- )   <'FORTH  SWAP 1+ !LITERAL ;

: CALL,   ( to -- )   ALIGN  $0307 ,  <'FORTH , ;
\ FIXME: name the phrase ">INFO 2 + C@" INLINE-SIZE
\ FIXME: 12 + below is a hack to skip over the primitive's prologue
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP 12 + @ ,  CELL+  LOOP  DROP
   ELSE CALL,  THEN ;

: LEAVE, ;