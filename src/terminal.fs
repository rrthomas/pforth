\ Terminal input/output
\
\ (c) Reuben Thomas 1995-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ I/O streams
CREATE IO-BUFFER  CELL ALLOT

: EMIT   IO-BUFFER  TUCK C!  1  STDOUT WRITE-FILE  DROP ;
: KEY   IO-BUFFER  DUP 1  STDIN READ-FILE  2DROP  C@ ;

: BL   32 ;
: CR   13 EMIT  10 EMIT ;
: DEL   8 EMIT  BL EMIT  8 EMIT ;

: DEL?   DUP 127 =  SWAP 8 =  OR ;
: CR?   DUP 13 =  SWAP 10 =  OR ;
HERE 10 C,  0 CALIGN \ FIXME: Make SLITERAL work here
: EOL  RELATIVE-LITERAL 1 ;

\ FIXME: implement GET-ENVIRONMENT-VARIABLE and use it to read $COLUMNS
77 CONSTANT WIDTH   \ width of display