\ Multi buffer blocks, including file buffering in block buffers
\ R.R.T.   15/4-8/5/96

VARIABLE SCR
64 DUP CONSTANT /LINE
16 DUP CONSTANT LINES/BLOCK
* DUP CONSTANT /BLOCK   \ save value for later
: OPEN-BLOCKS   ( fam -- fid )   "BLOCK COUNT ROT OPEN-FILE
   ABORT" blocks file can't be opened" ;
: @BLOCK   ( adr fid u1 -- u2 )   SWAP DUP 0= IF  DROP  R/O OPEN-BLOCKS  THEN
   >R  1- 10 LSHIFT U>UD  R@ REPOSITION-FILE DROP  /BLOCK R@ READ-FILE DROP
   R> CLOSE-FILE DROP ;
: !BLOCK   ( adr fid u1 u2 -- )   >R  SWAP DUP 0= IF  DROP  W/O OPEN-BLOCKS
   THEN  R> SWAP >R  SWAP 1- 10 LSHIFT U>UD  R@ REPOSITION-FILE DROP
   R@ WRITE-FILE DROP  R> CLOSE-FILE DROP ;

12 + CONSTANT /BLOCK-BUFFER   \ uses value saved above
4 CONSTANT #BLOCK-BUFFERS
: FIRST-BLOCK   R0 @ ;
VARIABLE BLOCK-BUFFER#   \ next block buffer to use
VARIABLE #UPDATE   \ current block buffer
: >BLOCK   3 CELLS + ;

HEX
: UPDATE   #UPDATE @  DUP @ 80000000 OR  SWAP ! ;
: UPDATED?   ( adr -- f )   @  80000000 AND  0<> ;
: BLOCK-BUFFER   ( fid u -- adr f )
   FIRST-BLOCK                       \ address of first block buffer
   #BLOCK-BUFFERS 0 DO               \ for each buffer...
      TUCK @                         \ get the block number
      7FFFFFFF AND  OVER = IF        \ if it is the one requested
         OVER 2SWAP CELL+ @          \ check file id is the same
         OVER = IF                   \ if so,
            DROP                     \ get rid of fid
            DUP #UPDATE !            \ set current buffer
            NIP  >BLOCK              \ leave address and true flag
            TRUE UNLOOP EXIT         \ and exit
         THEN
         -ROT                        \ put fid back
            ELSE
         SWAP                        \ put u back
      THEN
      /BLOCK-BUFFER +                \ address of next buffer
   LOOP  DROP                        \ get rid of address
   FIRST-BLOCK  BLOCK-BUFFER# @      \ address of buffer to put
   /BLOCK-BUFFER *  +                \ block in
   DUP UPDATED? IF                   \ was the last block updated?
      DUP >BLOCK  DUP CELL- @ >R     \ if so, save it
      2OVER  R>  !BLOCK
   THEN
   TUCK !                            \ set new block no., & clear update flag
   NIP                               \ drop fid
   BLOCK-BUFFER# @ 1+                \ increment next buffer number
   DUP #BLOCK-BUFFERS = IF  DROP 0  THEN
                                     \ and reset if too large
   BLOCK-BUFFER# !                   \ save next buffer number
   DUP #UPDATE !                     \ set current buffer
   >BLOCK  FALSE ;                   \ leave address and false flag
DECIMAL
: BUFFER-FILE   BLOCK-BUFFER DROP ;
: BLOCK-FILE   2DUP  BLOCK-BUFFER IF  NIP NIP  ELSE DUP >R  -ROT  @BLOCK
   R> TUCK CELL- !  THEN ;
: BUFFER   ?DUP IF  0 SWAP  BUFFER-FILE  THEN ;
: BLOCK   ?DUP IF  0 SWAP  BLOCK-FILE  THEN ;

128 CONSTANT /FILE-BUFFER
16 CONSTANT #FILE-BUFFERS
VARIABLE FILE-BUFFER#  0 V' FILE-BUFFER# !   \ next file buffer to use
: FIRST-FILE   LIMIT @  #FILE-BUFFERS /FILE-BUFFER * - ;
: ALLOCATE-BUFFER   ( -- adr ior )   FILE-BUFFER# @  DUP #FILE-BUFFERS = IF
   -1  ELSE DUP 1+ FILE-BUFFER# !  /FILE-BUFFER *  FIRST-FILE +  0  THEN ;
: FREE-BUFFER   ( -- ior )   FILE-BUFFER#  DUP @ 0= IF  DROP -1
   ELSE -1 SWAP +!  0  THEN ;

HEX
: SAVE-BUFFERS
   FIRST-BLOCK
   #BLOCK-BUFFERS 0 DO
      DUP UPDATED? IF
         >R
         R@ >BLOCK  DUP CELL- @
         R@  DUP CELL+ @  SWAP @ 7FFFFFFF AND  DUP >R
         ROT !BLOCK  R> R>  TUCK !
      THEN
   /BLOCK-BUFFER +  LOOP  DROP ;
DECIMAL
: EMPTY-BUFFERS   FIRST-BLOCK  #BLOCK-BUFFERS 0 DO  0 OVER !
   /BLOCK-BUFFER +  LOOP  DROP ;
