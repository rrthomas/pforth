\ Save an object file
\
\ (c) Reuben Thomas 1995-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ FIXME: Check I/O return codes
: SAVE-OBJECT   ( a-addr u1 c-addr u2 -- )
   W/O BIN CREATE-FILE DROP      \ open file
   >R                            \ save file-id
   S" BEE" R@ WRITE-FILE DROP    \ write header
   0 SCRATCH TUCK !  DUP 4  R@ WRITE-FILE DROP
   1 R@ WRITE-FILE DROP
   DUP CELL/ SCRATCH TUCK !  CELL R@ WRITE-FILE DROP
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file