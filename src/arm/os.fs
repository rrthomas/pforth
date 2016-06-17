\ OS access words
\ Reuben Thomas   started 29/4/96

CODE OS#
R2 LR MOV,
' C0END COMPILE,
R1 TOP MOV,
SWI," OS_SWINumberFromString"
TOP R0 MOV,
PC R2 MOV,
END-SUB

: OS"   ( name )   ( regs-in regs-out -- )   [CHAR] " PARSE OS#
   POSTPONE OS ; IMMEDIATE COMPILING
: *(   CR  [CHAR] ) PARSE  CLI ;
: *"   CR  POSTPONE S"  POSTPONE CLI ; IMMEDIATE COMPILING
: TIME   ( -- u )   [ 0 1 66 ] OS ;    \ return value of monotonic timer