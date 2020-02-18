\ (c) Reuben Thomas 1995-2018
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Convert SWI names to numbers
\ ad-hoc list for metacompiling
: OS#   ( c-addr n -- u )
   OVER C@  [CHAR] X = IF            \ If the string starts with X,
      1 /STRING                      \ strip the X
      $20000                         \ set X bit in SWI number
   ELSE
      0                              \ otherwise, clear X bit
   THEN
   -ROT                              \ save SWI number
   "CASE
      S" OS_WriteC"               "OF   $0  "ENDOF
      S" OS_NewLine"              "OF   $3  "ENDOF
      S" OS_ReadC"                "OF   $4  "ENDOF
      S" OS_CLI"                  "OF   $5  "ENDOF
      S" OS_Byte"                 "OF   $6  "ENDOF
      S" OS_File"                 "OF   $8  "ENDOF
      S" OS_Args"                 "OF   $9  "ENDOF
      S" OS_GBPB"                 "OF   $C  "ENDOF
      S" OS_Find"                 "OF   $D  "ENDOF
      S" OS_ReadLine"             "OF   $E  "ENDOF
      S" OS_GetEnv"               "OF  $10  "ENDOF
      S" OS_Exit"                 "OF  $11  "ENDOF
      S" OS_FSControl"            "OF  $29  "ENDOF
      S" OS_SWINumberFromString"  "OF  $39  "ENDOF
      S" OS_SynchroniseCodeAreas" "OF  $6E  "ENDOF
      ." Unknown SWI " TYPE ABORT
   "ENDCASE
   OR ;                              \ or with X bit

: OS   ( regs-in regs-out swi -- )
   -ROT 2DUP + >R ROT
   R@ IF  $E52CB004 CODE,  THEN
   ROT ?DUP IF
      1 SWAP LSHIFT 1- $E8BC0000 OR CODE,
   THEN
   $EF000000 OR CODE,
   ?DUP IF
      1 SWAP LSHIFT 1- $E92C0000 OR CODE,
   THEN
   R> IF  $E49CB004 CODE,  THEN ;
IMMEDIATE COMPILING
: OS"   ( name )   ( regs-in regs-out -- )   [CHAR] " PARSE OS#
   POSTPONE OS ; IMMEDIATE COMPILING