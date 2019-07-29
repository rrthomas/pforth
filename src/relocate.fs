INCLUDE" relocate-helper.fs"

\ Relocate literal addresses relative to 'FORTH using the given relocation
\ table.
\ N.B. RELOCATE and its callees (in particular RELOCATE-HELPER) must not use
\ addresses other than those passed to it as arguments (except for those
\ being processed as part of relocations, of course). In particular, this is
\ why current-base is passed as an argument rather than executing 'FORTH.
: RELOCATE ( current-base new-base 'table -- )
   2DUP 2>R                             \ save new-base & 'table
   DUP @                                \ ( curr new 'table old )
   SWAP CELL+ DUP @                     \ ( curr new old 'table+CELL #entries )
   2>R TUCK  -  -ROT -                  \ ( addr-offset loc-offset )
   2R> CELLS SWAP CELL+ TUCK + SWAP ?DO \ ( addr-offset loc-offset )
      2DUP                              \ copy offsets
      I @                               \ ( …loc-offset relocation )
      DUP CELL 1- AND                   \ ( …loc-offset relocation type )
      SWAP CELL 1- INVERT AND           \ ( …loc-offset type address )
      ROT +                             \ ( …addr-offset type address' )
      SWAP ?DUP IF                      \ if type is non-zero, use helper
         RELOCATE-HELPER
      ELSE
         +!                             \ otherwise simply relocate
      THEN
      OVER I +!                         \ update the relocation
   CELL +LOOP  2DROP                    \ drop offsets
   2R> ! ;                              \ update table base