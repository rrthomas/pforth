\ Defer address fetch/store
\ Defined early so they can be POSTPONEd
\
\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: DEFER!   >BODY REL! ;
: DEFER@   >BODY REL@ ;
