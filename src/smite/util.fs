\ FIXME: presumably move this into util-postpone.fs

INCLUDE" branch-cells.fs" CELLS CONSTANT PRIMITIVE-RP
: PRIMITIVE-LINK,
   PRIMITIVE-RP LITERAL,
   BSTORE ;

: PRIMITIVE-UNLINK,
   PRIMITIVE-RP LITERAL,
   BLOAD
   BBRANCH ;

\ Create SMite assembler primitives
: PRIMITIVE   ( args results -- code-start )
   2DROP
   CODE  PRIMITIVE-LINK,                \ make a word
   HERE ;

: END-PRIMITIVE   ( code-start -- )
   HERE  PRIMITIVE-UNLINK, END-CODE  >-< DROP ; \ FIXME: INLINE, not DROP!

\ Create SMite EXTRA calls
: EXTRA-PRIMITIVE   ( args results u xt -- )
   >R >R  PRIMITIVE           \ make a primitive
   R> LITERAL,                \ compile the extra instruction code
   R> EXECUTE                 \ compile the library extra instruction
   END-PRIMITIVE ;            \ finish the definition

: LIBC-PRIMITIVE   ( args results u -- )
   ['] BLIB_C EXTRA-PRIMITIVE ;

: SMITE-PRIMITIVE   ( args results u -- )
   ['] BLIB_SMITE EXTRA-PRIMITIVE ;
