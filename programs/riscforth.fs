( Screen Contents                                  BWL 22/01/88)
.(  Screen #             Contents  ) CR
.(  -------------------------------------------------------) CR
.(  39                   Screen editor ) CR
.(  48                   Screen/Block utilities ) CR
.(  60 61 62             Multiprogrammer. Load with THRU) CR
.( 107                   CALL Finder, Vocabulary Display ) CR
.( 108                   Single Step Debugger ) CR
.( 150                   Simple Text Formatter ) CR
.( 170                   Free Store Manager ) CR

( Screen Management utilities                      BWL 12/02/88)
FORTH-83 DECIMAL
: MAX-BLOCK  ( n -- )
  DUP SCREENLENGTH 1024 / 1+ < ABORT"  Inside Range "
  DUP BUFFER SWAP BLOCK-WRITE ;
: BLOCK-MOVE  ( from to number -- )
  DUP 0> 0= ABORT"  Bad Number" -ROT 2DUP = ABORT"  Bad Range"
  FLUSH 2DUP > >R OVER - -ROT DUP UNDER+ R> IF
  DO  DUP I BLOCK  4 - +!  UPDATE LOOP
         ELSE
  SWAP DO  DUP I BLOCK  4 - +!  UPDATE  -1 +LOOP
         THEN DROP ;
: BLOCK-WIPE  ( start number -- )
  OVER + SWAP DO I SCR ! [ EDITOR ] WIPE LOOP ;
FORTH
CR .( Screen Utilities )


( Tasking )
ONLY FORTH DECIMAL
: TASK'S   ( addr-task addr-user -- addr )
  TCB -  + ;
: TASK:   ( s r -- )
  VARIABLE   TCB HERE 4 -  1024 CMOVE
  2DUP + 1048 +   HERE 4 - >R   ALLOT
  SWAP 1024 + R@ + ALIGN   DUP R@ S0 TASK'S !  R@ 16 + !
  DROP    HERE 8 -   R> R0 TASK'S ! ;
: BUILD   ( task-addr -- )
  >R   1 R@ TASK-STATE TASK'S !
  TASK-LINK @   R@ TASK-LINK !   R> TASK-LINK TASK'S ! ;
: TASK-DO  ( addr task-addr -- ) 2DUP 20 +  ! 'QUIT TASK'S ! ;
: ACTIVATE   ( task-addr -- )   TASK-STATE TASK'S 0 SWAP ! ;
: DEACTIVATE   ( task-addr -- )   TASK-STATE TASK'S 2 SWAP ! ;
: STOP   ( -- )   2 TASK-STATE ! PAUSE ;
: MKEY   ( -- c)
  BEGIN   0 ?KEY PAUSE UNTIL ;
: MEMIT   ( c --)   (EMIT) PAUSE ;
' MKEY 'KEY !
' MEMIT 'EMIT !
CR .( Multiprogrammer )

( Multiprogrammer GET and RELEASE )
: GET   ( addr -- )
  BEGIN   PAUSE DUP @ 0= UNTIL   TCB SWAP ! ;
: RELEASE   ( addr -- )
  DUP @ TCB = IF 0 SWAP ! ELSE DROP THEN PAUSE ;
CR .( GET and RELEASE )


( Multi-Tasking Demo )
FORTH-83 DECIMAL
1000 1000 TASK: GTASK
: CIR 2DUP 50 1 3 GCOL CIRCLE 2DUP 2 3 GCOL 70 CIRCLE  90 3 3
  GCOL CIRCLE ;
: Movie   LP! SP! RP! BEGIN
  1280 0 DO I 500 CIR 100 0 DO PAUSE LOOP
            I 500 CIR 10 +LOOP
  AGAIN ;
' Movie GTASK TASK-DO
GTASK BUILD
GTASK ACTIVATE


( Debugger - in-line debugging words )
: COMMAND  BEGIN CR ." C for command else continue: "
  KEY ASCII C = WHILE CR ." command:"  QUERY INTERPRET REPEAT ;

: EntryPoint   R> VALID DUP 16 + >R  ( adjust return address )
  ( addr on stack is address of degugging info for word )
  DUP 4 + @  ?DUP IF
   CR ." Entering " SWAP @ COUNT TYPE
   1 - ?DUP IF >R ."  Stack: " .S R> 1 > IF
   COMMAND
  THEN THEN CR
  ELSE DROP THEN ;


( exit point debugging words )
: ExitPoint   R> VALID @ ( get addr of info )
  DUP 4 + @ ?DUP IF
   CR  ." Exiting " SWAP @ COUNT TYPE
   1 - ?DUP IF >R ."  Stack: " .S R> 1 > IF
   COMMAND
  THEN THEN CR
  ELSE DROP THEN ;
: SpyPoint   R> VALID DUP 4 + >R @
  DUP 4 + @ DUP 1 > IF
   CR  ." Spy in " SWAP @ COUNT TYPE
   >R ."  Stack: " .S R> 2 > IF
   COMMAND
   THEN CR
  ELSE 2DROP THEN ;


( TRON TROFF  )
: TRACEABLE  4 + @ADDRESS
  ['] EntryPoint - ABORT"  Cannot Trace" ;
DECIMAL
: TRON   ( use TRON word )  '
  DUP TRACEABLE
  12 +  1 SWAP ! ;
: TRON1   ' DUP TRACEABLE 12 + 2 SWAP ! ;
: TRON2   ' DUP TRACEABLE 12 + 3 SWAP ! ;
: TROFF  ( use TROFF word )  '
  DUP TRACEABLE
  12 + 0 SWAP ! ;


( New  :  ;  EXIT  )
: EXIT   STATE @ IF COMPILE ExitPoint LAST @ NAME> 8 + ,
                 ELSE R> DROP EXIT THEN ; IMMEDIATE
: SPY  ?COMP COMPILE SpyPoint LAST @ NAME> 8 + , ; IMMEDIATE

: ;   ?COMP ?CSP ?CLP [COMPILE] EXIT [COMPILE] [ SMUDGE ;
      IMMEDIATE SMUDGE LAST @
: :   [COMPILE] : COMPILE EntryPoint LAST @ , 0 , 0 , 0 , ;
      IMMEDIATE
LAST @ SWAP LAST ! SMUDGE LAST !


( CALL Finder & Vocabulary Display                 BWL 04/05/88)
FORTH-83 HEX CR .( CALL Finder & Vocabulary Display )
: CHECKOUT   ( addr1 addr2 -- )
  DUP @  18 >>> EB = IF DUP @ADDRESS ROT = IF
  CR WHEREIS ELSE DROP THEN ELSE 2DROP THEN ;
: WHOCALLS   20 WORD FIND 0= ABORT"  ?"
  HERE 9700 DO  DUP I CHECKOUT  4 +LOOP DROP ;
DECIMAL
: VOCABS   CR VOC-LINK BEGIN @ ?DUP WHILE DUP WHEREIS REPEAT ;
: (ORDER)  ( n -- )  DUP 0= IF ." ONLY " DROP EXIT THEN
  DUP 1 = IF ." FORTH " DROP EXIT THEN VOC-LINK PEANO -:
  DUP 0< IF ." %#@!$ " DROP EXIT THEN
  VOC-LINK SWAP 1+ 0 DO @ LOOP  WHEREIS  ;
: ORDER  CR ." CONTEXT is "
  CONTEXT DUP 4 UNDER+ DO I C@ 31 AND (ORDER) LOOP CR
  ." CURRENT is " CURRENT @ (ORDER) ;


( Debugger LOAD Screen and Global Flags )
FORTH-83 DECIMAL
VARIABLE Level 0 Level !
VARIABLE Action 0 Action !
VARIABLE Count 0 Count !
VARIABLE Holding
VARIABLE BreakPt 0 BreakPt !
109 113 THRU
CR .( Debugger Enabled ) CR

( Stack Display utilities ) FORTH-83  HEX
: XR  ( Pretty Print Return Stack )
 CR ." Return Stack  --  " RDEPTH DEC. ." Deep "
 RDEPTH 0< IF CR ." Underflow " CR EXIT THEN
 CR  ." Hex  Decimal Word  "  CR
 RP@ RDEPTH 0 DO DUP @ VALID
 DUP H. DUP DEC. WHEREIS CR 4 + LOOP DROP ;
: XL  ( ditto loop stack )
 CR ." Loop Stack  -- " LDEPTH DEC. ." Deep "
 LDEPTH 0= IF CR ." Empty " CR EXIT THEN
 LDEPTH 0< IF CR ." Underflow " CR EXIT THEN
 CR ." Value     I Value " CR
 0 >L LP@ LDEPTH 1  DO  DUP @ DUP .
   4 SPACES OVER 4 - @ 80000000 -: - . CR 4 - LOOP L> 2DROP ;
 DECIMAL

( Interact )
FORTH-83 DECIMAL
 : XS   CR ." Parameter stack: " .S ;
 : XOFF   0 Action ! CR ." Tracing Off " ;
 : XON   4 Action ! CR ." Trace on All " ;
 : XN   RDEPTH 3 - Level ! 3 Action !
     CR  ." Trace on level " RDEPTH 3 - . ;
: XA   Count ! Action @ 3 = IF 2 ELSE 1 THEN Action !
  CR ." Skip forward " Count ? ;
: Interact   BEGIN QUERY INTERPRET SPAN @ WHILE CR ." ?? "
             REPEAT ;
: XB   ' BreakPt ! CR ." Break on " PAST @ COUNT TYPE XOFF ;

( Debugger Call Handler )  FORTH-83 DECIMAL
: Act   Action @ ?DUP
  IF DUP 1 = IF DROP -1 Count +! EXIT THEN
   DUP 2 = IF DROP RDEPTH Level @ 2+ < IF -1 Count +! THEN EXIT
             THEN
     DUP 3 = IF DROP RDEPTH Level @ 2+ < 0= Count !  EXIT THEN
     4 = IF  0 Count ! EXIT THEN
  ELSE 1 Count ! THEN ;
: DebugCall   R> VALID DUP 4 + >R @
  Act Count @ IF DUP NAME> DUP BreakPt @ - IF NIP XEXIT
  ELSE CR ." Break Point " DROP THEN  THEN
  CR RDEPTH 60 MIN SPACES  DUP COUNT TYPE
."   level: " RDEPTH DEC. Holding ! Action @ 3 < IF 4 Action !
   THEN
  ." stack  " .S  ."  ?? "
  Interact Holding @ NAME> XEXIT ;

( Compiler Patch )
FORTH-83
: NewWord   DROP COMPILE DebugCall PAST @ , ;
: DEBUG  ['] NewWord 'COMPILE ! 4 Action ! ;
: NODEBUG ['] WORD, 'COMPILE ! ;
DEBUG


(  )
FORTH-83 DECIMAL
1000 1000 TASK: SPEC
: SPECDO  LP! SP! RP! STOP BEGIN 0 >IN ! 0 BLK ! INTERPRET STOP
  AGAIN ;
: SPECTIB   SPEC S0 TASK'S @ ;
: TELL   ASCII \ TEXT PAD 1+ SPECTIB 80 CMOVE
  80 SPEC #TIB TASK'S !
  SPEC ACTIVATE ;
' SPECDO SPEC TASK-DO
SPEC BUILD
SPEC ACTIVATE


HEX
*( LOAD TEXTUAL 20000 )
20000 CurrentPtr !
2018F EndText !
0 CurX !
30 CurY !
1FFFF StartText !
DECIMAL
StartText @ DUP  LineStart !  Next LineEnd !


( Simple Text Formatter                            BWL 03/05/88)
FORTH-83 DECIMAL
VARIABLE MW  64 MW !
VARIABLE TextBuffer 200 ALLOT
VARIABLE Tags 200 ALLOT
VARIABLE Flag1
VARIABLE Style
VARIABLE Ptr
VARIABLE Pl
VARIABLE LineCount
VARIABLE TI
VARIABLE IN
VARIABLE #SPACES
: MaxWidth MW @ ;
: SPREAD  ( offset addr -- ) + DUP 1+ MaxWidth CMOVE> ;
: WIDEN  ( i -- )  DUP Tags + C@ IF 1- DUP
         TextBuffer SPREAD 1 Ptr +!
         2+ Tags SPREAD ELSE DROP THEN ;
: EXPAND  ( -- f )  Ptr @ TI @ IN @ + DO
          Ptr @ MaxWidth 1+ = IF 1 LEAP THEN  I WIDEN  LOOP 0 ;
: JUSTIFY   #SPACES @ 0> IF
  0 TI @ IN @ + Tags + C! BEGIN EXPAND UNTIL THEN ;
: CENTRE   TextBuffer DUP MW @ Ptr @ - 2/ DUP UNDER+ >R Ptr @
  CMOVE> TextBuffer R> BLANK MW @ Ptr ! ;
: ADJUST   Style @ DUP 1 = IF JUSTIFY DROP EXIT THEN
                       2 = IF CENTRE THEN ;
: ADD  ( addr -- ) COUNT DUP >R TextBuffer Ptr @ + SWAP CMOVE
       1 Tags Ptr @ + C! R> 1+ Ptr +! 1 Flag1 ! 1 #SPACES +! ;
: SHORTEN   ( addr -- ) DUP C@ DUP >R 1- OVER C!
            2 + DUP 1- R> CMOVE ;
: COMMAND   ( addr -- f ) 1+ C@ ASCII \ = ;
: DOIT  ( addr -- ) DUP SHORTEN FIND IF EXECUTE ELSE
        NUMBER THEN ;
: -FINISHED   ( addr -- f ) C@ 0= SWAP 1+ C@ ASCII } = OR 0= ;
: SETUP   0 Flag1 ! TextBuffer MaxWidth 1+ 32 FILL IN @ Ptr !
          0 TI !    Tags MaxWidth 1+ 0 FILL 1 LineCount +!
          -1 #SPACES ! ;
: -FITS   ( addr -- f ) C@ Ptr @ + MaxWidth > ;
: BREAK   TextBuffer Ptr @ TYPE CR   SETUP ;
: BR   ADJUST BREAK ;
: (PROCESS)   ( addr -- ) DUP -FITS IF BR THEN ADD ;
: PROCESS ( addr -- ) DUP COMMAND IF DOIT ELSE (PROCESS) THEN ;
: FINIS   ( addr -- )  DROP Flag1 @ IF BREAK THEN ;
: INIT   0 IN ! ;
: Begin   INIT CR ;

: Format  0 LineCount ! BEGIN
  32 WORD DUP DUP -FINISHED WHILE  PROCESS  REPEAT FINIS ;
: L{  0 Style ! SETUP  Format ;
: J{  1 Style ! SETUP  Format ;
: I{  1 Style ! SETUP 5 DUP TI ! IN @ + Ptr !  Format ;
: P{  CR J{ ;
: Q{  1 Style ! CR SETUP  -5 MW +! 5 IN ! 5 Ptr !
      Format 0 IN ! 5 MW +! ;
: C{  2 Style ! SETUP  Format ;


( Freestore manager )
FORTH-83   DECIMAL
VARIABLE StoreBase
VARIABLE StoreLength

: NEWSTORAGE   ( n -- )   HERE  StoreBase ! HERE SWAP
  DUP  DUP ALIGN  StoreLength  ! 4 + ALLOT 1 OR SWAP  !
  0 StoreLength @ StoreBase @ + ! ;
: SKIPUSED   ( addr -- addr')  BEGIN DUP @ 1 AND 0= WHILE
  DUP @ ?DUP IF + ELSE DROP 0 EXIT THEN REPEAT ;
: COALESCE   ( addr -- addr')  BEGIN DUP @ 1 AND WHILE
  DUP @ 1 BIC + REPEAT ;
: FOUND   ( n addr1 addr2 -- n addr2 0 | n addr2 addr1 1 )
  SWAP 2DUP - 3 PICK < IF DROP 0 ELSE 1 THEN ;
: PLACE   ( n addr1 addr2 -- addr2 )   DUP >R - OVER R@ !
  OVER - ?DUP IF 1 OR SWAP R@ + ! ELSE DROP THEN R> ;
: NEW   ( n -- addr )   ALIGN
  StoreBase @ BEGIN  SKIPUSED ?DUP 0=  IF DROP 0 EXIT THEN
    DUP COALESCE FOUND UNTIL PLACE ;
: DISPOSE   ( addr -- )   ALIGN  1  SWAP  OR! ;
