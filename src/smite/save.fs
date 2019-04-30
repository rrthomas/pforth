\ Save an object file

: WRITE-BYTE   ( char fid -- ior )   SWAP  SCRATCH TUCK C!  1  ROT WRITE-FILE ;
: WRITE-WORD   ( x fid -- ior )   SWAP  SCRATCH TUCK !  CELL  ROT WRITE-FILE ;

\ FIXME: Check I/O return codes
: SAVE-OBJECT   ( a-addr u1 c-addr u2 -- )
   W/O CREATE-FILE DROP          \ open file
   >R                            \ save file-id
   S" SMITE" R@ WRITE-FILE DROP  \ write header
   0 R@ WRITE-BYTE DROP
   0 R@ WRITE-BYTE DROP          \ FIXME: write correct ENDISM
   CELL R@ WRITE-BYTE DROP       \ write WORD_SIZE
   DUP R@ WRITE-WORD DROP        \ write length
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file