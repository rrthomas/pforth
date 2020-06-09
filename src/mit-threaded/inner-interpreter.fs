\ Mit inner interpreter
\
\ (c) Reuben Thomas 2019-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

VARIABLE IP
CODE DOCOL                              \ thread interpreter:
0 MPUSHI MSWAP 1 MPUSHI MPOP            \ discard return address
MPUSHREL MLOAD MPUSHREL MCALL           \ save IP to return stack
' IP >BODY OFFSET,
' >R OFFSET,
HERE DUP DUP DUP                        \ start of loop (IP on stack)
0 MPUSHI MDUP 4 MPUSHI MADD             \ set IP to next cell
MPUSHREL MSTORE MEXTRA MEXTRA
' IP >BODY OFFSET,
0 MPUSHI MDUP MLOAD MADD                \ load next bead
MCALL MEXTRA MEXTRA MEXTRA              \ EXECUTE next bead
MPUSHREL MLOAD HERE SWAP OFFSET MPUSHRELI MJUMP           \ load IP and loop (EXIT exits)
' IP >BODY OFFSET,
END-CODE

CODE EXECUTE
1 MPUSHI MPOP MCALL MEXTRA              \ discard return address, call
MPUSHREL MLOAD MPUSHREL MJUMP           \ load IP and loop (EXIT exits)
' IP >BODY OFFSET,
OFFSET,
END-CODE

CODE @EXECUTE
1 MPUSHI MPOP MLOAD MCALL               \ discard return address, load & call
MPUSHREL MLOAD MPUSHREL MJUMP           \ load IP and loop (EXIT exits)
' IP >BODY OFFSET,
OFFSET,
END-CODE

CODE EXIT
1 MPUSHI MPOP MPUSHREL MCALL   \ Pop return address, then outer IP from return stack
' R> OFFSET,
MPUSHREL MJUMP                          \ Jump into colon interpreter
OFFSET,
END-CODE

\ FIXME: fold (LITERAL), (BRANCH) and (?BRANCH) into inner interpreter,
\ encoding calls as bottom bits 00, branch as 10, ?branch as 11, literal as 01

CODE (LITERAL)
MPUSHREL 0 MPUSHI MDUP MLOAD            \ get IP ( ret 'IP IP )
' IP >BODY OFFSET,
0 MPUSHI MDUP 4 MPUSHI MADD             \ load IP ( ret 'IP 'IP IP IP+CELL )
0 MPUSHI MSWAP 1 MPUSHI MSWAP           \ increment it ( ret 'IP IP+CELL IP 1 )
MSTORE MLOAD 0 MPUSHI MSWAP             \ update IP & fetch literal ( lit ret )
MJUMP                                   \ return ( lit )
END-CODE

CODE (BRANCH)
MPUSHREL 0 MPUSHI MDUP MLOAD            \ get IP ( ret 'IP IP )
' IP >BODY OFFSET,
0 MPUSHI MDUP MLOAD MADD                \ load branch address from IP, add IP
0 MPUSHI MSWAP MSTORE MJUMP             \ set IP ( ret ) & JUMP; return
END-CODE

CODE (?BRANCH)
MPUSHREL 0 MPUSHI MDUP MLOAD            \ get IP ( flag ret 'IP IP )
' IP >BODY OFFSET,
0 MPUSHI MSWAP 2 MPUSHI MSWAP           \ ( 'IP ret IP flag )
4 MPUSHRELI MJUMPZ MEXTRA MEXTRA        \ ( 'IP ret IP+CELL )
4 MPUSHI MADD 4 MPUSHRELI MJUMP         \ skip IP over address; jump to end
0 MPUSHI MDUP MLOAD MADD                \ load branch offset; compute destination
0 MPUSHI MSWAP 1 MPUSHI MSWAP           \ set IP
MSTORE MJUMP                            \ and return
END-CODE