\ Load and run John Hopkins test suite

\ Turn exceptions into error exit
: HALT-HANDLER   HALT ;
' HALT-HANDLER 'THROW!

\ Compatibility definition
/LINE CONSTANT /L
\ Load and run tests
FROM tests.fb  1 66 THRU