\ Writing code to memory

\ These words are not needed or used for Beetle (until such time as CPUs
\ start requiring special privileges for VM instructions?!), but are
\ provided so they can be used when cross-compiling with assemblers for
\ systems that need them when running natively.
: CODE!   ( x adr -- )   ! ;
: CODE,   ( x -- )   , ;