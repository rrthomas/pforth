\ Create SMite assembler primitives
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

\ Create SMite extra instructions
: EXTRA-INSTRUCTION   ( +n -- )
   CODE                       \ make a code word
   LITERAL,                   \ compile the extra instruction code
   0 LITERAL,  BEXTRA         \ +n 0 EXTRA
   BRET                       \ append RET
   END-CODE ;                 \ finish the definition

: EXTRA-INSTRUCTIONS   ( +n1 +n2 -- )
   SWAP 1+ SWAP DO  I EXTRA-INSTRUCTION  LOOP ;