\ Save an object file
: SAVE   ( a-addr u1 c-addr u2 -- )
   2SWAP 2OVER                   \ save filename
   SAVE-FILE
   [ HEX ] FF8 [ DECIMAL ] -ROT TYPE-FILE ;
                                 \ set filetype to Absolute

\ Compiler redefinition and additions
HEX
R: (POSTPONE)   R>  03FFFFFF AND  DUP 4 + >R  @ >NAME FIND  0= IF  UNDEFINED
   THEN  COMPILE, ;
R: !BRANCH   ( at from to op-mask -- )   OVER M0 < ABORT" !BRANCH out of image!"
   -ROT  >BRANCH  OR  SWAP CODE! ;
DECIMAL
RESOLVER (VALUE) WILL-DO VALUE
RESOLVER (VOCABULARY) WILL-DO VOCABULARY
4 REDEFINER >COMPILERS<

: V'   ' >BODY ;


\ Constants

32 1024 * CONSTANT SIZE