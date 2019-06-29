: INITIALIZE
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack
   START ;

CODE PRE-INITIALIZE
MLIT MLIT_0 MDUP MLIT                \ memory-limit RP !
' MEMORY-SIZE >BODY @ ,
' RP >BODY <'FORTH ,
MLIT_2 MSTORE MLIT MCALL             \ FIXME: constant!
' INITIALIZE <'FORTH ,               \ ( memory-limit )
END-CODE
