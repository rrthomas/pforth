: IS-BL   BL = ;
: ISN'T-BL   BL <> ;

: STRING-FILTER   ( c-addr u xt -- c-addr' u' )
   -ROT
   2DUP >R >R
   0 ?DO
      2DUP  C@  SWAP EXECUTE INVERT IF  LEAVE  THEN
      +CHAR +
   LOOP
   NIP  DUP R> -  R> >-< ;

: COUNT-RISCOS   ( c-addr -- c-addr u )
   DUP
   BEGIN
      DUP C@  32 < INVERT WHILE
      CHAR+
   REPEAT
   OVER - ;

\ Argument is put on stack by PRE-INITIALIZE
: PARSE-COMMAND-LINE   ( c-addr -- )
   COUNT-RISCOS
   0 -ROT
   BEGIN  ?DUP WHILE
      ['] IS-BL STRING-FILTER
      ?DUP IF
         OVER SWAP
         \  FIXME: Deal with quoted arguments, respecting "" and |"
         ['] ISN'T-BL STRING-FILTER
         -ROT TUCK  OVER -  2SWAP  SWAP
      ELSE
         DROP
      THEN
   REPEAT
   DROP
   ALIGN  HERE ARGV !
   BEGIN  ?DUP WHILE
      2,
   REPEAT
   HERE  ARGV @  -  2 CELLS /  TO TOTAL-ARGS ;