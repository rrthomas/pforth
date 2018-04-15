\ Code INCLUDEd into START
] ( a-addr )
DUP TO R0  DUP RP!                \ set R0 and RP
RETURN-STACK-CELLS CELLS -        \ make room for return stack
DUP TO S0  SP!                    \ set S0 and SP
[
