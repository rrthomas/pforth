: INITIALIZE
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack
   DUP TO S0                         \ set S0 (SP already set)
   STACK-CELLS CELLS -               \ make room for data stack
   START ;

CODE PRE-INITIALIZE
SWI," OS_GetEnv"
RP R1 MOV,
SP RP 32 # SUB,                      \ temporary stack space
' STACK-CELLS COMPILE,
TOP TOP 2 #ASL MOV,
SP RP TOP SUB,
TOP SP PUSH,
TOP R1 MOV,
' INITIALIZE COMPILE,
END-CODE