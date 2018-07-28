\ Forward branches

INCLUDE" resolver-branch.fs"


\ RESOLVERs allow forward branches. Define a resolver, use it in definitions
\ and then use RESOLVES to resolve it. RESOLVERs may be POSTPONEd but the
\ code to POSTPONE the RESOLVER will not itself be resolved.
: RESOLVER   ( name )
   CREATE IMMEDIATE COMPILING        \ create RESOLVER
   0 ,                               \ end-of-list marker (left by caller)
   DOES>
      HERE                           \ address of branch to resolve
      OVER @ ,                       \ compile previous branch address
      SWAP ! ;                       \ record next branch in list

\ RESOLVES is used to resolve WILL-DO defining words (see WILL-DO).
: RESOLVES   ( name )   ( a-addr -- )
   '
   >BODY @                           \ get first address in branch list
   BEGIN  ?DUP WHILE                 \ chain down list until null marker
      DUP @                          \ get next address in list
      -ROT 2DUP SWAP RESOLVER-BRANCH \ compile the call or branch
      SWAP
   REPEAT
   DROP ;                            \ drop to