\ FIXME: presumably move this into util-postpone.fs

INCLUDE" branch-cells.fs" CELLS CONSTANT PRIMITIVE-RP
: PRIMITIVE-LINK,
   PRIMITIVE-RP LITERAL,
   2 LITERAL, BSTORE ; \ FIXME: constant!

: PRIMITIVE-UNLINK,
   PRIMITIVE-RP LITERAL,
   2 LITERAL, BLOAD \ FIXME: constant!
   BBRANCH ;

\ Create Mit assembler primitives
: PRIMITIVE   ( args results -- code-start )
   2DROP
   CODE  PRIMITIVE-LINK,                \ make a word
   _FETCH HERE ; \ FIXME: _FETCH is a hack to enable inlining

: END-PRIMITIVE   ( code-start -- )
   _FETCH HERE \ FIXME: _FETCH is a hack to enable inlining
   PRIMITIVE-UNLINK, END-CODE  >-< CELL/ INLINE ;

\ Create Mit EXT calls
: EXT-PRIMITIVE   ( args results func lib -- )
   >R >R  PRIMITIVE           \ make a primitive
   R> LITERAL,                \ compile the function code
   NOPALIGN
   R> 8 LSHIFT $01 OR ,       \ compile the library call (FIXME: HACK!)
   END-PRIMITIVE ;            \ finish the definition

: LIBMIT-PRIMITIVE   ( args results func -- )   LIB_MIT EXT-PRIMITIVE ;
: LIBC-PRIMITIVE   ( args results func -- )   LIB_C EXT-PRIMITIVE ;
