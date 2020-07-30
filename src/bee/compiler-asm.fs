: (.SKIP)   ." .skip " . CR ;
:NONAME   ['] (.SKIP)  TO-ASMOUT ; IS .SKIP
: (.ALIGN)   ." .balign " CELL . CR ;
:NONAME   ['] (.ALIGN)  TO-ASMOUT ; IS .ALIGN
: (.CALIGN)   ." .balign " CELL . ." , 0x" H. CR ;
:NONAME   ['] (.CALIGN)  TO-ASMOUT ; IS .CALIGN
: (.INT)   ." .int " . CR ;
:NONAME   ['] (.INT)  TO-ASMOUT ; IS .INT
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
: (.PUSHRELI)   ." pushreli " . CR ;
:NONAME   ['] (.PUSHRELI)  TO-ASMOUT ; IS .PUSHRELI
: (.PUSH)   HERE ." calli " DUP FORWARD .LABEL CR
   SWAP (.INT)
   FORWARD .LABEL-DEF
   ." popr" CR
   ." load" CR ;
:NONAME   ['] (.PUSH)  TO-ASMOUT ; IS .PUSH
: (.LABEL)   SWAP ." .L" ADDR>LABEL 0 U.R EMIT ;
:NONAME   ['] (.LABEL)  TO-ASMOUT ; IS .LABEL
: (.LABEL-DEF)   .LABEL ." :" CR ;
:NONAME   ['] (.LABEL-DEF)  TO-ASMOUT ; IS .LABEL-DEF
: (.BRANCH)   ." jumpi " .LABEL CR ;
:NONAME   ['] (.BRANCH)  TO-ASMOUT ; IS .BRANCH
: (.IF)   ." jumpzi " .LABEL CR ;
:NONAME   ['] (.IF)  TO-ASMOUT ; IS .IF
: (.RET)   ." ret" CR ;
:NONAME   ['] (.RET)  TO-ASMOUT ; IS .RET
: (.COMPILE-METHOD)   ." .set " .NAME ." _compilation, 0x" H. CR ;
:NONAME   ['] (.COMPILE-METHOD)  TO-ASMOUT ; IS .COMPILE-METHOD
: (.INLINE-COUNT)   ." .set " .NAME ." _inline, " 0 U.R CR ;
:NONAME   ['] (.INLINE-COUNT)  TO-ASMOUT ; IS .INLINE-COUNT
: (.CREATED-CODE)   ." calli " .NAME ." _doer" CR ;
:NONAME   ['] (.CREATED-CODE)  TO-ASMOUT ; IS .CREATED-CODE