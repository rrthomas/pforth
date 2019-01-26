\ Mass storage input/output words
\ Reuben Thomas   started 2/7/96

ALSO ASSEMBLER
$7 $0 EXTRA-INSTRUCTIONS TOTAL-ARGS ARG-LEN ARG-COPY STDIN STDOUT STDERR OPEN_FILE CLOSE-FILE
$B $8 EXTRA-INSTRUCTIONS READ-FILE WRITE_FILE FILE-POSITION REPOSITION-FILE
$10 $C EXTRA-INSTRUCTIONS FLUSH-FILE RENAME_FILE DELETE_FILE FILE-SIZE RESIZE-FILE
$11 EXTRA-INSTRUCTION FILE-STATUS
PREVIOUS

0 CONSTANT R/O
1 CONSTANT W/O
2 CONSTANT R/W

4 CONSTANT CREATE-FAM

8 CONSTANT BIN-MODE
: BIN  BIN-MODE OR ;

: OPEN-FILE   ( c-addr u fam -- fid ior )   -ROT SCRATCH-C0END SWAP OPEN_FILE ;
: WRITE-FILE   ( c-addr u fileid -- ior )   WRITE_FILE NIP ;
: RENAME-FILE   ( c-addr1 u1 c-addr2 u2 -- ior )   SCRATCH-C0END -ROT HERE 256 C0END HERE
   SWAP RENAME_FILE ;
: DELETE-FILE   ( c-addr u -- ior )   SCRATCH-C0END DELETE_FILE ;
: CREATE-FILE   ( adr u fam -- fid ior )   CREATE-FAM OR  OPEN-FILE ;
: ABSOLUTE-ARG   ( u1 -- c-addr u2 )
   TOTAL-ARGS OVER > IF \ u1
      SCRATCH TUCK 256 ARG-COPY
   ELSE
      DROP  0 0
   THEN ;