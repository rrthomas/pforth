\ Compiler #2

: ",   ( c-addr u -- )   DUP C,  HERE SWAP  DUP ALLOT  CMOVE ;
: SLITERAL   POSTPONE (S")  ", 0 CALIGN ; IMMEDIATE COMPILING

: C"   [CHAR] " PARSE  POSTPONE (C")  ", 0 CALIGN ; IMMEDIATE COMPILING
: S"   [CHAR] " PARSE  S"B SWAP 2DUP 2>R  CMOVE  2R> ;
   :NONAME   [CHAR] " PARSE  POSTPONE SLITERAL ;IMMEDIATE

: ."   POSTPONE S"  POSTPONE TYPE ; IMMEDIATE COMPILING

: CHAR   BL WORD  CHAR+ C@ ;
: [CHAR]   CHAR  POSTPONE LITERAL ; IMMEDIATE COMPILING
