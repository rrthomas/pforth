\ Metacompile aForth
\ Reuben Thomas   started 15/4/96

MARKER DISPOSE
INCLUDE" platform.fs"
CR .( Metacompiling aForth for ) PLATFORM TYPE .( : )


INCLUDE" assembler.fs"


\ Meta-compiler utilities

VOCABULARY META  ALSO META DEFINITIONS
DECIMAL

INCLUDE" meta.fs"

\ Relocate new dictionary
: >ADDRESS<   DUP @  ?DUP IF  <'FORTH SWAP !  ELSE DROP  THEN ;
: RELOCATE   ( a-addr -- )
   CURRENT-VOLUME @ @  #THREADS CELLS  OVER + SWAP DO
      I @  ?DUP IF  <'FORTH I !  THEN
   CELL +LOOP
   BEGIN
      >LINK
      DUP @ ?DUP WHILE
      OVER 3 CELLS +   \ hack to go from link field to code field
      >COMPILE >ADDRESS<
      OVER CELL+   \ hack to go from link field to thread field
      >ADDRESS<
      DUP <'FORTH ROT !
   REPEAT
   DROP ;

VOLUME NEW   \ define a new hash table
NEW   \ make the new dictionary the current volume
VOCABULARY NEW-FORTH   \ define the new root vocabulary

SIZE DICTIONARY CROSS  \ define a new dictionary
'FORTH   \ save value of 'FORTH
' CROSS >BODY @  #THREADS 1+ CELLS -  TO 'FORTH
   \ make 'FORTH point to the start of it minus the threads table and
   \ initial branch

ALSO CROSS NEW-FORTH DEFINITIONS FOREIGN
>COMPILERS<
'FORTH <'FORTH   \ 'FORTH of new system
INCLUDE" primitives.fs"

INCLUDE" highlevel.fs"
INCLUDE" initialize.fs"

HERE <'FORTH  V' ROOTDP !   \ patch ROOTDP
HEX
' NEW-FORTH >BODY @ @ <'FORTH  ' FORTH >BODY @ >'FORTH  !
   \ patch root wordlist
' FORTH >BODY @ CELL+  ' CHAIN >BODY  !   \ patch CHAIN
' FORTH >NAME 8 -  'FORTH <'FORTH CELL+ OVER !  4 -  0 OVER !  4 -  0 SWAP !
   \ patch FORTH wordlist
1  ' KERNEL >NAME 8 -  !   \ patch #WORDLISTS
DECIMAL
' VALUE >DOES> RESOLVES (VALUE)   \ resolve run-times
' VECTOR >DOES> RESOLVES (VECTOR)
' VOCABULARY >DOES> RESOLVES (VOCABULARY)
' NEW-FORTH >BODY @ @  PREVIOUS  DUP RELOCATE   \ relocate the new dictionary
   \ leave initial branch target on the stack
>COMPILERS<

ALIGN HERE 'FORTH -   \ ( length ) of binary image
ROOT HERE OVER ALLOT   \ make space for binary image ( length start )
TUCK   \ ( start length start )
'FORTH  #THREADS 1+ CELLS   \ ( s l s 'FORTH (#THREADS+1)CELLS )
TUCK + -ROT +   \ ( s l 'FORTH+(#T+1)CELLS H+(#T+1)CELLS )
2 PICK MOVE   \ copy dictionary ( s l )
OVER CELL+ CURRENT-VOLUME @ @  SWAP   \ ( s l 'THREADS s+CELL )
#THREADS CELLS MOVE   \ copy threads ( s l )

OVER TUCK DUP 2ROT  + 'FORTH -  >COMPILERS< BRANCH >COMPILERS<   \ patch in initial branch

S" aForthImage" SAVE   \ write system image

KERNEL PREVIOUS DEFINITIONS   \ restore original order
TO 'FORTH   \ restore 'FORTH