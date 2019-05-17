: INITIALIZE
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack
   START ;

CODE PRE-INITIALIZE
' MEMORY-SIZE >BODY @ LITERAL,       \ FIXME: do this portably
BLIT_0 BDUP                          \ memory-limit RP !
' RP >BODY <'FORTH LITERAL,
BLIT_2 BSTORE                        \ FIXME: constant!
' INITIALIZE COMPILE,                \ ( memory-limit )
END-CODE
