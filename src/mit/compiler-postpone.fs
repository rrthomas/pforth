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

: LINK,   POSTPONE LINK ;
: UNLINK,   POSTPONE UNLINK ;
: DOES-LINK,   POSTPONE (DOES)  LINK, ;

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: END-LOOP,   POSTPONE UNLOOP ; COMPILING


\ Data structures

\ FIXME: Redesign LITERAL, to return address of cell to store value in
: LITERAL,   $44 , ['] (LITERAL) OFFSET,  $0C180240 , ( FIXME: PUSH 0 MPUSHI MSWAP MCALL ) ;
\ Compile code that will push the cell immediately after it
: LITERAL   ( n -- )
   DUP 32 +  64 U< IF \ Use a PUSHI instruction if possible
      2 LSHIFT  $2 OR  C,  NOPALIGN >S, \ FIXME: ALIGN shouldn't be needed
   ELSE
      LITERAL, ,
   THEN ; IMMEDIATE COMPILING

: CREATE,   $0C4403 , ['] (CREATE) OFFSET, ( FIXME: 0 MPUSHRELI MPUSHREL MCALL ) ;