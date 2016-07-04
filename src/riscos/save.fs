\ Save an object file
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