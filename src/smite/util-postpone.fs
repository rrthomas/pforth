\ Create SMite extra instructions
STUB PUSH-ZEROS

: EXTRA-INSTRUCTION   ( +n -- )
   CODE                       \ make a code word
   BLITERAL                   \ compile the extra instruction code
   BPUSH_PSIZE
   POSTPONE PUSH-ZEROS        \ host address 0
   BLINK                      \ +n 0..0 LINK
   BRET                       \ append RET
   END-CODE ;                 \ finish the definition

: EXTRA-INSTRUCTIONS   ( +n1 +n2 -- )
   SWAP 1+ SWAP DO  I EXTRA-INSTRUCTION  LOOP ;