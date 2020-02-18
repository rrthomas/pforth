\ Mit inner interpreter
\
\ (c) Reuben Thomas 2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

VARIABLE IP
CODE DOCOL                              \ thread interpreter:
MLIT_0 MSWAP MLIT_1 MPOP                \ discard return address
MLIT_PC_REL MLIT_2 MLOAD MLIT_PC_REL
' IP >BODY OFFSET,
' >R OFFSET,
MCALL MNEXT MNEXT MNEXT                 \ save IP to return stack
HERE DUP DUP DUP                        \ start of loop (IP on stack)
MLIT_0 MDUP MLIT MADD                   \ set IP to next cell
4 ,
MLIT_PC_REL MLIT_2 MSTORE MNEXT
' IP >BODY OFFSET,
MLIT_2 MLOAD MCALL MNEXT                \ @EXECUTE next bead
MLIT_PC_REL MLIT_2 MLOAD MLIT_PC_REL    \ load IP
' IP >BODY OFFSET,
OFFSET,
MJUMP                                   \ loop (EXIT exits)
END-CODE

CODE EXECUTE
MLIT_1 MPOP MCALL MNEXT                 \ discard return address, call
MLIT_PC_REL MLIT_2 MLOAD MLIT_PC_REL    \ load IP
' IP >BODY OFFSET,
OFFSET,
MJUMP                                   \ loop (EXIT exits)
END-CODE

CODE @EXECUTE
MLIT_1 MPOP MLIT_2 MLOAD                \ discard return address, load
MCALL MNEXT MNEXT MNEXT                 \ call
MLIT_PC_REL MLIT_2 MLOAD MLIT_PC_REL    \ load IP
' IP >BODY OFFSET,
OFFSET,
MJUMP                                   \ loop (EXIT exits)
END-CODE

CODE EXIT
MLIT_1 MPOP MLIT_PC_REL MCALL   \ Pop return address, then outer IP from return stack
' R> OFFSET,
MLIT_PC_REL MJUMP   \ Jump into colon interpreter
OFFSET,
END-CODE

\ FIXME: fold (LITERAL), (BRANCH) and (?BRANCH) into inner interpreter,
\ encoding calls as bottom bits 00, branch as 10, ?branch as 11, literal as 01

CODE (LITERAL)
MLIT_PC_REL MLIT_0 MDUP MLIT_2          \ get IP ( ret 'IP 'IP 2 )
' IP >BODY OFFSET,
MLOAD MLIT_0 MDUP MLIT                  \ load IP ( ret 'IP IP IP CELL )
4 ,
MADD MLIT_0 MSWAP MLIT_1                \ increment it ( ret 'IP IP+CELL IP 1 )
MSWAP MLIT_2 MSTORE MLIT_2              \ update IP ( ret IP 2 )
MLOAD MLIT_0 MSWAP MJUMP                \ fetch literal & return ( lit )
END-CODE

CODE (BRANCH)
MLIT_PC_REL MLIT_0 MDUP MLIT_2          \ get IP ( ret 'IP 'IP 2 )
' IP >BODY OFFSET,
MLOAD MLIT_2 MLOAD MLIT_0               \ load branch address from IP
MSWAP MLIT_2 MSTORE MJUMP               \ set IP ( ret ) & JUMP: return
END-CODE

CODE (?BRANCH)
MLIT_PC_REL MLIT_0 MDUP MLIT_2          \ get IP ( flag ret 'IP 'IP 2 )
' IP >BODY OFFSET,
MLOAD MLIT_0 MSWAP MLIT_2               \ ( flag ret IP 'IP )
MSWAP MLIT_PC_REL MJUMPZ MLIT           \ ( 'IP ret IP )
16 ,
4 ,
MADD MLIT_PC_REL MJUMP                  \ skip IP over address
8 ,                                     \ jump to end
MLIT_2 MLOAD MNEXT MNEXT                \ load branch address
MLIT_0 MSWAP MLIT_1 MSWAP               \ set IP
MLIT_2 MSTORE MJUMP                     \ and return
END-CODE