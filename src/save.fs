\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Save an object file
\ FIXME: Check I/O return codes
: SAVE-FILE   ( a-addr u1 c-addr u2 -- )
   W/O BIN CREATE-FILE DROP      \ open file
   >R                            \ save file-id
   R@ WRITE-FILE DROP            \ write data
   R> CLOSE-FILE DROP ;          \ close file

: SAVE-OBJECT   SAVE-FILE ;