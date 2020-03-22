\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

0 VALUE PRIMITIVE-RP
: PRIMITIVE-LINK,
   MLIT_PC_REL MLIT_2 MSTORE NOPALIGN \ FIXME: constant!
   PRIMITIVE-RP OFFSET, ;

: PRIMITIVE-UNLINK,
   MLIT_PC_REL MLIT_2 MLOAD MJUMP \ FIXME: constant!
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
   R> MLIT NOPALIGN ,         \ compile the function code
   R> 8 LSHIFT $01 OR ,       \ compile the library call (FIXME: HACK!)
   END-PRIMITIVE ;            \ finish the definition

: LIBMIT-PRIMITIVE   ( func -- )   LIB_MIT EXT-PRIMITIVE ;
: LIBC-PRIMITIVE   ( func -- )   LIB_C EXT-PRIMITIVE ;
