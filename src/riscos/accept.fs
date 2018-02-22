\ FIXME: The line terminator is appended to the string, so that +n1-1 is the
\ maximum possible length of the string.
: ACCEPT ( c-addr +n1 -- +n2 ) >R >R 255 32 R> R> SWAP
   [ 4 2 ] OS" OS_ReadLine" DROP ;