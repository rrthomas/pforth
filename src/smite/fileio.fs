\ Mass storage input/output words
\ Reuben Thomas   started 2/7/96

0 CONSTANT R/O
1 CONSTANT W/O
2 CONSTANT R/W

4 CONSTANT CREATE-FAM

8 CONSTANT BIN-MODE
: BIN  BIN-MODE OR ;

: CREATE-FILE   ( adr u fam -- fid ior )   CREATE-FAM OR  OPEN-FILE ;
