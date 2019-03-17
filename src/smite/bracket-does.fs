\ Same as (CREATE) but MUST be inlined
0 0 PRIMITIVE (DOES) \ Lie about number of arguments/results!
BLIT 2 C, 0 C, 0 C, 0 C, BADD \ FIXME: 2 LITERAL,
BLIT 1 C, 0 C, 0 C, 0 C, BSWAP \ FIXME: 1 LITERAL,
END-PRIMITIVE
COMPILING