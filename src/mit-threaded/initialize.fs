\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: INITIALIZE
   RELOCATE                          \ perform relocations (must be done first)
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack
   START ;

CODE PRE-INITIALIZE
\ Assume that we were called by a call instruction at 'FORTH, and
\ use our return address to calculate the new value of 'FORTH.
MLIT MNEGATE MADD MLIT_PC_REL
2 CELLS ,
' 'FORTH >BODY OFFSET,
MLIT_2 MSTORE MLIT MLIT_0            \ ret 2 CELLS - 'FORTH !
' MEMORY-SIZE >BODY @ ,
MDUP MLIT_PC_REL MLIT_2 MSTORE       \ memory-limit RP !
' RP >BODY OFFSET,
MLIT_PC_REL MLIT_2 MLOAD MLIT_0      \ FIXME: constant × 2!
' 'FORTH >BODY OFFSET,               \ 'FORTH @ DUP
MDUP MLIT_PC_REL MLIT_PC_REL MCALL
HERE 0 ,
' INITIALIZE OFFSET,                 \ ( memory-limit 'FORTH 'FORTH )
END-CODE
ALIGN  HERE OVER -  SWAP !