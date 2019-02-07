CR .( Required primitives )

\ Stack primitives

CODE DROP
TOP SP POP,
END-SUB

CODE PICK
TOP SP TOP 2 #ASL +@ LDR,
END-SUB

CODE ROLL
R0 SP TOP 2 #ASL ADD,
SP SP 4 # ADD,
TOP R0 0@ LDR,
BEGIN,
   R1 R0 4 #-@! LDR,
   R1 R0 4 #+@ STR,
   R0 SP CMP,
LO UNTIL,
END-SUB

CODE >R
TOP RP PUSH,
TOP SP POP,
END-SUB
COMPILING

CODE R>
TOP SP PUSH,
TOP RP POP,
END-SUB
COMPILING

CODE R@
TOP SP PUSH,
TOP RP 0@ LDR,
END-SUB
COMPILING


\ Arithmetic and logical primitives #1

CODE <
R0 SP POP,
R0 TOP CMP,
TOP 0 # LT MVN,
TOP 0 # GE MOV,
END-SUB

CODE =
R0 SP POP,
R0 TOP CMP,
TOP 0 # EQ MVN,
TOP 0 # NE MOV,
END-SUB

CODE U<
R0 SP POP,
TOP R0 TOP SET SUB,
TOP TOP TOP SBC,
END-SUB

CODE CELL
TOP SP PUSH,
TOP CELL # MOV,
END-SUB

CODE -CELL
TOP SP PUSH,
TOP CELL 1- # MVN,
END-SUB

CODE +
R0 SP 4 #@+ LDR,
TOP TOP R0 ADD,
END-SUB

CODE -
R0 SP 4 #@+ LDR,
TOP R0 TOP SUB,
END-SUB

CODE *
R0 SP 4 #@+ LDR,
TOP R0 TOP MUL,
END-SUB


\ Control primitives

CODE (CREATE)
TOP SP PUSH,
TOP LR 252 24 LSHIFT # BIC,
UNLINK,
END-CODE
COMPILING

INCLUDE" bracket-does.fs"

VARIABLE 'THROW

CODE (LITERAL)
TOP SP PUSH,
TOP LR 252 24 LSHIFT # BIC,
TOP TOP 0@ LDR,
PC LR 4 # ADD,
END-CODE
COMPILING

CODE THROW
PC ' 'THROW >BODY PCR LDR,
END-CODE

CODE 'THROW!
TOP ' 'THROW >BODY PCR STR,
TOP SP POP,
END-SUB

CODE EXECUTE
R0 TOP MOV,
TOP SP POP,
PC R0 MOV,
END-CODE

CODE @EXECUTE
R0 TOP MOV,
TOP SP POP,
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

CODE (U/MOD)
R3 LR MOV,                       \ save link
R0 SP 0@ LDR,                    \ get dividend
' ((U/MOD)) COMPILE,
R0 SP 0@ STR,                    \ push remainder
TOP R2 MOV,                      \ get quotient
PC R3 MOV,
END-CODE

CODE (/MOD)
R5 LR MOV,                       \ save link
R0 SP 0@ LDR,                    \ get dividend
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
R0 SP 0@ STR,                    \ push remainder
TOP R2 MOV,                      \ get quotient
PC R5 MOV,
END-CODE

CODE (S/REM)
R4 LR MOV,                       \ save link
R0 SP 0@ LDR,                    \ get dividend
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
R0 SP 0@ STR,                    \ push remainder
TOP R2 MOV,                      \ get quotient
PC R4 MOV,
END-CODE

CODE NEGATE
TOP TOP 0 # RSB,
END-SUB

CODE AND
R0 SP POP,
TOP TOP R0 AND,
END-SUB

CODE OR
R0 SP POP,
TOP TOP R0 ORR,
END-SUB

CODE XOR
R0 SP POP,
TOP TOP R0 EOR,
END-SUB

CODE INVERT
TOP TOP MVN,
END-SUB

CODE LSHIFT
R0 SP POP,
TOP R0 TOP LSL MOV,
END-SUB

CODE RSHIFT
R0 SP POP,
TOP R0 TOP LSR MOV,
END-SUB


\ Memory primitives

CODE @
TOP TOP 0@ LDR,
END-SUB

CODE !
R0 SP POP,
R0 TOP 0@ STR,
TOP SP POP,
END-SUB

CODE C@
TOP TOP 0@ BYTE LDR,
END-SUB

CODE C!
R0 SP POP,
R0 TOP 0@ BYTE STR,
TOP SP POP,
END-SUB


\ System primitives

CODE HALT
R0 0 # MOV,
R1 PC 4 #+@ LDR,
R2 1 # MOV, \ FIXME: Take error code on stack, truncate to range 0..Sys$RCLimit
SWI," XOS_Exit"
$58454241 , \ "ABEX"
END-CODE


\ Control primitives

CODE (LOOP)
RP { R0 R1 } FD LDM,             \ get the index and limit
R0 R0 1 # ADD,                   \ increment the index
TOP SP PUSH,                     \ save the top of stack
R0 R1 CMP,                       \ set a flag on top of stack
TOP 0 # EQ MVN,                  \ to TRUE if index=limit
TOP 0 # NE MOV,                  \ or FALSE otherwise
R0 RP 0@ STR,                    \ save new index
END-SUB
COMPILING

CODE (+LOOP)
RP { R0 R1 } FD LDM,             \ get the index and limit
R2 R0 R1 SUB,                    \ index-limit
R0 R0 TOP ADD,                   \ alter the index
R0 RP 0@ STR,                    \ save new index
R1 R0 R1 SUB,                    \ new index-limit
R1 R1 R2 EOR,                    \ find whether signs the same
TOP R1 31 #ASR MOV,              \ set flag TRUE if signs
END-SUB                          \ different or FALSE if not
COMPILING


\ Stack management primitives

0 VALUE S0
4096 CONSTANT STACK-CELLS
0 VALUE R0
4096 CONSTANT RETURN-STACK-CELLS

CODE SP@
TOP SP PUSH,
TOP SP 4 # ADD,
END-SUB

CODE SP!
SP TOP MOV,
END-SUB

CODE RP@
TOP SP PUSH,
TOP RP MOV,
END-SUB

CODE RP!
RP TOP MOV,
TOP SP POP,
END-SUB
