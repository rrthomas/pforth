\ Metacompile pForth base image
\
\ (c) Reuben Thomas 1996-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

MARKER DISPOSE

\ Halt immediately on exception, for easier debugging
\ FIXME: Only halt on memory exceptions (or use core dump facility)
\ : HALT-HANDLER   HALT ;
\ ' HALT-HANDLER 'THROW!

DEPTH VALUE INITIAL-DEPTH   \ Note initial stack depth
INCLUDE" platform.fs"
CR .( Metacompiling pForth for ) "PLATFORM TYPE .( : )


INCLUDE" assembler.fs"
INCLUDE" save.fs"


\ Meta-compiler utilities

\ ALSO ASSEMBLER
64 1024 * CONSTANT DICTIONARY-SIZE

: .ASM(   ['] .( TO-ASMOUT  ['] CR TO-ASMOUT ;

: .CALL   ." calli " .SYMBOL CR ;
: ASM-COMPILE,   DUP >INFO 2 + C@  ?DUP IF
      0 DO  DUP  DUP @ RAW,  DUP @ ['] DISASSEMBLE TO-ASMOUT  CELL+  LOOP  DROP
   ELSE
      DUP  ['] .CALL TO-ASMOUT
      CALL,
   THEN ;

: .FORTH-ADDRESS   ." .int last_word - ." CR ;
: .FORTH-LINK   ." .set last_word, " LAST >NAME .NAME CR ;

\ STUB FOO creates an empty word.
\ This is used to POSTPONE target words that may not exist on the host.
: STUB   BL WORD  HEADER ;

\ Create stubs for words that may not exist on host
STUB IP
STUB DOCOL
STUB LINK
STUB UNLINK
STUB (LITERAL)
STUB (BRANCH)
STUB (?BRANCH)
STUB (LOOP)
STUB (+LOOP)
STUB UNLOOP
STUB (CREATE)
STUB (C")
STUB (S")


\ Machinery for compiling forward references to defining words' DOES> code

: ADD-RESOLVE   DUP @  LAST CELL+  TUCK  !  SWAP ! ;
: (DOES>)   DUP >NAME CREATED !  >DOES> ADD-RESOLVE ;
: DOES-LINK,   0 , ;
: .DOES-LABEL   .NAME ." _does:" CR ;
INCLUDE" does.fs"


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
INCLUDE" relocate-compiler.fs"
INCLUDE" native-call.fs"
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
      POSTPONE RAW-RELATIVE-LITERAL  .PUSHRELI-SYMBOL  C" (RAW-POSTPONE)" ?FIND CURRENT-COMPILE,
   THEN ;
IMMEDIATE COMPILING
: POSTPONE
   BL WORD  DUP FIND
   ?DUP 0= IF  UNDEFINED  THEN
   0> IF
      >COMPILE REL@  CALL,  .CALL-COMPILE-METHOD
   ELSE
      POSTPONE RAW-RELATIVE-LITERAL  .PUSHRELI-SYMBOL  C" (POSTPONE)" ?FIND CURRENT-COMPILE,
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
: SET-IMMEDIATE   LAST >INFO  DUP @  $80000000 OR  SWAP ! ;
INCLUDE" compiler4.fs"
INCLUDE" compiler5.fs"
INCLUDE" defer-fetch-store.fs"
INCLUDE" defining.fs"
INCLUDE" vocabulary.fs"
INCLUDE" resolver-branch.fs"

: RESOLVES   ( name )   ( a-addr -- )
   '
   >DOES> @                          \ get first address in branch list
   BEGIN  ?DUP WHILE                 \ chain down list until null marker
      DUP  @                         \ get next address in list
      -ROT 2DUP SWAP RESOLVER-BRANCH \ compile the call or branch
      SWAP
   REPEAT
   DROP ;                            \ drop a-addr


\ Constants

DICTIONARY-SIZE CONSTANT SIZE
INCLUDE" call-cells.fs" CONSTANT #TARGET-CALL-CELLS

NATIVE  ' LOCAL?  ' 'SELECTOR >BODY REL! \ now meta-compiler is built, allow it to run

ALSO FORTH   \ use FORTH's VOCABULARY
VOCABULARY NEW-FORTH   \ define the new root vocabulary
PREVIOUS

SIZE DICTIONARY CROSS  \ define a new dictionary
' CURRENT-COMPILE, >BODY @   \ save compiler
' CURRENT-LITERAL >BODY @
' CURRENT-RELATIVE-LITERAL >BODY @
' ASM-COMPILE, ' CURRENT-COMPILE, >BODY REL!   \ use target compiler
' LITERAL ' CURRENT-LITERAL >BODY REL!
' RELATIVE-LITERAL ' CURRENT-RELATIVE-LITERAL >BODY REL!
'FORTH   \ save value of 'FORTH
' CROSS >BODY @  INCLUDE" init-space.fs" CELLS -  TO 'FORTH
   \ make 'FORTH point to the start of it minus the initial branch

ALSO CROSS NEW-FORTH DEFINITIONS FOREIGN
STDERR-FILENO TO ASMOUT
.ASM( calli INITIALIZE)
INCLUDE" primitives.fs"
INCLUDE" system-params.fs"
[UNDEFINED] MINIMAL-PRIMITIVES [IF]
   INCLUDE" extra-primitives.fs"
[THEN]

INCLUDE" highlevel.fs"
INCLUDE" initialize.fs"

' .FORTH-LINK TO-ASMOUT
' NEW-FORTH >BODY REL@ REL@  ' FORTH >BODY REL@ REL!   \ patch root wordlist
' FORTH >BODY REL@ CELL+  CHAIN REL!   \ patch CHAIN
' FORTH >NAME CELL-  0 OVER !  CELL-  0 SWAP !   \ zero FORTH wordlist's info and link fields
' VALUE >DOES>  ALSO META  RESOLVES VALUE  PREVIOUS \ resolve run-times
' DEFER >DOES>  ALSO META  RESOLVES DEFER  PREVIOUS
' VOCABULARY >DOES>  ALSO META  RESOLVES VOCABULARY  PREVIOUS
' ABORT ' SCAN-TEST >BODY REL!
' ABORT ' VISIBLE? >BODY REL!
' NEW-FORTH >BODY REL@ REL@  PREVIOUS   \ leave initial branch target on the stack

.PREVIOUS-INFO \ output info field of last word defined
-1 TO ASMOUT
HERE 'FORTH -   \ ( length ) of binary image
ROOT HERE OVER ALLOT   \ make space for binary image ( length start )
TUCK   \ ( start length start )
'FORTH  INCLUDE" init-space.fs" CELLS   \ ( s l s 'FORTH nCELLS )
TUCK + -ROT +   \ ( s l 'FORTH+nCELLS s+nCELLS )
2 PICK MOVE   \ copy dictionary ( s l )

OVER INCLUDE" init-space.fs" CELLS ERASE   \ zero initial branch space
OVER SWAP 2SWAP 'FORTH ROT  NATIVE-CALL   \ patch in initial branch

S" pforth-new" SAVE-OBJECT   \ write system image

( PREVIOUS) PREVIOUS DEFINITIONS   \ restore original order
TO 'FORTH   \ restore 'FORTH
TO CURRENT-RELATIVE-LITERAL   \ restore original compiler
TO CURRENT-LITERAL
TO CURRENT-COMPILE,

ALSO META
??STACK   \ check stack is balanced
PREVIOUS
