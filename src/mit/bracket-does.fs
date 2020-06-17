\ (c) Reuben Thomas 2018-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Same as (CREATE) but MUST be inlined
\ FIXME: different definition from primitives.fs
: (DOES)   $70F2140A ,  $70F2180A ,  $24 , ( FIXME: almost >S, but with 2 items on stack ) ;
IMMEDIATE COMPILING
