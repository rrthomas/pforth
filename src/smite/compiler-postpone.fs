\ Compiler words that need special treatment during meta-compilation owing
\ to their use of POSTPONE

\ Compiler

\ FIXME: Common up with riscos, as defaults for platforms without special versions?
: DO,   POSTPONE 2>R ; COMPILING


\ Data structures

: CREATE,   LINK,  NOPALIGN  POSTPONE (CREATE)  UNLINK,  NOPALIGN ;