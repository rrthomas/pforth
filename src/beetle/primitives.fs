\ (c) Reuben Thomas 1995-2021
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Required primitives )

\ Stack primitives
3 PRIMITIVES DROP PICK ROLL
3 PRIMITIVES >R R> R@
2 PRIMITIVES CELL -CELL

\ Stack management primitives
7 PRIMITIVES S0! R0! SP@ SP! RP@ RP! MEMORY@
CODE S0   BS0@ BEXIT END-CODE  1 INLINE
CODE R0   BR0@ BEXIT END-CODE  1 INLINE

\ Memory primitives
4 PRIMITIVES @ ! C@ C!

\ Arithmetic and logical primitives
3 PRIMITIVES + NEGATE *
2 PRIMITIVES U/MOD S/REM
3 PRIMITIVES = < U<
4 PRIMITIVES AND OR XOR INVERT
2 PRIMITIVES LSHIFT RSHIFT

\ System primitives
2 PRIMITIVES HALT LIB
