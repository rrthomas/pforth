\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;

: EXECUTE   STATE @ IF  $0C ,  ELSE  [ $0C , ]  THEN ; IMMEDIATE
: @EXECUTE   STATE @ IF  $0C20 ,  ELSE  [ $0C20 , ]  THEN ; IMMEDIATE


\ Data structures

\ Compile code that will push the cell immediately after it
: LITERAL,   ( n -- a-addr )   $40 C,  NOPALIGN ;
: LITERAL   ( n -- )
   DUP 32 +  64 U< IF \ Use a PUSHI instruction if possible
      2 LSHIFT  $2 OR  C,  NOPALIGN \ FIXME: ALIGN shouldn't be needed
   ELSE
      LITERAL, ,
   THEN ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
: CREATE,   $04180203 , 0 , ; \ ( 1 MPUSHRELI  0 MPUSHI  MSWAP MJUMP )
: (DOES>)   LAST  DUP  R> R>ADDRESS ALIGNED @  CALL ;