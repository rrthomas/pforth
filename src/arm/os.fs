\ OS access words
\ Reuben Thomas   29/4/96-18/3/99

\ FIXME: Define OS# here so it can take a counted string
: OS"   ( name )   ( regs-in regs-out -- )   [CHAR] " PARSE  C0END OS#
   POSTPONE OS ; IMMEDIATE COMPILING
: *(   CR  [CHAR] ) PARSE  C0END CLI ;
: *"   CR  POSTPONE S"  POSTPONE C0END  POSTPONE CLI ; IMMEDIATE COMPILING
: TIME   ( -- u )   [ 0 1 66 ] OS ;    \ return value of monotonic timer