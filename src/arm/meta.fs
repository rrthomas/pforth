\ Save an object file
: '?   BL WORD FIND  0= IF  DROP 0  THEN ;
'? TYPE-FILE VALUE 'TYPE-FILE
HEX
: SAVE   ( a-addr u1 c-addr u2 -- )
   2SWAP 2OVER                   \ save filename
   SAVE-FILE
   'TYPE-FILE  ?DUP IF           \ if we have TYPE-FILE,
      >R FF8 -ROT R> EXECUTE     \ set filetype to Absolute
   ELSE
      2DROP                      \ otherwise drop file name
   THEN ;

\ Compiler redefinition and additions

\ STUB FOO creates an empty word if FOO doesn't exist.
\ This is used to POSTPONE target words that don't exist
\ on the host.
: STUB
   BL WORD FIND  0= IF
      HEADER  0 ,                \ reserve enough space for redefinition
                                 \ FIXME: make portable; see REDEFINER
   ELSE
      DROP                       \ if found, discard xt
   THEN ;

\ Create stubs for words that may not exist on host
STUB (LITERAL)
STUB (LOOP)
STUB (+LOOP)
STUB UNLOOP
STUB (CREATE)

8000 CONSTANT TARGET-'FORTH
ALSO ASSEMBLER   \ access to compiling words that may not exist on host

: OS   ( regs-in regs-out swi -- )
   -ROT 2DUP + >R ROT
   R@ IF  E52CB004 CODE,  THEN
   ROT ?DUP IF
      1 SWAP LSHIFT 1- E8BC0000 OR CODE,
   THEN
   EF000000 OR CODE,
   ?DUP IF
      1 SWAP LSHIFT 1- E92C0000 OR CODE,
   THEN
   R> IF  E49CB004 CODE,  THEN ;
IMMEDIATE COMPILING
: OS"   ( name )   ( regs-in regs-out -- )   [CHAR] " PARSE OS#
   POSTPONE OS ; IMMEDIATE COMPILING

: DOES>-RESOLVER   RESOLVER ;

R: <'FORTH   'FORTH - TARGET-'FORTH + ;
R: >'FORTH   'FORTH + TARGET-'FORTH - ;
\ FIXME: Share the following with machdeps.fs
R: NOPALIGN   ALIGN ;
R: AHEAD   HERE  EA000000 CODE, ;
R: IF   E35B0000 CODE, E49CB004 CODE,  HERE  0A000000 CODE, ;
R: R>ADDRESS   03FFFFFF POSTPONE LITERAL  POSTPONE AND ;
R: !BRANCH   ( at from to op-mask -- )   OVER 'FORTH < ABORT" !BRANCH out of image"
   -ROT  >BRANCH  OR  SWAP CODE! ;
R: BRANCH   ( at from to -- )    EA000000 !BRANCH ;
R: CALL   ( at from to -- )   EB000000 !BRANCH ;
R: JOIN   ( from to -- )   OVER TUCK @ !BRANCH ;
R: CALL,   ( to -- )   HERE  4 ALLOT  DUP ROT CALL ;
R: COMPILE,   CALL, ;
R: LINK,   E52DE004 CODE, ;
R: UNLINK,   E49DF004 CODE, ;
R: LEAVE,   E51FF004 CODE, ;
R: (POSTPONE)   R> R>ADDRESS  DUP CELL+ >R  FIND-AND-COMPILE, ;
R: DO,   POSTPONE 2>R ;
R: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ;
R: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ;
R: UNLOOP,   POSTPONE UNLOOP ;
R: LITERAL   POSTPONE (LITERAL) ALIGN  , ;
R: CREATE,   LINK,  POSTPONE (CREATE) ;
R: >DOES   ( xt -- adr )   4 + ;
R: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS @  CALL ;
R: DOES>   POSTPONE (DOES>) ALIGN  HERE CELL+  LAST  2DUP - CELL/  SWAP >INFO
   DUP @ ROT OR  SWAP !  <'FORTH ,  LINK,  POSTPONE (DOES) ;
DECIMAL
\ FIXME: These are duplicated in beetle/meta.fs
DOES>-RESOLVER (VALUE) WILL-DO VALUE
DOES>-RESOLVER (VECTOR) WILL-DO VECTOR
DOES>-RESOLVER (VOCABULARY) WILL-DO VOCABULARY
\ On Beetle (DOES) is IMMEDIATE, so "POSTPONE (DOES)" in DOES> simply runs
\ it; on ARM, (DOES) is not immediate; hence, do a manual POSTPONE.
\ This must NOT be redefined on RISC OS, as it upsets all words created by defining words!
R: (DOES)   C" (DOES)" FIND  DROP COMPILE, ;
29   \ number of redefinitions
: CHECK-(DOES)   C" (DOES)" FIND  NIP -1 = IF  1- -ROT 2DROP  THEN ;
CHECK-(DOES)   \ don't redefine (DOES) if it already exists
REDEFINER >COMPILERS<


\ Constants

32 1024 * CONSTANT SIZE
0 VALUE 'THROW-CONTENTS
