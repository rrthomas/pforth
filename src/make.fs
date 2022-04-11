\ Metacompile pForth base image
\
\ (c) Reuben Thomas 1996-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Halt immediately on exception, for easier debugging
\ FIXME: Only halt on memory exceptions (or use core dump facility)
\ : HALT-HANDLER   HALT ;
\ ' HALT-HANDLER 'THROW!

DEPTH VALUE INITIAL-DEPTH   \ Note initial stack depth
INCLUDE" platform.fs"
CR .( Metacompiling pForth for ) "PLATFORM TYPE .( : )


INCLUDE" assembler.fs"


\ Meta-compiler utilities

\ ALSO ASSEMBLER
92 1024 * CONSTANT DICTIONARY-SIZE

: .[   [CHAR] ] PARSE TYPE ; IMMEDIATE
: .ASM[   ['] .[ TO-ASMOUT  ['] CR TO-ASMOUT ;

: .CALL   ." calli " .SYMBOL CR ;
: ASM-COMPILE,   DUP >INFO 2 + C@  ?DUP IF
      0 DO  DUP  DUP @ RAW,  DUP @ ['] DISASSEMBLE TO-ASMOUT  CELL+  LOOP  DROP
   ELSE
      DUP  ['] .CALL TO-ASMOUT
      CALL,
   THEN ;

: .FORTH-ADDRESS   ." .word last_word - ." CR ;
: .FORTH-LINK   ." .set last_word, " LAST >NAME .NAME CR ;


VOCABULARY META  ALSO META DEFINITIONS
FOREIGN  ' NON-META?  ' 'SELECTOR >BODY REL! \ build meta-compiler using native compiler
DECIMAL

\ Check stack is balanced
: ??STACK
   DEPTH INITIAL-DEPTH <> IF
      .S ." stack not balanced" CR  ABORT
   THEN ;


INCLUDE" compiler-defer.fs"
INCLUDE" compiler-asm.fs"
INCLUDE" compiler.fs"
INCLUDE" compiler1.fs"


\ Special definition of POSTPONE, to cope with FOREIGN vocabularies

: ?FIND   ( c-addr -- xt )   FIND  0= IF  UNDEFINED  THEN ;
: (POSTPONE)   >NAME  ?FIND CURRENT-COMPILE, ;
: (RAW-POSTPONE)   >NAME  ?FIND CALL, ;

\ POSTPONE itself must be defined in FORTH, so that it can be run during the
\ compilation of the rest of META, which is FOREIGN while it is being built.
ALSO FORTH DEFINITIONS

: RAW-POSTPONE
   BL WORD  DUP FIND
   ?DUP 0= IF  UNDEFINED  THEN
   0> IF
      ABORT \ We never RAW-POSTPONE IMMEDIATE words
   ELSE
      PUSHREL,  .PUSHRELI-SYMBOL  C" (RAW-POSTPONE)" ?FIND CURRENT-COMPILE,
   THEN ;
IMMEDIATE COMPILING
: POSTPONE
   BL WORD  DUP FIND
   ?DUP 0= IF  UNDEFINED  THEN
   0> IF
      >COMPILE REL@  CALL,  .CALL-COMPILE-METHOD
   ELSE
      PUSHREL,  .PUSHRELI-SYMBOL  C" (POSTPONE)" ?FIND CURRENT-COMPILE,
   THEN ;
IMMEDIATE COMPILING

META DEFINITIONS  PREVIOUS  \ use META POSTPONE and LINK,
INCLUDE" compiler-postpone.fs"
ALSO META  FOREIGN  PREVIOUS


INCLUDE" code.fs"
INCLUDE" util.fs"
INCLUDE" control2.fs"
INCLUDE" control3.fs"
INCLUDE" strings2b.fs"
INCLUDE" compiler2.fs"
INCLUDE" interpreter3.fs"
: SET-IMMEDIATE   LAST >INFO  DUP @  TOP-BIT-SET OR  SWAP ! ;
INCLUDE" compiler4.fs"
INCLUDE" compiler5.fs"
INCLUDE" defer-fetch-store.fs"
INCLUDE" defining.fs"
INCLUDE" vocabulary.fs"


\ Constants

NATIVE  ' LOCAL?  ' 'SELECTOR >BODY REL! \ now meta-compiler is built, allow it to run

ALSO FORTH   \ use FORTH's VOCABULARY
VOCABULARY NEW-FORTH   \ define the new root vocabulary
PREVIOUS

DICTIONARY-SIZE DICTIONARY CROSS  \ define a new dictionary
' CURRENT-COMPILE, >BODY @   \ save compiler
' CURRENT-LITERAL >BODY @
' CURRENT-RELATIVE-LITERAL >BODY @
' ASM-COMPILE, ' CURRENT-COMPILE, >BODY REL!   \ use target compiler
' LITERAL ' CURRENT-LITERAL >BODY REL!
' RELATIVE-LITERAL ' CURRENT-RELATIVE-LITERAL >BODY REL!
'FORTH   \ save value of 'FORTH
' CROSS >BODY @  CELL-  TO 'FORTH
   \ make 'FORTH point to the start of it minus the initial branch

ALSO CROSS NEW-FORTH DEFINITIONS FOREIGN
STDERR-FILENO TO ASMOUT
.ASM[ calli INITIALIZE]
.ASM[ .set _byte_bits, 8]
.ASM[ .set _immediate_bit, 1 << (bee_word_bits - 1)]
.ASM[ .set _compiling_bit, 1 << (bee_word_bits - 2)]
.ASM[ .set _smudge_bit, 1 << (bee_word_bits - 3)]
.ASM[ .set _name_length_bits, bee_word_bits - _byte_bits]
INCLUDE" primitives.fs"
INCLUDE" system-params.fs"
[UNDEFINED] MINIMAL-PRIMITIVES [IF]
   INCLUDE" extra-primitives.fs"
[THEN]

INCLUDE" highlevel.fs"
INCLUDE" initialize.fs"

' .FORTH-LINK TO-ASMOUT
PREVIOUS

.PREVIOUS-INFO \ output info field of last word defined
-1 TO ASMOUT

( PREVIOUS) PREVIOUS DEFINITIONS   \ restore original order
TO 'FORTH   \ restore 'FORTH
TO CURRENT-RELATIVE-LITERAL   \ restore original compiler
TO CURRENT-LITERAL
TO CURRENT-COMPILE,

ALSO META
??STACK   \ check stack is balanced
PREVIOUS
