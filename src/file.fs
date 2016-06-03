\ File words
\ Reuben Thomas   15/4/96-17/3/98

\ Save u1 bytes at a-addr in the file given by c-addr u2
: SAVE-FILE   ( a-addr u1 c-addr u2 -- )
   W/O CREATE-FILE DROP          \ open file
   >R                            \ save file-id
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file

\ Set the type of the file given by c-addr u2 to u1
: TYPE-FILE   ( u1 c-addr u2 -- )   C0END  18  [ 3 0 8 ] OS ;
