\ Command-line arguments

\ The number of command-line arguments
: TOTAL-ARGS   ( -- u )   13 LIB ;

\ The uth command-line argument
: ABSOLUTE-ARG   ( -- c-addr u )   14 LIB ;
