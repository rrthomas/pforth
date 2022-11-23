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
CREATE IO-BUFFER  1 ALLOT-CELLS
0 VALUE STDIN  0 VALUE STDOUT  0 VALUE STDERR

: INITIALIZE-TERMINAL
   STDIN-FILENO TO STDIN
   STDOUT-FILENO TO STDOUT
   STDERR-FILENO TO STDERR ;

: EMIT   IO-BUFFER  TUCK C!  1  STDOUT WRITE-FILE  DROP ;
: KEY   IO-BUFFER  DUP 1  STDIN READ-FILE  2DROP  C@ ;

: BL   32 ;
: CR   13 EMIT  10 EMIT ;
: DEL   8 EMIT  BL EMIT  8 EMIT ;

: DEL?   DUP 127 =  SWAP 8 =  OR ;
: CR?   DUP 13 =  SWAP 10 =  OR ;
CREATE EOL" 10 C,  0 CALIGN \ FIXME: Make SLITERAL work here
: EOL  EOL" 1 ;

\ FIXME: implement GET-ENVIRONMENT-VARIABLE and use it to read $COLUMNS
77 CONSTANT WIDTH   \ width of display

: REDIRECT-STDOUT   ( xt fd -- )
   STDOUT >R
   TO STDOUT
   EXECUTE
   R> TO STDOUT ;

-1 VALUE ASMOUT \ is this really an acceptable way to swallow output?
: TO-ASMOUT   ASMOUT REDIRECT-STDOUT ;
