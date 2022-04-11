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


\ Check stack is balanced
: ??STACK
   DEPTH INITIAL-DEPTH <> IF
      .S ." stack not balanced" CR  ABORT
   THEN ;

: LIBC-PRIMITIVE   NIP NIP  CODE  BPUSHI LIBC BRET  END-CODE  2 INLINE ;

VOCABULARY NEW-FORTH   \ define the new root vocabulary

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
.ASM[ calli START]
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

' .FORTH-LINK TO-ASMOUT
PREVIOUS

.PREVIOUS-INFO \ output info field of last word defined
-1 TO ASMOUT

( PREVIOUS) DEFINITIONS   \ restore original order
TO 'FORTH   \ restore 'FORTH
TO CURRENT-RELATIVE-LITERAL   \ restore original compiler
TO CURRENT-LITERAL
TO CURRENT-COMPILE,

??STACK   \ check stack is balanced
