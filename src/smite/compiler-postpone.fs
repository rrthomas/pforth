\ Compiler words that need special treatment during meta-compilation owing
\ to their use of POSTPONE

\ Compiler

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: UNLOOP,   POSTPONE UNLOOP ; COMPILING


\ Data structures

\ FIXME: Hack to change CALL to BRANCH; instead, want POSTPONE that does
\ BRANCH (including version in make-base.fs)
: CREATE,   $A6 C,  POSTPONE (CREATE)  $99 HERE 1- C!  NOPALIGN ;
: (DOES)   POSTPONE (CREATE) ; IMMEDIATE COMPILING