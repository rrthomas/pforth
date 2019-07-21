\ FIXME: use JOIN instead
: RESOLVER-BRANCH   ( at from to -- )
   >-< SWAP
   $030B OVER CELL- !
   ! ;