\ Save an object file
: SAVE   ( a-addr u1 c-addr u2 -- )
   W/O CREATE-FILE DROP          \ open file
   >R                            \ save file-id
   S" BEETLE" R@ WRITE-FILE DROP \ write header
   0 SCRATCH TUCK C!  1  2DUP  R@ WRITE-FILE DROP
   R@ WRITE-FILE DROP
   DUP CELL/ SCRATCH TUCK !  CELL R@ WRITE-FILE DROP
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file

\ Create bForth assembler primitives
: PRIMITIVES   ( +n -- )
   0 DO
      [CHAR] B PAD CHAR+ C!      \ store "B" at start of name
      BL WORD  COUNT TUCK        \ get name
      PAD 2 CHARS + SWAP CMOVE   \ append it to the "B"
      CHAR+ DUP NEGATE >IN +!    \ move >IN back over the name
      PAD  TUCK C!               \ save PAD; store name's length
      CODE                       \ make an inline code word
      [ ALSO ASSEMBLER ]
      1 INLINE                   \ with one byte of code
      FIND DROP  EXECUTE         \ append the opcode      
      BEXIT                      \ append EXIT
      END-CODE                   \ finish the definition
      [ PREVIOUS ]
   LOOP ;


\ Compiler redefinition and additions

ALSO ASSEMBLER
: ADR,   ( to opcode -- )   OVER 'FORTH < ABORT" ADR, out of image!"
   OVER HERE 1+ ALIGNED - CELL/  DUP HERE 1+ FITS
   IF  SWAP 1+ C, FIT,  DROP  ELSE DROP C,  NOPALIGN  <'FORTH ,  THEN ;

HEX
: EXECUTE   STATE @ IF  46 C,  ALIGN  ELSE  EXECUTE  THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  47 C,  ALIGN  ELSE  @EXECUTE  THEN ; IMMEDIATE
: OFFSET   ( from to -- offset )   OVER 'FORTH <  OVER 'FORTH <  OR ABORT" OFFSET out of image!"
   >-<  CELL/ 1-  00FFFFFF AND ;

10 CONSTANT TARGET-'FORTH

R: DOES>-RESOLVER   BRANCH-RESOLVER ;
1 REDEFINER DOES>-RESOLVER-REDEFINER
DOES>-RESOLVER-REDEFINER

\ FIXME: The following two definitions are the same on any platform; move to make.fs
R: <'FORTH   'FORTH - TARGET-'FORTH + ;
R: >'FORTH   'FORTH + TARGET-'FORTH - ;
R: AHEAD   HERE  42 C,  NOPALIGN  0 , ;
R: IF   HERE  44 C,  NOPALIGN  0 , ;
R: LITERAL   B(LITERAL) ;
R: NOPALIGN   0 FIT, ;
R: !BRANCH   ( at from to opcode -- )   -ROT  OFFSET  8 LSHIFT  OR  SWAP ! ;
R: BRANCH   ( at from to -- )   43 !BRANCH ;
R: CALL   ( at from to -- )   49 !BRANCH ;
R: JOIN   ( from to -- )   <'FORTH  SWAP 1+ ALIGNED  ! ;
R: CALL,   ( to -- )   48 ADR, ;
R: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP C@ C,  1+  LOOP  DROP
   ELSE CALL,  THEN ;
R: LINK, ;
R: UNLINK,   4A C, ;
R: DO,   4B C, ;
R: LOOP,   4C ADR, ;
R: +LOOP,   4E ADR, ;
R: LEAVE,   50 C, 42 C, ;
R: UNLOOP, ;
R: CREATE,   LINK,  56 C,  23 C,  NOPALIGN  UNLINK,  NOPALIGN ;
\ FIXME: use more efficient definition on Beetle?
\ R: VALUE   CREATE HERE 2 CELLS - DP !  56 C,  23 C,  39 C,  4A C,  NOPALIGN  0 ,  , ;
R: >DOES   ( xt -- adr ) CELL+ ;
R: (DOES>)   LAST >DOES  DUP  R> @  BRANCH ;
\ FIXME: Redefining (DOES) in the meta-compiler does not suffice to stop a call being
\ compiled, as on RISC OS it is a normal word, so a call to an empty word would
\ be compiled; hence, redefine DOES>. This should really be fixed: POSTPONE
\ should re-check the IMMEDIATE flag of the word it's compiling in the meta-compiler.
\ But to make that work, it would need to compile a harmless call before each
\ IMMEDIATE word, so that that can itself be redefined.
R: DOES>   POSTPONE (DOES>) ALIGN  HERE CELL+  LAST  2DUP - CELL/  SWAP >INFO
   DUP @ ROT OR  SWAP !  <'FORTH , ;
R: (POSTPONE)   R>  03FFFFFF AND  DUP CELL+ >R  FIND-AND-COMPILE, ;
DECIMAL
DOES>-RESOLVER (VALUE) WILL-DO VALUE
DOES>-RESOLVER (VECTOR) WILL-DO VECTOR
DOES>-RESOLVER (VOCABULARY) WILL-DO VOCABULARY
27 REDEFINER >COMPILERS<
DOES>-RESOLVER-REDEFINER

PREVIOUS


\ Constants and patch phrases

32 1024 * CONSTANT SIZE
: 'THROW-CONTENTS   'FORTH TARGET-'FORTH - ;
