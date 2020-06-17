\ (c) Reuben Thomas 2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Same as (CREATE) but MUST be inlined
0 0 PRIMITIVE (DOES) \ Lie about number of arguments/results!
\ FIXME: 1 COMPILE->S, but with two items on stack on top of SP
2 MPUSHI MDUP -4 MPUSHI MADD \ FIXME: target CELL, not 4
2 MPUSHI MSWAP -4 MPUSHI MADD \ FIXME: target CELL, not 4
MSTORE
END-PRIMITIVE
COMPILING