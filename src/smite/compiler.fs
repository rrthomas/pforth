\ Machine-dependent words (SMite)
\ FIXME: Use assembler
\ Reuben Thomas


\ Branches

\ FIXME: once assembler "built-in", remove the following
: FITS   ( x addr -- flag )   DUP ALIGNED >-<  DUP IF  8 * 1-  1 SWAP LSHIFT
   SWAP DUP 0< IF  INVERT  THEN  U>  ELSE NIP  THEN ;
: FIT,   ( x -- )   HERE DUP ALIGNED >-<  0 ?DO  DUP C,  8 RSHIFT  LOOP
   DROP ;
: NOPALIGN   0 FIT, ;


\ Core compiler

: AHEAD   HERE  $4C C,  NOPALIGN  0 , ; IMMEDIATE COMPILING
: IF   HERE  $4E C,  NOPALIGN  0 , ; IMMEDIATE COMPILING

: !BRANCH   ( at from to opcode -- )   HERE >R  >R  ROT DP !  R> C,  NIP
   NOPALIGN  <'FORTH ,  R> DP ! ;

: BRANCH   ( at from to -- )   $4C !BRANCH ;
: CALL   ( at from to -- )   $52 !BRANCH ;

: JOIN   ( from to -- )   <'FORTH  SWAP  1+ ALIGNED  ! ;

: ADR,   ( to opcode -- )   C,  NOPALIGN  <'FORTH , ;
: CALL,   ( to -- )   $52 ADR, ;
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;

: LINK, ;
: UNLINK,   $54 C, ;
: LEAVE,   $4C C, ;
