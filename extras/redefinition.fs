\ REDEFINERs swap the execution semantics of words redefined with R: between
\ the old and new semantics. u is the number of words to swap. The words
\ must have been defined with R: name ... ; as R; consumes information that
\ REDEFINER needs.

\ R: redefines name. old is the old xt and new the new xt.
\ To redefine a word immediately use R: name ... R;; to set up a redefinition
\ for later use with a REDEFINER use R: name ... ;.
: R:   ( name )   ( -- old new )
   '                                 \ get old xt
   :NONAME ;                         \ start the redefinition; leave new xt

\ R; makes a redefinition created with R: take effect immediately.
: R;   ( old new -- )
   OVER SWAP BRANCH                  \ compile a branch in the old word
   POSTPONE ; ;                      \ end the redefinition
IMMEDIATE COMPILING

\ DOES>: allows the run-time code of a defining word to be redefined. Use
\ like R:; name must be the name of a defining word.
: DOES>:   ( name )   ( -- old new )
   ' >DOES>                          \ get address of old DOES> code
   :NONAME ;                         \ start new definition

\ RESOLVE: is used to supply the definition of a RESOLVER; the branch list is
\ resolved to calls to the new definition.
: RESOLVE:   ( name )
   BL WORD                           \ get name
   DUP FIND 0= IF  UNDEFINED  THEN   \ get RESOLVER's execution token
   TRUE OVER SMUDGE!                 \ remove RESOLVER from search order
   SWAP HEADER  TRUE SMUDGE          \ start creating new definition
   HERE RESOLVE                      \ resolve calls to new definition
   LINK,  ] ;                        \ add link code and start compiling