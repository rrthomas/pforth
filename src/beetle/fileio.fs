\ Mass storage input/output words
\ Reuben Thomas   started 2/7/96

0 CONSTANT R/O
1 CONSTANT W/O
2 CONSTANT R/W

4 CONSTANT CREATE-FAM

8 CONSTANT BIN-MODE
: BIN  BIN-MODE OR ;

: CREATE-FILE   ( adr u fam -- fid ior )   CREATE-FAM OR  OPEN-FILE ;

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
