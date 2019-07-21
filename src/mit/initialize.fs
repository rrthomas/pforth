: INITIALIZE
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack
   START ;

CODE PRE-INITIALIZE
MLIT MLIT_0 MDUP MLIT_PC_REL         \ memory-limit RP !
' MEMORY-SIZE >BODY @ ,
 ' RP >BODY OFFSET,
MLIT_2 MSTORE MLIT_PC_REL MCALL      \ FIXME: constant!
' INITIALIZE OFFSET,                 \ ( memory-limit )
END-CODE
