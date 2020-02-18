\ Create Beetle assembler primitives
\
\ (c) Reuben Thomas 1995-2016
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

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
      BEXIT                      \ append EXIT
      END-CODE                   \ finish the definition
   LOOP ;