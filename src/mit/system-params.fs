\ FIXME: Put in better order; must be defined after (CREATE) because of use of VALUE
1024 1024 * VALUE MEMORY-SIZE \ FIXME: command-line parameter

\ FIXME: Make these optional in pForth (highlevel.fs does not need them)
4096 CONSTANT STACK-CELLS
4096 CONSTANT RETURN-STACK-CELLS

0 CONSTANT S0
0 VALUE R0