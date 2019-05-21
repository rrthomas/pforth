: INITIALIZE
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack
   START ;

CODE PRE-INITIALIZE
' MEMORY-SIZE >BODY @ LITERAL,       \ FIXME: do this portably
MLIT_0 MDUP                          \ memory-limit RP !
' RP >BODY <'FORTH LITERAL,
MLIT_2 MSTORE                        \ FIXME: constant!
' INITIALIZE COMPILE,                \ ( memory-limit )
END-CODE
