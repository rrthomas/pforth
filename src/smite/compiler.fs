\ Machine-dependent words (SMite)
\ FIXME: Use assembler
\ Reuben Thomas


\ Branches

\ FIXME: once assembler "built-in", remove the following
: FIT,   ( x -- )   HERE DUP ALIGNED >-<  0 ?DO  DUP C,  8 RSHIFT  LOOP
   DROP ;
: NOPALIGN   0 FIT, ;


\ Core compiler

: BRANCH-ALIGN   HERE 1+  CELL 1- AND  0= IF  0 C,  THEN ;
: AHEAD   BRANCH-ALIGN  HERE  $2E C,  $1E C,  NOPALIGN  0 , ;
IMMEDIATE COMPILING
: IF   BRANCH-ALIGN  HERE  $2E C,  $1F C,  NOPALIGN  0 , ;
IMMEDIATE COMPILING

: !BRANCH   ( at from to opcode -- )   HERE >R  >R  ROT DP !
   BRANCH-ALIGN  $2E C,  R> C,  NIP  NOPALIGN  <'FORTH ,  R> DP ! ;

: BRANCH   ( at from to -- )   $1E !BRANCH ;
: CALL   ( at from to -- )   $29 !BRANCH ;

: JOIN   ( from to -- )   <'FORTH  SWAP  1+ ALIGNED  ! ;

: ADR,   ( to opcode -- )   BRANCH-ALIGN  $2E C,  C,  NOPALIGN  <'FORTH , ;
: CALL,   ( to -- )   $29 ADR, ;
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;

: LINK, ;
: UNLINK,   $2A C, ;
: LEAVE,   BRANCH-ALIGN  $2E C,  $1E C, ;