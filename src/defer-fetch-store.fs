\ Defer address fetch/store
\ Defined early so they can be POSTPONEd

: DEFER!   >BODY ! ;
: DEFER@   >BODY @ ;
