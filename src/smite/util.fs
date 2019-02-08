\ Create SMite assembler primitives
: PRIMITIVE   ( args results -- results args )
   CODE                       \ make a code word
   HERE -ROT
   SWAP ;                     \ ( here results args )

: END-PRIMITIVE-CODE   ( here results args -- )
   2DROP DROP ;

: END-PRIMITIVE
   2DROP  HERE  BRET  END-CODE  >-< INLINE ;

: PRIMITIVES   ( +n -- )
   0 DO
      [CHAR] B PAD CHAR+ C!      \ store "B" at start of name
      BL WORD  COUNT TUCK        \ get name
      PAD 2 CHARS + SWAP CMOVE   \ append it to the "B"
      CHAR+ DUP NEGATE >IN +!    \ move >IN back over the name
      PAD  TUCK C!               \ save PAD; store name's length
      CODE                       \ make an inline code word
      1 INLINE                   \ with one byte of code
      FIND DROP  EXECUTE         \ append the opcode
      BRET                       \ append RET
      END-CODE                   \ finish the definition
   LOOP ;

\ Create SMite EXTRA calls
: EXTRA-PRIMITIVE   ( args results u -- )
   >R  PRIMITIVE              \ make a primitive
   R> LITERAL,                \ compile the extra instruction code
   0 LITERAL,  BEXTRA         \ u 0 EXTRA
   END-PRIMITIVE ;            \ finish the definition
