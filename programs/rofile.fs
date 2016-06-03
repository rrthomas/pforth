\ RISC OS file words
\ Reuben Thomas   15/4/96-17/3/98

\ Set the type of the file given by c-addr u2 to u1
: TYPE-FILE   ( u1 c-addr u2 -- )   C0END  18  [ 3 0 8 ] OS ;
