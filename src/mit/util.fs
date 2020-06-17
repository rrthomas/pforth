\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

0 VALUE PRIMITIVE-RP
: PRIMITIVE-LINK,
   MPUSHREL MSTORE NOPALIGN
   PRIMITIVE-RP OFFSET, ;

: PRIMITIVE-UNLINK,
   MPUSHREL MLOAD MJUMP NOPALIGN
   PRIMITIVE-RP OFFSET, ;


\ Create Mit assembler primitives

: PRIMITIVE   ( args results -- results code-start )
   SWAP                         \ ( results args )
   CODE  PRIMITIVE-LINK,        \ make a word
   _FETCH HERE \ FIXME: _FETCH is a hack to enable inlining
   SWAP COMPILE-S> ; \ pop arguments from pForth data stack

: END-PRIMITIVE   ( results code-start -- )
   SWAP COMPILE->S    \ push results to pForth data stack
   _FETCH HERE \ FIXME: _FETCH is a hack to enable inlining
   PRIMITIVE-UNLINK, END-CODE  >-< INLINE ;

\ Create Mit trap calls
: TRAP   ( func lib -- )
   >R >R  PRIMITIVE           \ make a primitive
   MPUSH NOPALIGN  R> ,       \ compile the function code
   R> INSTRUCTION-BIT LSHIFT  $FF OR , \ compile the lib code
   END-PRIMITIVE ;            \ finish the definition

: LIBC-PRIMITIVE   ( args results func -- )   LIB_C TRAP ;
