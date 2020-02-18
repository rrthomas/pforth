\ (c) Reuben Thomas 2018
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ FIXME: The line terminator is appended to the string, so that +n1-1 is the
\ maximum possible length of the string.
: ACCEPT ( c-addr +n1 -- +n2 ) >R >R 255 32 R> R> SWAP
   [ 4 2 ] OS" OS_ReadLine" DROP ;