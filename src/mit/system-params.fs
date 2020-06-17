\ (c) Reuben Thomas 2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ FIXME: Put in better order; must be defined after (CREATE) because of use of VALUE
1024 1024 * VALUE MEMORY-SIZE \ FIXME: command-line parameter

\ FIXME: Make these optional in pForth (highlevel.fs does not need them)
4096 CONSTANT STACK-CELLS
4096 CONSTANT RETURN-STACK-CELLS

0 VALUE S0
0 VALUE R0