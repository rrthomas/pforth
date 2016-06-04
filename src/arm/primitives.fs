\ Kernel assembler words
\ Reuben Thomas   15/4/96-13/7/99

CR .( Assembler words )


\ Stack manipulation

CODE DUP
TOP SP PUSH,
END-SUB

CODE DROP
TOP SP POP,
END-SUB

CODE SWAP
R0 SP 0@ LDR,
TOP SP 0@ STR,
TOP R0 MOV,
END-SUB

CODE OVER
TOP SP PUSH,
TOP SP 4 #+@ LDR,
END-SUB

CODE ROT
R0 TOP MOV,
SP @! { R1 TOP } FD LDM,
SP @! { R0 R1 } FD STM,
END-SUB

CODE -ROT
SP @! { R0 R1 } FD LDM,
SP @! { R1 TOP } FD STM,
TOP R0 MOV,
END-SUB

CODE TUCK
R0 SP 0@ LDR,
TOP SP 0@ STR,
R0 SP 4 #-@! STR,
END-SUB

CODE NIP
SP SP 4 # ADD,
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

CODE ?DUP
TOP 0 # CMP,
TOP SP NE PUSH,
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


\ Comparison

CODE <
R0 SP POP,
R0 TOP CMP,
TOP 0 # LT MVN,
TOP 0 # GE MOV,
END-SUB

CODE >
R0 SP POP,
R0 TOP CMP,
TOP 0 # GT MVN,
TOP 0 # LE MOV,
END-SUB

CODE =
R0 SP POP,
R0 TOP CMP,
TOP 0 # EQ MVN,
TOP 0 # NE MOV,
END-SUB

CODE <>
R0 SP POP,
TOP TOP R0 SET EOR,
TOP 0 # NE MVN,
END-SUB

CODE 0<
TOP TOP 31 #ASR MOV,
END-SUB

CODE 0>
TOP TOP 0 # RSB,
TOP TOP 31 #ASR MOV,
END-SUB

CODE 0=
TOP TOP 1 # SET SUB,
TOP TOP TOP SBC,
END-SUB

CODE 0<>
TOP 0 # CMP,
TOP 0 # NE MVN,
END-SUB

CODE U<
R0 SP POP,
TOP R0 TOP SET SUB,
TOP TOP TOP SBC,
END-SUB

CODE U>
R0 SP POP,
TOP TOP R0 SET SUB,
TOP TOP TOP SBC,
END-SUB


\ Arithmetic and logical #1

CODE 0
TOP SP PUSH,
TOP 0 # MOV,
END-SUB

CODE 1
TOP SP PUSH,
TOP 1 # MOV,
END-SUB

CODE -1
TOP SP PUSH,
TOP 0 # MVN,
END-SUB

CODE CELL
TOP SP PUSH,
TOP CELL # MOV,
END-SUB

CODE -CELL
TOP SP PUSH,
TOP CELL 1- # MVN,
END-SUB

CODE TRUE
TOP SP PUSH,
TOP FALSE # MVN,
END-SUB

CODE FALSE
TOP SP PUSH,
TOP FALSE # MOV,
END-SUB

CODE 1+
TOP TOP 1 # ADD,
END-SUB

CODE 1-
TOP TOP 1 # SUB,
END-SUB

CODE CELL+
TOP TOP CELL # ADD,
END-SUB

CODE CELL-
TOP TOP CELL # SUB,
END-SUB

CODE 2*
TOP TOP 1 #ASL MOV,
END-SUB

CODE 2/
TOP TOP 1 #ASR MOV,
END-SUB

CODE CELLS
TOP TOP 2 #ASL MOV,
END-SUB

CODE CELL/
TOP TOP 2 #ASR MOV,
END-SUB

CODE 1LSHIFT
TOP TOP 1 #LSL MOV,
END-SUB

CODE 1RSHIFT
TOP TOP 1 #LSR MOV,
END-SUB

CODE +
R0 SP 4 #@+ LDR,
TOP TOP R0 ADD,
END-SUB

CODE -
R0 SP 4 #@+ LDR,
TOP R0 TOP SUB,
END-SUB

CODE >-<
R0 SP 4 #@+ LDR,
TOP TOP R0 SUB,
END-SUB

CODE *
R0 SP 4 #@+ LDR,
TOP R0 TOP MUL,
END-SUB


\ Compiler #1

CODE (LITERAL)
TOP SP PUSH,
TOP LR 252 24 LSHIFT # BIC,
TOP TOP 0@ LDR,
PC LR 4 # ADD,
END-CODE
COMPILING


\ Arithmetic and logical #2

CODE (U/MOD)
R1 TOP SET MOV,                  \ copy divisor
EQ IF,                           \ abort on divide by 0
   ' (LITERAL) COMPILE,  -10 ,   \ -10 THROW
   ' THROW COMPILE,
THEN,
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

CODE U/MOD
R3 LR MOV,                       \ save link
R0 SP 0@ LDR,                    \ get dividend
' (U/MOD) COMPILE,
R0 SP 0@ STR,                    \ push remainder
TOP R2 MOV,                      \ get quotient
PC R3 MOV,
END-CODE

CODE /MOD
R5 LR MOV,                       \ save link
R0 SP 0@ LDR,                    \ get dividend
R3 R0 TOP EOR,                   \ get signs of quotient
R3 R3 1 31 LSHIFT # AND,
R3 R3 TOP 1 #LSR ADD,            \ and remainder (=divisor's)
TOP 0 # CMP,                     \ ensure divisor is positive
TOP TOP 0 # MI RSB,
R0 0 # CMP,                      \ ensure dividend is positive
R0 R0 0 # MI RSB,
' (U/MOD) COMPILE,
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

CODE S/REM
R4 LR MOV,                       \ save link
R0 SP 0@ LDR,                    \ get dividend
R3 R0 TOP EOR,                   \ get signs of quotient
R3 R3 1 31 LSHIFT # AND,
R3 R3 R0 1 #LSR ADD,             \ and remainder (=dividend's)
TOP 0 # CMP,                     \ ensure divisor is positive
TOP TOP 0 # MI RSB,
R0 0 # CMP,                      \ ensure dividend is positive
R0 R0 0 # MI RSB,
' (U/MOD) COMPILE,
R3 1 31 LSHIFT # TST,            \ replace sign on quotient
R2 R2 0 # NE RSB,
R3 1 30 LSHIFT # TST,            \ replace sign on remainder
R0 R0 0 # NE RSB,
R0 SP 0@ STR,                    \ push remainder
TOP R2 MOV,                      \ get quotient
PC R4 MOV,
END-CODE

CODE /
R4 LR MOV,
' /MOD COMPILE,                  \ do the division
SP SP 4 # ADD,                   \ get rid of remainder
PC R4 MOV,
END-CODE

CODE MOD
R4 LR MOV,
' /MOD COMPILE,                  \ do the division
TOP SP POP,                      \ get remainder not quotient
PC R4 MOV,
END-CODE

CODE MAX
R0 SP POP,
R0 TOP CMP,
TOP R0 GT MOV,
END-SUB

CODE MIN
R0 SP POP,
R0 TOP CMP,
TOP R0 LT MOV,
END-SUB

CODE ABS
TOP 0 # CMP,
TOP TOP 0 # MI RSB,
END-SUB

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


\ Compiler #2

CODE THROW
PC SWAP PCR LDR,   \ 'THROW's contents put on stack by make/fs
END-CODE


\ Memory

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

CODE +!
R0 SP POP,
R1 TOP 0@ LDR,
R0 R0 R1 ADD,
R0 TOP 0@ STR,
TOP SP POP,
END-SUB


\ System

CODE BYE
R0 0 # MOV,
R1 0 # MOV,
SWI," XOS_Exit"
END-CODE


\ Control structures

CODE J
TOP SP PUSH,
TOP RP 8 #+@ LDR,
END-SUB
COMPILING

CODE EXIT
UNLINK,
END-CODE
COMPILING

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


\ Defining

CODE (CREATE)
TOP SP PUSH,
TOP LR 252 24 LSHIFT # BIC,
UNLINK,
END-CODE
COMPILING

CODE (DOES)
TOP SP PUSH,
R0 RP POP,
TOP R0 252 24 LSHIFT # BIC,
END-SUB
COMPILING


\ Stack pointers

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


\ Extras

\ Arithmetic and logic

CODE ARSHIFT
R0 SP POP,
TOP R0 TOP ASR MOV,
END-SUB

\ System

CODE OS#
R1 TOP MOV,
SWI," OS_SWINumberFromString"
TOP R0 MOV,
END-SUB
