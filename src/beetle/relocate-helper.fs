: RELOCATE-HELPER   ( offset address type -- )
   1 <> ABORT" Invalid relocation type!"
   DUP @ $FF AND
   CASE
      $5C OF  CELL+ +!  ENDOF
      $5D OF  TUCK @  8 ARSHIFT +  8 LSHIFT  $5D OR  SWAP !  ENDOF
      TRUE ABORT" Invalid LITERAL relocation!"
   ENDCASE ;