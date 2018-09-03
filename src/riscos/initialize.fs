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
' CELLS COMPILE,
SP RP TOP SUB,                       \ set SP properly
TOP SP PUSH,                         \ dummy top of stack
TOP RP MOV,
R0 SP PUSH,                          \ ( -- 'command R0 )
' INITIALIZE COMPILE,
END-CODE