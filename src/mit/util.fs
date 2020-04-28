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
: PRIMITIVE   ( args results -- code-start )
   2DROP
   CODE  PRIMITIVE-LINK,                \ make a word
   _FETCH HERE ; \ FIXME: _FETCH is a hack to enable inlining

: END-PRIMITIVE   ( code-start -- )
   _FETCH HERE \ FIXME: _FETCH is a hack to enable inlining
   PRIMITIVE-UNLINK, END-CODE  >-< INLINE ;

\ Create Mit EXT calls
: EXT-PRIMITIVE   ( func lib -- )
   >R >R  0 0 PRIMITIVE       \ make a primitive (FIXME: don't call PRIMITIVE)
   MPUSH MPUSH MTRAP NOPALIGN
   R> ,                       \ compile the function code
   R> ,                       \ compile the library call
   END-PRIMITIVE ;            \ finish the definition

: LIBC-PRIMITIVE   ( func -- )   LIB_C EXT-PRIMITIVE ;
