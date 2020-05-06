\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ FIXME: compile with mit backend on mit-threaded so it can work there (the
\ error handler does not work as threaded code).

\ Mit native code access
\ FIXME: The following should really be in primitives.fs, but can't have constants there
0 CONSTANT MIT_ERROR_OK
-1 CONSTANT MIT_ERROR_INVALID_OPCODE
-2 CONSTANT MIT_ERROR_STACK_OVERFLOW
-3 CONSTANT MIT_ERROR_INVALID_STACK_READ
-4 CONSTANT MIT_ERROR_INVALID_STACK_WRITE
-5 CONSTANT MIT_ERROR_INVALID_MEMORY_READ
-6 CONSTANT MIT_ERROR_INVALID_MEMORY_WRITE
-7 CONSTANT MIT_ERROR_UNALIGNED_ADDRESS
-8 CONSTANT MIT_ERROR_DIVISION_BY_ZERO
-127 CONSTANT MIT_ERROR_INSTRUCTION_COMPLETED

\ mit_state * for inner state
CREATE MIT-STATE  CELL ALLOT

: CLEAR-IR   0 MIT-STATE @ [ MSET_IR ] ;
: PUSH-INNER   ( x -- ) ( Inner: -- x )
   MIT-STATE @ [ MPUSH_STACK ]
   ?DUP IF  HALT  THEN ;
: PUSH-INNER-CALL ( a-addr -- )
   MIT-STATE @ [ MSET_PC ]
   0 PUSH-INNER ;                    \ dummy return address

: INITIALIZE
   RELOCATE                          \ perform relocations (must be done first)
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack

   \ Set up inner Mit state
   [ MSIZEOF_STATE ] -  DUP MIT-STATE !
   STACK-CELLS CELLS -  DUP MIT-STATE @ [ MSET_STACK ]
   STACK-CELLS  MIT-STATE @ [ MSET_STACK_WORDS ]
   \ Push TOS to inner stack
   PUSH-INNER
   ['] START PUSH-INNER-CALL

   \ Error handler loop
   BEGIN
      CLEAR-IR
      MIT-STATE @ [ MRUN ]

      \ Handle error code
      CASE
         \ FIXME: signal stack under/overflow
         MIT_ERROR_STACK_OVERFLOW OF  -1  ENDOF
         MIT_ERROR_INVALID_STACK_READ OF  -1  ENDOF
         MIT_ERROR_INVALID_STACK_WRITE OF  -1  ENDOF
         MIT_ERROR_INVALID_MEMORY_READ OF  -9  ENDOF
         MIT_ERROR_INVALID_MEMORY_WRITE OF  -9  ENDOF
         MIT_ERROR_UNALIGNED_ADDRESS OF  -23  ENDOF
         MIT_ERROR_DIVISION_BY_ZERO OF  -10  ENDOF
         MIT_ERROR_OK OF  BYE  ENDOF
         \ Otherwise, return error code
         HALT
      ENDCASE
      PUSH-INNER
      ['] THROW PUSH-INNER-CALL
   AGAIN ;

CODE PRE-INITIALIZE
\ Assume that we were called by a call instruction at 'FORTH, and
\ use our return address to calculate the new value of 'FORTH.
MPUSH MNEGATE MADD 0 MPUSHI
#TARGET-CALL-CELLS CELLS ,
MDUP MPUSHREL MSTORE MPUSH           \ ret 2 CALL-CELLS - 'FORTH !
' 'FORTH >BODY OFFSET,
' MEMORY-SIZE >BODY @ ,
MADD 0 MPUSHI MDUP MPUSHREL
' RP >BODY OFFSET,
MSTORE MPUSHREL MLOAD 0 MPUSHI       \ memory-limit RP !
' 'FORTH >BODY OFFSET,               \ 'FORTH @ DUP
MDUP MPUSHREL MPUSHREL MCALL
HERE 0 ,                             \ ( memory-limit 'FORTH 'FORTH )
' INITIALIZE OFFSET,
END-CODE
ALIGN  HERE OVER -  SWAP !