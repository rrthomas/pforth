\ (c) Reuben Thomas 2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: ACCEPT   ( c-addr +n1 -- +n2 )   STDIN READ-LINE  2DROP ;