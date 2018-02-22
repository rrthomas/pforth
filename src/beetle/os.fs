\ Command-line arguments

\ The number of command-line arguments
: TOTAL-ARGS   ( -- u )   0 LIB ;

\ The uth command-line argument
: ABSOLUTE-ARG   ( -- c-addr u )   1 LIB ;

\ I/O streams
: STDIN   2 LIB ;
: STDOUT   3 LIB ;
: STDERR   4 LIB ;