\ Compiler #4

: :   BL WORD HEADER  TRUE SMUDGE  LINK,  ] ;
: CURRENT?   ( wid xt n -- f )   2DROP  GET-CURRENT = ;
: PROVIDE:
   BL WORD
   DUP ['] CURRENT? SELECT NIP IF
      DROP
      POSTPONE [ELSE]
   ELSE
      HEADER
      TRUE SMUDGE
      LINK, ]
   THEN ;
: ;   UNLINK,  POSTPONE [  FALSE SMUDGE ; IMMEDIATE COMPILING
: :NONAME   ALIGN HERE LINK,  ] ;
\ FIXME: This is the only use for a separate compilation method
: ;IMMEDIATE   POSTPONE ;  IMMEDIATE  LAST >COMPILE ! ; IMMEDIATE COMPILING
