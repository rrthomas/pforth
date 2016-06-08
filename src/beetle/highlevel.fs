\ aForth high level words
\ Reuben Thomas   1/91-14/5/00

CR .( aForth high level words )


\ System variables

CONSTANT 'THROW   \ value is put on stack by make.fs
VARIABLE S0
4096 CONSTANT CELLS/S
VARIABLE R0
4096 CONSTANT CELLS/R
DUP VALUE M0   \ value is put on stack by make.fs
: <M0   M0 -  LITERAL + ;   \ value is put on stack by make.fs


\ Stack manipulation

: 2DUP   OVER OVER ;
: 2DROP   DROP DROP ;
: 2SWAP   3 ROLL  3 ROLL ;
: 2OVER   3 PICK  3 PICK ;
: 2ROT   5 ROLL  5 ROLL ;

: 2>R   R> -ROT  SWAP >R >R  >R ; COMPILING
: 2R>   R>  R> R> SWAP  ROT >R ; COMPILING
: 2R@   R>  R> R>  2DUP >R >R  SWAP  ROT >R ; COMPILING

: DEPTH   SP@ S0 @ >-< CELL/ ;


\ Characters

: +CHAR   1 ;
: -CHAR   -1 ;
: CHAR+   1+ ;
: CHAR-   1- ;
: CHARS ; IMMEDIATE
: CHAR/ ; IMMEDIATE


\ Arithmetic

: S>D   DUP 0< ;
: D>S   DROP ;

: U>UD   0 ;
: UD>U   DROP ;

: WITHIN   OVER -  >R -  R> U< ;

: M*   * S>D ;
: UM*   * U>UD ;
: FM/MOD   NIP /MOD ;
: SM/REM   NIP S/REM ;
: UM/MOD   NIP U/MOD ;
: */   >R * R> / ;
: */MOD   >R * R> /MOD ;

: D0=   0= SWAP 0= AND ;
: D+   D>S >R D>S R>  +  S>D ;
: DNEGATE   D>S NEGATE S>D ;
: D-   DNEGATE D+ ;
: M+   S>D D+ ;
: D*   D>S >R D>S R>  *  S>D ;
: UD/MOD  UD>U >R UD>U R>  U/MOD  >R U>UD R> U>UD ;
: DABS   IF  NEGATE  THEN  U>UD ;


\ Exceptions #1

VARIABLE 'THROWN
: (ABORT")   SWAP IF  'THROWN !  -2 THROW  ELSE DROP  THEN ;
: UNDEFINED   ( c-addr -- )   'THROWN !  -13 THROW ;


\ Strings #1

: COUNT   DUP CHAR+ SWAP C@ ;
: /STRING   ( c-addr1 u1 n -- c-addr2 u2 )   TUCK -  -ROT CHARS +  SWAP ;
: CMOVE   CHARS OVER + SWAP ?DO  DUP C@ I C! CHAR+  +CHAR +LOOP  DROP ;
: CMOVE>   ?DUP IF  CHARS CHAR- TUCK  + -ROT  OVER +  DO  I C@ OVER C!  CHAR-
   -CHAR +LOOP  ELSE DROP  THEN  DROP ;
: MOVE   -ROT 2DUP > IF  ROT CMOVE  ELSE ROT CMOVE>  THEN ;
: FILL   -ROT  CHARS OVER + SWAP ?DO  DUP I C!  +CHAR +LOOP  DROP ;
: ERASE   0 FILL ;


\ Compiler #1

0 VALUE DP
: ALLOT   DP +! ;
: HERE   DP @ ;

VARIABLE ROOTDP

: ALIGNED   CELL+ 1-  -CELL AND ;
: ALIGN   HERE ALIGNED  DP ! ;
: ,   HERE  CELL ALLOT  ! ;
: C,   HERE  +CHAR ALLOT  C! ;
: CALIGN   DUP DUP C, C, C,  -3 ALLOT  ALIGN ;

VARIABLE STATE
: [   0 STATE ! ; IMMEDIATE COMPILING
: ]   1 STATE ! ;

VARIABLE #ORDER
CREATE CONTEXT  8 CELLS ALLOT

VARIABLE CURRENT
: GET-CURRENT   CURRENT @ ;
: SET-CURRENT   CURRENT ! ;
: LAST   GET-CURRENT  @ ;

: >LINK   3 CELLS - ;
: >THREAD   2 CELLS - ;
: >INFO   CELL- ;
: >NAME   DUP >INFO 3 + C@  31 AND 1+ CHARS ALIGNED  SWAP >LINK  >-< ;
: >COMPILE   >NAME CELL- ;
: >BODY   2 CELLS + ;

HEX
: IMMEDIATE   LAST >INFO  DUP @  80000000 OR  SWAP !  LAST DUP >COMPILE ! ;
: COMPILING   LAST >INFO  DUP @  40000000 OR  SWAP ! ;
: SMUDGE!   ( f a-addr -- )   >INFO  TUCK @  20000000 DUP INVERT  ROT AND
   -ROT AND  OR  SWAP ! ;
DECIMAL
: SMUDGE   ( f -- )   LAST  SMUDGE! ;


\ Interpreter #2

: PAD   R0 @ 256 + ;
: TOKEN   R0 @ 512 + ;
: S"B   R0 @ 768 + ;
: SCRATCH   R0 @ 1024 + ;


\ Strings #2

: C0END   ( c-addr1 u -- c-addr2 )   SCRATCH SWAP  2DUP + >R  MOVE  0 R> C!
   SCRATCH ;


INCLUDE machdeps/fs   \ include machine-dependent words


\ Interpreter #3

: ABORT   -1 THROW ;
: QUIT   -56 THROW ;


\ Control structures #1

: BEGIN   NOPALIGN HERE ; IMMEDIATE COMPILING
: AGAIN   POSTPONE AHEAD  SWAP JOIN ; IMMEDIATE COMPILING
: UNTIL   POSTPONE IF  SWAP JOIN ; IMMEDIATE COMPILING
: THEN   NOPALIGN HERE JOIN ; IMMEDIATE

: CS-PICK   PICK ; COMPILING
: CS-ROLL   ROLL ; COMPILING

: WHILE   POSTPONE IF  1 CS-ROLL ; IMMEDIATE COMPILING
: REPEAT   POSTPONE AGAIN  POSTPONE THEN ; IMMEDIATE COMPILING
: ELSE   POSTPONE AHEAD  1 CS-ROLL  POSTPONE THEN ; IMMEDIATE COMPILING

VARIABLE 'NODE
: >NODE   'NODE @  BEGIN  ?DUP WHILE  DUP @  HERE <M0 ROT !  REPEAT ;
: I   POSTPONE R@ ; IMMEDIATE COMPILING
: LEAVE   LEAVE, NOPALIGN  HERE 'NODE  DUP @ ,  ! ; IMMEDIATE COMPILING
: DO   'NODE @  0 'NODE !  DO,  POSTPONE BEGIN ; IMMEDIATE COMPILING
: ?DO   'NODE @  0 'NODE !  POSTPONE 2DUP DO,  POSTPONE = POSTPONE IF
   POSTPONE LEAVE POSTPONE THEN  POSTPONE BEGIN ; IMMEDIATE COMPILING
: LOOP   LOOP,  >NODE  'NODE !  POSTPONE UNLOOP ; IMMEDIATE COMPILING
: +LOOP   +LOOP,  >NODE  'NODE !  POSTPONE UNLOOP ; IMMEDIATE COMPILING

: RECURSE   LAST COMPILE, ; IMMEDIATE COMPILING

: CASE   0 ; IMMEDIATE COMPILING
: OF   1+ >R  POSTPONE OVER POSTPONE = POSTPONE IF  POSTPONE DROP  R> ;
IMMEDIATE COMPILING
: ENDOF   >R  POSTPONE ELSE  R> ; IMMEDIATE COMPILING
: ENDCASE   POSTPONE DROP  0 ?DO  POSTPONE THEN  LOOP ; IMMEDIATE COMPILING


\ Memory

: 2@   DUP CELL+ @  SWAP @ ;
: 2!   TUCK !  CELL+ ! ;
: 2,   , , ;


\ Strings #3

: BLANK   BL FILL ;

: COMPARE   ( c-addr1 u1 c-addr2 u2 -- n )
   ROT 2SWAP 2OVER MIN               \ no. of characters to check
   DUP 0> IF                         \ if strings not both length 0
      0 DO                           \ for each character
         OVER C@  OVER C@            \ get the characters
         <> IF                       \ if they're unequal
            C@ SWAP  C@              \ retrieve the characters
            < 2* INVERT              \ construct the return code
            NIP NIP UNLOOP EXIT      \ and exit
         THEN
         CHAR+ SWAP  CHAR+ SWAP      \ increment addresses
      LOOP
      2DROP                          \ get rid of addresses
      2DUP <> -ROT < 2* INVERT AND   \ construct return code
   ELSE                              \ if strings are both length 0
      2DROP 2DROP                    \ leave 0
   THEN ;

: SEARCH   ( c-addr1 u1 c-addr2 u2 -- c-addr3 u3 f )
   ROT 2DUP                          \ copy lengths
   OVER SWAP U>  SWAP 0=  OR IF      \ if u2>u1 or u2=0
      NIP NIP  FALSE EXIT            \ exit with false flag
   THEN
   -ROT 2OVER                        \ save c-addr1 u1
   2SWAP TUCK 2>R                    \ save c-addr2 u2
   - 1+ OVER +  SWAP                 \ make c-addr1 c-addr1+u1-u2
   2R> 2SWAP                         \ retrieve c-addr2 u2
   DO
      2DUP I OVER COMPARE 0= IF      \ if we find the string
         2DROP +  I TUCK -           \ calculate c-addr3 u3
         TRUE UNLOOP EXIT            \ exit with true flag
      THEN
   LOOP
   2DROP FALSE ;                     \ leave c-addr1 u1 false


\ Control structures #2

: "CASE   POSTPONE CASE ; IMMEDIATE COMPILING
: "OF   1+ >R  POSTPONE 2OVER POSTPONE COMPARE POSTPONE 0= POSTPONE IF
  POSTPONE 2DROP  R> ; IMMEDIATE COMPILING
: "ENDOF   POSTPONE ENDOF ; IMMEDIATE COMPILING
: "ENDCASE   POSTPONE 2DROP  0 ?DO  POSTPONE THEN  LOOP ; IMMEDIATE COMPILING


\ Mass storage input/output

INCLUDE fileio/fs

: READ-LINE   ( c-addr u1 fid -- u2 flag ior )
   >R  OVER SWAP                     \ save fid and copy c-addr
   R@ READ-FILE                      \ fill buffer
   ?DUP IF                           \ if an error occurred
      NIP NIP 0 FALSE ROT            \ leave 0 false ior
      R> DROP EXIT                   \ drop fid and exit
   THEN
   DUP 0= IF                         \ if the line is of length 0,
      NIP FALSE 0  R> DROP  EXIT     \ exit with false flag
   THEN
   TUCK                              \ save no. of chars read
   EOL SEARCH ROT DROP               \ search for EOL; drop address
   IF                                \ if found,
      R@ FILE-POSITION               \ get the current file position
      ?DUP IF                        \ if an error occurred
         >R 2DROP DROP  FALSE R>     \ clear up, leave false flag
         R> DROP  EXIT               \ and ior, and exit
      THEN
      2 PICK EOL NIP -  U>UD D-      \ set pointer to just after EOL
      R> REPOSITION-FILE
      ?DUP IF                        \ if there was an error
         NIP  FALSE SWAP  EXIT       \ exit with error code
      THEN
      -                              \ make length
   ELSE
      DROP  R> DROP                  \ else u2=u1; drop fid
   THEN
   TRUE 0 ;                          \ leave true flag, ior=0
: WRITE-LINE   ( c-addr u fid -- ior )
   >R                                \ save fid
   R@ WRITE-FILE                     \ write the line
   ?DUP IF                           \ if there was an error
      R> DROP  EXIT                  \ drop fid and exit
   THEN
   EOL R> WRITE-FILE ;               \ write the line terminator;
                                     \ ior is WRITE-FILE's result


\ Terminal input/output #1

: SPACE   BL EMIT ;
: SPACES   0 ?DO  SPACE  LOOP ;
: TYPE   CHARS OVER + SWAP ?DO  I C@ EMIT  +CHAR +LOOP ;
: -TRAILING   BEGIN  DUP IF  2DUP 1- CHARS + C@  BL =  ELSE FALSE  THEN
   WHILE  1-  REPEAT ;


\ Mass storage input/output #1

0 VALUE "BLOCK
VARIABLE SCR
64 DUP CONSTANT /LINE
16 DUP CONSTANT LINES/BLOCK
* DUP CONSTANT /BLOCK   \ save value for later
: OPEN-BLOCKS   ( fam -- fid )   "BLOCK ?DUP IF  COUNT ROT OPEN-FILE
   ABORT" blocks file can't be opened"  ELSE ." no blocks file" ABORT  THEN ;
: @BLOCK   ( c-addr u -- )   R/O OPEN-BLOCKS  >R  1- 10 LSHIFT U>UD
   R@ REPOSITION-FILE DROP  /BLOCK R@ READ-FILE 2DROP  R> CLOSE-FILE DROP ;
: !BLOCK   ( c-addr u -- )   W/O OPEN-BLOCKS  >R  1- 10 LSHIFT U>UD
   R@ REPOSITION-FILE DROP  /BLOCK R@ WRITE-FILE DROP  R> CLOSE-FILE DROP ;

CELL+ CONSTANT /BLOCK-BUFFER   \ uses value saved above
: 'BUFFER   R0 @  1280 + ;   \ address of block buffer
: >BLOCK   CELL+ ;

HEX
: UPDATE   'BUFFER  DUP @ 80000000 OR  SWAP ! ;
: UPDATED?   ( -- f )   'BUFFER @  0< ;
: (BUFFER)   ( u -- c-addr f )
   'BUFFER                           \ address of block buffer
   TUCK @ 7FFFFFFF AND  OVER = IF    \ if the current block was requested,
      DROP >BLOCK  TRUE EXIT         \ leave address and true flag, and exit
   THEN
   UPDATED? IF                       \ was the last block updated?
      OVER >BLOCK OVER !BLOCK        \ if so, save it
   THEN
   OVER !                            \ set new block no., & clear update flag
   >BLOCK  FALSE ;                   \ leave address and false flag
DECIMAL
: BUFFER   ?DUP IF  (BUFFER) DROP  THEN ;
: BLOCK   ?DUP IF  DUP (BUFFER) IF  NIP  ELSE DUP ROT @BLOCK  THEN  THEN ;

128 CONSTANT /FILE-BUFFER
16 CONSTANT #FILE-BUFFERS
VARIABLE FILE-BUFFER#  0 V' FILE-BUFFER# !   \ next file buffer to use
: FIRST-FILE   LIMIT  #FILE-BUFFERS /FILE-BUFFER * - ;
: ALLOCATE-BUFFER   ( -- c-addr ior )   FILE-BUFFER# @  DUP #FILE-BUFFERS
   = IF  -1  ELSE DUP 1+ FILE-BUFFER# !  /FILE-BUFFER *  FIRST-FILE +  0
   THEN ;
: FREE-BUFFER   ( -- ior )   FILE-BUFFER#  DUP @ 0= IF  DROP -1
   ELSE -1 SWAP +!  0  THEN ;


\ Terminal input/output #2

: ACCEPT   ( c-addr +n1 -- +n2 )
   >R                                \ save count
   DUP BEGIN
      KEY                            \ get key
      DUP DEL? IF                    \ if Delete
         DROP                        \ get rid of key
         2DUP < IF                   \ if there's room to delete,
            CHAR-  DEL               \ decrement pointer & emit DEL
         THEN
      ELSE
         DUP CR? IF                  \ if end of line then
            R> 2DROP                 \ drop max. length & character,
            BL EMIT                  \ emit a space
            >-<                      \ get the length of the input
            EXIT                     \ and exit
         ELSE
            -ROT  2DUP >-<           \ if another key, check there's
            R@ < IF                  \ room for it,
               ROT DUP EMIT          \ print it,
               OVER C!               \ store it
               CHAR+                 \ and increment the pointer
            ELSE
               ROT DROP              \ otherwise drop the character
            THEN
         THEN
      THEN
   AGAIN ;

VARIABLE >IN

VARIABLE BLK

VARIABLE EVALUAND
VARIABLE #EVALUAND

VARIABLE #TIB
: TIB   R0 @ ;

VARIABLE #FIB
0 VALUE FIB

0 VALUE SOURCE-ID
: SOURCE
   BLK @ ?DUP IF
      BLOCK /BLOCK
   ELSE
      CASE SOURCE-ID
         -1 OF  EVALUAND @  #EVALUAND @  ENDOF
         0 OF  TIB  #TIB @  ENDOF
         >R  FIB  #FIB @  R>
      ENDCASE
   THEN ;

\ SAVE-INPUT returns the current input source immediately under the number of
\ items returned, encoded as:
\       0 = user input device, -1 = string
\       1 = block,              2 = file
: SAVE-INPUT   ( -- xn...x1 n )
   >IN @                             \ get >IN
   BLK @ ?DUP IF                     \ if input source is a block,
      1 3                            \ leave >IN BLK 1
   ELSE
      CASE SOURCE-ID                 \ otherwise look at SOURCE-ID
         0 OF  0 2  ENDOF            \ if 0, leave >IN 0
         -1 OF                       \ if -1, leave >IN EVALUAND
            EVALUAND @ #EVALUAND @   \ #EVALUAND -1
            -1 4
         ENDOF
         >R  FIB #FIB @ SOURCE-ID 2 5  R>
                                     \ if a file leave >IN FIB #FIB fid 2
      ENDCASE
   THEN ;
\ RESTORE-INPUT always succeeds unless the input source buffer being restored
\ has been altered, which it has no way of telling.
: RESTORE-INPUT   ( xn...x1 n -- f )
   DROP
   CASE
      0 OF  0 BLK !  0 TO SOURCE-ID  ENDOF
      1 OF  BLK !  ENDOF
      2 OF  0 BLK !  TO SOURCE-ID  #FIB !  TO FIB  ENDOF
      -1 OF
         0 BLK !   #EVALUAND ! EVALUAND !  -1 TO SOURCE-ID
      ENDOF
   ENDCASE
   >IN !
   FALSE ;

VARIABLE 'RETURN
: SAVE-INPUT>R   \ save input specification to return stack
   R> 'RETURN !                      \ save return address
   SAVE-INPUT                        \ get input specification
   DUP                               \ push it to return stack
   BEGIN  ?DUP WHILE                 \ can't use a DO loop as this would
      ROT >R                         \ interfere with the return stack
      1-
   REPEAT
   >R
   'RETURN @ >R ;                    \ restore return address
: R>RESTORE-INPUT   \ restore input specification from return stack
   R> 'RETURN !                      \ save return address
   R> DUP                            \ pop input specification
   BEGIN  ?DUP WHILE                 \ from return stack
      R> -ROT                        \ can't use a DO loop as this would
      1-                             \ interfere with the return stack
   REPEAT
   RESTORE-INPUT DROP                \ set input specification
   'RETURN @ >R ;                    \ restore return address

VARIABLE 'SCAN-TEST
: SCAN-TEST   'SCAN-TEST @EXECUTE ;
: SCAN   ( char xt -- c-addr u )
   'SCAN-TEST !
   SOURCE CHARS                      \ get input source
   OVER +                            \ end of input buffer + 1
   SWAP >IN @ CHARS +                \ start of parse area
   SWAP ROT  OVER 3 PICK ?DO         \ save start & end of input buffer
      DUP I C@ SCAN-TEST IF          \ if test true,
         NIP  I SWAP  LEAVE          \ drop end, leave I and exit
      THEN
   +CHAR +LOOP                       \ if end of loop reached, end left
   DROP                              \ get rid of delimiter
   OVER -  DUP >IN +!                \ advance >IN
   CHAR/ ;                           \ leave count and length

: PARSE   ( char -- c-addr u )
   ['] = SCAN                        \ search for delimiter
   >IN DUP @ CHAR+                   \ advance >IN past delimiter
   SOURCE NIP  MIN SWAP ! ;          \ making sure it stays in the source

: WORD   ( char -- c-addr )
   DUP                               \ copy delimiter
   ['] <> SCAN  2DROP                \ skip delimiter
   PARSE                             \ get the delimited string
   TOKEN  2DUP C!                    \ store count
   CHAR+  2DUP +  BL SWAP C!         \ store blank at end of string
   SWAP CMOVE                        \ store string
   TOKEN ;                           \ leave the string's address

: .(   [CHAR] ) PARSE TYPE ; IMMEDIATE


\ Compiler #3

: ",   ( c-addr u -- )   DUP C,  HERE SWAP  DUP ALLOT  CMOVE ;
: SLITERAL   POSTPONE (S")  ", 0 CALIGN ; IMMEDIATE

: C"   [CHAR] " PARSE  POSTPONE (C")  ", 0 CALIGN ; IMMEDIATE COMPILING
: S"   [CHAR] " PARSE  S"B SWAP 2DUP 2>R  CMOVE  2R> ;
   :NONAME   [CHAR] " PARSE  POSTPONE SLITERAL ;IMMEDIATE

: ."   POSTPONE S"  POSTPONE TYPE ; IMMEDIATE COMPILING

: CHAR   BL WORD  CHAR+ C@ ;
: [CHAR]   CHAR  POSTPONE LITERAL ; IMMEDIATE COMPILING


\ Interpreter #4

: ABORT"   POSTPONE C"  POSTPONE (ABORT") ; IMMEDIATE COMPILING


\ Numeric conversion

VARIABLE BASE
VARIABLE HELD

: DECIMAL   10 BASE ! ;
: HEX   16 BASE ! ;
: HOLD   -CHAR HELD +!  HELD @ C! ;
: SIGN   0< IF  [CHAR] - HOLD  THEN ;
: <#   TOKEN HELD ! ;
: #>   2DROP  HELD @  TOKEN OVER - ;
: #   BASE @ U>UD UD/MOD  2SWAP UD>U  DUP 10 < IF  [CHAR] 0 +
   ELSE [ CHAR A 10 - ] LITERAL +  THEN  HOLD ;
: #S   BEGIN  #  2DUP D0= UNTIL ;

: D.R   -ROT  TUCK DABS  <# #S ROT SIGN #>  ROT OVER -  0 MAX SPACES  TYPE ;
: D.   0 D.R  SPACE ;
: .R   SWAP S>D  ROT D.R ;
: .   0 .R  SPACE ;
: DEC.   BASE @ SWAP  DECIMAL .  BASE ! ;
: U.R   SWAP U>UD  ROT D.R ;
: U.   0 U.R  SPACE ;
: H.   BASE @  SWAP HEX U.  BASE ! ;

: >NUMBER   ( ud1 c-addr1 u1 -- ud2 c-addr2 u2 )
   DUP IF                            \ if something to convert
      CHARS OVER + SWAP              \ form limits for a loop
      TUCK  OVER >R                  \ save initial address and
                                     \ address of last character + 1
      DO
         C@                          \ get next character
         DUP [CHAR] A < IF           \ convert to a digit
            [CHAR] 0 -  ELSE [ CHAR A 10 - ] LITERAL -
         THEN
         DUP  BASE @ - 0< INVERT     \ if digit is too large...
         OVER 0<  OR IF              \ or too small
            DROP I  LEAVE            \ leave address of character
         THEN                        \ and exit the loop
         >R BASE @ U>UD D*           \ multiply n by BASE
         R> M+                       \ add new digit
         I CHAR+                     \ address of next character
      LOOP
      DUP R> >-<                     \ construct u'
   THEN ;
: NUMBER   ( c-addr -- d true | n false )
   DUP >R                            \ save address of string
   0. ROT                            \ make accumulator for >NUMBER
   COUNT                             \ count the string
   OVER C@                           \ get the first character
   [CHAR] - =  DUP >R  IF            \ skip first character if it's
      1- SWAP  CHAR+ SWAP            \ a minus and save the flag
   THEN
   FALSE >R                          \ save false flag
   BEGIN
      >NUMBER                        \ convert up to non-digit
      ?DUP WHILE                     \ if the string's not finished,
      OVER C@ 4 / 11 <> IF           \ is the non-digit punctuation?
         2R> 2DROP  R> UNDEFINED     \ if not, then not a number
      THEN
      R> DROP  TRUE >R               \ if so, set double no. flag
      SWAP CHAR+  SWAP CHAR-         \ and skip the punctuation
   REPEAT
   DROP                              \ drop string address
   2R> >R                            \ retrieve first character
   IF  DNEGATE  THEN                 \ if leading minus, negate no.
   R@ INVERT IF  D>S  THEN           \ return single or double no.
   R>                                \ and flag as appropriate
   R> DROP ;                         \ drop address of string


\ Mass storage input/output #2

: LIST   ( u -- )
   BASE @ SWAP  DECIMAL              \ save BASE and switch to decimal
   DUP SCR !                         \ set SCR to block displayed
   CR  ." Block " DUP .              \ print heading
   BLOCK                             \ fetch block
   LINES/BLOCK 0 DO                  \ for each line
      CR SPACE  I 2 .R  SPACE        \ print line number
      DUP /LINE TYPE                 \ print text
      /LINE +                        \ advance pointer to block
   LOOP
   CR                                \ put CR at end of last line
   DROP                              \ get rid of pointer
   BASE ! ;                          \ restore BASE


\ Compiler #4

: DEFINITIONS   CONTEXT @  SET-CURRENT ;

1024 CONSTANT #THREADS
: WORDLIST#   ( a-addr -- +n )   2 CELLS +  @ ;
: THREADS   ( a-addr1 -- a-addr2 )   3 CELLS +  @ ;
BL  DUP 8 LSHIFT OR  DUP 16 LSHIFT OR  CONSTANT BLS
: THREAD   ( a-addr u wid -- 'thread )   -ROT  SCRATCH  BLS OVER !  2DUP C!
   DUP 1+  2SWAP 3 MIN  ROT SWAP CMOVE  @  DUP 7 RSHIFT  DUP 7 RSHIFT
   XOR XOR  OVER WORDLIST# +  #THREADS 1- AND  CELLS  SWAP THREADS + ;

: GET-ORDER   #ORDER @  DUP IF  DUP >R  CELLS  CONTEXT TUCK + CELL-  DO
   I @  -CELL +LOOP  R>  THEN ;

VARIABLE VISIBLE?   \ holds execution token of word visibility test
: ALL-VISIBLE   ( wid xt n -- true )   2DROP DROP  TRUE ;
\ VISIBLE? must be set up before VET-WORDLIST is called with a word whose
\ stack effect is [ wid xt n -- f ], where wid is the word list and xt the
\ execution token of the found word and n its immediacy flag, and f is true
\ if the word is deemed visible by the test.
: VET-WORDLIST   ( c-addr u wid -- 0 | xt 1 | xt -1 )
   >R                                \ save wid
   2DUP R@ THREAD                    \ get address of thread
   BEGIN  @  ?DUP WHILE              \ for all words in thread
      DUP >NAME                      \ get name field address
      2OVER ROT COUNT                \ COUNT the strings
      COMPARE 0= IF                  \ if the name matches
         DUP >INFO @                 \ and the word is not SMUDGEd
         [ HEX ] 20000000 [ DECIMAL ] AND 0= IF
            R@ OVER                  \ get wid and xt of word
            DUP >INFO @ 0< 2* INVERT \ get immediacy flag
            DUP >R                   \ save flag
            VISIBLE? @EXECUTE IF     \ if word is deemed visible
               NIP NIP  R>  R> DROP  \ get flag, drop string and wid,
               EXIT                  \ and exit
            ELSE
               R> DROP               \ else drop immediacy flag
            THEN
         THEN
      THEN
      >THREAD                        \ leave next thread field
   REPEAT
   2DROP  R> DROP                    \ get rid of c-addr, u and wid,
   0 ;                               \ and set flag to 0
: SEARCH-WORDLIST   ( c-addr u wid -- 0 | xt 1 | xt -1 )
   ['] ALL-VISIBLE VISIBLE? !  VET-WORDLIST ;

: SELECT   ( a-addr1 xt -- a-addr2 n )
   VISIBLE? !                        \ set up visibility selector
   >R  GET-ORDER  R> SWAP            \ get search order
   ?DUP IF                           \ if search order non-empty
      1 SWAP DO                      \ for each word list in order
         TUCK COUNT ROT VET-WORDLIST \ search it
         ?DUP IF                     \ if the word is found
            I -ROT  2>R              \ save xt and immediacy flag
            0 DO  DROP  LOOP         \ drop wids and string address
            2R> UNLOOP EXIT          \ retrieve results and exit
         THEN
      -1 +LOOP
   THEN
   0 ;                               \ if not found leave string & 0 flag
: FIND   ( c-addr -- a-addr n )   ['] ALL-VISIBLE SELECT ;

: POSTPONE   BL WORD FIND  ?DUP 0= IF  UNDEFINED  THEN  0> IF  >COMPILE @
   COMPILE,  ELSE  POSTPONE (POSTPONE) ALIGN  <M0 ,  THEN ; IMMEDIATE
COMPILING

: HEADER   ( c-addr -- )
   ALIGN                             \ align DP for new definition
   0 ,                               \ store null compilation method
   DUP C@ 31 MIN                     \ get name (max. 31 chars)
   2DUP SWAP C!
   OVER COUNT
   GET-CURRENT SEARCH-WORDLIST IF    \ check name is unique
      DROP OVER COUNT TYPE ."  is not unique "
   THEN
   DUP >R                            \ save length
   CHAR+                             \ length of name field
   HERE TUCK >R                      \ beginning of name field
   DUP ALLOT                         \ reserve space for name field
   CMOVE                             \ write name in name field
   BL CALIGN                         \ pad with nulls to next cell boundary
   LAST ,                            \ store link to last word
   GET-CURRENT DUP                   \ get CURRENT word list chain
   R> COUNT ROT THREAD  DUP @ ,      \ store link to last word
   R> 24 LSHIFT  ,                   \ save length of name field
   HERE TUCK  SWAP !  SWAP ! ;       \ update CURRENT word list and thread


\ Exceptions #2

VARIABLE 'FRAME  0 V' 'FRAME !
: CATCH
   'FRAME @ >R                       \ push pointer to last frame
   SAVE-INPUT>R                      \ push current input source
   SP@ >R                            \ push data stack pointer
   RP@ 'FRAME !                      \ set pointer to current frame
   EXECUTE                           \ execute guarded word
   R> DROP                           \ pop exception frame
   R> BEGIN  ?DUP WHILE              \ can't use a DO loop as that would
     R> DROP  1-                     \ interfere with the return stack
   REPEAT
   R> 'FRAME !                       \ reset pointer to previous frame
   0 ;                               \ leave OK flag


\ Interpreter #5

: FOREIGN?   ( wid -- f )   2 CELLS + @  1023 > ;
: LOCAL?   ( wid xt n -- f )
   NIP  1 <> IF                      \ is the word non-immediate?
      STATE @                        \ if so, if we are compiling,
      GET-CURRENT FOREIGN?  AND IF   \ and CURRENT is foreign,
         GET-CURRENT =               \ word must be in CURRENT to be compiled
         EXIT
      THEN
   THEN
   FOREIGN? INVERT ;                 \ otherwise word must be native
: INTERPRET
   BEGIN  BL WORD  DUP C@ WHILE      \ while text in input stream
      ['] LOCAL? SELECT              \ search for word
      DUP IF                         \ if word found in dictionary
         STATE @ 0= IF               \ if interpreting, execute it
            DROP                     \ drop found flag
            DUP >INFO @ [ HEX ] 40000000 [ DECIMAL ] AND
            IF  -14 THROW  THEN
            EXECUTE
         ELSE
            0> IF                    \ if immediate, execute compile method
               >COMPILE @EXECUTE
            ELSE
               COMPILE,              \ if non-immediate, compile it
            THEN
         THEN
      ELSE                           \ if word is not found
         DROP                        \ drop found flag
         NUMBER                      \ try getting a number
         STATE @ IF                  \ compile if STATE is non-zero
            IF                       \ if a double number
               SWAP                  \ compile MS word
               POSTPONE LITERAL
            THEN
            POSTPONE LITERAL         \ compile single no./LS word
         ELSE
            DROP                     \ else get rid of flag
         THEN
      THEN
   REPEAT DROP ;                     \ get rid of input address

: EVALUATE   SAVE-INPUT>R  -1 TO SOURCE-ID  #EVALUAND !  EVALUAND !  0 >IN !
   0 BLK !  INTERPRET  R>RESTORE-INPUT ;

: REFILL   ( -- f )
   BLK @ IF                          \ if interpreting a block
      1 BLK +!  0 >IN !  TRUE        \ go on to next block
   ELSE
      CASE SOURCE-ID                 \ switch on SOURCE-ID
         0 OF                        \ if user input device
            TIB 80 ACCEPT            \ get a line of text to TIB
            #TIB !  0 >IN !  TRUE
         ENDOF
         -1 OF  FALSE  ENDOF         \ if a string, return false
         >R                          \ save switch
         FIB 128 R@ READ-LINE        \ else read a line from file
         ABORT" file read error during REFILL"
                                     \ if an exception occurred, abort
         SWAP #FIB !  0 >IN !        \ set no. of chars in line
         R>                          \ restore switch
      ENDCASE
   THEN ;

: ?STACK   DEPTH 0< ABORT" stack underflow" ;
: (QUIT)
   POSTPONE [  R0 @ RP!
   0 BLK !  0 TO SOURCE-ID
   BEGIN  CR REFILL WHILE
      INTERPRET  ?STACK  STATE @ 0= IF  ." ok"  THEN
   REPEAT
   ." parse area empty" ABORT ;


\ Compiler #5

: :   BL WORD HEADER  TRUE SMUDGE  LINK,  ] ;
: ;   UNLINK,  POSTPONE [  FALSE SMUDGE ; IMMEDIATE COMPILING
: :NONAME   ALIGN HERE LINK,  ] ;
: ;IMMEDIATE   POSTPONE ;  IMMEDIATE  LAST >COMPILE ! ; IMMEDIATE COMPILING


\ Miscellaneous

: (
   BEGIN
      [CHAR] ) PARSE 2DROP           \ parse up to ) or end of area
      SOURCE-ID 1+  2 U< IF          \ exit if not reading from file
         EXIT
      THEN
      >IN @ IF                       \ was parse area empty?
         SOURCE DROP >IN @ 1- CHARS +  C@ [CHAR] ) <>
                                     \ if not, was last character )?
      ELSE
         TRUE                        \ if empty we must refill
      THEN
      WHILE                          \ if parse area empty or no )
      REFILL 0=                      \ found, refill and parse again
   UNTIL THEN ;
IMMEDIATE

: \   BLK @ IF  >IN  DUP @ CHARS  /LINE +  /LINE / /LINE *  CHAR/ SWAP !
   ELSE  SOURCE NIP >IN ! THEN ; IMMEDIATE
: ?   @ . ;
: .S   ?STACK  DEPTH ?DUP IF  1- 0 SWAP DO  I PICK .  -1 +LOOP
   ELSE ." stack empty "  THEN ;


\ Mass storage input/output #3

: FROM   ( name )   BL WORD COUNT  HERE TO "BLOCK  ", ;

: LOAD   SAVE-INPUT>R  BLK !  0 >IN !  INTERPRET  R>RESTORE-INPUT ;
: THRU   1+ SWAP DO  I LOAD  LOOP ;
HEX
: SAVE-BUFFERS
   UPDATED? IF                       \ if the buffer was updated
      'BUFFER  DUP @ 7FFFFFFF AND    \ get the block number
      OVER >BLOCK OVER !BLOCK        \ save the block
      SWAP !                         \ mark the buffer as not updated
   THEN ;
DECIMAL
: EMPTY-BUFFERS   0 'BUFFER ! ;
: FLUSH   SAVE-BUFFERS EMPTY-BUFFERS ;

: -->   0 >IN !  1 BLK +! ;

: INCLUDE-FILE   ( i*x fid -- j*x )
   SAVE-INPUT>R                      \ save current input source
   TO SOURCE-ID  0 BLK !             \ set up new input source
   ALLOCATE-BUFFER IF                \ allocate new file buffer
     SOURCE-ID CLOSE-FILE
     ." no more file buffers"  ABORT
   THEN
   TO FIB
   BEGIN  REFILL WHILE               \ interpret the file
      ['] INTERPRET CATCH ?DUP IF    \ close the file if an exception is
         SOURCE-ID CLOSE-FILE DROP   \ generated, then pass the exception on
         FREE-BUFFER DROP            \ having freed the buffer
         NIP                         \ and dropped the input source address
         THROW
      THEN
   REPEAT
   FREE-BUFFER ABORT" no file buffer to free"
                                     \ free the file buffer
   R>RESTORE-INPUT ;                 \ restore the input source
: INCLUDED   ( i*x c-addr u -- j*x )
   2DUP  R/O OPEN-FILE IF            \ open file; if error,
      DROP                           \ get rid of bad fid
      TYPE ."  can't be INCLUDED"    \ give error message
      ABORT                          \ and abort
   THEN
   >R                                \ save fid
   2DROP                             \ drop c-addr u
   R@ INCLUDE-FILE                   \ include the file
   R> CLOSE-FILE                     \ close the file; if error,
   ABORT" error after INCLUDEing" ;  \ give error message and abort
: INCLUDE   ( file )   BL WORD COUNT  INCLUDED ;


\ Compiler #6

: '   BL WORD FIND  0= IF  UNDEFINED  THEN ;
: [']   ' <M0  POSTPONE LITERAL ; IMMEDIATE COMPILING
: [COMPILE]   '  DUP >COMPILE @  ?DUP IF NIP  THEN  COMPILE, ; IMMEDIATE
COMPILING


\ Defining

: CREATE   BL WORD HEADER  CREATE, ;
: DOES>   POSTPONE (DOES>) ALIGN  HERE CELL+  LAST  2DUP - CELL/  SWAP >INFO
   DUP @ ROT OR  SWAP !  <M0 ,  LINK,  POSTPONE (DOES) ; IMMEDIATE COMPILING

: VARIABLE   CREATE  CELL ALLOT ;
: CONSTANT   BL WORD HEADER  LINK,  POSTPONE LITERAL  UNLINK, ;
: VALUE   CREATE  ,  DOES>  @ ; \ FIXME: Make this work as for the metacompiler (i.e. machine-specific, sigh)
: TO   ' >BODY ! ;
   :NONAME   ' >BODY  <M0  POSTPONE LITERAL  POSTPONE ! ;IMMEDIATE


\ Word lists

: DICTIONARY   CREATE  HERE CELL+ ,  HERE  OVER ALLOT  SWAP ERASE
   DOES>  TO DP ;
: ROOT   ROOTDP TO DP ;

VARIABLE CODEX  0 V' CODEX !
VARIABLE CURRENT-VOLUME
: VOLUME   CREATE  HERE 3 CELLS + ,  HERE CODEX  DUP @ ,  !  0 ,
   #THREADS 0 DO  0 ,  LOOP  DOES>  CURRENT-VOLUME ! ;
: #WORDLISTS   ( volume -- '#wordlists )   2 CELLS + ; \ FIXME: make ; execute ALIGN?
ALIGN HERE <M0   \ leave address of data structure
SWAP ,   \ address is HERE put on stack by make.fs
HERE <M0  V' CODEX  DUP @ ,  !  0 ,
: KERNEL   LITERAL ( address is HERE left earlier ) CURRENT-VOLUME ! ;

VARIABLE CHAIN  0 V' CHAIN !
: WORDLIST   ALIGN HERE  0 ,  HERE <M0  CHAIN  DUP @ ,  !  CURRENT-VOLUME @
   TUCK #WORDLISTS  DUP @  DUP ,  1+ SWAP !  SWAP @ <M0 , ;
: VOCABULARY   WORDLIST <M0  CREATE  ,  DOES>  #ORDER @ 0= IF  1 #ORDER +!
   THEN  @ CONTEXT ! ;
VOCABULARY FORTH
' FORTH >BODY @ <M0 CONSTANT FORTH-WORDLIST
: ALSO   CONTEXT  DUP CELL+  #ORDER @ CELLS  MOVE  1 #ORDER +! ;
: ONLY   FORTH  1 #ORDER ! ;
: FOREIGN   CONTEXT @  2 CELLS +  DUP @ 1024 OR  SWAP ! ;
: NATIVE   CONTEXT @  2 CELLS +  DUP @ 1023 AND  SWAP ! ;
: SET-ORDER   DUP -1 = IF  ONLY  ELSE  DUP #ORDER !  CELLS  CONTEXT TUCK +
   SWAP ?DO  I !  CELL +LOOP  THEN ;
: PREVIOUS   GET-ORDER  DUP 0> IF  NIP 1-  THEN  SET-ORDER ;
: ORDER   ." CONTEXT : "  GET-ORDER  0 ?DO  H.  LOOP  CR ." CURRENT : "
   GET-CURRENT H. ;

: (FORGET)
   >COMPILE DP !
   CHAIN DUP  BEGIN  @  DUP HERE < UNTIL  OVER !
   BEGIN  @ ?DUP WHILE
      DUP CELL-  DUP
      BEGIN  @  DUP HERE < INVERT WHILE  >LINK  REPEAT
      SWAP !
   REPEAT
   CODEX DUP  BEGIN  @  DUP HERE < UNTIL  OVER !
   BEGIN  @ ?DUP WHILE
      DUP CELL- @  DUP  #THREADS CELLS +  SWAP DO
         I DUP
         BEGIN  @ DUP  HERE < INVERT WHILE  >THREAD  REPEAT
         SWAP !
      CELL +LOOP
   REPEAT ;
: FORGET   ( name )   ' (FORGET) ;
: MARKER   ( name )
   CREATE                            \ create the MARKER word
   GET-ORDER  DUP ,                  \ save the search order
   0 ?DO  ,  LOOP
   LAST ,                            \ and the last definition.
   DP ,                              \ and the current DP
   DOES>                             \ at runtime:
      DUP @  DUP >R                  \ save no. of lists in order
      CELLS 2DUP + CELL+             \ get old value of HERE
      DP >R                          \ save current DP
      DUP CELL+ @  TO DP             \ restore old DP
      @ (FORGET)                     \ delete words after old HERE
      R> TO DP                       \ restore current DP
      OVER CELL+ -ROT + DO           \ retrieve the search order
         I @
      -CELL +LOOP
      R>                             \ retrieve size of search order
      SET-ORDER ;                    \ restore the search order

VARIABLE CURSORX   \ cursor x position during WORDS
: ADVANCE   ( +n -- )   CURSORX +! ;
: WRAP?   ( -- f )   CURSORX @ + WIDTH < INVERT ;
: NEWLINE   0 CURSORX !  CR ;
: WORDS
   NEWLINE                           \ start listing on a new line
   CONTEXT @                         \ get start of chain
   BEGIN  @ ?DUP WHILE               \ for each word in the chain
      DUP >NAME COUNT                \ get the name
      DUP WRAP? IF  NEWLINE  THEN    \ new line if necessary
      DUP ADVANCE                    \ advance the cursor
      TYPE                           \ type the name
      3 WRAP? IF                     \ leave a gap or move to a new
         NEWLINE                     \ line
      ELSE
         3 SPACES  3 ADVANCE
      THEN
      >LINK                          \ get link to next word
   REPEAT
   CURSORX @ IF  NEWLINE  THEN ;     \ ensure we're on a new line


\ Environmental queries

: ENVIRONMENT?
   "CASE
      S" /COUNTED-STRING"    "OF  255             "ENDOF
      S" /HOLD"              "OF  256             "ENDOF
      S" /PAD"               "OF  256             "ENDOF
      S" ADDRESS-UNIT-BITS"  "OF  8               "ENDOF
      S" BLOCK"              "OF  TRUE            "ENDOF
      S" BLOCK-EXT"          "OF  TRUE            "ENDOF
      S" CORE"               "OF  TRUE            "ENDOF
      S" CORE-EXT"           "OF  FALSE           "ENDOF
      S" DOUBLE"             "OF  FALSE           "ENDOF
      S" DOUBLE-EXT"         "OF  FALSE           "ENDOF
      S" EXCEPTION"          "OF  TRUE            "ENDOF
      S" EXCEPTION-EXT"      "OF  TRUE            "ENDOF
      S" FACILITY"           "OF  FALSE           "ENDOF
      S" FACILITY-EXT"       "OF  FALSE           "ENDOF
      S" FILE"               "OF  TRUE            "ENDOF
      S" FILE-EXT"           "OF  TRUE            "ENDOF
      S" FLOORED"            "OF  TRUE            "ENDOF
      S" MAX-CHAR"           "OF  255             "ENDOF
      S" MAX-D"              "OF  -1 1RSHIFT S>D  "ENDOF
      S" MAX-N"              "OF  -1 1RSHIFT      "ENDOF
      S" MAX-U"              "OF  -1              "ENDOF
      S" MAX-UD"             "OF  -1 0            "ENDOF
      S" RETURN-STACK-CELLS" "OF  CELLS/R         "ENDOF
      S" SEARCH-ORDER"       "OF  TRUE            "ENDOF
      S" SEARCH-ORDER-EXT"   "OF  TRUE            "ENDOF
      S" STACK-CELLS"        "OF  CELLS/S         "ENDOF
      S" STRING"             "OF  TRUE            "ENDOF
      S" STRING-EXT"         "OF  TRUE            "ENDOF
      S" TOOLS"              "OF  FALSE           "ENDOF
      S" TOOLS-EXT"          "OF  FALSE           "ENDOF
      S" WORDLISTS"          "OF  8               "ENDOF
      2DROP FALSE EXIT
   "ENDCASE
   TRUE ;


\ Exceptions #3

: (THROW)
   ?DUP IF                           \ if flag is true
      'FRAME @ ?DUP IF               \ and there's a frame to pop
         RP!                         \ set return stack to frame
         R>  SWAP >R  SP!  R>        \ reset data stack, keep exception no.
         R>RESTORE-INPUT             \ reset input source
         R> 'FRAME !                 \ set pointer to next frame
      ELSE
         CASE                        \ if no frame, act on code
            -1 OF  S0 @ SP!  QUIT  ENDOF
            -2 OF  'THROWN @ COUNT TYPE  ABORT  ENDOF
            -10 OF  ." division by zero"  ABORT  ENDOF
            -11 OF  ." quotient too large"  ABORT  ENDOF
            -13 OF  'THROWN @ COUNT TYPE  ."  ?"  ABORT  ENDOF
            -14 OF  ." compilation only"  ABORT  ENDOF
            -56 OF  (QUIT)  ENDOF
            ." exception " DUP . ." raised"  ABORT
         ENDCASE
      THEN
   THEN ;


\ Forward branches

\ RESOLVERs allow forward branches. Define a resolver, use it in definitions
\ and then use RESOLVE: or RESOLVES to resolve it. RESOLVERs may be POSTPONEd
\ but the code to POSTPONE the RESOLVER will not itself be resolved.
: (RESOLVER)   ( name )
   CREATE IMMEDIATE COMPILING        \ create RESOLVER
   ,                                 \ end-of-list marker (left by caller)
   DOES>
      HERE                           \ address of branch to resolve
      SWAP DUP @  DUP 1 AND          \ get previous branch address & type flag
      SWAP ,                         \ compile previous branch address & flag
      ROT OR SWAP ! ;                \ record next branch in list
                                     \ with call/branch flag in LSB
: RESOLVER   ( name )
   0 (RESOLVER) ;
\ A RESOLVER which compiles branches instead of calls
: BRANCH-RESOLVER   ( name )
   1 (RESOLVER) ;

\ RESOLVE resolves all occurrences of the RESOLVER whose execution token is
\ from with calls to to. RESOLVE must not be used directly, but via RESOLVE:
\ or RESOLVES.
: RESOLVE   ( from to -- )
   SWAP >BODY @                      \ get first address in branch list
   DUP 1 AND >R                      \ get and save call/branch flag
   BEGIN  1 INVERT AND ?DUP WHILE    \ chain down list until null marker,
                                     \ clearing call/branch flag
      DUP @                          \ get next address in list
      -ROT  2DUP SWAP  R@ IF         \ compile the call or branch
         BRANCH
      ELSE
         CALL
      THEN
      SWAP
   REPEAT
   R> 2DROP ;                        \ drop to and flag

\ RESOLVES is used to resolve WILL-DO defining words (see WILL-DO).
: RESOLVES   ( name )   ( a-addr -- )   '  SWAP RESOLVE ;

\ RESOLVE: is used to supply the definition of a RESOLVER; the branch list is
\ resolved to calls to the new definition.
: RESOLVE:   ( name )
   BL WORD                           \ get name
   DUP FIND 0= IF  UNDEFINED  THEN   \ get RESOLVER's execution token
   TRUE OVER SMUDGE!                 \ remove RESOLVER from search order
   SWAP HEADER  TRUE SMUDGE          \ start creating new definition
   HERE RESOLVE                      \ resolve calls to new definition
   LINK,  ] ;                        \ add link code and start compiling


\ Redefinition

\ R: redefines name. old is the old xt and new the new xt.
\ To redefine a word immediately use R: name ... R;; to set up a redefinition
\ for later use with a REDEFINER use R: name ... ;.
: R:   ( name )   ( -- old new )
   '                                 \ get old xt
   :NONAME ;                         \ start the redefinition; leave new xt

\ R; makes a redefinition created with R: take effect immediately.
: R;   ( old new -- )
   OVER SWAP BRANCH                  \ compile a branch in the old word
   POSTPONE ; ;                      \ end the redefinition
IMMEDIATE COMPILING

\ >DOES>, given the xt of a defining word, returns the address of the DOES>
\ code.
HEX
: >DOES>   ( xt -- 'does )   DUP >INFO @ FFFF AND CELLS  + ;
DECIMAL

\ DOES>: allows the run-time code of a defining word to be redefined. Use
\ like R:; name must be the name of a defining word.
: DOES>:   ( name )   ( -- old new )
   ' >DOES>                          \ get address of old DOES> code
   :NONAME ;                         \ start new definition

\ WILL-DO makes a defining word compile a BRANCH-RESOLVER rather than its
\ normal code. Use as BRANCH-RESOLVER X WILL-DO Y, where Y must be a defining
\ word. This can be used both to revector existing defining words, and to
\ resolve the run-time code of new ones. Use RESOLVES or RESOLVE: to resolve
\ WILL-DO, e.g.: ' NEW-DEFINER >DOES> RESOLVES DEFINER-RESOLVER
\ WILL-DO must be used as part of a REDEFINER.
: WILL-DO   ( -- old new )   ' >DOES> 8 -  :NONAME POSTPONE LAST
   POSTPONE >DOES POSTPONE HERE POSTPONE SWAP POSTPONE DP POSTPONE !
   LAST COMPILE,  POSTPONE DP POSTPONE ! POSTPONE ; ;


\ REDEFINERs swap the execution semantics of words redefined with R: between
\ the old and new semantics. u is the number of words to swap. The words must
\ have been defined with R: name ... ; as R; consumes information that
\ REDEFINER needs.
: REDEFINER   ( name )   ( old1 new1...oldu newu u -- )
   CREATE  DUP ,                     \ write the no. of words to redefine
   0 ?DO                             \ for each word
      OVER ,                         \ record the old xt
      POSTPONE AHEAD                 \ compile a branch
      -ROT BRANCH                    \ from the old word to the new
   LOOP
   DOES>
      DUP @                          \ no. of words to redefine
      CELLS 2*  SWAP CELL+           \ get start address and offset of data
      TUCK + SWAP ?DO
         I CELL+ @                   \ get code to patch word with
         I @                         \ get address to patch
         DUP @                       \ get old contents of cell to patch
         I CELL+ !                   \ save old contents
         CODE!                       \ patch word
      [ 2 CELLS ] LITERAL +LOOP ;


INCLUDE os/fs   \ include OS access words


\ Initialisation and version number

77 CONSTANT VERSION
: %.   S>D  <# # # [CHAR] . HOLD #S #>  TYPE ;

: START
   LIMIT                             \ get address of end of memory
   /BLOCK-BUFFER -                   \ reserve block buffer,
   #FILE-BUFFERS /FILE-BUFFER * -    \ file buffers,
   256 5 * -                         \ and TIB, PAD, TOKEN, SCRATCH and S"B
   DUP R0 !  DUP RP!                 \ set R0 and RP
   CELLS/R CELLS -                   \ make room for return stack
   DUP S0 !  SP!                     \ set S0 and SP
   [ ' (THROW) <M0 ] LITERAL 'THROW !
   R0 @  LIMIT  OVER -  ERASE        \ erase buffers
   ROOT KERNEL                       \ use ROOT dictionary and KERNEL volume
   ONLY FORTH DEFINITIONS            \ minimal word list
   DECIMAL                           \ numbers treated as base 10
   ." aForth for "  "ENVIRONMENT TYPE ."  v" VERSION %.
   ."  14th May 2000"
   CR "COPYRIGHT TYPE  ."  Reuben Thomas 1991-2000" CR
                                     \ display the start message
   QUIT ;                            \ enter the main loop