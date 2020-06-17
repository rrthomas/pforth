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

: CLEAR-IR   0 MIT-STATE @ MIT_SET_IR ;
: PUSH-INNER   ( x -- ) ( Inner: -- x )
   MIT-STATE @ MIT_PUSH_STACK
   ?DUP IF  HALT  THEN ;
: PUSH-INNER-CALL ( a-addr -- )
   MIT-STATE @ MIT_SET_PC
   0 PUSH-INNER ;                    \ dummy return address

: INITIALIZE
   RELOCATE                          \ perform relocations (must be done first)
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack
   DUP TO S0                         \ set S0 (SP already set)
   STACK-CELLS CELLS -               \ make room for data stack

   \ Set up inner Mit state
   MIT_SIZEOF_STATE -  DUP MIT-STATE !
   STACK-CELLS CELLS -  DUP MIT-STATE @ MIT_SET_STACK
   STACK-CELLS  MIT-STATE @ MIT_SET_STACK_WORDS
   \ Push SP to inner stack
   SP@ PUSH-INNER
   ['] START PUSH-INNER-CALL

   \ Error handler loop
   BEGIN
      CLEAR-IR
      MIT-STATE @ MIT_RUN

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
      SP@ PUSH-INNER
      ['] THROW PUSH-INNER-CALL
   AGAIN ;

CODE PRE-INITIALIZE
\ Assume that we were called by a call instruction at 'FORTH, and
\ use our return address to calculate the new value of 'FORTH.
MPUSH MADD 0 MPUSHI MDUP
#TARGET-CALL-CELLS CELLS NEGATE ,
MPUSHREL MSTORE MPUSH MADD       \ ret CALL-CELLS CELLS - 'FORTH !
' 'FORTH >BODY OFFSET,
' MEMORY-SIZE >BODY @ ,
0 MPUSHI MDUP MPUSHREL MSTORE    \ memory-limit RP !
' RP >BODY OFFSET,
0 MPUSHI MDUP MPUSH MADD         \ SP = memory-limit - RETURN-STACK-CELLS CELLS
RETURN-STACK-CELLS CELLS NEGATE ,
0 MPUSHI MSWAP MPUSHREL MLOAD
' 'FORTH >BODY OFFSET,
0 MPUSHI MDUP MPUSHREL           \ SP memory-limit 'FORTH 'FORTH
HERE 0 ,                         \ ( SP memory-limit 'FORTH 'FORTH 'table )
4 COMPILE->S                     \ push INITIALIZE's arguments to pForth stack
MPUSHREL MCALL MEXTRA MEXTRA
' INITIALIZE OFFSET,
END-CODE
ALIGN  HERE OVER -  SWAP !
