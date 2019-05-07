\ Mass storage input/output words
\ Reuben Thomas   started 2/7/96

ALSO ASSEMBLER

\ FIXME: we can't know this at build time, hence we'll need a suite of type
\ conversion words, and LIBC-PRIMITIVE will have to use the normal calling
\ convention, not PRIMITIVE.
\ FIXME: The following is a duplicate of the definition in assembler.fs
2 CONSTANT NATIVE-POINTER-CELLS
0 1  $0 LIBC-PRIMITIVE TOTAL-ARGS
1 NATIVE-POINTER-CELLS $1 LIBC-PRIMITIVE _ARG
NATIVE-POINTER-CELLS 2 * 1 +  NATIVE-POINTER-CELLS $4 LIBC-PRIMITIVE STRNCPY
0 1  $5 LIBC-PRIMITIVE STDIN
0 1  $6 LIBC-PRIMITIVE STDOUT
0 1  $7 LIBC-PRIMITIVE STDERR
0 1  $8 LIBC-PRIMITIVE R/O
0 1  $9 LIBC-PRIMITIVE W/O
0 1  $A LIBC-PRIMITIVE R/W
0 1  $B LIBC-PRIMITIVE O_CREAT
0 1  $C LIBC-PRIMITIVE O_TRUNC
2 1  $D LIBC-PRIMITIVE OPEN
1 1  $E LIBC-PRIMITIVE CLOSE-FILE
3 1  $F LIBC-PRIMITIVE READ
3 1 $10 LIBC-PRIMITIVE WRITE
0 1 $11 LIBC-PRIMITIVE SEEK_SET
0 1 $12 LIBC-PRIMITIVE SEEK_CUR
0 1 $13 LIBC-PRIMITIVE SEEK_END
2 NATIVE-POINTER-CELLS + NATIVE-POINTER-CELLS $14 LIBC-PRIMITIVE LSEEK
1 1 $15 LIBC-PRIMITIVE FLUSH-FILE
2 1 $16 LIBC-PRIMITIVE RENAME
1 1 $17 LIBC-PRIMITIVE REMOVE
1 NATIVE-POINTER-CELLS 1+ $18 LIBC-PRIMITIVE FILE_SIZE
NATIVE-POINTER-CELLS 1+ 1 $19 LIBC-PRIMITIVE RESIZE_FILE
1 2 $1A LIBC-PRIMITIVE FILE-STATUS

PREVIOUS


: CREATE-FAM   O_CREAT O_TRUNC OR ;

0 CONSTANT BIN-MODE
: BIN ;

: OPEN-FILE   ( c-addr u fam -- fid ior )   -ROT SCRATCH-C0END SWAP OPEN
    DUP 0 < ;
: READ-FILE   ( c-addr u fileid -- nread ior )   READ  DUP 0 < ;
: WRITE-FILE   ( c-addr u fileid -- ior )   WRITE  0 < ;
: RENAME-FILE   ( c-addr1 u1 c-addr2 u2 -- ior )   SCRATCH-C0END -ROT HERE 256 C0END HERE
   SWAP RENAME ;
: DELETE-FILE   ( c-addr u -- ior )   SCRATCH-C0END REMOVE ;
: CREATE-FILE   ( adr u fam -- fid ior )   CREATE-FAM OR  OPEN-FILE ;
\ FIXME: Next two words depend on ENDISM
: D>OFF_T ;
: OFF_T>D ;
: FILE-POSITION   0. D>OFF_T SEEK_CUR LSEEK
   OFF_T>D  OVER -1 =  OVER -1 =  AND ;
: REPOSITION-FILE   NATIVE-POINTER-CELLS ROLL NATIVE-POINTER-CELLS ROLL
   D>OFF_T SEEK_SET LSEEK  OFF_T>D  -1 =  SWAP -1 =  AND ;
: FILE-SIZE   FILE_SIZE  >R OFF_T>D R> ;
: RESIZE-FILE   >R D>OFF_T R>  RESIZE_FILE ;
: STRLEN   ( c-addr u1 -- u2 ) \ FIXME: Use C extra instruction
   TUCK 0 ?DO
      DUP I + C@ 0= IF
         2DROP I  UNLOOP EXIT
      THEN
   LOOP  DROP ;
: ARG-COPY   ( n adr u -- len )
   DUP >R  ROT >R                       \ ( adr u )
   OVER SWAP                            \ ( adr adr u )
   MIT_CURRENT_STATE                  \ ( adr adr u ptr:uint8_t* )
   NATIVE_ADDRESS_OF_RANGE              \ ( adr dest:char* )
   R> _ARG                              \ ( adr dest:char* src:char* )
   R> STRNCPY                           \ ( adr dest:char* )
   NATIVE-POINTER-CELLS 0 DO  DROP  LOOP
   DUP STRLEN ;
CREATE ARG-BUFFER  256 ALLOT \ FIXME: this is ugly
: ABSOLUTE-ARG   ( u1 -- c-addr u2 )
   TOTAL-ARGS OVER > IF \ u1
      ARG-BUFFER TUCK 256 ARG-COPY
   ELSE
      DROP  0 0
   THEN ;