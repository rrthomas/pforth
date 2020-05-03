\ Compiler words that need special treatment during meta-compilation owing
\ to their use of POSTPONE
\
\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

: LINK,   POSTPONE >R ;
: UNLINK,   POSTPONE R>  $04 , ;
: DOES-LINK,   POSTPONE (DOES)  LINK, ;

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: END-LOOP,   POSTPONE UNLOOP ; COMPILING


\ Data structures

: CREATE,   POSTPONE (CREATE) ;