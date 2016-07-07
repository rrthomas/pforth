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