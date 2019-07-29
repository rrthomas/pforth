: INITIALIZE
   \ Assume that we were called by a call instruction at 'FORTH, and
   \ use our return address to calculate the new value of 'FORTH.
   [ BR> BCELL- BDUP BDUP BEP@ HERE 0 B(LITERAL) NOPALIGN HERE ] +
   RELOCATE   \ perform relocations (must be done first)
   TO 'FORTH
   MEMORY@  RETURN-STACK-CELLS CELLS -  SP! \ set SP
   MEMORY@  RETURN-STACK-CELLS CELLS -  \ now start as if stack were empty
   DUP S0!                              \ set S0
   STACK-CELLS CELLS -  START ;
ALIGN  HERE >-<  SWAP FIT!