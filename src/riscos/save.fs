\ Save an object file
\
\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ FIXME: Check I/O return codes
\ Save u1 bytes at a-addr in the file given by c-addr u2
: SAVE-FILE   ( a-addr u1 c-addr u2 -- )
   W/O BIN CREATE-FILE DROP      \ open file
   >R                            \ save file-id
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file

0 VALUE 'TYPE-FILE
[PROVIDED] TYPE-FILE  [IF]
   ' TYPE-FILE ADDRESS-TO 'TYPE-FILE
[THEN]

: SAVE-OBJECT   ( a-addr u1 c-addr u2 -- )
   2SWAP 2OVER                   \ save filename
   SAVE-FILE
   'TYPE-FILE  ?DUP IF           \ if we have TYPE-FILE,
      >R $FF8 -ROT R> EXECUTE    \ set filetype to Absolute
   ELSE
      2DROP                      \ otherwise drop file name
   THEN ;
