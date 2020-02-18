\ Compiler #5
\
\ (c) Reuben Thomas 1995-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: '   BL WORD FIND  0= IF  UNDEFINED  THEN ;
: [']   '  POSTPONE LITERAL ; IMMEDIATE COMPILING
