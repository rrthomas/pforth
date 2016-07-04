\ Mass storage input/output words
\ Reuben Thomas   1/7/96-18/3/99


: BIN ; IMMEDIATE
HEX
: R/O   40 ;
: W/O   C0 ;
: R/W   C0 ;
DECIMAL

\ Convert between RISC OS and POSIX file names:
\ Exchange . and /
CREATE FILE-NAME   256 ALLOT
: (>FILE-NAME<)   ( c-addr1 u1 -- c-addr2 u2 )
   TUCK TUCK
   FILE-NAME SWAP CMOVE
   FILE-NAME  TUCK + SWAP DO
      I C@ CASE
         [CHAR] . OF  [CHAR] /  ENDOF
         [CHAR] / OF  [CHAR] .  ENDOF
         DUP
      ENDCASE
      I C!
   LOOP
   FILE-NAME SWAP ;
' (>FILE-NAME<) <'FORTH VECTOR >FILE-NAME<
: POSIX-FILE-NAMES   ['] (>FILE-NAME<) TO >FILE-NAME< ;
: RISC-OS-FILE-NAMES   ['] NOTHING TO >FILE-NAME< ;

: OPEN-FILE   ( c-addr u fam -- fid ior )   -ROT >FILE-NAME< C0END SWAP
   [ 2 1 ] OS" OS_Find"  DUP 0= IF  -1  ELSE 0  THEN ;
: CLOSE-FILE   ( fid -- ior )   0  [ 2 0 ] OS" OS_Find"  0 ;

: CREATE-FILE   ( c-addr u fam -- fid ior )   DROP
   [ HEX ] 80 [ DECIMAL ] OPEN-FILE ;
: RENAME-FILE   ( c-addr1 u1 c-addr2 u2 -- ior )   >FILE-NAME< C0END -ROT
   >FILE-NAME< C0END  25 [ 3 0 ] OS" OS_FSControl"  0 ;
: DELETE-FILE   ( c-addr u -- ior )   >FILE-NAME< C0END
   6  [ 2 1 ] OS" OS_File"  0= ;

: READ-FILE   ( c-addr u1 fid -- u2 ior )   SWAP DUP 2SWAP  4  [ 4 4 ]
   OS" OS_GBPB"  DROP 2DROP -  0 ;
: WRITE-FILE   ( c-addr u fid -- ior )   ROT SWAP  2  [ 4 0 ] OS" OS_GBPB"
   0 ;
: FLUSH-FILE   ( fid -- ior )   255  [ 2 0 ] OS" OS_Args"  0 ;

: FILE-POSITION   ( fid -- ud ior )   0  [ 2 3 ] OS" OS_Args"  2DROP U>UD  0
   ;
: REPOSITION-FILE   ( ud fid -- ior )   -ROT UD>U SWAP  1  [ 3 0 ]
   OS" OS_Args"  0 ;

: FILE-SIZE   ( fid -- ud ior )   2  [ 2 3 ] OS" OS_Args"  2DROP U>UD  0 ;
: RESIZE-FILE   ( ud fid -- ior )   -ROT UD>U SWAP  3  [ 3 0 ] OS" OS_Args"
   0 ;

: FILE-STATUS   ( c-addr u -- x ior )   >FILE-NAME< C0END  5
   [ 2 6 ] OS" OS_File"  NIP NIP NIP NIP ;