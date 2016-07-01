\ Convert SWI names to numbers
\ ad-hoc list for metacompiling
HEX
: OS#   ( c-addr n -- u )
   OVER C@  [CHAR] X = IF            \ If the string starts with X,
      1 /STRING                      \ strip the X
      20000                          \ set X bit in SWI number
   ELSE
      0                              \ otherwise, clear X bit
   THEN
   -ROT                              \ save SWI number
   "CASE
      S" OS_WriteC"               "OF   0  "ENDOF
      S" OS_NewLine"              "OF   3  "ENDOF
      S" OS_ReadC"                "OF   4  "ENDOF
      S" OS_CLI"                  "OF   5  "ENDOF
      S" OS_Byte"                 "OF   6  "ENDOF
      S" OS_File"                 "OF   8  "ENDOF
      S" OS_Args"                 "OF   9  "ENDOF
      S" OS_GBPB"                 "OF  0C  "ENDOF
      S" OS_Find"                 "OF  0D  "ENDOF
      S" OS_ReadLine"             "OF  0E  "ENDOF
      S" OS_GetEnv"               "OF  10  "ENDOF
      S" OS_Exit"                 "OF  11  "ENDOF
      S" OS_FSControl"            "OF  29  "ENDOF
      S" OS_SWINumberFromString"  "OF  39  "ENDOF
      S" OS_SynchroniseCodeAreas" "OF  6E  "ENDOF
      ." Unknown SWI " TYPE ABORT
   "ENDCASE
   OR ;                              \ or with X bit
DECIMAL