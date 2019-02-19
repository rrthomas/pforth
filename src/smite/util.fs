\ FIXME: presumably move this into util-postpone.fs

\ Create SMite assembler primitives
\ FIXME: optimise out 0 ROTATE
: PRIMITIVE   ( args results -- results args )
   SWAP                       \ ( results args )
   CODE                       \ make a code word
   DUP NEGATE LITERAL,        \ save return address under arguments
   BROTATE
   HERE -ROT ;                \ ( here results args )

: END-PRIMITIVE-CODE   ( here results args -- )
   2DROP DROP ;

: END-PRIMITIVE   ( here results args -- )
   DROP  HERE SWAP  LITERAL, BROTATE BBRANCH  END-CODE  >-< INLINE ;

\ Create SMite EXTRA calls
: EXTRA-PRIMITIVE   ( args results u -- )
   >R  PRIMITIVE              \ make a primitive
   R> LITERAL,                \ compile the extra instruction code
   0 LITERAL,  BEXTRA         \ u 0 EXTRA
   END-PRIMITIVE ;            \ finish the definition
