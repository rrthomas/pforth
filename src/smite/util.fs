\ Create SMite assembler primitives
: PRIMITIVE   ( args results -- results args )
   CODE                       \ make a code word
   HERE -ROT
   SWAP ;                     \ ( here results args )

: END-PRIMITIVE-CODE   ( here results args -- )
   2DROP DROP ;

: END-PRIMITIVE
   2DROP  HERE  BPOP_FRAME BBRANCH  END-CODE  >-< INLINE ;

\ Create SMite EXTRA calls
: EXTRA-PRIMITIVE   ( args results u -- )
   >R  PRIMITIVE              \ make a primitive
   R> LITERAL,                \ compile the extra instruction code
   0 LITERAL,  BEXTRA         \ u 0 EXTRA
   END-PRIMITIVE ;            \ finish the definition
