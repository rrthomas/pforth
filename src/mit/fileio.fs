\ Mass storage input/output words
\
\ (c) Reuben Thomas 1996-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

ALSO ASSEMBLER

 $0 LIBC-PRIMITIVE TOTAL-ARGS
 $1 LIBC-PRIMITIVE _ARG
 $4 LIBC-PRIMITIVE STRNCPY
 $5 LIBC-PRIMITIVE STDIN
 $6 LIBC-PRIMITIVE STDOUT
 $7 LIBC-PRIMITIVE STDERR
 $8 LIBC-PRIMITIVE R/O
 $9 LIBC-PRIMITIVE W/O
 $A LIBC-PRIMITIVE R/W
 $B LIBC-PRIMITIVE O_CREAT
 $C LIBC-PRIMITIVE O_TRUNC
 $D LIBC-PRIMITIVE OPEN
 $E LIBC-PRIMITIVE CLOSE-FILE
 $F LIBC-PRIMITIVE READ
$10 LIBC-PRIMITIVE WRITE
$11 LIBC-PRIMITIVE SEEK_SET
$12 LIBC-PRIMITIVE SEEK_CUR
$13 LIBC-PRIMITIVE SEEK_END
$14 LIBC-PRIMITIVE LSEEK
$15 LIBC-PRIMITIVE FLUSH-FILE
$16 LIBC-PRIMITIVE RENAME
$17 LIBC-PRIMITIVE REMOVE
$18 LIBC-PRIMITIVE FILE_SIZE
$19 LIBC-PRIMITIVE RESIZE_FILE
$1A LIBC-PRIMITIVE FILE-STATUS

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
\ FIXME: Next two words depend on ENDISM and sizeof(off_t)
: D>OFF_T ;
: OFF_T>D ;
: FILE-POSITION   0. D>OFF_T SEEK_CUR LSEEK
   OFF_T>D  OVER -1 =  OVER -1 =  AND ;
: REPOSITION-FILE   -ROT  D>OFF_T SEEK_SET LSEEK  OFF_T>D  -1 =  SWAP -1 =  AND ;
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
   MIT_CURRENT_STATE                    \ ( adr adr u ptr:uint8_t* )
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