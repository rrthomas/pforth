\ Compiler #4

: :   BL WORD HEADER  TRUE SMUDGE  LINK,  ] ;
: CURRENT?   ( wid xt n -- f )   2DROP  GET-CURRENT = ;
: PROVIDED?  ['] CURRENT? SELECT NIP ;
: [PROVIDED]   BL WORD PROVIDED? ; IMMEDIATE
: PROVIDE:
   BL WORD  DUP PROVIDED? IF
      DROP
      POSTPONE [ELSE]
   ELSE \ FIXME: factor this out here and in :
      HEADER
      TRUE SMUDGE
      LINK, ]
   THEN ;
: ;   UNLINK,  POSTPONE [  FALSE SMUDGE ; IMMEDIATE COMPILING
: :NONAME   ALIGN  0 ,  HERE LINK,  ] ;
\ FIXME: This is the only use for a separate compilation method
: ;IMMEDIATE   POSTPONE ;  IMMEDIATE  LAST >COMPILE ! ; IMMEDIATE COMPILING
