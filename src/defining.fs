\ Defining

: CREATE   BL WORD HEADER  CREATE, ;
INCLUDE" does.fs"

: VARIABLE   CREATE  CELL ALLOT ;
: CONSTANT   BL WORD HEADER  LINK,  POSTPONE LITERAL  UNLINK, ;
: VALUE   CREATE  ,  DOES>  @ ;
: TO   ' >BODY ! ;
   :NONAME   ' >BODY  <'FORTH  POSTPONE LITERAL  POSTPONE ! ;IMMEDIATE

: DEFER   CREATE  ['] ABORT ,  DOES>  @EXECUTE ;
: ACTION-OF   STATE @ IF  POSTPONE [']  POSTPONE DEFER@  ELSE  ' DEFER@  THEN ; IMMEDIATE
: IS   STATE @ IF  POSTPONE [']  POSTPONE DEFER!  ELSE  ' DEFER!  THEN ; IMMEDIATE
