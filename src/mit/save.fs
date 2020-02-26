\ (c) Reuben Thomas 2018-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Save an object file

: WRITE-BYTE   ( char fid -- ior )   SWAP  SCRATCH TUCK C!  1  ROT WRITE-FILE ;
: WRITE-WORD   ( x fid -- ior )   SWAP  SCRATCH TUCK !  CELL  ROT WRITE-FILE ;

\ FIXME: Check I/O return codes
: SAVE-OBJECT   ( a-addr u1 c-addr u2 -- )
   W/O BIN CREATE-FILE DROP      \ open file
   >R                            \ save file-id
   S" MIT" R@ WRITE-FILE DROP    \ write header
   0 R@ WRITE-BYTE DROP
   0 R@ WRITE-BYTE DROP
   0 R@ WRITE-BYTE DROP
   0 R@ WRITE-BYTE DROP          \ FIXME: write correct ENDISM
   CELL R@ WRITE-BYTE DROP       \ write WORD_SIZE
   DUP CELL/ R@ WRITE-WORD DROP  \ write length
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file