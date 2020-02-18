\ Ackermann function
\ (c) Reuben Thomas 2020
\ This file is in the public domain.

: ACKERMANN-RECURSIVE   ( m n -- result )
   OVER 0= IF                           \ n + 1 if m = 0
      NIP 1+
   ELSE
      DUP 0= IF                         \ A(m - 1, 1) if m > 0 and n = 0
         DROP 1- 1 RECURSE
      ELSE                              \ A(m - 1, A(m, n - 1)) if m > 0 and n > 0
         OVER SWAP 1- RECURSE           \ compute a = A(m, n - 1), saving m
         SWAP 1- SWAP RECURSE           \ A(m - 1, a)
      THEN
   THEN ;

: ACKERMANN-ITERATIVE   ( m n -- result )
   BEGIN OVER 0> WHILE
      DUP 0= IF
         DROP 1
      ELSE
         OVER SWAP 1- RECURSE
      THEN
      SWAP 1- SWAP
   REPEAT
   NIP 1+ ;