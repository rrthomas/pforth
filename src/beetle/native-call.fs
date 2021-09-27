\ (c) Reuben Thomas 2019-2021
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: NATIVE-CALL   ( at from to -- )   $49 3 PICK C!  !BRANCH ;
\ FIXME: : NATIVE-CALL   CALL ;