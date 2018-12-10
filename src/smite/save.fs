\ Save an object file
HERE <'FORTH  1 C, 10 C,
: CR"   LITERAL COUNT ;

: SAVE-OBJECT   ( a-addr u1 c-addr u2 -- )
   W/O CREATE-FILE DROP          \ open file
   >R                            \ save file-id
   S" #!/usr/bin/env smite"     \ write hash-bang line
   R@ WRITE-FILE DROP
   CR" R@ WRITE-FILE DROP
   S" SMITE" R@ WRITE-FILE DROP  \ write header
   0 SCRATCH TUCK C!  1  2DUP  R@ WRITE-FILE DROP
   2DUP  R@ WRITE-FILE DROP  R@ WRITE-FILE DROP
   DUP SCRATCH TUCK !  CELL R@ WRITE-FILE DROP
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file