\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: INITIALIZE
   RELOCATE
   DUP TO R0                         \ set pForth-R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack
   DUP TO S0                         \ set S0 (SP already set)
   STACK-CELLS CELLS -               \ make room for data stack
   START ;

CODE PRE-INITIALIZE
R10 LR 252 24 LSHIFT # BIC,          \ save LR to calculate 'FORTH
SWI," OS_GetEnv"
RP R1 MOV,
SP RP 32 # SUB,                      \ temporary stack space
' RETURN-STACK-CELLS COMPILE,
' CELLS COMPILE,
SP RP TOP SUB,                       \ set SP properly
TOP SP PUSH,                         \ dummy top of stack
R0 SP PUSH,                          \ ( 'command pForth-R0 )
RP SP PUSH,
TOP R10 4 # SUB,
TOP SP PUSH,                         \ ( 'command pForth-R0 'FORTH 'FORTH )
TOP SP PUSH,
TOP PC 0 # ADD,                      \ FIXME: hack to push address of relocation table
' INITIALIZE COMPILE,
END-CODE
