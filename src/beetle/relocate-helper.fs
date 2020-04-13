\ (c) Reuben Thomas 2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: RELOCATE-HELPER   ( offset address type -- )
   1 <> ABORT" Invalid relocation type!"
   DUP @ $FF AND
   CASE
      $5C OF  CELL+ +!  ENDOF
      \ FIXME: See add-relocate-helpers.fs
      \ $5D OF  TUCK @  8 ARSHIFT +  8 LSHIFT  $5D OR  SWAP !  ENDOF
      TRUE ABORT" Invalid LITERAL relocation!"
   ENDCASE ;