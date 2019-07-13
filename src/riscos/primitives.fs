CR .( Required primitives )

\ Stack primitives

1 0 PRIMITIVE DROP
END-PRIMITIVE

1 1 PRIMITIVE PICK
TOP SP TOP 2 #ASL +@ LDR,
END-PRIMITIVE

1 1 PRIMITIVE ROLL
R0 SP TOP 2 #ASL ADD,
SP SP 4 # ADD,
TOP R0 0@ LDR,
BEGIN,
   R1 R0 4 #-@! LDR,
   R1 R0 4 #+@ STR,
   R0 SP CMP,
LO UNTIL,
END-PRIMITIVE

1 0 PRIMITIVE >R
TOP RP PUSH,
END-PRIMITIVE
COMPILING

0 1 PRIMITIVE R>
TOP RP POP,
END-PRIMITIVE
COMPILING

0 1 PRIMITIVE R@
TOP RP 0@ LDR,
END-PRIMITIVE
COMPILING


\ Arithmetic and logical primitives #1

2 1 PRIMITIVE <
R0 TOP CMP,
TOP 0 # LT MVN,
TOP 0 # GE MOV,
END-PRIMITIVE

2 1 PRIMITIVE =
R0 TOP CMP,
TOP 0 # EQ MVN,
TOP 0 # NE MOV,
END-PRIMITIVE

2 1 PRIMITIVE U<
TOP R0 TOP SET SUB,
TOP TOP TOP SBC,
END-PRIMITIVE

0 1 PRIMITIVE CELL
TOP CELL # MOV,
END-PRIMITIVE

0 1 PRIMITIVE -CELL
TOP CELL 1- # MVN,
END-PRIMITIVE

2 1 PRIMITIVE +
TOP TOP R0 ADD,
END-PRIMITIVE

2 1 PRIMITIVE *
TOP R0 TOP MUL,
END-PRIMITIVE


\ Control primitives

0 1 PRIMITIVE (CREATE)
TOP LR 252 24 LSHIFT # BIC,
END-PRIMITIVE-CODE
UNLINK,
END-CODE
COMPILING

INCLUDE" bracket-does.fs"

0 1 PRIMITIVE (LITERAL)
TOP LR 252 24 LSHIFT # BIC,
TOP TOP 0@ LDR,
END-PRIMITIVE-CODE
PC LR 4 # ADD,
END-CODE
COMPILING

1 0 PRIMITIVE EXECUTE
R0 TOP MOV,
END-PRIMITIVE-CODE
PC R0 MOV,
END-CODE

1 0 PRIMITIVE @EXECUTE
R0 TOP MOV,
END-PRIMITIVE-CODE
PC R0 0@ LDR,
END-CODE


\ Arithmetic and logical primitives #2

CODE ((U/MOD))
R1 TOP SET MOV,                  \ copy divisor
R1 R0 16 #LSR CMP,               \ shift divisor left as far as possible
R1 R1 16 #LSL LS MOV,            \ without exceeding dividend
R1 R0 8 #LSR CMP,
R1 R1 8 #LSL LS MOV,
R1 R0 4 #LSR CMP,
R1 R1 4 #LSL LS MOV,
R1 R0 2 #LSR CMP,
R1 R1 2 #LSL LS MOV,
R1 R0 1 #LSR CMP,
R1 R1 1 #LSL LS MOV,
R2 0 # MOV,                      \ quotient=0
BEGIN,
   R0 R1 CMP,                    \ if dividend>divisor
   R0 R0 R1 HS SUB,              \ then dividend-=divisor
   R2 R2 R2 ADC,                 \ shift quotient and add carry
   R1 R1 1 #LSR MOV,             \ shift divisor
   R1 TOP CMP,                   \ continue until
LO UNTIL,                        \ divisor<original
END-SUB

2 2 PRIMITIVE (U/MOD)
R3 LR MOV,                       \ save link
' ((U/MOD)) COMPILE,
TOP R2 MOV,                      \ get quotient
END-PRIMITIVE-CODE
PC R3 MOV,
END-CODE

2 2 PRIMITIVE (/MOD)
R5 LR MOV,                       \ save link
R3 R0 TOP EOR,                   \ get signs of quotient
R3 R3 1 31 LSHIFT # AND,
R3 R3 TOP 1 #LSR ADD,            \ and remainder (=divisor's)
TOP 0 # CMP,                     \ ensure divisor is positive
TOP TOP 0 # MI RSB,
R0 0 # CMP,                      \ ensure dividend is positive
R0 R0 0 # MI RSB,
' ((U/MOD)) COMPILE,
R3 1 31 LSHIFT # TST,            \ replace sign on quotient
R2 R2 0 # NE RSB,
R0 0 # NE CMP,                   \ if quotient is negative and
R2 R2 1 # NE SUB,                \ remainder non-zero, floor
R0 TOP R0 NE SUB,                \ quotient & correct remainder
R3 1 30 LSHIFT # TST,            \ replace sign on remainder
R0 R0 0 # NE RSB,
TOP R2 MOV,                      \ get quotient
END-PRIMITIVE-CODE
PC R5 MOV,
END-CODE

2 2 PRIMITIVE (S/REM)
R4 LR MOV,                       \ save link
R3 R0 TOP EOR,                   \ get signs of quotient
R3 R3 1 31 LSHIFT # AND,
R3 R3 R0 1 #LSR ADD,             \ and remainder (=dividend's)
TOP 0 # CMP,                     \ ensure divisor is positive
TOP TOP 0 # MI RSB,
R0 0 # CMP,                      \ ensure dividend is positive
R0 R0 0 # MI RSB,
' ((U/MOD)) COMPILE,
R3 1 31 LSHIFT # TST,            \ replace sign on quotient
R2 R2 0 # NE RSB,
R3 1 30 LSHIFT # TST,            \ replace sign on remainder
R0 R0 0 # NE RSB,
TOP R2 MOV,                      \ get quotient
END-PRIMITIVE-CODE
PC R4 MOV,
END-CODE

1 1 PRIMITIVE NEGATE
TOP TOP 0 # RSB,
END-PRIMITIVE

2 1 PRIMITIVE AND
TOP TOP R0 AND,
END-PRIMITIVE

2 1 PRIMITIVE OR
TOP TOP R0 ORR,
END-PRIMITIVE

2 1 PRIMITIVE XOR
TOP TOP R0 EOR,
END-PRIMITIVE

1 1 PRIMITIVE INVERT
TOP TOP MVN,
END-PRIMITIVE

2 1 PRIMITIVE LSHIFT
TOP R0 TOP LSL MOV,
END-PRIMITIVE

2 1 PRIMITIVE RSHIFT
TOP R0 TOP LSR MOV,
END-PRIMITIVE


\ Memory primitives

1 1 PRIMITIVE @
TOP TOP 0@ LDR,
END-PRIMITIVE

2 0 PRIMITIVE !
R0 TOP 0@ STR,
END-PRIMITIVE

1 1 PRIMITIVE C@
TOP TOP 0@ BYTE LDR,
END-PRIMITIVE

2 0 PRIMITIVE C!
R0 TOP 0@ BYTE STR,
END-PRIMITIVE


\ System primitives

CODE HALT
R0 0 # MOV,
R1 PC 4 #+@ LDR,
R2 1 # MOV, \ FIXME: Take error code on stack, truncate to range 0..Sys$RCLimit
SWI," XOS_Exit"
$58454241 , \ "ABEX"
END-CODE


\ Control primitives

1 1 PRIMITIVE (LOOP)
RP { R0 R1 } FD LDM,             \ get the index and limit
R0 R0 1 # ADD,                   \ increment the index
TOP SP PUSH,                     \ save the top of stack
R0 R1 CMP,                       \ set a flag on top of stack
TOP 0 # EQ MVN,                  \ to TRUE if index=limit
TOP 0 # NE MOV,                  \ or FALSE otherwise
R0 RP 0@ STR,                    \ save new index
END-PRIMITIVE
COMPILING

1 1 PRIMITIVE (+LOOP)
RP { R0 R1 } FD LDM,             \ get the index and limit
R2 R0 R1 SUB,                    \ index-limit
R0 R0 TOP ADD,                   \ alter the index
R0 RP 0@ STR,                    \ save new index
R1 R0 R1 SUB,                    \ new index-limit
R1 R1 R2 EOR,                    \ find whether signs the same
TOP R1 31 #ASR MOV,              \ set flag TRUE if signs
END-PRIMITIVE                    \ different or FALSE if not
COMPILING


\ Stack management primitives

0 1 PRIMITIVE SP@
TOP SP 4 # ADD,
END-PRIMITIVE

\ FIXME: We don't load TOP at the end, because we treat the stack as full
\ (with POP, etc.) when in fact it should be empty (so that SP is the
\ "correct" address, but points to the slot occupied by TOP). This works for
\ now because of the limited ways we use SP!.
CODE SP!
SP TOP MOV,
END-SUB

0 1 PRIMITIVE RP@
TOP RP MOV,
END-PRIMITIVE

1 0 PRIMITIVE RP!
RP TOP MOV,
END-PRIMITIVE