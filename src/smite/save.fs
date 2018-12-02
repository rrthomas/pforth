\ Save an object file
: SAVE-OBJECT   ( a-addr u1 c-addr u2 -- )
   W/O CREATE-FILE DROP          \ open file
   >R                            \ save file-id
   S" SMITE" R@ WRITE-FILE DROP \ write header
   0 SCRATCH TUCK C!  1  2DUP  R@ WRITE-FILE DROP
   2DUP  R@ WRITE-FILE DROP  R@ WRITE-FILE DROP
   DUP SCRATCH TUCK !  CELL R@ WRITE-FILE DROP
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file