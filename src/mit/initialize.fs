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
1 CONSTANT MIT_ERROR_INVALID_OPCODE
2 CONSTANT MIT_ERROR_STACK_OVERFLOW
3 CONSTANT MIT_ERROR_INVALID_STACK_READ
4 CONSTANT MIT_ERROR_INVALID_STACK_WRITE
5 CONSTANT MIT_ERROR_INVALID_MEMORY_READ
6 CONSTANT MIT_ERROR_INVALID_MEMORY_WRITE
7 CONSTANT MIT_ERROR_UNALIGNED_ADDRESS
8 CONSTANT MIT_ERROR_BAD_SIZE
9 CONSTANT MIT_ERROR_DIVISION_BY_ZERO
127 CONSTANT MIT_ERROR_HALT

\ Native pointers are stored on the stack least significant word deepest
: PTR@   ( a-addr -- ptr:mit_state* )
   NATIVE-POINTER-CELLS CELLS  OVER + SWAP DO  I @  CELL +LOOP ;
: PTR!   ( ptr:mit_state* a-addr -- )
   NATIVE-POINTER-CELLS 1- CELLS  OVER + DO  I !  -CELL +LOOP ;

\ mit_state * for inner state
CREATE MIT-STATE  2 ( FIXME: NATIVE-POINTER-CELLS ) CELLS ALLOT

: CLEAR-IR   0 MIT-STATE PTR@ MIT_SET_IR ;
: PUSH-INNER   ( x -- ) ( Inner: -- x )
   MIT-STATE PTR@ MIT_PUSH_STACK
   ?DUP IF  HALT  THEN ;
: PUSH-INNER-CALL ( a-addr -- )
   MIT-STATE PTR@ MIT_SET_PC
   0 PUSH-INNER ;                    \ dummy return address

: INITIALIZE
   RELOCATE                          \ perform relocations (must be done first)
   DUP TO R0                         \ set R0 (RP already set)
   RETURN-STACK-CELLS CELLS -        \ make room for return stack

   \ Set up inner Mit state
   \ MEMORY_WORDS == 0: don't allocate a memory
   0 STACK-CELLS MIT_NEW_STATE
   MIT-STATE PTR!
   \ Set inner `memory` to our `memory`
   MIT_CURRENT_STATE MIT_GET_MEMORY
   MIT-STATE PTR@ MIT_SET_MEMORY
   \ Set inner `memory_words` to our `memory_words`
   MIT_CURRENT_STATE MIT_GET_MEMORY_WORDS
   MIT-STATE PTR@ MIT_SET_MEMORY_WORDS
   \ Push TOS to inner stack
   PUSH-INNER
   ['] START PUSH-INNER-CALL

   \ Error handler loop
   BEGIN
      CLEAR-IR
      MIT-STATE PTR@ MIT_SPECIALIZER_RUN

      \ Handle error code:
      DUP MIT_ERROR_INVALID_OPCODE = IF
         DROP
         MIT-STATE PTR@ MIT_GET_IR  $FF ( FIXME: INSTRUCTION-MASK ) AND
         1 ( FIXME: JUMP ) = IF
            MIT-STATE PTR@ MIT_EXTRA_INSTRUCTION
            ?DUP IF  HALT  THEN
         ELSE
            MIT_ERROR_INVALID_OPCODE HALT
         THEN
      ELSE
         CASE
            \ FIXME: signal stack under/overflow
            MIT_ERROR_STACK_OVERFLOW OF  -1  ENDOF
            MIT_ERROR_INVALID_STACK_READ OF  -1  ENDOF
            MIT_ERROR_INVALID_STACK_WRITE OF  -1  ENDOF
            MIT_ERROR_INVALID_MEMORY_READ OF  -9  ENDOF
            MIT_ERROR_INVALID_MEMORY_WRITE OF  -9  ENDOF
            MIT_ERROR_UNALIGNED_ADDRESS OF  -23  ENDOF
            MIT_ERROR_BAD_SIZE OF  -9  ENDOF
            MIT_ERROR_DIVISION_BY_ZERO OF  -10  ENDOF
            MIT_ERROR_HALT OF  BYE  ENDOF
            \ Otherwise, return error code
            HALT
         ENDCASE
         PUSH-INNER
         ['] THROW PUSH-INNER-CALL
      THEN
   AGAIN ;

CODE PRE-INITIALIZE
\ Assume that we were called by a call instruction at 'FORTH, and
\ use our return address to calculate the new value of 'FORTH.
MLIT MNEGATE MADD MLIT_PC_REL
2 CELLS ,
' 'FORTH >BODY OFFSET,
MLIT_2 MSTORE MLIT MLIT              \ ret 2 CELLS - 'FORTH !
' MEMORY-SIZE >BODY @ ,
\ MIT_CURRENT_STATE MIT_GET_MEMORY
14 ,  $0101 ,
MLIT NOPALIGN
6 ,  $0101 ,
MADD MLIT_0 MDUP MLIT_PC_REL         \ memory-limit RP !
' RP >BODY OFFSET,
MLIT_2 MSTORE MLIT_PC_REL MLIT_2     \ FIXME: constant × 2!
' 'FORTH >BODY OFFSET,               \ 'FORTH @ DUP
MLOAD MLIT_0 MDUP MLIT_PC_REL
HERE 0 ,                             \ ( memory-limit 'FORTH 'FORTH )
MLIT_PC_REL MCALL NOPALIGN
' INITIALIZE OFFSET,
END-CODE
ALIGN  HERE OVER -  SWAP !