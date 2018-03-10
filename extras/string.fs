: CHOMP   ( c-addr u1 -- c-addr u2 )
   2DUP +                            \ end of string
   EOL TUCK  2SWAP - OVER            \ calculate where EOL would start
   COMPARE 0= IF                     \ if string ends with EOL,
      EOL NIP  -                     \ reduce its length accordingly
   THEN ;
