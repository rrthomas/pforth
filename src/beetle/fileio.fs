\ Mass storage input/output words
\
\ (c) Reuben Thomas 1996-2021
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.


\ System primitives
: TOTAL-ARGS    16 LIB ;
: ARGLEN   17 LIB ;
: ARGCOPY   18 LIB ;
: STDIN-FILENO   19 LIB ;
: STDOUT-FILENO   20 LIB ;
: STDERR-FILENO   21 LIB ;

0 CONSTANT R/O
1 CONSTANT W/O
2 CONSTANT R/W

4 CONSTANT CREATE-FAM

8 CONSTANT BIN-MODE
: BIN  BIN-MODE OR ;

: OPEN-FILE   ( adr u fam -- fid ior )   4 LIB ;
: CLOSE-FILE   ( fid -- ior )   5 LIB ;

: CREATE-FILE   ( adr u fam -- fid ior )   CREATE-FAM OR  OPEN-FILE ;
: RENAME-FILE   ( adr1 u1 adr2 u2 -- ior )   11 LIB ;
: DELETE-FILE   ( adr u -- ior )   12 LIB ;

: READ-FILE   ( adr u1 fid -- u2 ior )   6 LIB ;
: WRITE-FILE   ( adr u fid -- ior )   7 LIB ;
: FLUSH-FILE   ( fid -- ior )   10 LIB ;

: FILE-POSITION   ( fid -- ud ior )   8 LIB ;
: REPOSITION-FILE   ( ud fid -- ior )   9 LIB ;

: FILE-SIZE   ( fid -- ud ior )  13 LIB ;
: RESIZE-FILE   ( ud fid -- ior )  14 LIB ;

: FILE-STATUS   ( c-addr u -- x ior )  15 LIB ;

CREATE ARG-BUFFER  256 ALLOT \ FIXME: this is ugly
: ABSOLUTE-ARG   ( u1 -- c-addr u2 )
   TOTAL-ARGS OVER > IF \ u1
      DUP ARGLEN  DUP 256 < IF \ FIXME: cope with longer arguments
         SWAP  ARG-BUFFER TUCK
         ARGCOPY
         SWAP
      ELSE
         DROP DROP  0 0
      THEN
   ELSE
      DROP  0 0
   THEN ;
