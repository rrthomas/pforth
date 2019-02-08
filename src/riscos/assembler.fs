\ ARM assembler
\ Based on Silicon Vision's RiscForth's assembler
\ Reuben Thomas   started 15/4/96

CR .( ARM assembler )


VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS


\ Utility words

: OR!   TUCK @  OR SWAP ! ;
: LROTATE   2DUP LSHIFT  -ROT 32 >-< RSHIFT  OR ;
: RROTATE   2DUP RSHIFT  -ROT 32 >-< LSHIFT  OR ;
: BITCOUNT   ( x -- #1s )   0 SWAP  BEGIN  ?DUP WHILE  DUP 1- AND
   SWAP 1+ SWAP  REPEAT ;

: ?INVERT   ( x1 -- x2 f )   DUP BITCOUNT 16 > TUCK IF  INVERT  THEN SWAP ;
: MINIMIZE   ( x1 -- x2 u )   \ minimize unsigned value of x1 by rotation
   DUP $100 U< IF  0 EXIT  THEN      \ stop if already a usable constant
   0 SWAP  DUP                       \ rotation and minimum value found so far
   16 1 DO                           \ try each rotation
      2 LROTATE  2DUP U> IF
         NIP NIP  I SWAP  DUP
      THEN
   LOOP DROP
   SWAP ;

: CONSTB   ( const1 -- const2 op-field )
   DUP MINIMIZE                      \ pack constant
   SWAP $FF AND SWAP                 \ make byte
   2DUP  8 LSHIFT OR                 \ make op-field
   >R 2* RROTATE XOR R> ;            \ make new constant
: +CONSTB   ( const1 -- const2 op-field )
   MINIMIZE OVER                     \ pack constant
   DUP $100 AND IF                   \ according to ninth bit
      NEGATE $FF AND                 \ calculate constant byte
      DUP SWAP
      $2400000                       \ SUB ?,#?
   ELSE
      $FF AND                        \ same as above when ninth bit clear
      DUP NEGATE SWAP
      $2800000                       \ ADD ?,#?
   THEN
   SWAP 3 PICK  8 LSHIFT OR OR       \ make op-field
   >R ROT +  SWAP 2* RROTATE R> ;    \ make new constant


VARIABLE INST   \ holds the instruction being built up


\ Condition codes

: COND   CREATE , DOES> @ 28 LSHIFT INST @ $FFFFFFF AND OR  INST ! ;
: CONDS   15 0 DO   I COND   LOOP ;
CONDS  EQ NE CS CC MI PL VS VC HI LS GE LT GT LE AL
3 COND LO  2 COND HS

: REVERSE  INST @  $10000000 XOR  INST ! ;


: RESET   0 INST !  AL ;   \ (re)initialise assembler


\ Registers

: REGS   16 0 DO  I CONSTANT  LOOP ;
REGS R0 R1 R2 R3 R4 R5 R6 R7 R8 R9 R10 R11 R12 R13 R14 R15
R15 CONSTANT PC  R14 CONSTANT LR  R13 CONSTANT RP
R12 CONSTANT SP  R11 CONSTANT TOP


\ Data operations

: (OP)   INST @  OR OR  SWAP 16 LSHIFT OR  SWAP 12 LSHIFT OR  CODE,  RESET ;
: OP   CREATE , DOES> @ (OP) ;
: OPS   16 0 DO  I 21 LSHIFT OP  LOOP ;
OPS AND, EOR, SUB, RSB, ADD, ADC, SBC, RSC, xx1 xx2 xx3 xx4 ORR, xx5 BIC, xx6

: SET   $100000 INST OR! ;

: TST,   R0 -ROT SET xx1 ;      : TEQ,   R0 -ROT SET xx2 ;
: CMP,   R0 -ROT SET xx3 ;      : CMN,   R0 -ROT SET xx4 ;
: MOV,   R0 SWAP xx5 ;          : MVN,   R0 SWAP xx6 ;

: MUL,   8 LSHIFT INST @ $90 OR OR OR SWAP 16 LSHIFT OR CODE,  RESET ;
: MLA,   12 LSHIFT $200090 OR INST OR! MUL, ;


\ Shifts

: LSL   8 LSHIFT $10 OR  INST OR! ;   : ASL   LSL ;
: #LSL   7 LSHIFT        INST OR! ;   : #ASL   #LSL ;
: LSR   8 LSHIFT $30 OR  INST OR! ;
: #LSR   7 LSHIFT $20 OR INST OR! ;
: ASR   8 LSHIFT $50 OR  INST OR! ;
: #ASR   7 LSHIFT $40 OR INST OR! ;
: ROR   8 LSHIFT $70 OR  INST OR! ;
: #ROR   7 LSHIFT $60 OR INST OR! ;   : RRX   0 #ROR ;


\ Immediate constants

: IMM   INST  DUP @ $2000000 XOR  SWAP ! ;
: #   MINIMIZE  OVER $FF U> ABORT" immediate constant too large"  IMM  8 LSHIFT
   INST OR! ;


\ Branches

: >BRANCH   ( from to -- offset )   >-<  2 RSHIFT 2 -  $00FFFFFF AND ;

: (B)   HERE ROT >BRANCH  INST @  OR OR CODE,  RESET ;
: B,    $A000000 (B) ;
: BL,    $B000000 (B) ;


\ Addressing modes

: @- ; IMMEDIATE                : @+     $800000 INST OR! ;
: -@    $1000000 INST OR! ;     : +@    $1800000 INST OR! ;
: -@!   $1200000 INST OR! ;     : +@!   $1A00000 INST OR! ;

: OFFSET   DUP $FFF U> ABORT" immediate offset too large"  IMM  INST OR!  0 ;
: #@-    OFFSET @- ;              : #@+    OFFSET @+ ;
: #-@    OFFSET -@ ;              : #+@    OFFSET +@ ;
: #-@!   OFFSET -@! ;             : #+@!   OFFSET +@! ;
: 0@   0 #+@ ;


\ Loading and storing

: BYTE   $400000 INST OR! ;
: LDR,   IMM $4100000 (OP) ;
: STR,   IMM $4000000 (OP) ;

: PCR   HERE 8 + -  DUP ABS $FFF > ABORT" address out of range"
  DUP 0< IF  NEGATE #-@  ELSE #+@  THEN  PC SWAP ;

: PUSH,   4 #-@! STR, ;
: POP,   4 #@+ LDR, ;

: MODES   4 0 DO  I 23 LSHIFT  CONSTANT  LOOP ;
: STACKS   4 0 DO  I 23 LSHIFT  $8000000 OR  CONSTANT  LOOP ;
MODES  DA IA DB IB
STACKS ED EA FD FA

: @!   $200000 INST OR! ;
: ^   $400000 INST OR! ;
: (LDSTM)   INST @  OR OR OR  SWAP 16 LSHIFT OR CODE,  RESET ;
: LDM,   DUP $8000000 AND IF  $1800000 XOR  THEN  $8100000 (LDSTM) ;
: STM,   $8000000 (LDSTM) ;
: {   32 ;
: }   0  BEGIN  OVER 32 <> WHILE  1 ROT LSHIFT OR  REPEAT  NIP ;


\ SWIs

: SWI,   INST @ $F000000 OR OR CODE,  RESET ;
: X   $20000 INST OR! ;
: SWI"   [CHAR] " PARSE OS# ;
: SWI,"   SWI"  SWI, ;
   :NONAME   SWI"  POSTPONE LITERAL  POSTPONE SWI, ;IMMEDIATE


\ Control structure macros

\ FIXME: Following three words are copied from arm/compiler.fs; when
\ assembler is merged into base system, de-duplicate
: >BRANCH   ( from to -- offset )   >-<  2 RSHIFT 2 -  $00FFFFFF AND ;
: !BRANCH   ( at from to op-mask -- )   -ROT  >BRANCH  OR  SWAP CODE! ;
: JOIN   ( from to -- )   OVER TUCK @  $FF000000 AND  !BRANCH ;

: AHEAD,   HERE  DUP 8 + B, ;
: IF,   REVERSE AHEAD, ;
: THEN,   HERE JOIN ;
: ELSE,   AHEAD,  SWAP THEN, ;

: BEGIN,   HERE ;
: AGAIN,   B, ;
: UNTIL,   REVERSE B, ;
: WHILE,   IF, SWAP ;
: REPEAT,  AGAIN, THEN, ;

: RET,   PC LR SET MOV, ;


\ Large immediate constant instructions

VARIABLE 'NEXTB
: NEXTB   'NEXTB @EXECUTE ;
: ADDING   ['] +CONSTB 'NEXTB ! ;
: ORING   ['] CONSTB 'NEXTB ! ;

: (#OP,)   ( dest const op -- )
   SWAP
   BEGIN ?DUP WHILE                  \ while constant non-zero
      NEXTB                          \ get op-field
      2OVER SWAP DUP 2SWAP (OP)      \ write instruction
   REPEAT
   2DROP ;                           \ drop dest and op

( #OP, generates 1-4 instructions to op the given register and constant into
  the given destination )
: #OP,   ( dest src const op -- )
   IMM                               \ set immediate flag
   SWAP NEXTB                        \ get op-field
   -ROT >R >R                        \ save op and new const
   2 PICK -ROT  R@ (OP)              \ write first instruction
   2R> (#OP,) ;                      \ write following instructions

: #ORR,   ORING  $1800000 #OP, ;
: #EOR,   ORING  $0200000 #OP, ;

: #ADD,   ( dest src const -- )   ADDING
   0 ( +CONSTB takes care of opcode ) #OP, ;
: #SUB,   NEGATE #ADD, ;

: #AND,   ( dest src const -- )  ORING  ?INVERT IF  $1C00000  ELSE $0000000
   THEN  #OP, ;
: #BIC,   INVERT #AND, ;

( #MOV, generates 1-4 MOV/MVN and ADD/SUB instructions to move a constant
  into the given destination )
: #MOV,   ( dest const -- )
   ORING
   ?INVERT IF  $3E00000  ELSE $3A00000  THEN
                                     \ invert const and choose opcode
   SWAP NEXTB                        \ get first byte
   2SWAP SWAP DUP  DUP >R  2SWAP     ( d op c b -- c d d b op )
   (OP)                              \ write first instruction
   R> OVER IF                        \ if more constant
      DUP ROT #ADD,                  \ do rest of instructions
   ELSE
      2DROP                          \ get rid of dest and 0
   THEN ;
: #MVN,   INVERT #MOV, ;


\ Defining words for machine code words

PREVIOUS  DEFINITIONS  ALSO ASSEMBLER
: CODE   BL WORD HEADER  ALSO ASSEMBLER  RESET ;
: END-CODE   PREVIOUS ;
: END-SUB   RET,  PREVIOUS ;


\ Define a primitive which takes x arguments and returns y results.
\
\ FIXME: This should really be in util.fs, but then it doesn't work for
\ bracket-does.fs.
\
\ Usage:
\
\ x y PRIMITIVE FOO
\ ( arg2 arg1 -> TOP=arg1, R0=arg2 )
\ ( assembly code )
\ ( TOP=result1, R0=result2 -> result2 result1 )
\ END-SUB

: PRIMITIVE   ( args results -- args results )
   CODE                       \ make a code word
   SWAP                       \ ( results args )
   DUP 3 < INVERT ABORT" PRIMITIVE needs < 3 arguments"
   OVER  3 < INVERT ABORT" PRIMITIVE needs < 3 results"
   2DUP  0=  SWAP 0<>  AND IF \ push cached stack item if args=0 & results<>0
      TOP SP PUSH,
   THEN
   DUP 1 > IF                 \ pop second argument if args>1
      R0 SP POP,
   THEN ;

: END-PRIMITIVE-CODE   ( args results -- )
   OVER  2 = IF               \ push R0 if results=2
      R0 SP PUSH,
   THEN
   2DUP 0>  SWAP 0=  AND IF   \ pop TOP if args>0 & results=0
      TOP SP POP,
   THEN
   2DROP ;

: END-PRIMITIVE
   END-PRIMITIVE-CODE
   END-SUB ;


PREVIOUS
