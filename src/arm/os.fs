\ OS access words
\ Reuben Thomas   started 29/4/96

\ OS-specific file access

\ Set the type of the file given by c-addr u2 to u1
: TYPE-FILE   ( u1 c-addr u2 -- )   C0END  18  [ 3 0 8 ] OS ;


\ OS function access

HEX
: OS   ( #regs-in #regs-out swi -- )
   -ROT 2DUP + >R ROT
   R@ IF  E52CB004 CODE,  THEN
   ROT ?DUP IF
      1 SWAP LSHIFT 1- E8BC0000 OR CODE,
   THEN
   EF000000 OR CODE,
   ?DUP IF
      1 SWAP LSHIFT 1- E92C0000 OR CODE,
   THEN
   R> IF  E49CB004 CODE,  THEN ;
IMMEDIATE COMPILING
DECIMAL

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


\ Time

: TIME   ( -- u )   [ 0 1 66 ] OS ;    \ return value of monotonic timer


\ Command-line access

: CLI   ( c-addr -- )   C0END  [ 1 0 ] OS" OS_CLI" ;
: *(   CR  [CHAR] ) PARSE  CLI ;
: *"   CR  POSTPONE S"  POSTPONE CLI ; IMMEDIATE COMPILING