: (.ALIGN)   ." .balign bee_word_bytes" CR ;
:NONAME   ['] (.ALIGN)  TO-ASMOUT ; IS .ALIGN
: (.CALIGN)   ." .balign bee_word_bytes, 0x" H. CR ;
:NONAME   ['] (.CALIGN)  TO-ASMOUT ; IS .CALIGN
: (.REL-OFFSET)   ." .word "  ?DUP IF   BACKWARD .LABEL ."  - ."  ELSE ." 0"  THEN CR  ;
:NONAME   ['] (.REL-OFFSET)  TO-ASMOUT ; IS .REL-OFFSET
: (.NOP)   ." nop " CR ;
:NONAME   ['] (.NOP)  TO-ASMOUT ; IS .NOP
: (.ALLOT)   ." .ds.b " . CR ;
:NONAME   ['] (.ALLOT)  TO-ASMOUT ; IS .ALLOT
: (.ALLOT-CELLS)   ." .ds.b " . ." * bee_word_bytes" CR ;
:NONAME   ['] (.ALLOT-CELLS)  TO-ASMOUT ; IS .ALLOT-CELLS
: (.WORD)   ." .word " . CR ;
:NONAME   ['] (.WORD)  TO-ASMOUT ; IS .WORD
: (.BYTE)   ." .byte 0x" H. CR ;
:NONAME   ['] (.BYTE)  TO-ASMOUT ; IS .BYTE
: (.STRING)   ." .ascii "
   [CHAR] " DUP EMIT -ROT
   OVER + SWAP DO
      I C@
      DUP [CHAR] " =  OVER [CHAR] \ = OR  IF
         [CHAR] \ EMIT
      THEN
      EMIT
   LOOP
   EMIT  CR ;
:NONAME   ['] (.STRING)  TO-ASMOUT ; IS .STRING
: (.PUSHI)   ." pushi " . CR ;
:NONAME   ['] (.PUSHI)  TO-ASMOUT ; IS .PUSHI
: (.PUSHRELI)   ." pushreli " .SYMBOL CR ;
:NONAME   ['] (.PUSHRELI)  TO-ASMOUT ; IS .PUSHRELI
: (.PUSH)   HERE ." calli " DUP FORWARD .LABEL CR
   SWAP (.WORD)
   FORWARD .LABEL-DEF
   ." pops" CR
   ." load" CR ;
:NONAME   ['] (.PUSH)  TO-ASMOUT ; IS .PUSH
: (.LABEL)   SWAP ." .L" ADDR>LABEL 0 U.R EMIT ;
:NONAME   ['] (.LABEL)  TO-ASMOUT ; IS .LABEL
: (.LABEL-DEF)   .LABEL ." :" CR ;
:NONAME   ['] (.LABEL-DEF)  TO-ASMOUT ; IS .LABEL-DEF
: (.BODY-LABEL-DEF)   >NAME .NAME ." _body:" CR ;
:NONAME   ['] (.BODY-LABEL-DEF)  TO-ASMOUT ; IS .BODY-LABEL-DEF
: (.BRANCH)   ." jumpi " .LABEL CR ;
:NONAME   ['] (.BRANCH)  TO-ASMOUT ; IS .BRANCH
: (.IF)   ." jumpzi " .LABEL CR ;
:NONAME   ['] (.IF)  TO-ASMOUT ; IS .IF
: (.RET)   ." ret" CR ;
:NONAME   ['] (.RET)  TO-ASMOUT ; IS .RET
: (.IMMEDIATE-METHOD)   ." .set " .NAME ." _compilation, (2 * bee_word_bytes)" CR ;
:NONAME   ['] (.IMMEDIATE-METHOD)  TO-ASMOUT ; IS .IMMEDIATE-METHOD
: (.COMPILE-METHOD)   ." .set " TUCK .NAME ." _compilation, " NONAME .LABEL ."  - (" .NAME ."  - 2 * bee_word_bytes)" CR ;
:NONAME   ['] (.COMPILE-METHOD)  TO-ASMOUT ; IS .COMPILE-METHOD
: (.CALL-COMPILE-METHOD)   ." calli " DUP .NAME ."  - (2 * bee_word_bytes) + " .NAME ." _compilation" CR ;
:NONAME   ['] (.CALL-COMPILE-METHOD)  TO-ASMOUT ; IS .CALL-COMPILE-METHOD
: (.INLINE-COUNT)   ." .set " .NAME ." _inline, " 0 U.R CR ;
:NONAME   ['] (.INLINE-COUNT)  TO-ASMOUT ; IS .INLINE-COUNT
: (.CREATED-CODE)   ." calli " .NAME ." _doer" CR ;
:NONAME   ['] (.CREATED-CODE)  TO-ASMOUT ; IS .CREATED-CODE
: (.PUSHRELI-SYMBOL)   ." pushreli " .NAME CR ;
:NONAME   ['] (.PUSHRELI-SYMBOL)  TO-ASMOUT ; IS .PUSHRELI-SYMBOL
