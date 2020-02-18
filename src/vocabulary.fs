\ (c) Reuben Thomas 2016-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: VOCABULARY   WORDLIST  CREATE  ,  DOES>  #ORDER @ 0= IF  1 #ORDER +!
   THEN  @ CONTEXT ! ;