\ Mass storage input/output words
\ Reuben Thomas   started 2/7/96

4 CONSTANT BIN-MODE
: BIN  BIN-MODE OR ;
1 CONSTANT R/O
2 CONSTANT W/O
3 CONSTANT R/W
0 CONSTANT CREATE-FAM

: OPEN-FILE   ( adr u fam -- fid ior )   4 LIB ;
: CLOSE-FILE   ( fid -- ior )   5 LIB ;

: CREATE-FILE   ( adr u fam -- fid ior )   BIN-MODE AND  CREATE-FAM OR  OPEN-FILE ;
: RENAME-FILE   ( adr1 u1 adr2 u2 -- ior )   11 LIB ;
: DELETE-FILE   ( adr u -- ior )   12 LIB ;

: READ-FILE   ( adr u1 fid -- u2 ior )   6 LIB ;
: WRITE-FILE   ( adr u fid -- ior )   7 LIB ;
: FLUSH-FILE   ( fid -- ior )   10 LIB ;

: FILE-POSITION   ( fid -- ud ior )   8 LIB ;
: REPOSITION-FILE   ( ud fid -- ior )   9 LIB ;

\ FIXME: FILE-SIZE   ( fid -- ud ior ) ;
\ FIXME: RESIZE-FILE   ( ud fid -- ior ) ;

\ FIXME: FILE-STATUS   ( c-addr u -- x ior ) ;