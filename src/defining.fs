\ Defining
\
\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: CREATE   BL WORD HEADER  CREATE,  ALIGN ;
INCLUDE" does.fs"

: VARIABLE   CREATE  CELL ALLOT ;
: CONSTANT   BL WORD HEADER  LINK,  POSTPONE LITERAL  UNLINK, ;
: VALUE   CREATE  ,  DOES>  @ ;
: TO   ' >BODY ! ;
   :NONAME   ' >BODY  POSTPONE LITERAL  POSTPONE ! ;IMMEDIATE

: DEFER   CREATE  HERE  ['] ABORT >REL ,  DOES>  REL@ EXECUTE ;
: ACTION-OF   ' DEFER@ ;
   :NONAME   POSTPONE [']  POSTPONE DEFER@ ;IMMEDIATE
: IS   ' DEFER! ;
   :NONAME   POSTPONE [']  POSTPONE DEFER! ;IMMEDIATE
