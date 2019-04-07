\ FIXME: presumably move this into util-postpone.fs

INCLUDE" branch-cells.fs" CELLS CONSTANT PRIMITIVE-RP
: PRIMITIVE-LINK,
   PRIMITIVE-RP LITERAL,
   2 LITERAL, BSTORE ; \ FIXME: constant!

: PRIMITIVE-UNLINK,
   PRIMITIVE-RP LITERAL,
   2 LITERAL, BLOAD \ FIXME: constant!
   BBRANCH ;

\ Create SMite assembler primitives
: PRIMITIVE   ( args results -- code-start )
   2DROP
   CODE  PRIMITIVE-LINK,                \ make a word
   _FETCH HERE ; \ FIXME: _FETCH is a hack to enable inlining

: END-PRIMITIVE   ( code-start -- )
   _FETCH HERE \ FIXME: _FETCH is a hack to enable inlining
   PRIMITIVE-UNLINK, END-CODE  >-< CELL/ INLINE ;

\ Create SMite EXT calls
: EXT-PRIMITIVE   ( args results func lib -- )
   >R >R  PRIMITIVE           \ make a primitive
   R> LITERAL,                \ compile the function code
   R> LITERAL,                \ compile the library code
   BEXT
   END-PRIMITIVE ;            \ finish the definition

: LIBSMITE-PRIMITIVE   ( args results func -- )   LIB_SMITE EXT-PRIMITIVE ;
: LIBC-PRIMITIVE   ( args results func -- )   LIB_C EXT-PRIMITIVE ;
