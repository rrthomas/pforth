\ Mass storage input/output words
\ Reuben Thomas   2/7/96-3/6/16

: BIN ; IMMEDIATE
CREATE R/O  CHAR r C, CHAR b C, 0 C,
CREATE W/O  CHAR w C, CHAR b C, 0 C,
CREATE R/W  CHAR r C, CHAR + C, CHAR b C, 0 C,
CREATE CREATE-FAM  CHAR w C, CHAR + C, CHAR b C, 0 C,

: OPEN-FILE   ( adr u fam -- fid ior )   -ROT C0END SWAP  4 LIB ;
: CLOSE-FILE   ( fid -- ior )   5 LIB ;

: CREATE-FILE   ( adr u fam -- fid ior )   DROP  CREATE-FAM OPEN-FILE ;
: RENAME-FILE   ( adr1 u1 adr2 u2 -- ior )   C0END -ROT C0END  SWAP 11 LIB ;
: DELETE-FILE   ( adr u -- ior )   C0END 12 LIB ;

: READ-FILE   ( adr u1 fid -- u2 ior )   6 LIB ;
: WRITE-FILE   ( adr u fid -- ior )   7 LIB ;
: FLUSH-FILE   ( fid -- ior )   10 LIB ;

: FILE-POSITION   ( fid -- ud ior )   8 LIB ;
: REPOSITION-FILE   ( ud fid -- ior )   9 LIB ;

\ FIXME: FILE-SIZE   ( fid -- ud ior ) ;
\ FIXME: RESIZE-FILE   ( ud fid -- ior ) ;

\ FIXME: FILE-STATUS   ( c-addr u -- x ior ) ;