\ Save an object file

\ Save u1 bytes at a-addr in the file given by c-addr u2
: SAVE-FILE   ( a-addr u1 c-addr u2 -- )
   W/O CREATE-FILE DROP          \ open file
   >R                            \ save file-id
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file

'? TYPE-FILE VALUE 'TYPE-FILE
HEX
: SAVE   ( a-addr u1 c-addr u2 -- )
   2SWAP 2OVER                   \ save filename
   SAVE-FILE
   'TYPE-FILE  ?DUP IF           \ if we have TYPE-FILE,
      >R FF8 -ROT R> EXECUTE     \ set filetype to Absolute
   ELSE
      2DROP                      \ otherwise drop file name
   THEN ;