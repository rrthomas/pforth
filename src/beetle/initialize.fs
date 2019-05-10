: INITIALIZE
   MEMORY@  RETURN-STACK-CELLS CELLS -  SP! \ set SP
   MEMORY@  RETURN-STACK-CELLS CELLS -  \ now start as if stack were empty
   DUP S0!                              \ set S0
   STACK-CELLS CELLS -  START ;