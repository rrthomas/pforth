\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ FIXME: To make this work again, RELOCATE needs to be compiled to native
\ code.
: INITIALIZE
   RELOCATE                          \ perform relocations (must be done first)
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack
   START ;

CODE PRE-INITIALIZE
\ Assume that we were called by a call instruction at 'FORTH, and
\ use our return address to calculate the new value of 'FORTH.
#TARGET-CALL-CELLS CELLS MPUSHI  MNEGATE MADD  0 MPUSHI
MDUP MPUSHREL MSTORE MPUSH           \ ret CALL-CELLS - 'FORTH !
' 'FORTH >BODY OFFSET,
' MEMORY-SIZE >BODY @ ,
MADD 0 MPUSHI MDUP MPUSHREL
' RP >BODY OFFSET,
MSTORE MPUSHREL MLOAD 0 MPUSHI       \ memory-limit RP !
' 'FORTH >BODY OFFSET,               \ 'FORTH @ DUP
MDUP MPUSHREL MPUSHREL MCALL
HERE 0 ,                             \ ( memory-limit 'FORTH 'FORTH )
' INITIALIZE OFFSET,
END-CODE
ALIGN  HERE OVER -  SWAP !
