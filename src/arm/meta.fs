\ Save an object file
: SAVE   ( a-addr u1 c-addr u2 -- )
   2SWAP 2OVER                   \ save filename
   SAVE-FILE
   [ HEX ] FF8 [ DECIMAL ] -ROT TYPE-FILE ;
                                 \ set filetype to Absolute

\ Compiler redefinition and additions
HEX
8000 CONSTANT TARGET-'FORTH

R: <'FORTH   'FORTH - TARGET-'FORTH + ;
R: >'FORTH   'FORTH + TARGET-'FORTH - ;
\ FIXME: Share the following with machdeps.fs
\ FIXME: Words defined in highlevel.fs should not be redefined here
R: AHEAD   HERE  EA000000 CODE, ;
R: IF   E35B0000 CODE, E49CB004 CODE,  HERE  0A000000 CODE, ;
R: LITERAL   POSTPONE (LITERAL) ALIGN  , ;
R: NOPALIGN   ALIGN ;
R: !BRANCH   ( at from to op-mask -- )   OVER 'FORTH < ABORT" !BRANCH out of image!"
   -ROT  >BRANCH  OR  SWAP CODE! ;
R: BRANCH   ( at from to -- )    EA000000 !BRANCH ;
R: CALL   ( at from to -- )   EB000000 !BRANCH ;
R: JOIN   ( from to -- )   OVER TUCK @ !BRANCH ;
R: CALL,   ( to -- )   HERE  4 ALLOT  DUP ROT CALL ;
R: COMPILE,   CALL, ;
R: LINK,   E52DE004 CODE, ;
R: UNLINK,   E49DF004 CODE, ;
R: DO,   POSTPONE 2>R ;
R: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ;
R: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ;
R: LEAVE,   E51FF004 CODE, ;
R: UNLOOP,   POSTPONE UNLOOP ;
R: I   POSTPONE R@ ;
R: ?DO   'NODE @  0 'NODE !  POSTPONE 2DUP DO,  POSTPONE = POSTPONE IF
   POSTPONE LEAVE POSTPONE THEN  POSTPONE BEGIN ;
R: >NODE   'NODE @  BEGIN  ?DUP WHILE  DUP @  HERE <'FORTH ROT !  REPEAT ;
R: CREATE,   LINK,  POSTPONE (CREATE) ;
R: >DOES   ( xt -- adr )   4 + ;
R: (DOES>)   LAST >DOES  DUP  R> 03FFFFFF AND @  CALL ;
R: DOES>   POSTPONE (DOES>) ALIGN  HERE CELL+  LAST  2DUP - CELL/  SWAP >INFO
   DUP @ ROT OR  SWAP !  <'FORTH ,  LINK,  POSTPONE (DOES) ;
\ FIXME: (POSTPONE)'s redefinition is tricky: it uses FIND to ensure that
\ the target word is found, not the host word, and the 03FFFFFF AND pattern
\ is only needed on RISC OS. In other words, this should be a standard
\ meta-compiler definition made with a system-specific piece to get the
\ return address.
R: (POSTPONE)   R>  03FFFFFF AND  DUP 4 + >R  @ >NAME FIND  0= IF  UNDEFINED
   THEN  COMPILE, ;
DECIMAL
\ FIXME: These are duplicated in beetle/make.fs
RESOLVER (VALUE) WILL-DO VALUE
RESOLVER (VECTOR) WILL-DO VECTOR
RESOLVER (VOCABULARY) WILL-DO VOCABULARY
30 REDEFINER >COMPILERS<

: V'   ' >BODY ;


\ Constants

32 1024 * CONSTANT SIZE
0 VALUE 'THROW-CONTENTS
