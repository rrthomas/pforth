\ Name mangling
: ISDIGIT   DUP [CHAR] 0 < INVERT  SWAP [CHAR] 9 > INVERT  AND ;
: ISUPPER   DUP [CHAR] A < INVERT  SWAP [CHAR] Z > INVERT  AND ;
: ISLOWER   DUP [CHAR] a < INVERT  SWAP [CHAR] z > INVERT  AND ;
: ISALPHA   DUP ISUPPER  SWAP ISLOWER  OR ;
: ISALNUM   DUP ISDIGIT  SWAP ISALPHA  OR ;
: 2.H   BASE @ >R  HEX  <# # # #>  R> BASE !  TYPE ;
: .MANGLE   ( c-addr u -- ) \ print a Forth name mangled
   OVER + SWAP  ?DO
      I C@  DUP ISALPHA IF              \ output letters literally (FIXME: only mangle leading digit)
         EMIT
      ELSE                              \ escape everything else
         [CHAR] _ EMIT  2.H  [CHAR] _ EMIT
      THEN
   LOOP ;
