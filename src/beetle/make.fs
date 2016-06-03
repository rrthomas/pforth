\ Metacompile aForth for Beetle
\ Reuben Thomas   15/4/96-9/4/99


CR .( Metacompiling aForth for Beetle: )

INCLUDE file/fs
INCLUDE assembler/fs

\ Meta-compiler utilities

VOCABULARY META  ALSO META DEFINITIONS
DECIMAL

\ Relocate new dictionary
: >ADDRESS<   DUP @  ?DUP IF  <M0 SWAP !  ELSE DROP  THEN ;
: RELOCATE   ( adr -- )
   CURRENT-VOLUME @ @  #THREADS CELLS  OVER + SWAP DO
      I @  ?DUP IF  <M0 I !  THEN
   CELL +LOOP
   BEGIN
      >LINK
      DUP @ ?DUP WHILE
      OVER 3 CELLS +   \ hack to go from link field to code field
      >COMPILE >ADDRESS<
      OVER CELL+   \ hack to go from link field to thread field
      >ADDRESS<
      DUP <M0 ROT !
   REPEAT
   DROP ;

INCLUDE meta/fs

VOLUME NEW   \ define a new hash table
NEW VOCABULARY NEW-FORTH   \ define the new root vocabulary
NEW   \ make the new dictionary the current volume

SIZE DICTIONARY CROSS  \ define a new dictionary
M0   \ save value of M0

' CROSS >BODY @  #THREADS 1+ CELLS  -  TO M0
   \ make M0 point to the start of it minus the threads table, initial
   \ branch and Beetle's reserved 16 bytes

ALSO CROSS NEW-FORTH DEFINITIONS FOREIGN
>COMPILERS<
M0 CELL+  <M0   \ address of start of threads hash table
0   \ M0 of new system
'THROW-CONTENTS <M0   \ address of contents of 'THROW
ALSO ASSEMBLER
INCLUDE primitives/fs
PREVIOUS

INCLUDE highlevel/fs
INCLUDE initialize/fs
HERE <M0  V' ROOTDP !   \ patch DP
HEX
' NEW-FORTH >BODY @ @ <M0  ' FORTH >BODY @ ( FIXME: this calculation is >M0 ) M0 + 10 -  !
   \ patch root wordlist
' FORTH >BODY @ CELL+  ' CHAIN >BODY  !   \ patch CHAIN
' FORTH >NAME 8 -  14 ( FIXME: calculate? ) OVER !  4 -  0 OVER !  4 -  0 SWAP !
   \ patch FORTH wordlist
1  ' KERNEL >NAME 8 -  !   \ patch #WORDLISTS
DECIMAL
\ ' VALUE >DOES> RESOLVES (VALUE)   \ resolve run-times
' VOCABULARY >DOES> RESOLVES (VOCABULARY)
' FORTH >DOES DUP  ' VOCABULARY >DOES>  BRANCH   \ patch FORTH to call Beetle's VOCABULARY's DOES> code, not aForth's
' NEW-FORTH >BODY @ @  PREVIOUS  DUP RELOCATE   \ relocate the new dictionary
   \ leave address of START on the stack
>COMPILERS<

ALIGN HERE M0 -   \ ( length ) of binary image
ROOT HERE OVER ALLOT   \ make space for binary image ( length start )
TUCK   \ ( start length start )
M0  #THREADS 1+ CELLS   \ ( s l s M0 (#THREADS+1)CELLS )
TUCK + -ROT +   \ ( s l M0+(#T+1)CELLS H+(#T+1)CELLS )
2 PICK MOVE   \ copy dictionary ( s l )
OVER CELL+ CURRENT-VOLUME @ @  SWAP   \ ( s l 'THREADS s+CELL )
#THREADS CELLS MOVE   \ copy threads ( s l )

OVER TUCK DUP 2ROT  + M0 -  >COMPILERS< BRANCH >COMPILERS<   \ patch in branch to START

S" mImg"  SAVE   \ write object module

KERNEL PREVIOUS DEFINITIONS   \ restore original order
TO M0   \ restore M0
