\ FIXME: use curses instead.
: AT-XY
   27 EMIT  [CHAR] [ EMIT  SWAP 0 .R  [CHAR] ; EMIT  0 .R  [CHAR] H EMIT ;

INCLUDE" compiler-asm.fs"
