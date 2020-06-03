\ (c) Reuben Thomas 2019-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Relocate literal addresses relative to 'FORTH using the given relocation
\ table.
\ N.B. RELOCATE and its callees must not use addresses other than those
\ passed to it as arguments (except for those being processed as part of
\ relocations, of course). In particular, this is why current-base is passed
\ as an argument rather than executing 'FORTH.
\ FIXME: Make relocation entries use relative addresses, and do not update
\ the relocations (only the base address).
: RELOCATE ( current-base new-base 'table -- )
   2DUP 2>R                             \ save new-base & 'table
   DUP @                                \ ( curr new 'table old )
   SWAP CELL+ DUP @                     \ ( curr new old 'table+CELL #entries )
   2>R TUCK  -  -ROT -                  \ ( addr-offset loc-offset )
   2R> CELLS SWAP CELL+ TUCK + SWAP ?DO \ ( addr-offset loc-offset )
      2DUP                              \ copy offsets
      I @                               \ ( …loc-offset relocation )
      SWAP +                            \ ( …addr-offset address' )
      +!                                \ perform relocation
      OVER I +!                         \ update the relocation
   CELL +LOOP  2DROP                    \ drop offsets
   2R> ! ;                              \ update table base