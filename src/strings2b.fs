\ (c) Reuben Thomas 1991-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: CLITERAL   POSTPONE (C")  ", 0 CALIGN ; IMMEDIATE COMPILING
: SLITERAL   POSTPONE (S")  ", 0 CALIGN ; IMMEDIATE COMPILING
