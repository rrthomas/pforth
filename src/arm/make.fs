\ Metacompile aForth
\ Reuben Thomas   started 15/4/96

INCLUDE platform/fs

CR .( Metacompiling aForth for ) PLATFORM TYPE .( : )


MARKER DISPOSE
INCLUDE assembler/fs


\ Meta-compiler utilities

VOCABULARY META  ALSO META DEFINITIONS
DECIMAL

\ Relocate new dictionary
: >ADDRESS<   DUP @  ?DUP IF  <M0 SWAP !  ELSE DROP  THEN ;
: RELOCATE   ( a-addr -- )
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
' CROSS >BODY @  #THREADS 1+ CELLS -  TO M0   \ make M0 point to the start
\ of it minus the threads table and branch to startup code

ALSO CROSS NEW-FORTH DEFINITIONS FOREIGN

>COMPILERS<
4 ALLOT   \ space for 'THROW's contents
HEX
#THREADS 1+ CELLS  DUP  8000 + SWAP  M0 +   \ address of 'THROW's contents
DECIMAL
INCLUDE primitives/fs
M0 4 + <M0 SWAP   \ address of start of threads hash table
HEX 8000 DECIMAL SWAP   \ M0 of new system
INCLUDE highlevel/fs
INCLUDE initialize/fs

HERE <M0  V' ROOTDP !   \ patch ROOTDP
HEX
' NEW-FORTH >BODY @ @ <M0  ' FORTH >BODY @ M0 + 8000 -  !
   \ patch root wordlist
' FORTH >BODY @ CELL+  ' CHAIN >BODY  !   \ patch CHAIN
' FORTH >NAME 8 -  8004 OVER !  4 -  0 OVER !  4 -  0 SWAP !
   \ patch FORTH wordlist
1  ' KERNEL >NAME 8 -  !   \ patch #WORDLISTS
DECIMAL
' VALUE >DOES> RESOLVES (VALUE)   \ resolve run-times
' VOCABULARY >DOES> RESOLVES (VOCABULARY)
' NEW-FORTH >BODY @ @  PREVIOUS  DUP RELOCATE   \ relocate the new dictionary
>COMPILERS<

ALIGN HERE M0 -   \ ( length ) of binary image
ROOT HERE OVER ALLOT   \ make space for binary image ( length start )
TUCK   \ ( start length start )
M0  #THREADS 1+ CELLS   \ ( s l s M0 (#THREADS+1)CELLS )
TUCK + -ROT +   \ ( s l M0+(#T+1)CELLS H+(#T+1)CELLS )
2 PICK MOVE   \ copy dictionary ( s l )
OVER CELL+ CURRENT-VOLUME @ @  SWAP   \ ( s l 'THREADS s+CELL )
#THREADS CELLS MOVE   \ copy threads ( s l )

OVER TUCK DUP 2ROT  + M0 -  >COMPILERS< BRANCH >COMPILERS<   \ patch in initial branch

S" aForthImage" SAVE   \ write system image

KERNEL PREVIOUS DEFINITIONS   \ restore original order
TO M0   \ restore M0