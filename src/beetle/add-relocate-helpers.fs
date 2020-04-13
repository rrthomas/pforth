\ (c) Reuben Thomas 2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Use special relocation type 1
\ FIXME: Don't use (LITERAL)I, as that prevents the build being reproducible.
: ADDRESS-LITERAL   ALIGN HERE 1 (ADD-RELOCATION)  ( POSTPONE LITERAL ) $5C C,  NOPALIGN  , ; IMMEDIATE
