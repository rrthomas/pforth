\ Compiler

HEX
\ IMMEDIATE both for run-time speed and R: compatibility with Beetle version
\ FIXME: It should not be necessary for the latter reason
\ FIXME: Inline POSTPONE LITERAL to avoid loop with definition of (POSTPONE)
: R>ADDRESS   ['] (LITERAL) COMPILE, 03FFFFFF ,  ['] AND COMPILE, ;
IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS  DUP 4 + >R  @ CURRENT-COMPILE, ; COMPILING
DECIMAL

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: UNLOOP,   POSTPONE UNLOOP ; COMPILING


\ Data structures

: LITERAL   POSTPONE (LITERAL) ALIGN  , ; IMMEDIATE COMPILING

: CREATE,   LINK,  POSTPONE (CREATE) ;
: >BODY   2 CELLS + ;
( >DOES given an execution token returns the address of the branch to the
DOES> code. There is always at least an aligned cell after this address free
for messing around, although adr itself may not be aligned. )
: >DOES   ( xt -- adr )   4 + ;
: (DOES>)   LAST >DOES  DUP  R> R>ADDRESS @  CALL ;