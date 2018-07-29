\ WILL-DO makes a defining word compile a RESOLVER rather than its normal
\ code. Use as RESOLVER X WILL-DO Y, where Y must be a defining word. This
\ can be used both to revector existing defining words, and to resolve the
\ run-time code of new ones. Use RESOLVES or RESOLVE: to resolve WILL-DO,
\ e.g.: ' NEW-DEFINER >DOES> RESOLVES DEFINER-RESOLVER WILL-DO must be used
\ as part of a REDEFINER.
: WILL-DO   ( -- old new )   ' >DOES> #BRANCH-CELLS 1+ CELLS -  :NONAME POSTPONE LAST
   POSTPONE >DOES POSTPONE HERE POSTPONE SWAP POSTPONE DP POSTPONE !
   LAST CURRENT-COMPILE,  POSTPONE DP POSTPONE ! POSTPONE ; ;