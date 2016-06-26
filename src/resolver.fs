\ Forward branches

\ RESOLVERs allow forward branches. Define a resolver, use it in definitions
\ and then use RESOLVE: or RESOLVES to resolve it. RESOLVERs may be POSTPONEd
\ but the code to POSTPONE the RESOLVER will not itself be resolved.
: (RESOLVER)   ( name )
   CREATE IMMEDIATE COMPILING        \ create RESOLVER
   ,                                 \ end-of-list marker (left by caller)
   DOES>
      HERE                           \ address of branch to resolve
      SWAP DUP @  DUP 1 AND          \ get previous branch address & type flag
      SWAP ,                         \ compile previous branch address & flag
      ROT OR SWAP ! ;                \ record next branch in list
                                     \ with call/branch flag in LSB
: RESOLVER   ( name )
   0 (RESOLVER) ;
\ A RESOLVER which compiles branches instead of calls
: BRANCH-RESOLVER   ( name )
   1 (RESOLVER) ;

\ RESOLVE resolves all occurrences of the RESOLVER whose execution token is
\ from with calls to to. RESOLVE must not be used directly, but via RESOLVE:
\ or RESOLVES.
: RESOLVE   ( from to -- )
   SWAP >BODY @                      \ get first address in branch list
   DUP 1 AND >R                      \ get and save call/branch flag
   BEGIN  1 INVERT AND ?DUP WHILE    \ chain down list until null marker,
                                     \ clearing call/branch flag
      DUP @                          \ get next address in list
      -ROT  2DUP SWAP  R@ IF         \ compile the call or branch
         BRANCH
      ELSE
         CALL
      THEN
      SWAP
   REPEAT
   R> 2DROP ;                        \ drop to and flag

\ RESOLVES is used to resolve WILL-DO defining words (see WILL-DO).
: RESOLVES   ( name )   ( a-addr -- )   '  SWAP RESOLVE ;

\ RESOLVE: is used to supply the definition of a RESOLVER; the branch list is
\ resolved to calls to the new definition.
: RESOLVE:   ( name )
   BL WORD                           \ get name
   DUP FIND 0= IF  UNDEFINED  THEN   \ get RESOLVER's execution token
   TRUE OVER SMUDGE!                 \ remove RESOLVER from search order
   SWAP HEADER  TRUE SMUDGE          \ start creating new definition
   HERE RESOLVE                      \ resolve calls to new definition
   LINK,  ] ;                        \ add link code and start compiling