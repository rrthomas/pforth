calli INITIALIZE
.set _byte_bits, 8
.set _immediate_bit, 1 << (bee_word_bits - 1)
.set _compiling_bit, 1 << (bee_word_bits - 2)
.set _smudge_bit, 1 << (bee_word_bits - 3)
.set _name_length_bits, bee_word_bits - _byte_bits
.balign bee_word_bytes
.byte 0x4 
.ascii "DROP"
.balign bee_word_bytes, 0x20 
.word .  - .
.word DROP_compilation
.word DROP_info 
.global DROP
DROP:
pop
ret
.balign bee_word_bytes
.set DROP_inline, 1
.set DROP_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x4 
.ascii "PICK"
.balign bee_word_bytes, 0x20 
.word DROP - .
.word PICK_compilation
.word PICK_info 
.global PICK
PICK:
dup
ret
.balign bee_word_bytes
.set PICK_inline, 1
.set PICK_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x2 
.ascii ">R"
.balign bee_word_bytes, 0x20 
.word PICK - .
.word _3E_R_compilation
.word _3E_R_info 
.global _3E_R
_3E_R:
pushs
ret
.balign bee_word_bytes
.set _3E_R_inline, 1
.set _3E_R_info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x2 
.ascii "R>"
.balign bee_word_bytes, 0x20 
.word _3E_R - .
.word R_3E__compilation
.word R_3E__info 
.global R_3E_
R_3E_:
pops
ret
.balign bee_word_bytes
.set R_3E__inline, 1
.set R_3E__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x2 
.ascii "R@"
.balign bee_word_bytes, 0x20 
.word R_3E_ - .
.word R_40__compilation
.word R_40__info 
.global R_40_
R_40_:
dups
ret
.balign bee_word_bytes
.set R_40__inline, 1
.set R_40__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x4 
.ascii "CELL"
.balign bee_word_bytes, 0x20 
.word R_40_ - .
.word CELL_compilation
.word CELL_info 
.global CELL
CELL:
word_bytes
ret
.balign bee_word_bytes
.set CELL_inline, 1
.set CELL_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x3 
.ascii "SP@"
.balign bee_word_bytes, 0x20 
.word CELL - .
.word SP_40__compilation
.word SP_40__info 
.global SP_40_
SP_40_:
get_dp
word_bytes
mul
ret
.balign bee_word_bytes
.set SP_40__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "SP!"
.balign bee_word_bytes, 0x20 
.word SP_40_ - .
.word SP_21__compilation
.word SP_21__info 
.global SP_21_
SP_21_:
word_bytes
udivmod
pop
set_dp
ret
.balign bee_word_bytes
.set SP_21__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "RP@"
.balign bee_word_bytes, 0x20 
.word SP_21_ - .
.word RP_40__compilation
.word RP_40__info 
.global RP_40_
RP_40_:
get_sp
ret
.balign bee_word_bytes
.set RP_40__inline, 1
.set RP_40__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x3 
.ascii "RP!"
.balign bee_word_bytes, 0x20 
.word RP_40_ - .
.word RP_21__compilation
.word RP_21__info 
.global RP_21_
RP_21_:
set_sp
ret
.balign bee_word_bytes
.set RP_21__inline, 1
.set RP_21__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x7 
.ascii "MEMORY@"
.balign bee_word_bytes, 0x20 
.word RP_21_ - .
.word MEMORY_40__compilation
.word MEMORY_40__info 
.global MEMORY_40_
MEMORY_40_:
get_msize
ret
.balign bee_word_bytes
.set MEMORY_40__inline, 1
.set MEMORY_40__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x3 
.ascii "M0@"
.balign bee_word_bytes, 0x20 
.word MEMORY_40_ - .
.word M_30__40__compilation
.word M_30__40__info 
.global M_30__40_
M_30__40_:
get_m0
ret
.balign bee_word_bytes
.set M_30__40__inline, 1
.set M_30__40__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x2 
.ascii "S0"
.balign bee_word_bytes, 0x20 
.word M_30__40_ - .
.word S_30__compilation
.word S_30__info 
.global S_30_
S_30_:
pushi 0x0 
ret
.balign bee_word_bytes
.set S_30__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "R0"
.balign bee_word_bytes, 0x20 
.word S_30_ - .
.word R_30__compilation
.word R_30__info 
.global R_30_
R_30_:
pushi 0x0 
ret
.balign bee_word_bytes
.set R_30__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "@"
.balign bee_word_bytes, 0x20 
.word R_30_ - .
.word _40__compilation
.word _40__info 
.global _40_
_40_:
load
ret
.balign bee_word_bytes
.set _40__inline, 1
.set _40__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x1 
.ascii "!"
.balign bee_word_bytes, 0x20 
.word _40_ - .
.word _21__compilation
.word _21__info 
.global _21_
_21_:
store
ret
.balign bee_word_bytes
.set _21__inline, 1
.set _21__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x2 
.ascii "C@"
.balign bee_word_bytes, 0x20 
.word _21_ - .
.word C_40__compilation
.word C_40__info 
.global C_40_
C_40_:
load1
ret
.balign bee_word_bytes
.set C_40__inline, 1
.set C_40__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x2 
.ascii "C!"
.balign bee_word_bytes, 0x20 
.word C_40_ - .
.word C_21__compilation
.word C_21__info 
.global C_21_
C_21_:
store1
ret
.balign bee_word_bytes
.set C_21__inline, 1
.set C_21__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x1 
.ascii "+"
.balign bee_word_bytes, 0x20 
.word C_21_ - .
.word _2B__compilation
.word _2B__info 
.global _2B_
_2B_:
add
ret
.balign bee_word_bytes
.set _2B__inline, 1
.set _2B__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x6 
.ascii "NEGATE"
.balign bee_word_bytes, 0x20 
.word _2B_ - .
.word NEGATE_compilation
.word NEGATE_info 
.global NEGATE
NEGATE:
neg
ret
.balign bee_word_bytes
.set NEGATE_inline, 1
.set NEGATE_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x1 
.ascii "*"
.balign bee_word_bytes, 0x20 
.word NEGATE - .
.word _2A__compilation
.word _2A__info 
.global _2A_
_2A_:
mul
ret
.balign bee_word_bytes
.set _2A__inline, 1
.set _2A__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x7 
.ascii "(U/MOD)"
.balign bee_word_bytes, 0x20 
.word _2A_ - .
.word _28_U_2F_MOD_29__compilation
.word _28_U_2F_MOD_29__info 
.global _28_U_2F_MOD_29_
_28_U_2F_MOD_29_:
udivmod
pushi 0x0 
swap
ret
.balign bee_word_bytes
.set _28_U_2F_MOD_29__inline, 3
.set _28_U_2F_MOD_29__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x30000 
.balign bee_word_bytes
.byte 0x7 
.ascii "(S/REM)"
.balign bee_word_bytes, 0x20 
.word _28_U_2F_MOD_29_ - .
.word _28_S_2F_REM_29__compilation
.word _28_S_2F_REM_29__info 
.global _28_S_2F_REM_29_
_28_S_2F_REM_29_:
divmod
pushi 0x0 
swap
ret
.balign bee_word_bytes
.set _28_S_2F_REM_29__inline, 3
.set _28_S_2F_REM_29__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x30000 
.balign bee_word_bytes
.byte 0x1 
.ascii "="
.balign bee_word_bytes, 0x20 
.word _28_S_2F_REM_29_ - .
.word _3D__compilation
.word _3D__info 
.global _3D_
_3D_:
eq
neg
ret
.balign bee_word_bytes
.set _3D__inline, 2
.set _3D__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x1 
.ascii "<"
.balign bee_word_bytes, 0x20 
.word _3D_ - .
.word _3C__compilation
.word _3C__info 
.global _3C_
_3C_:
lt
neg
ret
.balign bee_word_bytes
.set _3C__inline, 2
.set _3C__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x2 
.ascii "U<"
.balign bee_word_bytes, 0x20 
.word _3C_ - .
.word U_3C__compilation
.word U_3C__info 
.global U_3C_
U_3C_:
ult
neg
ret
.balign bee_word_bytes
.set U_3C__inline, 2
.set U_3C__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x6 
.ascii "INVERT"
.balign bee_word_bytes, 0x20 
.word U_3C_ - .
.word INVERT_compilation
.word INVERT_info 
.global INVERT
INVERT:
not
ret
.balign bee_word_bytes
.set INVERT_inline, 1
.set INVERT_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x3 
.ascii "AND"
.balign bee_word_bytes, 0x20 
.word INVERT - .
.word AND_compilation
.word AND_info 
.global AND
AND:
and
ret
.balign bee_word_bytes
.set AND_inline, 1
.set AND_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x2 
.ascii "OR"
.balign bee_word_bytes, 0x20 
.word AND - .
.word OR_compilation
.word OR_info 
.global OR
OR:
or
ret
.balign bee_word_bytes
.set OR_inline, 1
.set OR_info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x3 
.ascii "XOR"
.balign bee_word_bytes, 0x20 
.word OR - .
.word XOR_compilation
.word XOR_info 
.global XOR
XOR:
xor
ret
.balign bee_word_bytes
.set XOR_inline, 1
.set XOR_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x6 
.ascii "LSHIFT"
.balign bee_word_bytes, 0x20 
.word XOR - .
.word LSHIFT_compilation
.word LSHIFT_info 
.global LSHIFT
LSHIFT:
lshift
ret
.balign bee_word_bytes
.set LSHIFT_inline, 1
.set LSHIFT_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x6 
.ascii "RSHIFT"
.balign bee_word_bytes, 0x20 
.word LSHIFT - .
.word RSHIFT_compilation
.word RSHIFT_info 
.global RSHIFT
RSHIFT:
rshift
ret
.balign bee_word_bytes
.set RSHIFT_inline, 1
.set RSHIFT_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x4 
.ascii "EXIT"
.balign bee_word_bytes, 0x20 
.word RSHIFT - .
.word EXIT_compilation
.word EXIT_info 
.global EXIT
EXIT:
ret
.balign bee_word_bytes
.set EXIT_inline, 1
.set EXIT_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x7 
.ascii "EXECUTE"
.balign bee_word_bytes, 0x20 
.word EXIT - .
.word EXECUTE_compilation
.word EXECUTE_info 
.global EXECUTE
EXECUTE:
call
ret
.balign bee_word_bytes
.set EXECUTE_inline, 1
.set EXECUTE_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x8 
.ascii "@EXECUTE"
.balign bee_word_bytes, 0x20 
.word EXECUTE - .
.word _40_EXECUTE_compilation
.word _40_EXECUTE_info 
.global _40_EXECUTE
_40_EXECUTE:
load
call
ret
.balign bee_word_bytes
.set _40_EXECUTE_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "HALT"
.balign bee_word_bytes, 0x20 
.word _40_EXECUTE - .
.word HALT_compilation
.word HALT_info 
.global HALT
HALT:
throw
.balign bee_word_bytes
.set HALT_inline, 1
.set HALT_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x8 
.ascii "(CREATE)"
.balign bee_word_bytes, 0x20 
.word HALT - .
.word _28_CREATE_29__compilation
.word _28_CREATE_29__info 
.global _28_CREATE_29_
_28_CREATE_29_:
_28_CREATE_29__does:
pops
ret
.balign bee_word_bytes
.set _28_CREATE_29__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "STACK-CELLS"
.balign bee_word_bytes, 0x20 
.word _28_CREATE_29_ - .
.word STACK_2D_CELLS_compilation
.word STACK_2D_CELLS_info 
.global STACK_2D_CELLS
STACK_2D_CELLS:
pushi 4096 
ret
.set STACK_2D_CELLS_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x12 
.ascii "RETURN-STACK-CELLS"
.balign bee_word_bytes, 0x20 
.word STACK_2D_CELLS - .
.word RETURN_2D_STACK_2D_CELLS_compilation
.word RETURN_2D_STACK_2D_CELLS_info 
.global RETURN_2D_STACK_2D_CELLS
RETURN_2D_STACK_2D_CELLS:
pushi 4096 
ret
.set RETURN_2D_STACK_2D_CELLS_info, 0 | 0 | 0 | (18 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "DUP"
.balign bee_word_bytes, 0x20 
.word RETURN_2D_STACK_2D_CELLS - .
.word DUP_compilation
.word DUP_info 
.global DUP
DUP:
pushi 0x0 
dup
ret
.balign bee_word_bytes
.set DUP_inline, 2
.set DUP_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x4 
.ascii "SWAP"
.balign bee_word_bytes, 0x20 
.word DUP - .
.word SWAP_compilation
.word SWAP_info 
.global SWAP
SWAP:
pushi 0x0 
swap
ret
.balign bee_word_bytes
.set SWAP_inline, 2
.set SWAP_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x4 
.ascii "OVER"
.balign bee_word_bytes, 0x20 
.word SWAP - .
.word OVER_compilation
.word OVER_info 
.global OVER
OVER:
pushi 0x1 
dup
ret
.balign bee_word_bytes
.set OVER_inline, 2
.set OVER_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x3 
.ascii "ROT"
.balign bee_word_bytes, 0x20 
.word OVER - .
.word ROT_compilation
.word ROT_info 
.global ROT
ROT:
pushi 0x0 
swap
pushi 0x1 
swap
ret
.balign bee_word_bytes
.set ROT_inline, 4
.set ROT_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x40000 
.balign bee_word_bytes
.byte 0x4 
.ascii "-ROT"
.balign bee_word_bytes, 0x20 
.word ROT - .
.word _2D_ROT_compilation
.word _2D_ROT_info 
.global _2D_ROT
_2D_ROT:
pushi 0x1 
swap
pushi 0x0 
swap
ret
.balign bee_word_bytes
.set _2D_ROT_inline, 4
.set _2D_ROT_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x40000 
.balign bee_word_bytes
.byte 0x5 
.ascii "2SWAP"
.balign bee_word_bytes, 0x20 
.word _2D_ROT - .
.word _32_SWAP_compilation
.word _32_SWAP_info 
.global _32_SWAP
_32_SWAP:
pushi 0x1 
swap
pushi 0x0 
swap
pushi 0x2 
swap
pushi 0x0 
swap
ret
.balign bee_word_bytes
.set _32_SWAP_inline, 8
.set _32_SWAP_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x80000 
.balign bee_word_bytes
.byte 0x1 
.ascii "-"
.balign bee_word_bytes, 0x20 
.word _32_SWAP - .
.word _2D__compilation
.word _2D__info 
.global _2D_
_2D_:
neg
add
ret
.balign bee_word_bytes
.set _2D__inline, 2
.set _2D__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x2 
.ascii "1+"
.balign bee_word_bytes, 0x20 
.word _2D_ - .
.word _31__2B__compilation
.word _31__2B__info 
.global _31__2B_
_31__2B_:
not
neg
ret
.balign bee_word_bytes
.set _31__2B__inline, 2
.set _31__2B__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x2 
.ascii "1-"
.balign bee_word_bytes, 0x20 
.word _31__2B_ - .
.word _31__2D__compilation
.word _31__2D__info 
.global _31__2D_
_31__2D_:
neg
not
ret
.balign bee_word_bytes
.set _31__2D__inline, 2
.set _31__2D__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x7 
.ascii "ARSHIFT"
.balign bee_word_bytes, 0x20 
.word _31__2D_ - .
.word ARSHIFT_compilation
.word ARSHIFT_info 
.global ARSHIFT
ARSHIFT:
arshift
ret
.balign bee_word_bytes
.set ARSHIFT_inline, 1
.set ARSHIFT_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x10000 
.balign bee_word_bytes
.byte 0x7 
.ascii "NOTHING"
.balign bee_word_bytes, 0x20 
.word ARSHIFT - .
.word NOTHING_compilation
.word NOTHING_info 
.global NOTHING
NOTHING:
ret
.set NOTHING_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "'FORTH"
.balign bee_word_bytes, 0x20 
.word NOTHING - .
.word _27_FORTH_compilation
.word _27_FORTH_info 
.global _27_FORTH
_27_FORTH:
nop 
calli _27_FORTH_doer
.balign bee_word_bytes
_27_FORTH_body:
.word 0 
.set _27_FORTH_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set _27_FORTH_doer, VALUE_does
.balign bee_word_bytes
.byte 0x4 
.ascii "TRUE"
.balign bee_word_bytes, 0x20 
.word _27_FORTH - .
.word TRUE_compilation
.word TRUE_info 
.global TRUE
TRUE:
pushi -1 
ret
.set TRUE_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "FALSE"
.balign bee_word_bytes, 0x20 
.word TRUE - .
.word FALSE_compilation
.word FALSE_info 
.global FALSE
FALSE:
pushi 0 
ret
.set FALSE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "BYTE-BITS"
.balign bee_word_bytes, 0x20 
.word FALSE - .
.word BYTE_2D_BITS_compilation
.word BYTE_2D_BITS_info 
.global BYTE_2D_BITS
BYTE_2D_BITS:
pushi 8 
ret
.set BYTE_2D_BITS_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "-CELL"
.balign bee_word_bytes, 0x20 
.word BYTE_2D_BITS - .
.word _2D_CELL_compilation
.word _2D_CELL_info 
.global _2D_CELL
_2D_CELL:
word_bytes
neg
ret
.set _2D_CELL_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CELL+"
.balign bee_word_bytes, 0x20 
.word _2D_CELL - .
.word CELL_2B__compilation
.word CELL_2B__info 
.global CELL_2B_
CELL_2B_:
word_bytes
add
ret
.set CELL_2B__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CELL-"
.balign bee_word_bytes, 0x20 
.word CELL_2B_ - .
.word CELL_2D__compilation
.word CELL_2D__info 
.global CELL_2D_
CELL_2D_:
word_bytes
neg
add
ret
.set CELL_2D__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "?DUP"
.balign bee_word_bytes, 0x20 
.word CELL_2D_ - .
.word _3F_DUP_compilation
.word _3F_DUP_info 
.global _3F_DUP
_3F_DUP:
pushi 0 # 0x0 
dup
jumpzi .L389f
pushi 0 # 0x0 
dup
.L389f:
ret
.set _3F_DUP_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "ROLL"
.balign bee_word_bytes, 0x20 
.word _3F_DUP - .
.word ROLL_compilation
.word ROLL_info 
.global ROLL
ROLL:
pushi 0 # 0x0 
dup
pushi 1 
add
dup
pushs
pushi 0 # 0x0 
dup
pushs
.L406b:
calli _3F_DUP
jumpzi .L407f
pushi 0 # 0x0 
swap
pops
pushi 0 # 0x0 
swap
pushs
pushs
pushi 1 
neg
add
jumpi .L406b
.L407f:
pop
pops
.L421b:
calli _3F_DUP
jumpzi .L422f
pops
pushi 0 # 0x0 
swap
pushi 1 
neg
add
jumpi .L421b
.L422f:
pops
ret
.set ROLL_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "TUCK"
.balign bee_word_bytes, 0x20 
.word ROLL - .
.word TUCK_compilation
.word TUCK_info 
.global TUCK
TUCK:
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
dup
ret
.set TUCK_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "NIP"
.balign bee_word_bytes, 0x20 
.word TUCK - .
.word NIP_compilation
.word NIP_info 
.global NIP
NIP:
pushi 0 # 0x0 
swap
pop
ret
.set NIP_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "'THROW"
.balign bee_word_bytes, 0x20 
.word NIP - .
.word _27_THROW_compilation
.word _27_THROW_info 
.global _27_THROW
_27_THROW:
nop 
calli _27_THROW_doer
.balign bee_word_bytes
_27_THROW_body:
.ds.b 1 * bee_word_bytes
.set _27_THROW_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set _27_THROW_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x7 
.ascii "'THROW!"
.balign bee_word_bytes, 0x20 
.word _27_THROW - .
.word _27_THROW_21__compilation
.word _27_THROW_21__info 
.global _27_THROW_21_
_27_THROW_21_:
calli _27_THROW
store
ret
.set _27_THROW_21__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "THROW"
.balign bee_word_bytes, 0x20 
.word _27_THROW_21_ - .
.word THROW_compilation
.word THROW_info 
.global THROW
THROW:
calli _27_THROW
calli _40_EXECUTE
ret
.set THROW_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "'THROWN"
.balign bee_word_bytes, 0x20 
.word THROW - .
.word _27_THROWN_compilation
.word _27_THROWN_info 
.global _27_THROWN
_27_THROWN:
nop 
calli _27_THROWN_doer
.balign bee_word_bytes
_27_THROWN_body:
.ds.b 1 * bee_word_bytes
.set _27_THROWN_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set _27_THROWN_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x8 
.ascii "(ABORT\")"
.balign bee_word_bytes, 0x20 
.word _27_THROWN - .
.word _28_ABORT_22__29__compilation
.word _28_ABORT_22__29__info 
.global _28_ABORT_22__29_
_28_ABORT_22__29_:
pushi 0 # 0x0 
swap
jumpzi .L484f
calli _27_THROWN
store
pushi -2 
calli THROW
jumpi .L489f
.L484f:
pop
.L489f:
ret
.set _28_ABORT_22__29__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "UNDEFINED"
.balign bee_word_bytes, 0x20 
.word _28_ABORT_22__29_ - .
.word UNDEFINED_compilation
.word UNDEFINED_info 
.global UNDEFINED
UNDEFINED:
calli _27_THROWN
store
pushi -13 
calli THROW
ret
.set UNDEFINED_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "BYE"
.balign bee_word_bytes, 0x20 
.word UNDEFINED - .
.word BYE_compilation
.word BYE_info 
.global BYE
BYE:
pushi 0 
throw
ret
.set BYE_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii ">-<"
.balign bee_word_bytes, 0x20 
.word BYE - .
.word _3E__2D__3C__compilation
.word _3E__2D__3C__info 
.global _3E__2D__3C_
_3E__2D__3C_:
pushi 0 # 0x0 
swap
neg
add
ret
.set _3E__2D__3C__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii ">"
.balign bee_word_bytes, 0x20 
.word _3E__2D__3C_ - .
.word _3E__compilation
.word _3E__info 
.global _3E_
_3E_:
pushi 0 # 0x0 
swap
lt
neg
ret
.set _3E__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "U>"
.balign bee_word_bytes, 0x20 
.word _3E_ - .
.word U_3E__compilation
.word U_3E__info 
.global U_3E_
U_3E_:
pushi 0 # 0x0 
swap
ult
neg
ret
.set U_3E__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "<>"
.balign bee_word_bytes, 0x20 
.word U_3E_ - .
.word _3C__3E__compilation
.word _3C__3E__info 
.global _3C__3E_
_3C__3E_:
eq
neg
not
ret
.set _3C__3E__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "0<"
.balign bee_word_bytes, 0x20 
.word _3C__3E_ - .
.word _30__3C__compilation
.word _30__3C__info 
.global _30__3C_
_30__3C_:
pushi 0 
lt
neg
ret
.set _30__3C__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "0>"
.balign bee_word_bytes, 0x20 
.word _30__3C_ - .
.word _30__3E__compilation
.word _30__3E__info 
.global _30__3E_
_30__3E_:
pushi 0 
calli _3E_
ret
.set _30__3E__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "0="
.balign bee_word_bytes, 0x20 
.word _30__3E_ - .
.word _30__3D__compilation
.word _30__3D__info 
.global _30__3D_
_30__3D_:
pushi 0 
eq
neg
ret
.set _30__3D__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "0<>"
.balign bee_word_bytes, 0x20 
.word _30__3D_ - .
.word _30__3C__3E__compilation
.word _30__3C__3E__info 
.global _30__3C__3E_
_30__3C__3E_:
pushi 0 
calli _3C__3E_
ret
.set _30__3C__3E__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "ABS"
.balign bee_word_bytes, 0x20 
.word _30__3C__3E_ - .
.word ABS_compilation
.word ABS_info 
.global ABS
ABS:
pushi 0 # 0x0 
dup
calli _30__3C_
jumpzi .L581f
neg
.L581f:
ret
.set ABS_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "CHECK-DIVISOR"
.balign bee_word_bytes, 0x20 
.word ABS - .
.word CHECK_2D_DIVISOR_compilation
.word CHECK_2D_DIVISOR_info 
.global CHECK_2D_DIVISOR
CHECK_2D_DIVISOR:
pushi 0 # 0x0 
dup
calli _30__3D_
jumpzi .L592f
pushi -10 
calli THROW
.L592f:
ret
.set CHECK_2D_DIVISOR_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "S/REM"
.balign bee_word_bytes, 0x20 
.word CHECK_2D_DIVISOR - .
.word S_2F_REM_compilation
.word S_2F_REM_info 
.global S_2F_REM
S_2F_REM:
calli CHECK_2D_DIVISOR
divmod
pushi 0 # 0x0 
swap
ret
.set S_2F_REM_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "U/MOD"
.balign bee_word_bytes, 0x20 
.word S_2F_REM - .
.word U_2F_MOD_compilation
.word U_2F_MOD_info 
.global U_2F_MOD
U_2F_MOD:
calli CHECK_2D_DIVISOR
udivmod
pushi 0 # 0x0 
swap
ret
.set U_2F_MOD_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "/MOD"
.balign bee_word_bytes, 0x20 
.word U_2F_MOD - .
.word _2F_MOD_compilation
.word _2F_MOD_info 
.global _2F_MOD
_2F_MOD:
pushi 0 # 0x0 
dup
pushs
pushi 1 # 0x1 
dup
pushi 1 # 0x1 
dup
xor
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
calli S_2F_REM
pushi 0 # 0x0 
swap
pushi 0 # 0x0 
dup
pushi 3 
dup
calli _30__3C_
pushi 1 # 0x1 
dup
calli _30__3C__3E_
and
jumpzi .L642f
dups
calli ABS
pushi 0 # 0x0 
swap
calli ABS
neg
add
pops
calli _30__3E_
jumpzi .L652f
pushi 1 
jumpi .L654f
.L652f:
pushi -1 
.L654f:
mul
jumpi .L657f
.L642f:
pops
pop
.L657f:
pushs
calli _30__3C__3E_
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli _30__3C_
and
add
pops
pushi 0 # 0x0 
swap
ret
.set _2F_MOD_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "/"
.balign bee_word_bytes, 0x20 
.word _2F_MOD - .
.word _2F__compilation
.word _2F__info 
.global _2F_
_2F_:
calli _2F_MOD
calli NIP
ret
.set _2F__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "MOD"
.balign bee_word_bytes, 0x20 
.word _2F_ - .
.word MOD_compilation
.word MOD_info 
.global MOD
MOD:
calli _2F_MOD
pop
ret
.set MOD_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "2*"
.balign bee_word_bytes, 0x20 
.word MOD - .
.word _32__2A__compilation
.word _32__2A__info 
.global _32__2A_
_32__2A_:
pushi 1 
lshift
ret
.set _32__2A__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "2/"
.balign bee_word_bytes, 0x20 
.word _32__2A_ - .
.word _32__2F__compilation
.word _32__2F__info 
.global _32__2F_
_32__2F_:
pushi 1 
arshift
ret
.set _32__2F__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CELLS"
.balign bee_word_bytes, 0x20 
.word _32__2F_ - .
.word CELLS_compilation
.word CELLS_info 
.global CELLS
CELLS:
word_bytes
mul
ret
.set CELLS_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CELL/"
.balign bee_word_bytes, 0x20 
.word CELLS - .
.word CELL_2F__compilation
.word CELL_2F__info 
.global CELL_2F_
CELL_2F_:
word_bytes
calli _2F_
ret
.set CELL_2F__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "CELL-BITS"
.balign bee_word_bytes, 0x20 
.word CELL_2F_ - .
.word CELL_2D_BITS_compilation
.word CELL_2D_BITS_info 
.global CELL_2D_BITS
CELL_2D_BITS:
calli BYTE_2D_BITS
calli CELLS
ret
.set CELL_2D_BITS_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "TOP-BIT-SET"
.balign bee_word_bytes, 0x20 
.word CELL_2D_BITS - .
.word TOP_2D_BIT_2D_SET_compilation
.word TOP_2D_BIT_2D_SET_info 
.global TOP_2D_BIT_2D_SET
TOP_2D_BIT_2D_SET:
pushi 1 
calli CELL_2D_BITS
neg
not
lshift
ret
.set TOP_2D_BIT_2D_SET_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "J"
.balign bee_word_bytes, 0x20 
.word TOP_2D_BIT_2D_SET - .
.word J_compilation
.word J_info 
.global J
J:
pops
pops
pops
pops
pushi 0 # 0x0 
dup
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushs
pushs
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushs
pushs
ret
.set J_info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "(LOOP)"
.balign bee_word_bytes, 0x20 
.word J - .
.word _28_LOOP_29__compilation
.word _28_LOOP_29__info 
.global _28_LOOP_29_
_28_LOOP_29_:
pops
pops
not
neg
pushi 0 # 0x0 
dup
dups
eq
neg
pushi 0 # 0x0 
swap
pushs
pushi 0 # 0x0 
swap
pushs
ret
.set _28_LOOP_29__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "(+LOOP)"
.balign bee_word_bytes, 0x20 
.word _28_LOOP_29_ - .
.word _28__2B_LOOP_29__compilation
.word _28__2B_LOOP_29__info 
.global _28__2B_LOOP_29_
_28__2B_LOOP_29_:
pops
pushi 0 # 0x0 
swap
pops
dups
pushi 1 # 0x1 
dup
pushi 0 # 0x0 
swap
neg
add
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
add
dups
pushi 1 # 0x1 
dup
pushi 0 # 0x0 
swap
neg
add
pushi 0 # 0x0 
swap
pushs
xor
calli _30__3C_
pushi 0 # 0x0 
swap
pushs
ret
.set _28__2B_LOOP_29__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "UNLOOP"
.balign bee_word_bytes, 0x20 
.word _28__2B_LOOP_29_ - .
.word UNLOOP_compilation
.word UNLOOP_info 
.global UNLOOP
UNLOOP:
pops
pops
pop
pops
pop
pushs
ret
.set UNLOOP_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "2DUP"
.balign bee_word_bytes, 0x20 
.word UNLOOP - .
.word _32_DUP_compilation
.word _32_DUP_info 
.global _32_DUP
_32_DUP:
pushi 1 # 0x1 
dup
pushi 1 # 0x1 
dup
ret
.set _32_DUP_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "2DROP"
.balign bee_word_bytes, 0x20 
.word _32_DUP - .
.word _32_DROP_compilation
.word _32_DROP_info 
.global _32_DROP
_32_DROP:
pop
pop
ret
.set _32_DROP_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "2OVER"
.balign bee_word_bytes, 0x20 
.word _32_DROP - .
.word _32_OVER_compilation
.word _32_OVER_info 
.global _32_OVER
_32_OVER:
pushi 3 
dup
pushi 3 
dup
ret
.set _32_OVER_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "2ROT"
.balign bee_word_bytes, 0x20 
.word _32_OVER - .
.word _32_ROT_compilation
.word _32_ROT_info 
.global _32_ROT
_32_ROT:
pushi 5 
calli ROLL
pushi 5 
calli ROLL
ret
.set _32_ROT_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "2>R"
.balign bee_word_bytes, 0x20 
.word _32_ROT - .
.word _32__3E_R_compilation
.word _32__3E_R_info 
.global _32__3E_R
_32__3E_R:
pops
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushi 0 # 0x0 
swap
pushs
pushs
pushs
ret
.set _32__3E_R_info, 0 | _compiling_bit | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "2R>"
.balign bee_word_bytes, 0x20 
.word _32__3E_R - .
.word _32_R_3E__compilation
.word _32_R_3E__info 
.global _32_R_3E_
_32_R_3E_:
pops
pops
pops
pushi 0 # 0x0 
swap
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
pushs
ret
.set _32_R_3E__info, 0 | _compiling_bit | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "2R@"
.balign bee_word_bytes, 0x20 
.word _32_R_3E_ - .
.word _32_R_40__compilation
.word _32_R_40__info 
.global _32_R_40_
_32_R_40_:
pops
pops
pops
calli _32_DUP
pushs
pushs
pushi 0 # 0x0 
swap
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
pushs
ret
.set _32_R_40__info, 0 | _compiling_bit | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xF 
.ascii "STACK-DIRECTION"
.balign bee_word_bytes, 0x20 
.word _32_R_40_ - .
.word STACK_2D_DIRECTION_compilation
.word STACK_2D_DIRECTION_info 
.global STACK_2D_DIRECTION
STACK_2D_DIRECTION:
calli SP_40_
calli SP_40_
neg
add
calli _30__3C_
neg
calli _32__2A_
neg
not
ret
.set STACK_2D_DIRECTION_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "DEPTH"
.balign bee_word_bytes, 0x20 
.word STACK_2D_DIRECTION - .
.word DEPTH_compilation
.word DEPTH_info 
.global DEPTH
DEPTH:
calli SP_40_
calli S_30_
neg
add
calli CELL_2F_
calli STACK_2D_DIRECTION
mul
ret
.set DEPTH_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "+!"
.balign bee_word_bytes, 0x20 
.word DEPTH - .
.word _2B__21__compilation
.word _2B__21__info 
.global _2B__21_
_2B__21_:
calli TUCK
load
add
pushi 0 # 0x0 
swap
store
ret
.set _2B__21__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "+CHAR"
.balign bee_word_bytes, 0x20 
.word _2B__21_ - .
.word _2B_CHAR_compilation
.word _2B_CHAR_info 
.global _2B_CHAR
_2B_CHAR:
pushi 1 
ret
.set _2B_CHAR_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "-CHAR"
.balign bee_word_bytes, 0x20 
.word _2B_CHAR - .
.word _2D_CHAR_compilation
.word _2D_CHAR_info 
.global _2D_CHAR
_2D_CHAR:
pushi -1 
ret
.set _2D_CHAR_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CHAR+"
.balign bee_word_bytes, 0x20 
.word _2D_CHAR - .
.word CHAR_2B__compilation
.word CHAR_2B__info 
.global CHAR_2B_
CHAR_2B_:
not
neg
ret
.set CHAR_2B__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CHAR-"
.balign bee_word_bytes, 0x20 
.word CHAR_2B_ - .
.word CHAR_2D__compilation
.word CHAR_2D__info 
.global CHAR_2D_
CHAR_2D_:
neg
not
ret
.set CHAR_2D__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CHARS"
.balign bee_word_bytes, 0x20 
.word CHAR_2D_ - .
.word CHARS_compilation
.word CHARS_info 
.global CHARS
CHARS:
ret
.set CHARS_compilation, (2 * bee_word_bytes)
.set CHARS_info, _immediate_bit | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CHAR/"
.balign bee_word_bytes, 0x20 
.word CHARS - .
.word CHAR_2F__compilation
.word CHAR_2F__info 
.global CHAR_2F_
CHAR_2F_:
ret
.set CHAR_2F__compilation, (2 * bee_word_bytes)
.set CHAR_2F__info, _immediate_bit | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "MIN"
.balign bee_word_bytes, 0x20 
.word CHAR_2F_ - .
.word MIN_compilation
.word MIN_info 
.global MIN
MIN:
calli _32_DUP
calli _3E_
jumpzi .L986f
pushi 0 # 0x0 
swap
.L986f:
pop
ret
.set MIN_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "MAX"
.balign bee_word_bytes, 0x20 
.word MIN - .
.word MAX_compilation
.word MAX_info 
.global MAX
MAX:
calli _32_DUP
lt
neg
jumpzi .L998f
pushi 0 # 0x0 
swap
.L998f:
pop
ret
.set MAX_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "S>D"
.balign bee_word_bytes, 0x20 
.word MAX - .
.word S_3E_D_compilation
.word S_3E_D_info 
.global S_3E_D
S_3E_D:
pushi 0 # 0x0 
dup
calli _30__3C_
ret
.set S_3E_D_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "D>S"
.balign bee_word_bytes, 0x20 
.word S_3E_D - .
.word D_3E_S_compilation
.word D_3E_S_info 
.global D_3E_S
D_3E_S:
pop
ret
.set D_3E_S_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "U>UD"
.balign bee_word_bytes, 0x20 
.word D_3E_S - .
.word U_3E_UD_compilation
.word U_3E_UD_info 
.global U_3E_UD
U_3E_UD:
pushi 0 
ret
.set U_3E_UD_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "UD>U"
.balign bee_word_bytes, 0x20 
.word U_3E_UD - .
.word UD_3E_U_compilation
.word UD_3E_U_info 
.global UD_3E_U
UD_3E_U:
pop
ret
.set UD_3E_U_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "WITHIN"
.balign bee_word_bytes, 0x20 
.word UD_3E_U - .
.word WITHIN_compilation
.word WITHIN_info 
.global WITHIN
WITHIN:
pushi 1 # 0x1 
dup
neg
add
pushs
neg
add
pops
ult
neg
ret
.set WITHIN_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "M*"
.balign bee_word_bytes, 0x20 
.word WITHIN - .
.word M_2A__compilation
.word M_2A__info 
.global M_2A_
M_2A_:
mul
calli S_3E_D
ret
.set M_2A__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "UM*"
.balign bee_word_bytes, 0x20 
.word M_2A_ - .
.word UM_2A__compilation
.word UM_2A__info 
.global UM_2A_
UM_2A_:
mul
calli U_3E_UD
ret
.set UM_2A__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "FM/MOD"
.balign bee_word_bytes, 0x20 
.word UM_2A_ - .
.word FM_2F_MOD_compilation
.word FM_2F_MOD_info 
.global FM_2F_MOD
FM_2F_MOD:
calli NIP
calli _2F_MOD
ret
.set FM_2F_MOD_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "SM/REM"
.balign bee_word_bytes, 0x20 
.word FM_2F_MOD - .
.word SM_2F_REM_compilation
.word SM_2F_REM_info 
.global SM_2F_REM
SM_2F_REM:
calli NIP
calli S_2F_REM
ret
.set SM_2F_REM_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "UM/MOD"
.balign bee_word_bytes, 0x20 
.word SM_2F_REM - .
.word UM_2F_MOD_compilation
.word UM_2F_MOD_info 
.global UM_2F_MOD
UM_2F_MOD:
calli NIP
calli U_2F_MOD
ret
.set UM_2F_MOD_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "*/"
.balign bee_word_bytes, 0x20 
.word UM_2F_MOD - .
.word _2A__2F__compilation
.word _2A__2F__info 
.global _2A__2F_
_2A__2F_:
pushs
mul
pops
calli _2F_
ret
.set _2A__2F__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "*/MOD"
.balign bee_word_bytes, 0x20 
.word _2A__2F_ - .
.word _2A__2F_MOD_compilation
.word _2A__2F_MOD_info 
.global _2A__2F_MOD
_2A__2F_MOD:
pushs
mul
pops
calli _2F_MOD
ret
.set _2A__2F_MOD_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "D0="
.balign bee_word_bytes, 0x20 
.word _2A__2F_MOD - .
.word D_30__3D__compilation
.word D_30__3D__info 
.global D_30__3D_
D_30__3D_:
calli _30__3D_
pushi 0 # 0x0 
swap
calli _30__3D_
and
ret
.set D_30__3D__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "D+"
.balign bee_word_bytes, 0x20 
.word D_30__3D_ - .
.word D_2B__compilation
.word D_2B__info 
.global D_2B_
D_2B_:
calli D_3E_S
pushs
calli D_3E_S
pops
add
calli S_3E_D
ret
.set D_2B__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "DNEGATE"
.balign bee_word_bytes, 0x20 
.word D_2B_ - .
.word DNEGATE_compilation
.word DNEGATE_info 
.global DNEGATE
DNEGATE:
calli D_3E_S
neg
calli S_3E_D
ret
.set DNEGATE_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "D-"
.balign bee_word_bytes, 0x20 
.word DNEGATE - .
.word D_2D__compilation
.word D_2D__info 
.global D_2D_
D_2D_:
calli DNEGATE
calli D_2B_
ret
.set D_2D__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "M+"
.balign bee_word_bytes, 0x20 
.word D_2D_ - .
.word M_2B__compilation
.word M_2B__info 
.global M_2B_
M_2B_:
calli S_3E_D
calli D_2B_
ret
.set M_2B__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "D*"
.balign bee_word_bytes, 0x20 
.word M_2B_ - .
.word D_2A__compilation
.word D_2A__info 
.global D_2A_
D_2A_:
calli D_3E_S
pushs
calli D_3E_S
pops
mul
calli S_3E_D
ret
.set D_2A__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "UD/MOD"
.balign bee_word_bytes, 0x20 
.word D_2A_ - .
.word UD_2F_MOD_compilation
.word UD_2F_MOD_info 
.global UD_2F_MOD
UD_2F_MOD:
calli UD_3E_U
pushs
calli UD_3E_U
pops
calli U_2F_MOD
pushs
calli U_3E_UD
pops
calli U_3E_UD
ret
.set UD_2F_MOD_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "DABS"
.balign bee_word_bytes, 0x20 
.word UD_2F_MOD - .
.word DABS_compilation
.word DABS_info 
.global DABS
DABS:
jumpzi .L1169f
neg
.L1169f:
calli U_3E_UD
ret
.set DABS_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "COUNT"
.balign bee_word_bytes, 0x20 
.word DABS - .
.word COUNT_compilation
.word COUNT_info 
.global COUNT
COUNT:
pushi 0 # 0x0 
dup
calli CHAR_2B_
pushi 0 # 0x0 
swap
load1
ret
.set COUNT_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "/STRING"
.balign bee_word_bytes, 0x20 
.word COUNT - .
.word _2F_STRING_compilation
.word _2F_STRING_info 
.global _2F_STRING
_2F_STRING:
calli TUCK
neg
add
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
add
pushi 0 # 0x0 
swap
ret
.set _2F_STRING_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CMOVE"
.balign bee_word_bytes, 0x20 
.word _2F_STRING - .
.word CMOVE_compilation
.word CMOVE_info 
.global CMOVE
CMOVE:
pushi 1 # 0x1 
dup
add
pushi 0 # 0x0 
swap
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L1212f
jumpi .L1208f
.L1212f:
.L1214b:
pushi 0 # 0x0 
dup
load1
dups
store1
calli CHAR_2B_
calli _2B_CHAR
calli _28__2B_LOOP_29_
jumpzi .L1214b
.L1208f:
.L1213f:
calli UNLOOP
pop
ret
.set CMOVE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "CMOVE>"
.balign bee_word_bytes, 0x20 
.word CMOVE - .
.word CMOVE_3E__compilation
.word CMOVE_3E__info 
.global CMOVE_3E_
CMOVE_3E_:
calli _3F_DUP
jumpzi .L1231f
calli CHAR_2D_
calli TUCK
add
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
dup
add
calli _32__3E_R
.L1243b:
dups
load1
pushi 1 # 0x1 
dup
store1
calli CHAR_2D_
calli _2D_CHAR
calli _28__2B_LOOP_29_
jumpzi .L1243b
.L1242f:
calli UNLOOP
jumpi .L1253f
.L1231f:
pop
.L1253f:
pop
ret
.set CMOVE_3E__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "MOVE"
.balign bee_word_bytes, 0x20 
.word CMOVE_3E_ - .
.word MOVE_compilation
.word MOVE_info 
.global MOVE
MOVE:
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
calli _32_DUP
calli _3E_
jumpzi .L1267f
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli CMOVE
jumpi .L1273f
.L1267f:
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli CMOVE_3E_
.L1273f:
ret
.set MOVE_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "FILL"
.balign bee_word_bytes, 0x20 
.word MOVE - .
.word FILL_compilation
.word FILL_info 
.global FILL
FILL:
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
dup
add
pushi 0 # 0x0 
swap
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L1297f
jumpi .L1293f
.L1297f:
.L1299b:
pushi 0 # 0x0 
dup
dups
store1
calli _2B_CHAR
calli _28__2B_LOOP_29_
jumpzi .L1299b
.L1293f:
.L1298f:
calli UNLOOP
pop
ret
.set FILL_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "ERASE"
.balign bee_word_bytes, 0x20 
.word FILL - .
.word ERASE_compilation
.word ERASE_info 
.global ERASE
ERASE:
pushi 0 
calli FILL
ret
.set ERASE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii ".ALIGN"
.balign bee_word_bytes, 0x20 
.word ERASE - .
.word _2E_ALIGN_compilation
.word _2E_ALIGN_info 
.global _2E_ALIGN
_2E_ALIGN:
nop 
calli _2E_ALIGN_doer
.balign bee_word_bytes
_2E_ALIGN_body:
.word _2E_ALIGN_defer - .
.set _2E_ALIGN_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set _2E_ALIGN_doer, DEFER_does
.balign bee_word_bytes
.byte 0x7 
.ascii ".CALIGN"
.balign bee_word_bytes, 0x20 
.word _2E_ALIGN - .
.word _2E_CALIGN_compilation
.word _2E_CALIGN_info 
.global _2E_CALIGN
_2E_CALIGN:
nop 
calli _2E_CALIGN_doer
.balign bee_word_bytes
_2E_CALIGN_body:
.word _2E_CALIGN_defer - .
.set _2E_CALIGN_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set _2E_CALIGN_doer, DEFER_does
.balign bee_word_bytes
.byte 0xB 
.ascii ".REL-OFFSET"
.balign bee_word_bytes, 0x20 
.word _2E_CALIGN - .
.word _2E_REL_2D_OFFSET_compilation
.word _2E_REL_2D_OFFSET_info 
.global _2E_REL_2D_OFFSET
_2E_REL_2D_OFFSET:
nop 
calli _2E_REL_2D_OFFSET_doer
.balign bee_word_bytes
_2E_REL_2D_OFFSET_body:
.word _2E_REL_2D_OFFSET_defer - .
.set _2E_REL_2D_OFFSET_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.set _2E_REL_2D_OFFSET_doer, DEFER_does
.balign bee_word_bytes
.byte 0x4 
.ascii ".NOP"
.balign bee_word_bytes, 0x20 
.word _2E_REL_2D_OFFSET - .
.word _2E_NOP_compilation
.word _2E_NOP_info 
.global _2E_NOP
_2E_NOP:
nop 
calli _2E_NOP_doer
.balign bee_word_bytes
_2E_NOP_body:
.word _2E_NOP_defer - .
.set _2E_NOP_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.set _2E_NOP_doer, DEFER_does
.balign bee_word_bytes
.byte 0x6 
.ascii ".ALLOT"
.balign bee_word_bytes, 0x20 
.word _2E_NOP - .
.word _2E_ALLOT_compilation
.word _2E_ALLOT_info 
.global _2E_ALLOT
_2E_ALLOT:
nop 
calli _2E_ALLOT_doer
.balign bee_word_bytes
_2E_ALLOT_body:
.word _2E_ALLOT_defer - .
.set _2E_ALLOT_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set _2E_ALLOT_doer, DEFER_does
.balign bee_word_bytes
.byte 0xC 
.ascii ".ALLOT-CELLS"
.balign bee_word_bytes, 0x20 
.word _2E_ALLOT - .
.word _2E_ALLOT_2D_CELLS_compilation
.word _2E_ALLOT_2D_CELLS_info 
.global _2E_ALLOT_2D_CELLS
_2E_ALLOT_2D_CELLS:
nop 
calli _2E_ALLOT_2D_CELLS_doer
.balign bee_word_bytes
_2E_ALLOT_2D_CELLS_body:
.word _2E_ALLOT_2D_CELLS_defer - .
.set _2E_ALLOT_2D_CELLS_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.set _2E_ALLOT_2D_CELLS_doer, DEFER_does
.balign bee_word_bytes
.byte 0x5 
.ascii ".WORD"
.balign bee_word_bytes, 0x20 
.word _2E_ALLOT_2D_CELLS - .
.word _2E_WORD_compilation
.word _2E_WORD_info 
.global _2E_WORD
_2E_WORD:
nop 
calli _2E_WORD_doer
.balign bee_word_bytes
_2E_WORD_body:
.word _2E_WORD_defer - .
.set _2E_WORD_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set _2E_WORD_doer, DEFER_does
.balign bee_word_bytes
.byte 0x5 
.ascii ".BYTE"
.balign bee_word_bytes, 0x20 
.word _2E_WORD - .
.word _2E_BYTE_compilation
.word _2E_BYTE_info 
.global _2E_BYTE
_2E_BYTE:
nop 
calli _2E_BYTE_doer
.balign bee_word_bytes
_2E_BYTE_body:
.word _2E_BYTE_defer - .
.set _2E_BYTE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set _2E_BYTE_doer, DEFER_does
.balign bee_word_bytes
.byte 0x7 
.ascii ".STRING"
.balign bee_word_bytes, 0x20 
.word _2E_BYTE - .
.word _2E_STRING_compilation
.word _2E_STRING_info 
.global _2E_STRING
_2E_STRING:
nop 
calli _2E_STRING_doer
.balign bee_word_bytes
_2E_STRING_body:
.word _2E_STRING_defer - .
.set _2E_STRING_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set _2E_STRING_doer, DEFER_does
.balign bee_word_bytes
.byte 0x6 
.ascii ".PUSHI"
.balign bee_word_bytes, 0x20 
.word _2E_STRING - .
.word _2E_PUSHI_compilation
.word _2E_PUSHI_info 
.global _2E_PUSHI
_2E_PUSHI:
nop 
calli _2E_PUSHI_doer
.balign bee_word_bytes
_2E_PUSHI_body:
.word _2E_PUSHI_defer - .
.set _2E_PUSHI_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set _2E_PUSHI_doer, DEFER_does
.balign bee_word_bytes
.byte 0x9 
.ascii ".PUSHRELI"
.balign bee_word_bytes, 0x20 
.word _2E_PUSHI - .
.word _2E_PUSHRELI_compilation
.word _2E_PUSHRELI_info 
.global _2E_PUSHRELI
_2E_PUSHRELI:
nop 
calli _2E_PUSHRELI_doer
.balign bee_word_bytes
_2E_PUSHRELI_body:
.word _2E_PUSHRELI_defer - .
.set _2E_PUSHRELI_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.set _2E_PUSHRELI_doer, DEFER_does
.balign bee_word_bytes
.byte 0x5 
.ascii ".PUSH"
.balign bee_word_bytes, 0x20 
.word _2E_PUSHRELI - .
.word _2E_PUSH_compilation
.word _2E_PUSH_info 
.global _2E_PUSH
_2E_PUSH:
nop 
calli _2E_PUSH_doer
.balign bee_word_bytes
_2E_PUSH_body:
.word _2E_PUSH_defer - .
.set _2E_PUSH_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set _2E_PUSH_doer, DEFER_does
.balign bee_word_bytes
.byte 0x6 
.ascii ".LABEL"
.balign bee_word_bytes, 0x20 
.word _2E_PUSH - .
.word _2E_LABEL_compilation
.word _2E_LABEL_info 
.global _2E_LABEL
_2E_LABEL:
nop 
calli _2E_LABEL_doer
.balign bee_word_bytes
_2E_LABEL_body:
.word _2E_LABEL_defer - .
.set _2E_LABEL_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set _2E_LABEL_doer, DEFER_does
.balign bee_word_bytes
.byte 0xA 
.ascii ".LABEL-DEF"
.balign bee_word_bytes, 0x20 
.word _2E_LABEL - .
.word _2E_LABEL_2D_DEF_compilation
.word _2E_LABEL_2D_DEF_info 
.global _2E_LABEL_2D_DEF
_2E_LABEL_2D_DEF:
nop 
calli _2E_LABEL_2D_DEF_doer
.balign bee_word_bytes
_2E_LABEL_2D_DEF_body:
.word _2E_LABEL_2D_DEF_defer - .
.set _2E_LABEL_2D_DEF_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.set _2E_LABEL_2D_DEF_doer, DEFER_does
.balign bee_word_bytes
.byte 0xF 
.ascii ".BODY-LABEL-DEF"
.balign bee_word_bytes, 0x20 
.word _2E_LABEL_2D_DEF - .
.word _2E_BODY_2D_LABEL_2D_DEF_compilation
.word _2E_BODY_2D_LABEL_2D_DEF_info 
.global _2E_BODY_2D_LABEL_2D_DEF
_2E_BODY_2D_LABEL_2D_DEF:
nop 
calli _2E_BODY_2D_LABEL_2D_DEF_doer
.balign bee_word_bytes
_2E_BODY_2D_LABEL_2D_DEF_body:
.word _2E_BODY_2D_LABEL_2D_DEF_defer - .
.set _2E_BODY_2D_LABEL_2D_DEF_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.set _2E_BODY_2D_LABEL_2D_DEF_doer, DEFER_does
.balign bee_word_bytes
.byte 0x7 
.ascii ".BRANCH"
.balign bee_word_bytes, 0x20 
.word _2E_BODY_2D_LABEL_2D_DEF - .
.word _2E_BRANCH_compilation
.word _2E_BRANCH_info 
.global _2E_BRANCH
_2E_BRANCH:
nop 
calli _2E_BRANCH_doer
.balign bee_word_bytes
_2E_BRANCH_body:
.word _2E_BRANCH_defer - .
.set _2E_BRANCH_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set _2E_BRANCH_doer, DEFER_does
.balign bee_word_bytes
.byte 0x3 
.ascii ".IF"
.balign bee_word_bytes, 0x20 
.word _2E_BRANCH - .
.word _2E_IF_compilation
.word _2E_IF_info 
.global _2E_IF
_2E_IF:
nop 
calli _2E_IF_doer
.balign bee_word_bytes
_2E_IF_body:
.word _2E_IF_defer - .
.set _2E_IF_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.set _2E_IF_doer, DEFER_does
.balign bee_word_bytes
.byte 0x4 
.ascii ".RET"
.balign bee_word_bytes, 0x20 
.word _2E_IF - .
.word _2E_RET_compilation
.word _2E_RET_info 
.global _2E_RET
_2E_RET:
nop 
calli _2E_RET_doer
.balign bee_word_bytes
_2E_RET_body:
.word _2E_RET_defer - .
.set _2E_RET_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.set _2E_RET_doer, DEFER_does
.balign bee_word_bytes
.byte 0x11 
.ascii ".IMMEDIATE-METHOD"
.balign bee_word_bytes, 0x20 
.word _2E_RET - .
.word _2E_IMMEDIATE_2D_METHOD_compilation
.word _2E_IMMEDIATE_2D_METHOD_info 
.global _2E_IMMEDIATE_2D_METHOD
_2E_IMMEDIATE_2D_METHOD:
nop 
calli _2E_IMMEDIATE_2D_METHOD_doer
.balign bee_word_bytes
_2E_IMMEDIATE_2D_METHOD_body:
.word _2E_IMMEDIATE_2D_METHOD_defer - .
.set _2E_IMMEDIATE_2D_METHOD_info, 0 | 0 | 0 | (17 <<_name_length_bits) | 0x0 
.set _2E_IMMEDIATE_2D_METHOD_doer, DEFER_does
.balign bee_word_bytes
.byte 0xF 
.ascii ".COMPILE-METHOD"
.balign bee_word_bytes, 0x20 
.word _2E_IMMEDIATE_2D_METHOD - .
.word _2E_COMPILE_2D_METHOD_compilation
.word _2E_COMPILE_2D_METHOD_info 
.global _2E_COMPILE_2D_METHOD
_2E_COMPILE_2D_METHOD:
nop 
calli _2E_COMPILE_2D_METHOD_doer
.balign bee_word_bytes
_2E_COMPILE_2D_METHOD_body:
.word _2E_COMPILE_2D_METHOD_defer - .
.set _2E_COMPILE_2D_METHOD_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.set _2E_COMPILE_2D_METHOD_doer, DEFER_does
.balign bee_word_bytes
.byte 0x14 
.ascii ".CALL-COMPILE-METHOD"
.balign bee_word_bytes, 0x20 
.word _2E_COMPILE_2D_METHOD - .
.word _2E_CALL_2D_COMPILE_2D_METHOD_compilation
.word _2E_CALL_2D_COMPILE_2D_METHOD_info 
.global _2E_CALL_2D_COMPILE_2D_METHOD
_2E_CALL_2D_COMPILE_2D_METHOD:
nop 
calli _2E_CALL_2D_COMPILE_2D_METHOD_doer
.balign bee_word_bytes
_2E_CALL_2D_COMPILE_2D_METHOD_body:
.word _2E_CALL_2D_COMPILE_2D_METHOD_defer - .
.set _2E_CALL_2D_COMPILE_2D_METHOD_info, 0 | 0 | 0 | (20 <<_name_length_bits) | 0x0 
.set _2E_CALL_2D_COMPILE_2D_METHOD_doer, DEFER_does
.balign bee_word_bytes
.byte 0xD 
.ascii ".INLINE-COUNT"
.balign bee_word_bytes, 0x20 
.word _2E_CALL_2D_COMPILE_2D_METHOD - .
.word _2E_INLINE_2D_COUNT_compilation
.word _2E_INLINE_2D_COUNT_info 
.global _2E_INLINE_2D_COUNT
_2E_INLINE_2D_COUNT:
nop 
calli _2E_INLINE_2D_COUNT_doer
.balign bee_word_bytes
_2E_INLINE_2D_COUNT_body:
.word _2E_INLINE_2D_COUNT_defer - .
.set _2E_INLINE_2D_COUNT_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.set _2E_INLINE_2D_COUNT_doer, DEFER_does
.balign bee_word_bytes
.byte 0xD 
.ascii ".CREATED-CODE"
.balign bee_word_bytes, 0x20 
.word _2E_INLINE_2D_COUNT - .
.word _2E_CREATED_2D_CODE_compilation
.word _2E_CREATED_2D_CODE_info 
.global _2E_CREATED_2D_CODE
_2E_CREATED_2D_CODE:
nop 
calli _2E_CREATED_2D_CODE_doer
.balign bee_word_bytes
_2E_CREATED_2D_CODE_body:
.word _2E_CREATED_2D_CODE_defer - .
.set _2E_CREATED_2D_CODE_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.set _2E_CREATED_2D_CODE_doer, DEFER_does
.balign bee_word_bytes
.byte 0x10 
.ascii ".PUSHRELI-SYMBOL"
.balign bee_word_bytes, 0x20 
.word _2E_CREATED_2D_CODE - .
.word _2E_PUSHRELI_2D_SYMBOL_compilation
.word _2E_PUSHRELI_2D_SYMBOL_info 
.global _2E_PUSHRELI_2D_SYMBOL
_2E_PUSHRELI_2D_SYMBOL:
nop 
calli _2E_PUSHRELI_2D_SYMBOL_doer
.balign bee_word_bytes
_2E_PUSHRELI_2D_SYMBOL_body:
.word _2E_PUSHRELI_2D_SYMBOL_defer - .
.set _2E_PUSHRELI_2D_SYMBOL_info, 0 | 0 | 0 | (16 <<_name_length_bits) | 0x0 
.set _2E_PUSHRELI_2D_SYMBOL_doer, DEFER_does
.balign bee_word_bytes
.byte 0x2 
.ascii "DP"
.balign bee_word_bytes, 0x20 
.word _2E_PUSHRELI_2D_SYMBOL - .
.word DP_compilation
.word DP_info 
.global DP
DP:
nop 
calli DP_doer
.balign bee_word_bytes
DP_body:
.word 0 
.set DP_info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.set DP_doer, VALUE_does
.balign bee_word_bytes
.byte 0x4 
.ascii "HERE"
.balign bee_word_bytes, 0x20 
.word DP - .
.word HERE_compilation
.word HERE_info 
.global HERE
HERE:
calli DP
load
ret
.set HERE_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "RAW-ALLOT"
.balign bee_word_bytes, 0x20 
.word HERE - .
.word RAW_2D_ALLOT_compilation
.word RAW_2D_ALLOT_info 
.global RAW_2D_ALLOT
RAW_2D_ALLOT:
calli HERE
pushi 1 # 0x1 
dup
calli ERASE
calli DP
calli _2B__21_
ret
.set RAW_2D_ALLOT_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "ALLOT"
.balign bee_word_bytes, 0x20 
.word RAW_2D_ALLOT - .
.word ALLOT_compilation
.word ALLOT_info 
.global ALLOT
ALLOT:
pushi 0 # 0x0 
dup
calli _2E_ALLOT
calli RAW_2D_ALLOT
ret
.set ALLOT_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "ALLOT-CELLS"
.balign bee_word_bytes, 0x20 
.word ALLOT - .
.word ALLOT_2D_CELLS_compilation
.word ALLOT_2D_CELLS_info 
.global ALLOT_2D_CELLS
ALLOT_2D_CELLS:
pushi 0 # 0x0 
dup
calli _2E_ALLOT_2D_CELLS
calli CELLS
calli RAW_2D_ALLOT
ret
.set ALLOT_2D_CELLS_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "ROOTDP"
.balign bee_word_bytes, 0x20 
.word ALLOT_2D_CELLS - .
.word ROOTDP_compilation
.word ROOTDP_info 
.global ROOTDP
ROOTDP:
nop 
calli ROOTDP_doer
.balign bee_word_bytes
ROOTDP_body:
.ds.b 1 * bee_word_bytes
.set ROOTDP_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set ROOTDP_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x7 
.ascii "ALIGNED"
.balign bee_word_bytes, 0x20 
.word ROOTDP - .
.word ALIGNED_compilation
.word ALIGNED_info 
.global ALIGNED
ALIGNED:
calli CELL_2B_
neg
not
calli _2D_CELL
and
ret
.set ALIGNED_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "ALIGN"
.balign bee_word_bytes, 0x20 
.word ALIGNED - .
.word ALIGN_compilation
.word ALIGN_info 
.global ALIGN
ALIGN:
calli _2E_ALIGN
calli HERE
calli ALIGNED
calli DP
store
ret
.set ALIGN_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "RAW,"
.balign bee_word_bytes, 0x20 
.word ALIGN - .
.word RAW_2C__compilation
.word RAW_2C__info 
.global RAW_2C_
RAW_2C_:
calli HERE
word_bytes
calli RAW_2D_ALLOT
store
ret
.set RAW_2C__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii ","
.balign bee_word_bytes, 0x20 
.word RAW_2C_ - .
.word _2C__compilation
.word _2C__info 
.global _2C_
_2C_:
pushi 0 # 0x0 
dup
calli _2E_WORD
calli RAW_2C_
ret
.set _2C__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "RAW-C,"
.balign bee_word_bytes, 0x20 
.word _2C_ - .
.word RAW_2D_C_2C__compilation
.word RAW_2D_C_2C__info 
.global RAW_2D_C_2C_
RAW_2D_C_2C_:
calli HERE
calli _2B_CHAR
calli RAW_2D_ALLOT
store1
ret
.set RAW_2D_C_2C__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "C,"
.balign bee_word_bytes, 0x20 
.word RAW_2D_C_2C_ - .
.word C_2C__compilation
.word C_2C__info 
.global C_2C_
C_2C_:
pushi 0 # 0x0 
dup
calli _2E_BYTE
calli RAW_2D_C_2C_
ret
.set C_2C__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "CALIGN"
.balign bee_word_bytes, 0x20 
.word C_2C_ - .
.word CALIGN_compilation
.word CALIGN_info 
.global CALIGN
CALIGN:
calli HERE
pushi 0 # 0x0 
dup
calli ALIGNED
calli _3E__2D__3C_
pushi 0 
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L1621f
jumpi .L1617f
.L1621f:
.L1623b:
pushi 0 # 0x0 
dup
calli RAW_2D_C_2C_
calli _28_LOOP_29_
jumpzi .L1623b
.L1617f:
.L1622f:
calli UNLOOP
calli _2E_CALIGN
ret
.set CALIGN_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "ADDRESS!"
.balign bee_word_bytes, 0x20 
.word CALIGN - .
.word ADDRESS_21__compilation
.word ADDRESS_21__info 
.global ADDRESS_21_
ADDRESS_21_:
store
ret
.set ADDRESS_21__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii ">REL"
.balign bee_word_bytes, 0x20 
.word ADDRESS_21_ - .
.word _3E_REL_compilation
.word _3E_REL_info 
.global _3E_REL
_3E_REL:
pushi 0 # 0x0 
dup
jumpzi .L1644f
calli _3E__2D__3C_
jumpi .L1646f
.L1644f:
calli NIP
.L1646f:
ret
.set _3E_REL_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "RAW-REL,"
.balign bee_word_bytes, 0x20 
.word _3E_REL - .
.word RAW_2D_REL_2C__compilation
.word RAW_2D_REL_2C__info 
.global RAW_2D_REL_2C_
RAW_2D_REL_2C_:
calli HERE
pushi 0 # 0x0 
swap
calli _3E_REL
calli RAW_2C_
ret
.set RAW_2D_REL_2C__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "REL,"
.balign bee_word_bytes, 0x20 
.word RAW_2D_REL_2C_ - .
.word REL_2C__compilation
.word REL_2C__info 
.global REL_2C_
REL_2C_:
pushi 0 # 0x0 
dup
calli RAW_2D_REL_2C_
calli _2E_REL_2D_OFFSET
ret
.set REL_2C__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "REL@"
.balign bee_word_bytes, 0x20 
.word REL_2C_ - .
.word REL_40__compilation
.word REL_40__info 
.global REL_40_
REL_40_:
pushi 0 # 0x0 
dup
load
calli _3F_DUP
jumpzi .L1677f
add
jumpi .L1679f
.L1677f:
pop
pushi 0 
.L1679f:
ret
.set REL_40__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "REL!"
.balign bee_word_bytes, 0x20 
.word REL_40_ - .
.word REL_21__compilation
.word REL_21__info 
.global REL_21_
REL_21_:
pushi 0 # 0x0 
dup
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli _3E_REL
pushi 0 # 0x0 
swap
store
ret
.set REL_21__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "STATE"
.balign bee_word_bytes, 0x20 
.word REL_21_ - .
.word STATE_compilation
.word STATE_info 
.global STATE
STATE:
nop 
calli STATE_doer
.balign bee_word_bytes
STATE_body:
.ds.b 1 * bee_word_bytes
.set STATE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set STATE_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x1 
.ascii "["
.balign bee_word_bytes, 0x20 
.word STATE - .
.word _5B__compilation
.word _5B__info 
.global _5B_
_5B_:
pushi 0 
calli STATE
store
ret
.set _5B__compilation, (2 * bee_word_bytes)
.set _5B__info, _immediate_bit | _compiling_bit | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "]"
.balign bee_word_bytes, 0x20 
.word _5B_ - .
.word _5D__compilation
.word _5D__info 
.global _5D_
_5D_:
pushi 1 
calli STATE
store
ret
.set _5D__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "#ORDER"
.balign bee_word_bytes, 0x20 
.word _5D_ - .
.word _23_ORDER_compilation
.word _23_ORDER_info 
.global _23_ORDER
_23_ORDER:
nop 
calli _23_ORDER_doer
.balign bee_word_bytes
_23_ORDER_body:
.ds.b 1 * bee_word_bytes
.set _23_ORDER_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set _23_ORDER_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x7 
.ascii "CONTEXT"
.balign bee_word_bytes, 0x20 
.word _23_ORDER - .
.word CONTEXT_compilation
.word CONTEXT_info 
.global CONTEXT
CONTEXT:
nop 
calli CONTEXT_doer
.balign bee_word_bytes
CONTEXT_body:
.ds.b 8 * bee_word_bytes
.set CONTEXT_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set CONTEXT_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x7 
.ascii "CURRENT"
.balign bee_word_bytes, 0x20 
.word CONTEXT - .
.word CURRENT_compilation
.word CURRENT_info 
.global CURRENT
CURRENT:
nop 
calli CURRENT_doer
.balign bee_word_bytes
CURRENT_body:
.ds.b 1 * bee_word_bytes
.set CURRENT_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set CURRENT_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0xB 
.ascii "GET-CURRENT"
.balign bee_word_bytes, 0x20 
.word CURRENT - .
.word GET_2D_CURRENT_compilation
.word GET_2D_CURRENT_info 
.global GET_2D_CURRENT
GET_2D_CURRENT:
calli CURRENT
load
ret
.set GET_2D_CURRENT_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "SET-CURRENT"
.balign bee_word_bytes, 0x20 
.word GET_2D_CURRENT - .
.word SET_2D_CURRENT_compilation
.word SET_2D_CURRENT_info 
.global SET_2D_CURRENT
SET_2D_CURRENT:
calli CURRENT
store
ret
.set SET_2D_CURRENT_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "LAST"
.balign bee_word_bytes, 0x20 
.word SET_2D_CURRENT - .
.word LAST_compilation
.word LAST_info 
.global LAST
LAST:
calli GET_2D_CURRENT
calli REL_40_
ret
.set LAST_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii ">LINK"
.balign bee_word_bytes, 0x20 
.word LAST - .
.word _3E_LINK_compilation
.word _3E_LINK_info 
.global _3E_LINK
_3E_LINK:
pushi 3 
calli CELLS
neg
add
ret
.set _3E_LINK_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii ">COMPILE"
.balign bee_word_bytes, 0x20 
.word _3E_LINK - .
.word _3E_COMPILE_compilation
.word _3E_COMPILE_info 
.global _3E_COMPILE
_3E_COMPILE:
pushi 2 
calli CELLS
neg
add
ret
.set _3E_COMPILE_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii ">INFO"
.balign bee_word_bytes, 0x20 
.word _3E_COMPILE - .
.word _3E_INFO_compilation
.word _3E_INFO_info 
.global _3E_INFO
_3E_INFO:
calli CELL_2D_
ret
.set _3E_INFO_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii ">NAME"
.balign bee_word_bytes, 0x20 
.word _3E_INFO - .
.word _3E_NAME_compilation
.word _3E_NAME_info 
.global _3E_NAME
_3E_NAME:
pushi 0 # 0x0 
dup
calli _3E_INFO
word_bytes
neg
not
add
load1
pushi 31 
and
not
neg
calli ALIGNED
pushi 0 # 0x0 
swap
calli _3E_LINK
calli _3E__2D__3C_
ret
.set _3E_NAME_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "IMMEDIATE-BIT"
.balign bee_word_bytes, 0x20 
.word _3E_NAME - .
.word IMMEDIATE_2D_BIT_compilation
.word IMMEDIATE_2D_BIT_info 
.global IMMEDIATE_2D_BIT
IMMEDIATE_2D_BIT:
calli TOP_2D_BIT_2D_SET
ret
.set IMMEDIATE_2D_BIT_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "SET-IMMEDIATE"
.balign bee_word_bytes, 0x20 
.word IMMEDIATE_2D_BIT - .
.word SET_2D_IMMEDIATE_compilation
.word SET_2D_IMMEDIATE_info 
.global SET_2D_IMMEDIATE
SET_2D_IMMEDIATE:
calli LAST
calli _3E_INFO
pushi 0 # 0x0 
dup
load
calli IMMEDIATE_2D_BIT
or
pushi 0 # 0x0 
swap
store
ret
.set SET_2D_IMMEDIATE_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "IMMEDIATE"
.balign bee_word_bytes, 0x20 
.word SET_2D_IMMEDIATE - .
.word IMMEDIATE_compilation
.word IMMEDIATE_info 
.global IMMEDIATE
IMMEDIATE:
calli SET_2D_IMMEDIATE
calli LAST
calli _3E_NAME
calli _2E_IMMEDIATE_2D_METHOD
calli LAST
pushi 0 # 0x0 
dup
calli _3E_COMPILE
calli REL_21_
ret
.set IMMEDIATE_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "COMPILING-BIT"
.balign bee_word_bytes, 0x20 
.word IMMEDIATE - .
.word COMPILING_2D_BIT_compilation
.word COMPILING_2D_BIT_info 
.global COMPILING_2D_BIT
COMPILING_2D_BIT:
calli TOP_2D_BIT_2D_SET
pushi 1 
rshift
ret
.set COMPILING_2D_BIT_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "COMPILING"
.balign bee_word_bytes, 0x20 
.word COMPILING_2D_BIT - .
.word COMPILING_compilation
.word COMPILING_info 
.global COMPILING
COMPILING:
calli LAST
calli _3E_INFO
pushi 0 # 0x0 
dup
load
calli COMPILING_2D_BIT
or
pushi 0 # 0x0 
swap
store
ret
.set COMPILING_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "SMUDGE-BIT"
.balign bee_word_bytes, 0x20 
.word COMPILING - .
.word SMUDGE_2D_BIT_compilation
.word SMUDGE_2D_BIT_info 
.global SMUDGE_2D_BIT
SMUDGE_2D_BIT:
calli TOP_2D_BIT_2D_SET
pushi 2 
rshift
ret
.set SMUDGE_2D_BIT_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "SMUDGE!"
.balign bee_word_bytes, 0x20 
.word SMUDGE_2D_BIT - .
.word SMUDGE_21__compilation
.word SMUDGE_21__info 
.global SMUDGE_21_
SMUDGE_21_:
calli _3E_INFO
calli TUCK
load
calli SMUDGE_2D_BIT
pushi 0 # 0x0 
dup
not
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
and
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
and
or
pushi 0 # 0x0 
swap
store
ret
.set SMUDGE_21__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "SMUDGE"
.balign bee_word_bytes, 0x20 
.word SMUDGE_21_ - .
.word SMUDGE_compilation
.word SMUDGE_info 
.global SMUDGE
SMUDGE:
calli LAST
calli SMUDGE_21_
ret
.set SMUDGE_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "PAD"
.balign bee_word_bytes, 0x20 
.word SMUDGE - .
.word PAD_compilation
.word PAD_info 
.global PAD
PAD:
nop 
calli PAD_doer
.balign bee_word_bytes
PAD_body:
.ds.b 256 
.set PAD_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.set PAD_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x5 
.ascii "TOKEN"
.balign bee_word_bytes, 0x20 
.word PAD - .
.word TOKEN_compilation
.word TOKEN_info 
.global TOKEN
TOKEN:
nop 
calli TOKEN_doer
.balign bee_word_bytes
TOKEN_body:
.ds.b 256 
.set TOKEN_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set TOKEN_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x9 
.ascii "TOKEN-END"
.balign bee_word_bytes, 0x20 
.word TOKEN - .
.word TOKEN_2D_END_compilation
.word TOKEN_2D_END_info 
.global TOKEN_2D_END
TOKEN_2D_END:
calli TOKEN
pushi 256 
add
ret
.set TOKEN_2D_END_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "S\"B"
.balign bee_word_bytes, 0x20 
.word TOKEN_2D_END - .
.word S_22_B_compilation
.word S_22_B_info 
.global S_22_B
S_22_B:
nop 
calli S_22_B_doer
.balign bee_word_bytes
S_22_B_body:
.ds.b 256 
.set S_22_B_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.set S_22_B_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x7 
.ascii "SCRATCH"
.balign bee_word_bytes, 0x20 
.word S_22_B - .
.word SCRATCH_compilation
.word SCRATCH_info 
.global SCRATCH
SCRATCH:
nop 
calli SCRATCH_doer
.balign bee_word_bytes
SCRATCH_body:
.ds.b 256 
.set SCRATCH_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set SCRATCH_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x5 
.ascii "CODE!"
.balign bee_word_bytes, 0x20 
.word SCRATCH - .
.word CODE_21__compilation
.word CODE_21__info 
.global CODE_21_
CODE_21_:
store
ret
.set CODE_21__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CODE,"
.balign bee_word_bytes, 0x20 
.word CODE_21_ - .
.word CODE_2C__compilation
.word CODE_2C__info 
.global CODE_2C_
CODE_2C_:
calli _2C_
ret
.set CODE_2C__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "CODE-MOVE"
.balign bee_word_bytes, 0x20 
.word CODE_2C_ - .
.word CODE_2D_MOVE_compilation
.word CODE_2D_MOVE_info 
.global CODE_2D_MOVE
CODE_2D_MOVE:
calli CELLS
pushi 1 # 0x1 
dup
add
pushi 0 # 0x0 
swap
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L2112f
jumpi .L2108f
.L2112f:
.L2114b:
pushi 0 # 0x0 
dup
load
dups
calli CODE_21_
calli CELL_2B_
word_bytes
calli _28__2B_LOOP_29_
jumpzi .L2114b
.L2108f:
.L2113f:
calli UNLOOP
pop
ret
.set CODE_2D_MOVE_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "OFFSET"
.balign bee_word_bytes, 0x20 
.word CODE_2D_MOVE - .
.word OFFSET_compilation
.word OFFSET_info 
.global OFFSET
OFFSET:
calli _3E__2D__3C_
ret
.set OFFSET_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "OP_CALLI"
.balign bee_word_bytes, 0x20 
.word OFFSET - .
.word OP_5F_CALLI_compilation
.word OP_5F_CALLI_info 
.global OP_5F_CALLI
OP_5F_CALLI:
pushi 0 
ret
.set OP_5F_CALLI_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "OP_PUSHI"
.balign bee_word_bytes, 0x20 
.word OP_5F_CALLI - .
.word OP_5F_PUSHI_compilation
.word OP_5F_PUSHI_info 
.global OP_5F_PUSHI
OP_5F_PUSHI:
pushi 1 
ret
.set OP_5F_PUSHI_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "OP_PUSHRELI"
.balign bee_word_bytes, 0x20 
.word OP_5F_PUSHI - .
.word OP_5F_PUSHRELI_compilation
.word OP_5F_PUSHRELI_info 
.global OP_5F_PUSHRELI
OP_5F_PUSHRELI:
pushi 2 
ret
.set OP_5F_PUSHRELI_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "OP2_JUMPI"
.balign bee_word_bytes, 0x20 
.word OP_5F_PUSHRELI - .
.word OP_32__5F_JUMPI_compilation
.word OP_32__5F_JUMPI_info 
.global OP_32__5F_JUMPI
OP_32__5F_JUMPI:
word_bytes
pushi 4 
eq
neg
jumpzi .L2162f
pushi 3 
jumpi .L2164f
.L2162f:
pushi 3 
.L2164f:
ret
.set OP_32__5F_JUMPI_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "OP2_JUMPZI"
.balign bee_word_bytes, 0x20 
.word OP_32__5F_JUMPI - .
.word OP_32__5F_JUMPZI_compilation
.word OP_32__5F_JUMPZI_info 
.global OP_32__5F_JUMPZI
OP_32__5F_JUMPZI:
word_bytes
pushi 4 
eq
neg
jumpzi .L2176f
pushi 7 
jumpi .L2178f
.L2176f:
pushi 4 
.L2178f:
ret
.set OP_32__5F_JUMPZI_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "OP2_TRAP"
.balign bee_word_bytes, 0x20 
.word OP_32__5F_JUMPZI - .
.word OP_32__5F_TRAP_compilation
.word OP_32__5F_TRAP_info 
.global OP_32__5F_TRAP
OP_32__5F_TRAP:
word_bytes
pushi 4 
eq
neg
jumpzi .L2190f
pushi 11 
jumpi .L2192f
.L2190f:
pushi 5 
.L2192f:
ret
.set OP_32__5F_TRAP_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "OP2_INSN"
.balign bee_word_bytes, 0x20 
.word OP_32__5F_TRAP - .
.word OP_32__5F_INSN_compilation
.word OP_32__5F_INSN_info 
.global OP_32__5F_INSN
OP_32__5F_INSN:
word_bytes
pushi 4 
eq
neg
jumpzi .L2204f
pushi 15 
jumpi .L2206f
.L2204f:
pushi 7 
.L2206f:
ret
.set OP_32__5F_INSN_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "OP1_SHIFT"
.balign bee_word_bytes, 0x20 
.word OP_32__5F_INSN - .
.word OP_31__5F_SHIFT_compilation
.word OP_31__5F_SHIFT_info 
.global OP_31__5F_SHIFT
OP_31__5F_SHIFT:
word_bytes
pushi 4 
eq
neg
jumpzi .L2218f
pushi 2 
jumpi .L2220f
.L2218f:
pushi 3 
.L2220f:
ret
.set OP_31__5F_SHIFT_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "OP2_SHIFT"
.balign bee_word_bytes, 0x20 
.word OP_31__5F_SHIFT - .
.word OP_32__5F_SHIFT_compilation
.word OP_32__5F_SHIFT_info 
.global OP_32__5F_SHIFT
OP_32__5F_SHIFT:
word_bytes
pushi 4 
eq
neg
jumpzi .L2232f
pushi 4 
jumpi .L2234f
.L2232f:
pushi 3 
.L2234f:
ret
.set OP_32__5F_SHIFT_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "OP1_MASK"
.balign bee_word_bytes, 0x20 
.word OP_32__5F_SHIFT - .
.word OP_31__5F_MASK_compilation
.word OP_31__5F_MASK_info 
.global OP_31__5F_MASK
OP_31__5F_MASK:
word_bytes
pushi 4 
eq
neg
jumpzi .L2246f
pushi 3 
jumpi .L2248f
.L2246f:
pushi 7 
.L2248f:
ret
.set OP_31__5F_MASK_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "OP2_MASK"
.balign bee_word_bytes, 0x20 
.word OP_31__5F_MASK - .
.word OP_32__5F_MASK_compilation
.word OP_32__5F_MASK_info 
.global OP_32__5F_MASK
OP_32__5F_MASK:
word_bytes
pushi 4 
eq
neg
jumpzi .L2260f
pushi 15 
jumpi .L2262f
.L2260f:
pushi 7 
.L2262f:
ret
.set OP_32__5F_MASK_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii ">OPCODE"
.balign bee_word_bytes, 0x20 
.word OP_32__5F_MASK - .
.word _3E_OPCODE_compilation
.word _3E_OPCODE_info 
.global _3E_OPCODE
_3E_OPCODE:
pushi 0 # 0x0 
swap
calli OP_31__5F_SHIFT
lshift
or
ret
.set _3E_OPCODE_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii ">OPCODE2"
.balign bee_word_bytes, 0x20 
.word _3E_OPCODE - .
.word _3E_OPCODE_32__compilation
.word _3E_OPCODE_32__info 
.global _3E_OPCODE_32_
_3E_OPCODE_32_:
pushi 0 # 0x0 
swap
calli OP_32__5F_SHIFT
lshift
or
ret
.set _3E_OPCODE_32__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "OPCODE>"
.balign bee_word_bytes, 0x20 
.word _3E_OPCODE_32_ - .
.word OPCODE_3E__compilation
.word OPCODE_3E__info 
.global OPCODE_3E_
OPCODE_3E_:
calli OP_31__5F_MASK
and
ret
.set OPCODE_3E__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "OPCODE2>"
.balign bee_word_bytes, 0x20 
.word OPCODE_3E_ - .
.word OPCODE_32__3E__compilation
.word OPCODE_32__3E__info 
.global OPCODE_32__3E_
OPCODE_32__3E_:
calli OP_32__5F_MASK
and
ret
.set OPCODE_32__3E__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_NOP"
.balign bee_word_bytes, 0x20 
.word OPCODE_32__3E_ - .
.word INSN_5F_NOP_compilation
.word INSN_5F_NOP_info 
.global INSN_5F_NOP
INSN_5F_NOP:
pushi 0 
ret
.set INSN_5F_NOP_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_NOT"
.balign bee_word_bytes, 0x20 
.word INSN_5F_NOP - .
.word INSN_5F_NOT_compilation
.word INSN_5F_NOT_info 
.global INSN_5F_NOT
INSN_5F_NOT:
pushi 1 
ret
.set INSN_5F_NOT_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_AND"
.balign bee_word_bytes, 0x20 
.word INSN_5F_NOT - .
.word INSN_5F_AND_compilation
.word INSN_5F_AND_info 
.global INSN_5F_AND
INSN_5F_AND:
pushi 2 
ret
.set INSN_5F_AND_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "INSN_OR"
.balign bee_word_bytes, 0x20 
.word INSN_5F_AND - .
.word INSN_5F_OR_compilation
.word INSN_5F_OR_info 
.global INSN_5F_OR
INSN_5F_OR:
pushi 3 
ret
.set INSN_5F_OR_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_XOR"
.balign bee_word_bytes, 0x20 
.word INSN_5F_OR - .
.word INSN_5F_XOR_compilation
.word INSN_5F_XOR_info 
.global INSN_5F_XOR
INSN_5F_XOR:
pushi 4 
ret
.set INSN_5F_XOR_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_LSHIFT"
.balign bee_word_bytes, 0x20 
.word INSN_5F_XOR - .
.word INSN_5F_LSHIFT_compilation
.word INSN_5F_LSHIFT_info 
.global INSN_5F_LSHIFT
INSN_5F_LSHIFT:
pushi 5 
ret
.set INSN_5F_LSHIFT_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_RSHIFT"
.balign bee_word_bytes, 0x20 
.word INSN_5F_LSHIFT - .
.word INSN_5F_RSHIFT_compilation
.word INSN_5F_RSHIFT_info 
.global INSN_5F_RSHIFT
INSN_5F_RSHIFT:
pushi 6 
ret
.set INSN_5F_RSHIFT_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "INSN_ARSHIFT"
.balign bee_word_bytes, 0x20 
.word INSN_5F_RSHIFT - .
.word INSN_5F_ARSHIFT_compilation
.word INSN_5F_ARSHIFT_info 
.global INSN_5F_ARSHIFT
INSN_5F_ARSHIFT:
pushi 7 
ret
.set INSN_5F_ARSHIFT_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_POP"
.balign bee_word_bytes, 0x20 
.word INSN_5F_ARSHIFT - .
.word INSN_5F_POP_compilation
.word INSN_5F_POP_info 
.global INSN_5F_POP
INSN_5F_POP:
pushi 8 
ret
.set INSN_5F_POP_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_DUP"
.balign bee_word_bytes, 0x20 
.word INSN_5F_POP - .
.word INSN_5F_DUP_compilation
.word INSN_5F_DUP_info 
.global INSN_5F_DUP
INSN_5F_DUP:
pushi 9 
ret
.set INSN_5F_DUP_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_SET"
.balign bee_word_bytes, 0x20 
.word INSN_5F_DUP - .
.word INSN_5F_SET_compilation
.word INSN_5F_SET_info 
.global INSN_5F_SET
INSN_5F_SET:
pushi 10 
ret
.set INSN_5F_SET_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "INSN_SWAP"
.balign bee_word_bytes, 0x20 
.word INSN_5F_SET - .
.word INSN_5F_SWAP_compilation
.word INSN_5F_SWAP_info 
.global INSN_5F_SWAP
INSN_5F_SWAP:
pushi 11 
ret
.set INSN_5F_SWAP_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "INSN_JUMP"
.balign bee_word_bytes, 0x20 
.word INSN_5F_SWAP - .
.word INSN_5F_JUMP_compilation
.word INSN_5F_JUMP_info 
.global INSN_5F_JUMP
INSN_5F_JUMP:
pushi 12 
ret
.set INSN_5F_JUMP_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INSN_JUMPZ"
.balign bee_word_bytes, 0x20 
.word INSN_5F_JUMP - .
.word INSN_5F_JUMPZ_compilation
.word INSN_5F_JUMPZ_info 
.global INSN_5F_JUMPZ
INSN_5F_JUMPZ:
pushi 13 
ret
.set INSN_5F_JUMPZ_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "INSN_CALL"
.balign bee_word_bytes, 0x20 
.word INSN_5F_JUMPZ - .
.word INSN_5F_CALL_compilation
.word INSN_5F_CALL_info 
.global INSN_5F_CALL
INSN_5F_CALL:
pushi 14 
ret
.set INSN_5F_CALL_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_RET"
.balign bee_word_bytes, 0x20 
.word INSN_5F_CALL - .
.word INSN_5F_RET_compilation
.word INSN_5F_RET_info 
.global INSN_5F_RET
INSN_5F_RET:
pushi 15 
ret
.set INSN_5F_RET_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "INSN_LOAD"
.balign bee_word_bytes, 0x20 
.word INSN_5F_RET - .
.word INSN_5F_LOAD_compilation
.word INSN_5F_LOAD_info 
.global INSN_5F_LOAD
INSN_5F_LOAD:
pushi 16 
ret
.set INSN_5F_LOAD_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INSN_STORE"
.balign bee_word_bytes, 0x20 
.word INSN_5F_LOAD - .
.word INSN_5F_STORE_compilation
.word INSN_5F_STORE_info 
.global INSN_5F_STORE
INSN_5F_STORE:
pushi 17 
ret
.set INSN_5F_STORE_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INSN_LOAD1"
.balign bee_word_bytes, 0x20 
.word INSN_5F_STORE - .
.word INSN_5F_LOAD_31__compilation
.word INSN_5F_LOAD_31__info 
.global INSN_5F_LOAD_31_
INSN_5F_LOAD_31_:
pushi 18 
ret
.set INSN_5F_LOAD_31__info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_STORE1"
.balign bee_word_bytes, 0x20 
.word INSN_5F_LOAD_31_ - .
.word INSN_5F_STORE_31__compilation
.word INSN_5F_STORE_31__info 
.global INSN_5F_STORE_31_
INSN_5F_STORE_31_:
pushi 19 
ret
.set INSN_5F_STORE_31__info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INSN_LOAD2"
.balign bee_word_bytes, 0x20 
.word INSN_5F_STORE_31_ - .
.word INSN_5F_LOAD_32__compilation
.word INSN_5F_LOAD_32__info 
.global INSN_5F_LOAD_32_
INSN_5F_LOAD_32_:
pushi 20 
ret
.set INSN_5F_LOAD_32__info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_STORE2"
.balign bee_word_bytes, 0x20 
.word INSN_5F_LOAD_32_ - .
.word INSN_5F_STORE_32__compilation
.word INSN_5F_STORE_32__info 
.global INSN_5F_STORE_32_
INSN_5F_STORE_32_:
pushi 21 
ret
.set INSN_5F_STORE_32__info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INSN_LOAD4"
.balign bee_word_bytes, 0x20 
.word INSN_5F_STORE_32_ - .
.word INSN_5F_LOAD_34__compilation
.word INSN_5F_LOAD_34__info 
.global INSN_5F_LOAD_34_
INSN_5F_LOAD_34_:
pushi 22 
ret
.set INSN_5F_LOAD_34__info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_STORE4"
.balign bee_word_bytes, 0x20 
.word INSN_5F_LOAD_34_ - .
.word INSN_5F_STORE_34__compilation
.word INSN_5F_STORE_34__info 
.global INSN_5F_STORE_34_
INSN_5F_STORE_34_:
pushi 23 
ret
.set INSN_5F_STORE_34__info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_NEG"
.balign bee_word_bytes, 0x20 
.word INSN_5F_STORE_34_ - .
.word INSN_5F_NEG_compilation
.word INSN_5F_NEG_info 
.global INSN_5F_NEG
INSN_5F_NEG:
pushi 24 
ret
.set INSN_5F_NEG_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_ADD"
.balign bee_word_bytes, 0x20 
.word INSN_5F_NEG - .
.word INSN_5F_ADD_compilation
.word INSN_5F_ADD_info 
.global INSN_5F_ADD
INSN_5F_ADD:
pushi 25 
ret
.set INSN_5F_ADD_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_MUL"
.balign bee_word_bytes, 0x20 
.word INSN_5F_ADD - .
.word INSN_5F_MUL_compilation
.word INSN_5F_MUL_info 
.global INSN_5F_MUL
INSN_5F_MUL:
pushi 26 
ret
.set INSN_5F_MUL_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_DIVMOD"
.balign bee_word_bytes, 0x20 
.word INSN_5F_MUL - .
.word INSN_5F_DIVMOD_compilation
.word INSN_5F_DIVMOD_info 
.global INSN_5F_DIVMOD
INSN_5F_DIVMOD:
pushi 27 
ret
.set INSN_5F_DIVMOD_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "INSN_UDIVMOD"
.balign bee_word_bytes, 0x20 
.word INSN_5F_DIVMOD - .
.word INSN_5F_UDIVMOD_compilation
.word INSN_5F_UDIVMOD_info 
.global INSN_5F_UDIVMOD
INSN_5F_UDIVMOD:
pushi 28 
ret
.set INSN_5F_UDIVMOD_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "INSN_EQ"
.balign bee_word_bytes, 0x20 
.word INSN_5F_UDIVMOD - .
.word INSN_5F_EQ_compilation
.word INSN_5F_EQ_info 
.global INSN_5F_EQ
INSN_5F_EQ:
pushi 29 
ret
.set INSN_5F_EQ_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "INSN_LT"
.balign bee_word_bytes, 0x20 
.word INSN_5F_EQ - .
.word INSN_5F_LT_compilation
.word INSN_5F_LT_info 
.global INSN_5F_LT
INSN_5F_LT:
pushi 30 
ret
.set INSN_5F_LT_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INSN_ULT"
.balign bee_word_bytes, 0x20 
.word INSN_5F_LT - .
.word INSN_5F_ULT_compilation
.word INSN_5F_ULT_info 
.global INSN_5F_ULT
INSN_5F_ULT:
pushi 31 
ret
.set INSN_5F_ULT_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INSN_PUSHR"
.balign bee_word_bytes, 0x20 
.word INSN_5F_ULT - .
.word INSN_5F_PUSHR_compilation
.word INSN_5F_PUSHR_info 
.global INSN_5F_PUSHR
INSN_5F_PUSHR:
pushi 32 
ret
.set INSN_5F_PUSHR_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "INSN_POPR"
.balign bee_word_bytes, 0x20 
.word INSN_5F_PUSHR - .
.word INSN_5F_POPR_compilation
.word INSN_5F_POPR_info 
.global INSN_5F_POPR
INSN_5F_POPR:
pushi 33 
ret
.set INSN_5F_POPR_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "INSN_DUPR"
.balign bee_word_bytes, 0x20 
.word INSN_5F_POPR - .
.word INSN_5F_DUPR_compilation
.word INSN_5F_DUPR_info 
.global INSN_5F_DUPR
INSN_5F_DUPR:
pushi 34 
ret
.set INSN_5F_DUPR_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INSN_CATCH"
.balign bee_word_bytes, 0x20 
.word INSN_5F_DUPR - .
.word INSN_5F_CATCH_compilation
.word INSN_5F_CATCH_info 
.global INSN_5F_CATCH
INSN_5F_CATCH:
pushi 35 
ret
.set INSN_5F_CATCH_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INSN_THROW"
.balign bee_word_bytes, 0x20 
.word INSN_5F_CATCH - .
.word INSN_5F_THROW_compilation
.word INSN_5F_THROW_info 
.global INSN_5F_THROW
INSN_5F_THROW:
pushi 36 
ret
.set INSN_5F_THROW_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INSN_BREAK"
.balign bee_word_bytes, 0x20 
.word INSN_5F_THROW - .
.word INSN_5F_BREAK_compilation
.word INSN_5F_BREAK_info 
.global INSN_5F_BREAK
INSN_5F_BREAK:
pushi 37 
ret
.set INSN_5F_BREAK_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xF 
.ascii "INSN_WORD_BYTES"
.balign bee_word_bytes, 0x20 
.word INSN_5F_BREAK - .
.word INSN_5F_WORD_5F_BYTES_compilation
.word INSN_5F_WORD_5F_BYTES_info 
.global INSN_5F_WORD_5F_BYTES
INSN_5F_WORD_5F_BYTES:
pushi 38 
ret
.set INSN_5F_WORD_5F_BYTES_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_GET_M0"
.balign bee_word_bytes, 0x20 
.word INSN_5F_WORD_5F_BYTES - .
.word INSN_5F_GET_5F_M_30__compilation
.word INSN_5F_GET_5F_M_30__info 
.global INSN_5F_GET_5F_M_30_
INSN_5F_GET_5F_M_30_:
pushi 39 
ret
.set INSN_5F_GET_5F_M_30__info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xE 
.ascii "INSN_GET_MSIZE"
.balign bee_word_bytes, 0x20 
.word INSN_5F_GET_5F_M_30_ - .
.word INSN_5F_GET_5F_MSIZE_compilation
.word INSN_5F_GET_5F_MSIZE_info 
.global INSN_5F_GET_5F_MSIZE
INSN_5F_GET_5F_MSIZE:
pushi 40 
ret
.set INSN_5F_GET_5F_MSIZE_info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xE 
.ascii "INSN_GET_SSIZE"
.balign bee_word_bytes, 0x20 
.word INSN_5F_GET_5F_MSIZE - .
.word INSN_5F_GET_5F_SSIZE_compilation
.word INSN_5F_GET_5F_SSIZE_info 
.global INSN_5F_GET_5F_SSIZE
INSN_5F_GET_5F_SSIZE:
pushi 41 
ret
.set INSN_5F_GET_5F_SSIZE_info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_GET_SP"
.balign bee_word_bytes, 0x20 
.word INSN_5F_GET_5F_SSIZE - .
.word INSN_5F_GET_5F_SP_compilation
.word INSN_5F_GET_5F_SP_info 
.global INSN_5F_GET_5F_SP
INSN_5F_GET_5F_SP:
pushi 42 
ret
.set INSN_5F_GET_5F_SP_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_SET_SP"
.balign bee_word_bytes, 0x20 
.word INSN_5F_GET_5F_SP - .
.word INSN_5F_SET_5F_SP_compilation
.word INSN_5F_SET_5F_SP_info 
.global INSN_5F_SET_5F_SP
INSN_5F_SET_5F_SP:
pushi 43 
ret
.set INSN_5F_SET_5F_SP_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xE 
.ascii "INSN_GET_DSIZE"
.balign bee_word_bytes, 0x20 
.word INSN_5F_SET_5F_SP - .
.word INSN_5F_GET_5F_DSIZE_compilation
.word INSN_5F_GET_5F_DSIZE_info 
.global INSN_5F_GET_5F_DSIZE
INSN_5F_GET_5F_DSIZE:
pushi 44 
ret
.set INSN_5F_GET_5F_DSIZE_info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_GET_DP"
.balign bee_word_bytes, 0x20 
.word INSN_5F_GET_5F_DSIZE - .
.word INSN_5F_GET_5F_DP_compilation
.word INSN_5F_GET_5F_DP_info 
.global INSN_5F_GET_5F_DP
INSN_5F_GET_5F_DP:
pushi 45 
ret
.set INSN_5F_GET_5F_DP_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "INSN_SET_DP"
.balign bee_word_bytes, 0x20 
.word INSN_5F_GET_5F_DP - .
.word INSN_5F_SET_5F_DP_compilation
.word INSN_5F_SET_5F_DP_info 
.global INSN_5F_SET_5F_DP
INSN_5F_SET_5F_DP:
pushi 46 
ret
.set INSN_5F_SET_5F_DP_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x13 
.ascii "INSN_GET_HANDLER_SP"
.balign bee_word_bytes, 0x20 
.word INSN_5F_SET_5F_DP - .
.word INSN_5F_GET_5F_HANDLER_5F_SP_compilation
.word INSN_5F_GET_5F_HANDLER_5F_SP_info 
.global INSN_5F_GET_5F_HANDLER_5F_SP
INSN_5F_GET_5F_HANDLER_5F_SP:
pushi 47 
ret
.set INSN_5F_GET_5F_HANDLER_5F_SP_info, 0 | 0 | 0 | (19 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "#INSTRUCTIONS"
.balign bee_word_bytes, 0x20 
.word INSN_5F_GET_5F_HANDLER_5F_SP - .
.word _23_INSTRUCTIONS_compilation
.word _23_INSTRUCTIONS_info 
.global _23_INSTRUCTIONS
_23_INSTRUCTIONS:
pushi 48 
ret
.set _23_INSTRUCTIONS_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "CALL"
.balign bee_word_bytes, 0x20 
.word _23_INSTRUCTIONS - .
.word CALL_compilation
.word CALL_info 
.global CALL
CALL:
calli _3E__2D__3C_
pushi 0 # 0x0 
swap
store
ret
.set CALL_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "NOP,"
.balign bee_word_bytes, 0x20 
.word CALL - .
.word NOP_2C__compilation
.word NOP_2C__info 
.global NOP_2C_
NOP_2C_:
calli INSN_5F_NOP
calli OP_32__5F_INSN
calli _3E_OPCODE_32_
calli RAW_2C_
ret
.set NOP_2C__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CALL,"
.balign bee_word_bytes, 0x20 
.word NOP_2C_ - .
.word CALL_2C__compilation
.word CALL_2C__info 
.global CALL_2C_
CALL_2C_:
calli HERE
pushi 0 # 0x0 
swap
calli OFFSET
calli CELL_2F_
calli OP_5F_CALLI
calli _3E_OPCODE
calli RAW_2C_
ret
.set CALL_2C__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "BRANCH,"
.balign bee_word_bytes, 0x20 
.word CALL_2C_ - .
.word BRANCH_2C__compilation
.word BRANCH_2C__info 
.global BRANCH_2C_
BRANCH_2C_:
pushi 0 
calli OP_32__5F_JUMPI
calli _3E_OPCODE_32_
calli RAW_2C_
ret
.set BRANCH_2C__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "IF,"
.balign bee_word_bytes, 0x20 
.word BRANCH_2C_ - .
.word IF_2C__compilation
.word IF_2C__info 
.global IF_2C_
IF_2C_:
pushi 0 
calli OP_32__5F_JUMPZI
calli _3E_OPCODE_32_
calli RAW_2C_
ret
.set IF_2C__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "PUSH,"
.balign bee_word_bytes, 0x20 
.word IF_2C_ - .
.word PUSH_2C__compilation
.word PUSH_2C__info 
.global PUSH_2C_
PUSH_2C_:
pushi 0 # 0x0 
dup
calli CELLS
calli CELL_2F_
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L2703f
calli OP_5F_PUSHI
calli _3E_OPCODE
calli RAW_2C_
jumpi .L2707f
.L2703f:
calli HERE
pushi 2 
calli CELLS
add
calli CALL_2C_
calli RAW_2C_
calli INSN_5F_POPR
calli OP_32__5F_INSN
calli _3E_OPCODE_32_
calli RAW_2C_
calli INSN_5F_LOAD
calli OP_32__5F_INSN
calli _3E_OPCODE_32_
calli RAW_2C_
.L2707f:
ret
.set PUSH_2C__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "PUSHREL,"
.balign bee_word_bytes, 0x20 
.word PUSH_2C_ - .
.word PUSHREL_2C__compilation
.word PUSHREL_2C__info 
.global PUSHREL_2C_
PUSHREL_2C_:
calli HERE
pushi 0 # 0x0 
swap
calli OFFSET
calli CELL_2F_
calli OP_5F_PUSHRELI
calli _3E_OPCODE
calli RAW_2C_
ret
.set PUSHREL_2C__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "@BRANCH"
.balign bee_word_bytes, 0x20 
.word PUSHREL_2C_ - .
.word _40_BRANCH_compilation
.word _40_BRANCH_info 
.global _40_BRANCH
_40_BRANCH:
pushi 0 # 0x0 
dup
load
calli OP_32__5F_SHIFT
arshift
calli CELLS
add
ret
.set _40_BRANCH_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "!BRANCH"
.balign bee_word_bytes, 0x20 
.word _40_BRANCH - .
.word _21_BRANCH_compilation
.word _21_BRANCH_info 
.global _21_BRANCH
_21_BRANCH:
pushi 1 # 0x1 
dup
pushi 0 # 0x0 
swap
calli OFFSET
calli CELL_2F_
pushi 1 # 0x1 
dup
load
calli OPCODE_32__3E_
calli _3E_OPCODE_32_
pushi 0 # 0x0 
swap
store
ret
.set _21_BRANCH_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "COMPILE,"
.balign bee_word_bytes, 0x20 
.word _21_BRANCH - .
.word COMPILE_2C__compilation
.word COMPILE_2C__info 
.global COMPILE_2C_
COMPILE_2C_:
pushi 0 # 0x0 
dup
calli _3E_INFO
pushi 2 
add
load1
calli _3F_DUP
jumpzi .L2780f
pushi 0 
calli _32__3E_R
.L2783b:
pushi 0 # 0x0 
dup
load
calli _2C_
calli CELL_2B_
calli _28_LOOP_29_
jumpzi .L2783b
.L2782f:
calli UNLOOP
pop
jumpi .L2792f
.L2780f:
calli CALL_2C_
.L2792f:
ret
.set COMPILE_2C__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "ADDR>LABEL"
.balign bee_word_bytes, 0x20 
.word COMPILE_2C_ - .
.word ADDR_3E_LABEL_compilation
.word ADDR_3E_LABEL_info 
.global ADDR_3E_LABEL
ADDR_3E_LABEL:
calli _27_FORTH
neg
add
calli CELL_2F_
ret
.set ADDR_3E_LABEL_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "BACKWARD"
.balign bee_word_bytes, 0x20 
.word ADDR_3E_LABEL - .
.word BACKWARD_compilation
.word BACKWARD_info 
.global BACKWARD
BACKWARD:
pushi 98 
ret
.set BACKWARD_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "FORWARD"
.balign bee_word_bytes, 0x20 
.word BACKWARD - .
.word FORWARD_compilation
.word FORWARD_info 
.global FORWARD
FORWARD:
pushi 102 
ret
.set FORWARD_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "NONAME"
.balign bee_word_bytes, 0x20 
.word FORWARD - .
.word NONAME_compilation
.word NONAME_info 
.global NONAME
NONAME:
pushi 110 
ret
.set NONAME_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "BEGIN"
.balign bee_word_bytes, 0x20 
.word NONAME - .
.word BEGIN_compilation
.word BEGIN_info 
.global BEGIN
BEGIN:
calli HERE
pushi 0 # 0x0 
dup
calli BACKWARD
calli _2E_LABEL_2D_DEF
ret
.set BEGIN_compilation, (2 * bee_word_bytes)
.set BEGIN_info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "AHEAD"
.balign bee_word_bytes, 0x20 
.word BEGIN - .
.word AHEAD_compilation
.word AHEAD_info 
.global AHEAD
AHEAD:
calli HERE
pushi 0 # 0x0 
dup
calli FORWARD
calli _2E_BRANCH
calli BRANCH_2C_
ret
.set AHEAD_compilation, (2 * bee_word_bytes)
.set AHEAD_info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "IF"
.balign bee_word_bytes, 0x20 
.word AHEAD - .
.word IF_compilation
.word IF_info 
.global IF
IF:
calli HERE
pushi 0 # 0x0 
dup
calli FORWARD
calli _2E_IF
calli IF_2C_
ret
.set IF_compilation, (2 * bee_word_bytes)
.set IF_info, _immediate_bit | _compiling_bit | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "THEN"
.balign bee_word_bytes, 0x20 
.word IF - .
.word THEN_compilation
.word THEN_info 
.global THEN
THEN:
pushi 0 # 0x0 
dup
calli FORWARD
calli _2E_LABEL_2D_DEF
calli HERE
calli _21_BRANCH
ret
.set THEN_compilation, (2 * bee_word_bytes)
.set THEN_info, _immediate_bit | _compiling_bit | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "LINK,"
.balign bee_word_bytes, 0x20 
.word THEN - .
.word LINK_2C__compilation
.word LINK_2C__info 
.global LINK_2C_
LINK_2C_:
ret
.set LINK_2C__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "UNLINK,"
.balign bee_word_bytes, 0x20 
.word LINK_2C_ - .
.word UNLINK_2C__compilation
.word UNLINK_2C__info 
.global UNLINK_2C_
UNLINK_2C_:
calli _2E_RET
calli INSN_5F_RET
calli OP_32__5F_INSN
calli _3E_OPCODE_32_
calli RAW_2C_
ret
.set UNLINK_2C__info, 0 | _compiling_bit | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "LEAVE,"
.balign bee_word_bytes, 0x20 
.word UNLINK_2C_ - .
.word LEAVE_2C__compilation
.word LEAVE_2C__info 
.global LEAVE_2C_
LEAVE_2C_:
ret
.set LEAVE_2C__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x10 
.ascii "CURRENT-COMPILE,"
.balign bee_word_bytes, 0x20 
.word LEAVE_2C_ - .
.word CURRENT_2D_COMPILE_2C__compilation
.word CURRENT_2D_COMPILE_2C__info 
.global CURRENT_2D_COMPILE_2C_
CURRENT_2D_COMPILE_2C_:
nop 
calli CURRENT_2D_COMPILE_2C__doer
.balign bee_word_bytes
CURRENT_2D_COMPILE_2C__body:
.word CURRENT_2D_COMPILE_2C__defer - .
.set CURRENT_2D_COMPILE_2C__defer, COMPILE_2C_
.set CURRENT_2D_COMPILE_2C__info, 0 | 0 | 0 | (16 <<_name_length_bits) | 0x0 
.set CURRENT_2D_COMPILE_2C__doer, DEFER_does
.balign bee_word_bytes
.byte 0xB 
.ascii "#CALL-CELLS"
.balign bee_word_bytes, 0x20 
.word CURRENT_2D_COMPILE_2C_ - .
.word _23_CALL_2D_CELLS_compilation
.word _23_CALL_2D_CELLS_info 
.global _23_CALL_2D_CELLS
_23_CALL_2D_CELLS:
pushi 1 
ret
.set _23_CALL_2D_CELLS_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xE 
.ascii "(RAW-POSTPONE)"
.balign bee_word_bytes, 0x20 
.word _23_CALL_2D_CELLS - .
.word _28_RAW_2D_POSTPONE_29__compilation
.word _28_RAW_2D_POSTPONE_29__info 
.global _28_RAW_2D_POSTPONE_29_
_28_RAW_2D_POSTPONE_29_:
calli CURRENT_2D_COMPILE_2C_
ret
.set _28_RAW_2D_POSTPONE_29__info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "(POSTPONE)"
.balign bee_word_bytes, 0x20 
.word _28_RAW_2D_POSTPONE_29_ - .
.word _28_POSTPONE_29__compilation
.word _28_POSTPONE_29__info 
.global _28_POSTPONE_29_
_28_POSTPONE_29_:
calli CURRENT_2D_COMPILE_2C_
ret
.set _28_POSTPONE_29__info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "CREATED"
.balign bee_word_bytes, 0x20 
.word _28_POSTPONE_29_ - .
.word CREATED_compilation
.word CREATED_info 
.global CREATED
CREATED:
nop 
calli CREATED_doer
.balign bee_word_bytes
CREATED_body:
.ds.b 1 * bee_word_bytes
.set CREATED_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set CREATED_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x7 
.ascii "LITERAL"
.balign bee_word_bytes, 0x20 
.word CREATED - .
.word LITERAL_compilation
.word LITERAL_info 
.global LITERAL
LITERAL:
pushi 0 # 0x0 
dup
pushi 0 # 0x0 
dup
calli CELLS
calli CELL_2F_
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L2938f
calli _2E_PUSHI
jumpi .L2940f
.L2938f:
calli _2E_PUSH
.L2940f:
calli PUSH_2C_
ret
.set LITERAL_compilation, (2 * bee_word_bytes)
.set LITERAL_info, _immediate_bit | _compiling_bit | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x10 
.ascii "RELATIVE-LITERAL"
.balign bee_word_bytes, 0x20 
.word LITERAL - .
.word RELATIVE_2D_LITERAL_compilation
.word RELATIVE_2D_LITERAL_info 
.global RELATIVE_2D_LITERAL
RELATIVE_2D_LITERAL:
pushi 0 # 0x0 
dup
calli _2E_PUSHRELI
calli PUSHREL_2C_
ret
.set RELATIVE_2D_LITERAL_compilation, (2 * bee_word_bytes)
.set RELATIVE_2D_LITERAL_info, _immediate_bit | _compiling_bit | 0 | (16 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii ">BODY"
.balign bee_word_bytes, 0x20 
.word RELATIVE_2D_LITERAL - .
.word _3E_BODY_compilation
.word _3E_BODY_info 
.global _3E_BODY
_3E_BODY:
pushi 2 
calli CELLS
add
ret
.set _3E_BODY_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii ">DOES>"
.balign bee_word_bytes, 0x20 
.word _3E_BODY - .
.word _3E_DOES_3E__compilation
.word _3E_DOES_3E__info 
.global _3E_DOES_3E_
_3E_DOES_3E_:
pushi 0 # 0x0 
dup
calli _3E_INFO
load
pushi 65535 
and
calli CELLS
add
ret
.set _3E_DOES_3E__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "(DOES>)"
.balign bee_word_bytes, 0x20 
.word _3E_DOES_3E_ - .
.word _28_DOES_3E__29__compilation
.word _28_DOES_3E__29__info 
.global _28_DOES_3E__29_
_28_DOES_3E__29_:
pushi 0 # 0x0 
dup
calli _3E_NAME
calli CREATED
store
calli _3E_DOES_3E_
calli LAST
calli CELL_2B_
pushi 0 # 0x0 
dup
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli CALL
ret
.set _28_DOES_3E__29__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "AGAIN"
.balign bee_word_bytes, 0x20 
.word _28_DOES_3E__29_ - .
.word AGAIN_compilation
.word AGAIN_info 
.global AGAIN
AGAIN:
pushi 0 # 0x0 
dup
calli BACKWARD
calli _2E_BRANCH
calli HERE
calli BRANCH_2C_
pushi 0 # 0x0 
swap
calli _21_BRANCH
ret
.set AGAIN_compilation, (2 * bee_word_bytes)
.set AGAIN_info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "UNTIL"
.balign bee_word_bytes, 0x20 
.word AGAIN - .
.word UNTIL_compilation
.word UNTIL_info 
.global UNTIL
UNTIL:
pushi 0 # 0x0 
dup
calli BACKWARD
calli _2E_IF
calli HERE
calli IF_2C_
pushi 0 # 0x0 
swap
calli _21_BRANCH
ret
.set UNTIL_compilation, (2 * bee_word_bytes)
.set UNTIL_info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "DOES-LINK,"
.balign bee_word_bytes, 0x20 
.word UNTIL - .
.word DOES_2D_LINK_2C__compilation
.word DOES_2D_LINK_2C__info 
.global DOES_2D_LINK_2C_
DOES_2D_LINK_2C_:
pushreli R_3E_
calli _28_POSTPONE_29_
ret
.set DOES_2D_LINK_2C__info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "DO,"
.balign bee_word_bytes, 0x20 
.word DOES_2D_LINK_2C_ - .
.word DO_2C__compilation
.word DO_2C__info 
.global DO_2C_
DO_2C_:
pushreli _32__3E_R
calli _28_POSTPONE_29_
ret
.set DO_2C__info, 0 | _compiling_bit | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "LOOP,"
.balign bee_word_bytes, 0x20 
.word DO_2C_ - .
.word LOOP_2C__compilation
.word LOOP_2C__info 
.global LOOP_2C_
LOOP_2C_:
pushreli _28_LOOP_29_
calli _28_POSTPONE_29_
calli UNTIL - (2 * bee_word_bytes) + UNTIL_compilation
ret
.set LOOP_2C__info, 0 | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "+LOOP,"
.balign bee_word_bytes, 0x20 
.word LOOP_2C_ - .
.word _2B_LOOP_2C__compilation
.word _2B_LOOP_2C__info 
.global _2B_LOOP_2C_
_2B_LOOP_2C_:
pushreli _28__2B_LOOP_29_
calli _28_POSTPONE_29_
calli UNTIL - (2 * bee_word_bytes) + UNTIL_compilation
ret
.set _2B_LOOP_2C__info, 0 | _compiling_bit | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "END-LOOP,"
.balign bee_word_bytes, 0x20 
.word _2B_LOOP_2C_ - .
.word END_2D_LOOP_2C__compilation
.word END_2D_LOOP_2C__info 
.global END_2D_LOOP_2C_
END_2D_LOOP_2C_:
pushreli UNLOOP
calli _28_POSTPONE_29_
ret
.set END_2D_LOOP_2C__info, 0 | _compiling_bit | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "CREATE,"
.balign bee_word_bytes, 0x20 
.word END_2D_LOOP_2C_ - .
.word CREATE_2C__compilation
.word CREATE_2C__info 
.global CREATE_2C_
CREATE_2C_:
calli _2E_NOP
calli NOP_2C_
pushreli _28_CREATE_29_
calli _28_RAW_2D_POSTPONE_29_
calli LAST
calli _3E_NAME
calli _2E_CREATED_2D_CODE
pushreli _28_CREATE_29_
calli _3E_NAME
calli CREATED
store
ret
.set CREATE_2C__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "DEFER!"
.balign bee_word_bytes, 0x20 
.word CREATE_2C_ - .
.word DEFER_21__compilation
.word DEFER_21__info 
.global DEFER_21_
DEFER_21_:
calli _3E_BODY
calli REL_21_
ret
.set DEFER_21__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "DEFER@"
.balign bee_word_bytes, 0x20 
.word DEFER_21_ - .
.word DEFER_40__compilation
.word DEFER_40__info 
.global DEFER_40_
DEFER_40_:
calli _3E_BODY
calli REL_40_
ret
.set DEFER_40__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "C0END"
.balign bee_word_bytes, 0x20 
.word DEFER_40_ - .
.word C_30_END_compilation
.word C_30_END_info 
.global C_30_END
C_30_END:
neg
not
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli MIN
calli _32_DUP
add
pushs
calli MOVE
pushi 0 
pops
store1
ret
.set C_30_END_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "SCRATCH-C0END"
.balign bee_word_bytes, 0x20 
.word C_30_END - .
.word SCRATCH_2D_C_30_END_compilation
.word SCRATCH_2D_C_30_END_info 
.global SCRATCH_2D_C_30_END
SCRATCH_2D_C_30_END:
calli SCRATCH
pushi 256 
calli C_30_END
calli SCRATCH
ret
.set SCRATCH_2D_C_30_END_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "\","
.balign bee_word_bytes, 0x20 
.word SCRATCH_2D_C_30_END - .
.word _22__2C__compilation
.word _22__2C__info 
.global _22__2C_
_22__2C_:
pushi 0 # 0x0 
dup
calli C_2C_
calli _32_DUP
calli _2E_STRING
calli HERE
pushi 0 # 0x0 
swap
pushi 0 # 0x0 
dup
calli RAW_2D_ALLOT
calli CMOVE
ret
.set _22__2C__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "(C\")"
.balign bee_word_bytes, 0x20 
.word _22__2C_ - .
.word _28_C_22__29__compilation
.word _28_C_22__29__info 
.global _28_C_22__29_
_28_C_22__29_:
pops
pushi 0 # 0x0 
dup
load1
not
neg
pushi 1 # 0x1 
dup
add
calli ALIGNED
pushs
ret
.set _28_C_22__29__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "(S\")"
.balign bee_word_bytes, 0x20 
.word _28_C_22__29_ - .
.word _28_S_22__29__compilation
.word _28_S_22__29__info 
.global _28_S_22__29_
_28_S_22__29_:
pops
pushi 0 # 0x0 
dup
load1
calli TUCK
not
neg
pushi 1 # 0x1 
dup
add
calli ALIGNED
pushs
calli CHAR_2B_
pushi 0 # 0x0 
swap
ret
.set _28_S_22__29__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "STRLEN"
.balign bee_word_bytes, 0x20 
.word _28_S_22__29_ - .
.word STRLEN_compilation
.word STRLEN_info 
.global STRLEN
STRLEN:
pushi 0x0 
trap 0x0 
ret
.balign bee_word_bytes
.set STRLEN_inline, 2
.set STRLEN_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x7 
.ascii "STRNCPY"
.balign bee_word_bytes, 0x20 
.word STRLEN - .
.word STRNCPY_compilation
.word STRNCPY_info 
.global STRNCPY
STRNCPY:
pushi 0x1 
trap 0x0 
ret
.balign bee_word_bytes
.set STRNCPY_inline, 2
.set STRNCPY_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0xC 
.ascii "STDIN-FILENO"
.balign bee_word_bytes, 0x20 
.word STRNCPY - .
.word STDIN_2D_FILENO_compilation
.word STDIN_2D_FILENO_info 
.global STDIN_2D_FILENO
STDIN_2D_FILENO:
pushi 0x2 
trap 0x0 
ret
.balign bee_word_bytes
.set STDIN_2D_FILENO_inline, 2
.set STDIN_2D_FILENO_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0xD 
.ascii "STDOUT-FILENO"
.balign bee_word_bytes, 0x20 
.word STDIN_2D_FILENO - .
.word STDOUT_2D_FILENO_compilation
.word STDOUT_2D_FILENO_info 
.global STDOUT_2D_FILENO
STDOUT_2D_FILENO:
pushi 0x3 
trap 0x0 
ret
.balign bee_word_bytes
.set STDOUT_2D_FILENO_inline, 2
.set STDOUT_2D_FILENO_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0xD 
.ascii "STDERR-FILENO"
.balign bee_word_bytes, 0x20 
.word STDOUT_2D_FILENO - .
.word STDERR_2D_FILENO_compilation
.word STDERR_2D_FILENO_info 
.global STDERR_2D_FILENO
STDERR_2D_FILENO:
pushi 0x4 
trap 0x0 
ret
.balign bee_word_bytes
.set STDERR_2D_FILENO_inline, 2
.set STDERR_2D_FILENO_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x3 
.ascii "R/O"
.balign bee_word_bytes, 0x20 
.word STDERR_2D_FILENO - .
.word R_2F_O_compilation
.word R_2F_O_info 
.global R_2F_O
R_2F_O:
pushi 0x5 
trap 0x0 
ret
.balign bee_word_bytes
.set R_2F_O_inline, 2
.set R_2F_O_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x3 
.ascii "W/O"
.balign bee_word_bytes, 0x20 
.word R_2F_O - .
.word W_2F_O_compilation
.word W_2F_O_info 
.global W_2F_O
W_2F_O:
pushi 0x6 
trap 0x0 
ret
.balign bee_word_bytes
.set W_2F_O_inline, 2
.set W_2F_O_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x3 
.ascii "R/W"
.balign bee_word_bytes, 0x20 
.word W_2F_O - .
.word R_2F_W_compilation
.word R_2F_W_info 
.global R_2F_W
R_2F_W:
pushi 0x7 
trap 0x0 
ret
.balign bee_word_bytes
.set R_2F_W_inline, 2
.set R_2F_W_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x7 
.ascii "O_CREAT"
.balign bee_word_bytes, 0x20 
.word R_2F_W - .
.word O_5F_CREAT_compilation
.word O_5F_CREAT_info 
.global O_5F_CREAT
O_5F_CREAT:
pushi 0x8 
trap 0x0 
ret
.balign bee_word_bytes
.set O_5F_CREAT_inline, 2
.set O_5F_CREAT_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x7 
.ascii "O_TRUNC"
.balign bee_word_bytes, 0x20 
.word O_5F_CREAT - .
.word O_5F_TRUNC_compilation
.word O_5F_TRUNC_info 
.global O_5F_TRUNC
O_5F_TRUNC:
pushi 0x9 
trap 0x0 
ret
.balign bee_word_bytes
.set O_5F_TRUNC_inline, 2
.set O_5F_TRUNC_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x4 
.ascii "OPEN"
.balign bee_word_bytes, 0x20 
.word O_5F_TRUNC - .
.word OPEN_compilation
.word OPEN_info 
.global OPEN
OPEN:
pushi 0xA 
trap 0x0 
ret
.balign bee_word_bytes
.set OPEN_inline, 2
.set OPEN_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0xA 
.ascii "CLOSE-FILE"
.balign bee_word_bytes, 0x20 
.word OPEN - .
.word CLOSE_2D_FILE_compilation
.word CLOSE_2D_FILE_info 
.global CLOSE_2D_FILE
CLOSE_2D_FILE:
pushi 0xB 
trap 0x0 
ret
.balign bee_word_bytes
.set CLOSE_2D_FILE_inline, 2
.set CLOSE_2D_FILE_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x4 
.ascii "READ"
.balign bee_word_bytes, 0x20 
.word CLOSE_2D_FILE - .
.word READ_compilation
.word READ_info 
.global READ
READ:
pushi 0xC 
trap 0x0 
ret
.balign bee_word_bytes
.set READ_inline, 2
.set READ_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x5 
.ascii "WRITE"
.balign bee_word_bytes, 0x20 
.word READ - .
.word WRITE_compilation
.word WRITE_info 
.global WRITE
WRITE:
pushi 0xD 
trap 0x0 
ret
.balign bee_word_bytes
.set WRITE_inline, 2
.set WRITE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x8 
.ascii "SEEK_SET"
.balign bee_word_bytes, 0x20 
.word WRITE - .
.word SEEK_5F_SET_compilation
.word SEEK_5F_SET_info 
.global SEEK_5F_SET
SEEK_5F_SET:
pushi 0xE 
trap 0x0 
ret
.balign bee_word_bytes
.set SEEK_5F_SET_inline, 2
.set SEEK_5F_SET_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x8 
.ascii "SEEK_CUR"
.balign bee_word_bytes, 0x20 
.word SEEK_5F_SET - .
.word SEEK_5F_CUR_compilation
.word SEEK_5F_CUR_info 
.global SEEK_5F_CUR
SEEK_5F_CUR:
pushi 0xF 
trap 0x0 
ret
.balign bee_word_bytes
.set SEEK_5F_CUR_inline, 2
.set SEEK_5F_CUR_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x8 
.ascii "SEEK_END"
.balign bee_word_bytes, 0x20 
.word SEEK_5F_CUR - .
.word SEEK_5F_END_compilation
.word SEEK_5F_END_info 
.global SEEK_5F_END
SEEK_5F_END:
pushi 0x10 
trap 0x0 
ret
.balign bee_word_bytes
.set SEEK_5F_END_inline, 2
.set SEEK_5F_END_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x5 
.ascii "LSEEK"
.balign bee_word_bytes, 0x20 
.word SEEK_5F_END - .
.word LSEEK_compilation
.word LSEEK_info 
.global LSEEK
LSEEK:
pushi 0x11 
trap 0x0 
ret
.balign bee_word_bytes
.set LSEEK_inline, 2
.set LSEEK_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0xA 
.ascii "FLUSH-FILE"
.balign bee_word_bytes, 0x20 
.word LSEEK - .
.word FLUSH_2D_FILE_compilation
.word FLUSH_2D_FILE_info 
.global FLUSH_2D_FILE
FLUSH_2D_FILE:
pushi 0x12 
trap 0x0 
ret
.balign bee_word_bytes
.set FLUSH_2D_FILE_inline, 2
.set FLUSH_2D_FILE_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x6 
.ascii "RENAME"
.balign bee_word_bytes, 0x20 
.word FLUSH_2D_FILE - .
.word RENAME_compilation
.word RENAME_info 
.global RENAME
RENAME:
pushi 0x13 
trap 0x0 
ret
.balign bee_word_bytes
.set RENAME_inline, 2
.set RENAME_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x6 
.ascii "REMOVE"
.balign bee_word_bytes, 0x20 
.word RENAME - .
.word REMOVE_compilation
.word REMOVE_info 
.global REMOVE
REMOVE:
pushi 0x14 
trap 0x0 
ret
.balign bee_word_bytes
.set REMOVE_inline, 2
.set REMOVE_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x9 
.ascii "FILE_SIZE"
.balign bee_word_bytes, 0x20 
.word REMOVE - .
.word FILE_5F_SIZE_compilation
.word FILE_5F_SIZE_info 
.global FILE_5F_SIZE
FILE_5F_SIZE:
pushi 0x15 
trap 0x0 
ret
.balign bee_word_bytes
.set FILE_5F_SIZE_inline, 2
.set FILE_5F_SIZE_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0xB 
.ascii "RESIZE_FILE"
.balign bee_word_bytes, 0x20 
.word FILE_5F_SIZE - .
.word RESIZE_5F_FILE_compilation
.word RESIZE_5F_FILE_info 
.global RESIZE_5F_FILE
RESIZE_5F_FILE:
pushi 0x16 
trap 0x0 
ret
.balign bee_word_bytes
.set RESIZE_5F_FILE_inline, 2
.set RESIZE_5F_FILE_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0xB 
.ascii "FILE-STATUS"
.balign bee_word_bytes, 0x20 
.word RESIZE_5F_FILE - .
.word FILE_2D_STATUS_compilation
.word FILE_2D_STATUS_info 
.global FILE_2D_STATUS
FILE_2D_STATUS:
pushi 0x17 
trap 0x0 
ret
.balign bee_word_bytes
.set FILE_2D_STATUS_inline, 2
.set FILE_2D_STATUS_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0xA 
.ascii "TOTAL-ARGS"
.balign bee_word_bytes, 0x20 
.word FILE_2D_STATUS - .
.word TOTAL_2D_ARGS_compilation
.word TOTAL_2D_ARGS_info 
.global TOTAL_2D_ARGS
TOTAL_2D_ARGS:
pushi 0x100 
trap 0x0 
ret
.balign bee_word_bytes
.set TOTAL_2D_ARGS_inline, 2
.set TOTAL_2D_ARGS_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0x4 
.ascii "ARGV"
.balign bee_word_bytes, 0x20 
.word TOTAL_2D_ARGS - .
.word ARGV_compilation
.word ARGV_info 
.global ARGV
ARGV:
pushi 0x101 
trap 0x0 
ret
.balign bee_word_bytes
.set ARGV_inline, 2
.set ARGV_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x20000 
.balign bee_word_bytes
.byte 0xA 
.ascii "CREATE-FAM"
.balign bee_word_bytes, 0x20 
.word ARGV - .
.word CREATE_2D_FAM_compilation
.word CREATE_2D_FAM_info 
.global CREATE_2D_FAM
CREATE_2D_FAM:
pushi 8 # 0x8 
trap 0x0 
pushi 9 # 0x9 
trap 0x0 
or
ret
.set CREATE_2D_FAM_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "BIN-MODE"
.balign bee_word_bytes, 0x20 
.word CREATE_2D_FAM - .
.word BIN_2D_MODE_compilation
.word BIN_2D_MODE_info 
.global BIN_2D_MODE
BIN_2D_MODE:
pushi 0 
ret
.set BIN_2D_MODE_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "BIN"
.balign bee_word_bytes, 0x20 
.word BIN_2D_MODE - .
.word BIN_compilation
.word BIN_info 
.global BIN
BIN:
ret
.set BIN_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "OPEN-FILE"
.balign bee_word_bytes, 0x20 
.word BIN - .
.word OPEN_2D_FILE_compilation
.word OPEN_2D_FILE_info 
.global OPEN_2D_FILE
OPEN_2D_FILE:
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
calli SCRATCH_2D_C_30_END
pushi 0 # 0x0 
swap
pushi 10 # 0xA 
trap 0x0 
pushi 0 # 0x0 
dup
pushi 0 
lt
neg
ret
.set OPEN_2D_FILE_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "READ-FILE"
.balign bee_word_bytes, 0x20 
.word OPEN_2D_FILE - .
.word READ_2D_FILE_compilation
.word READ_2D_FILE_info 
.global READ_2D_FILE
READ_2D_FILE:
pushi 12 # 0xC 
trap 0x0 
pushi 0 # 0x0 
dup
pushi 0 
lt
neg
ret
.set READ_2D_FILE_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "WRITE-FILE"
.balign bee_word_bytes, 0x20 
.word READ_2D_FILE - .
.word WRITE_2D_FILE_compilation
.word WRITE_2D_FILE_info 
.global WRITE_2D_FILE
WRITE_2D_FILE:
pushi 13 # 0xD 
trap 0x0 
pushi 0 
lt
neg
ret
.set WRITE_2D_FILE_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "RENAME-FILE"
.balign bee_word_bytes, 0x20 
.word WRITE_2D_FILE - .
.word RENAME_2D_FILE_compilation
.word RENAME_2D_FILE_info 
.global RENAME_2D_FILE
RENAME_2D_FILE:
calli SCRATCH_2D_C_30_END
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
calli HERE
pushi 256 
calli C_30_END
calli HERE
pushi 0 # 0x0 
swap
pushi 19 # 0x13 
trap 0x0 
ret
.set RENAME_2D_FILE_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "DELETE-FILE"
.balign bee_word_bytes, 0x20 
.word RENAME_2D_FILE - .
.word DELETE_2D_FILE_compilation
.word DELETE_2D_FILE_info 
.global DELETE_2D_FILE
DELETE_2D_FILE:
calli SCRATCH_2D_C_30_END
pushi 20 # 0x14 
trap 0x0 
ret
.set DELETE_2D_FILE_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "CREATE-FILE"
.balign bee_word_bytes, 0x20 
.word DELETE_2D_FILE - .
.word CREATE_2D_FILE_compilation
.word CREATE_2D_FILE_info 
.global CREATE_2D_FILE
CREATE_2D_FILE:
calli CREATE_2D_FAM
or
calli OPEN_2D_FILE
ret
.set CREATE_2D_FILE_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "D>OFF_T"
.balign bee_word_bytes, 0x20 
.word CREATE_2D_FILE - .
.word D_3E_OFF_5F_T_compilation
.word D_3E_OFF_5F_T_info 
.global D_3E_OFF_5F_T
D_3E_OFF_5F_T:
ret
.set D_3E_OFF_5F_T_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "OFF_T>D"
.balign bee_word_bytes, 0x20 
.word D_3E_OFF_5F_T - .
.word OFF_5F_T_3E_D_compilation
.word OFF_5F_T_3E_D_info 
.global OFF_5F_T_3E_D
OFF_5F_T_3E_D:
ret
.set OFF_5F_T_3E_D_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "FILE-POSITION"
.balign bee_word_bytes, 0x20 
.word OFF_5F_T_3E_D - .
.word FILE_2D_POSITION_compilation
.word FILE_2D_POSITION_info 
.global FILE_2D_POSITION
FILE_2D_POSITION:
pushi 0 
pushi 0 
calli D_3E_OFF_5F_T
pushi 15 # 0xF 
trap 0x0 
pushi 17 # 0x11 
trap 0x0 
calli OFF_5F_T_3E_D
pushi 1 # 0x1 
dup
pushi -1 
eq
neg
pushi 1 # 0x1 
dup
pushi -1 
eq
neg
and
ret
.set FILE_2D_POSITION_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xF 
.ascii "REPOSITION-FILE"
.balign bee_word_bytes, 0x20 
.word FILE_2D_POSITION - .
.word REPOSITION_2D_FILE_compilation
.word REPOSITION_2D_FILE_info 
.global REPOSITION_2D_FILE
REPOSITION_2D_FILE:
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
calli D_3E_OFF_5F_T
pushi 14 # 0xE 
trap 0x0 
pushi 17 # 0x11 
trap 0x0 
calli OFF_5F_T_3E_D
pushi -1 
eq
neg
pushi 0 # 0x0 
swap
pushi -1 
eq
neg
and
ret
.set REPOSITION_2D_FILE_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "FILE-SIZE"
.balign bee_word_bytes, 0x20 
.word REPOSITION_2D_FILE - .
.word FILE_2D_SIZE_compilation
.word FILE_2D_SIZE_info 
.global FILE_2D_SIZE
FILE_2D_SIZE:
pushi 21 # 0x15 
trap 0x0 
pushs
calli OFF_5F_T_3E_D
pops
ret
.set FILE_2D_SIZE_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "RESIZE-FILE"
.balign bee_word_bytes, 0x20 
.word FILE_2D_SIZE - .
.word RESIZE_2D_FILE_compilation
.word RESIZE_2D_FILE_info 
.global RESIZE_2D_FILE
RESIZE_2D_FILE:
pushs
calli D_3E_OFF_5F_T
pops
pushi 22 # 0x16 
trap 0x0 
ret
.set RESIZE_2D_FILE_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "ABSOLUTE-ARG"
.balign bee_word_bytes, 0x20 
.word RESIZE_2D_FILE - .
.word ABSOLUTE_2D_ARG_compilation
.word ABSOLUTE_2D_ARG_info 
.global ABSOLUTE_2D_ARG
ABSOLUTE_2D_ARG:
pushi 256 # 0x100 
trap 0x0 
pushi 1 # 0x1 
dup
calli _3E_
jumpzi .L3565f
pushi 257 # 0x101 
trap 0x0 
pushi 0 # 0x0 
swap
calli CELLS
add
load
pushi 0 # 0x0 
dup
pushi 0 # 0x0 
trap 0x0 
jumpi .L3577f
.L3565f:
pop
pushi 0 
pushi 0 
.L3577f:
ret
.set ABSOLUTE_2D_ARG_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "IO-BUFFER"
.balign bee_word_bytes, 0x20 
.word ABSOLUTE_2D_ARG - .
.word IO_2D_BUFFER_compilation
.word IO_2D_BUFFER_info 
.global IO_2D_BUFFER
IO_2D_BUFFER:
nop 
calli IO_2D_BUFFER_doer
.balign bee_word_bytes
IO_2D_BUFFER_body:
.ds.b 1 * bee_word_bytes
.set IO_2D_BUFFER_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.set IO_2D_BUFFER_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x5 
.ascii "STDIN"
.balign bee_word_bytes, 0x20 
.word IO_2D_BUFFER - .
.word STDIN_compilation
.word STDIN_info 
.global STDIN
STDIN:
nop 
calli STDIN_doer
.balign bee_word_bytes
STDIN_body:
.word 0 
.set STDIN_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set STDIN_doer, VALUE_does
.balign bee_word_bytes
.byte 0x6 
.ascii "STDOUT"
.balign bee_word_bytes, 0x20 
.word STDIN - .
.word STDOUT_compilation
.word STDOUT_info 
.global STDOUT
STDOUT:
nop 
calli STDOUT_doer
.balign bee_word_bytes
STDOUT_body:
.word 0 
.set STDOUT_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set STDOUT_doer, VALUE_does
.balign bee_word_bytes
.byte 0x6 
.ascii "STDERR"
.balign bee_word_bytes, 0x20 
.word STDOUT - .
.word STDERR_compilation
.word STDERR_info 
.global STDERR
STDERR:
nop 
calli STDERR_doer
.balign bee_word_bytes
STDERR_body:
.word 0 
.set STDERR_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set STDERR_doer, VALUE_does
.balign bee_word_bytes
.byte 0x13 
.ascii "INITIALIZE-TERMINAL"
.balign bee_word_bytes, 0x20 
.word STDERR - .
.word INITIALIZE_2D_TERMINAL_compilation
.word INITIALIZE_2D_TERMINAL_info 
.global INITIALIZE_2D_TERMINAL
INITIALIZE_2D_TERMINAL:
pushi 2 # 0x2 
trap 0x0 
pushreli STDIN_body
store
pushi 3 # 0x3 
trap 0x0 
pushreli STDOUT_body
store
pushi 4 # 0x4 
trap 0x0 
pushreli STDERR_body
store
ret
.set INITIALIZE_2D_TERMINAL_info, 0 | 0 | 0 | (19 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "EMIT"
.balign bee_word_bytes, 0x20 
.word INITIALIZE_2D_TERMINAL - .
.word EMIT_compilation
.word EMIT_info 
.global EMIT
EMIT:
calli IO_2D_BUFFER
calli TUCK
store1
pushi 1 
calli STDOUT
calli WRITE_2D_FILE
pop
ret
.set EMIT_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "KEY"
.balign bee_word_bytes, 0x20 
.word EMIT - .
.word KEY_compilation
.word KEY_info 
.global KEY
KEY:
calli IO_2D_BUFFER
pushi 0 # 0x0 
dup
pushi 1 
calli STDIN
calli READ_2D_FILE
calli _32_DROP
load1
ret
.set KEY_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "BL"
.balign bee_word_bytes, 0x20 
.word KEY - .
.word BL_compilation
.word BL_info 
.global BL
BL:
pushi 32 
ret
.set BL_info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "CR"
.balign bee_word_bytes, 0x20 
.word BL - .
.word CR_compilation
.word CR_info 
.global CR
CR:
pushi 10 
calli EMIT
ret
.set CR_info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "DEL"
.balign bee_word_bytes, 0x20 
.word CR - .
.word DEL_compilation
.word DEL_info 
.global DEL
DEL:
pushi 8 
calli EMIT
calli BL
calli EMIT
pushi 8 
calli EMIT
ret
.set DEL_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "DEL?"
.balign bee_word_bytes, 0x20 
.word DEL - .
.word DEL_3F__compilation
.word DEL_3F__info 
.global DEL_3F_
DEL_3F_:
pushi 0 # 0x0 
dup
pushi 127 
eq
neg
pushi 0 # 0x0 
swap
pushi 8 
eq
neg
or
ret
.set DEL_3F__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "CR?"
.balign bee_word_bytes, 0x20 
.word DEL_3F_ - .
.word CR_3F__compilation
.word CR_3F__info 
.global CR_3F_
CR_3F_:
pushi 0 # 0x0 
dup
pushi 13 
eq
neg
pushi 0 # 0x0 
swap
pushi 10 
eq
neg
or
ret
.set CR_3F__info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "EOL\""
.balign bee_word_bytes, 0x20 
.word CR_3F_ - .
.word EOL_22__compilation
.word EOL_22__info 
.global EOL_22_
EOL_22_:
nop 
calli EOL_22__doer
.balign bee_word_bytes
EOL_22__body:
.byte 0xA 
.balign bee_word_bytes, 0x0 
.set EOL_22__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.set EOL_22__doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x3 
.ascii "EOL"
.balign bee_word_bytes, 0x20 
.word EOL_22_ - .
.word EOL_compilation
.word EOL_info 
.global EOL
EOL:
calli EOL_22_
pushi 1 
ret
.set EOL_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "WIDTH"
.balign bee_word_bytes, 0x20 
.word EOL - .
.word WIDTH_compilation
.word WIDTH_info 
.global WIDTH
WIDTH:
pushi 77 
ret
.set WIDTH_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xF 
.ascii "REDIRECT-STDOUT"
.balign bee_word_bytes, 0x20 
.word WIDTH - .
.word REDIRECT_2D_STDOUT_compilation
.word REDIRECT_2D_STDOUT_info 
.global REDIRECT_2D_STDOUT
REDIRECT_2D_STDOUT:
calli STDOUT
pushs
pushreli STDOUT_body
store
call
pops
pushreli STDOUT_body
store
ret
.set REDIRECT_2D_STDOUT_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "ASMOUT"
.balign bee_word_bytes, 0x20 
.word REDIRECT_2D_STDOUT - .
.word ASMOUT_compilation
.word ASMOUT_info 
.global ASMOUT
ASMOUT:
nop 
calli ASMOUT_doer
.balign bee_word_bytes
ASMOUT_body:
.word -1 
.set ASMOUT_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.set ASMOUT_doer, VALUE_does
.balign bee_word_bytes
.byte 0x9 
.ascii "TO-ASMOUT"
.balign bee_word_bytes, 0x20 
.word ASMOUT - .
.word TO_2D_ASMOUT_compilation
.word TO_2D_ASMOUT_info 
.global TO_2D_ASMOUT
TO_2D_ASMOUT:
calli ASMOUT
calli REDIRECT_2D_STDOUT
ret
.set TO_2D_ASMOUT_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "ABORT"
.balign bee_word_bytes, 0x20 
.word TO_2D_ASMOUT - .
.word ABORT_compilation
.word ABORT_info 
.global ABORT
ABORT:
pushi -1 
calli THROW
ret
.set ABORT_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "QUIT"
.balign bee_word_bytes, 0x20 
.word ABORT - .
.word QUIT_compilation
.word QUIT_info 
.global QUIT
QUIT:
pushi -56 
calli THROW
ret
.set QUIT_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "CS-PICK"
.balign bee_word_bytes, 0x20 
.word QUIT - .
.word CS_2D_PICK_compilation
.word CS_2D_PICK_info 
.global CS_2D_PICK
CS_2D_PICK:
dup
ret
.set CS_2D_PICK_info, 0 | _compiling_bit | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "CS-ROLL"
.balign bee_word_bytes, 0x20 
.word CS_2D_PICK - .
.word CS_2D_ROLL_compilation
.word CS_2D_ROLL_info 
.global CS_2D_ROLL
CS_2D_ROLL:
calli ROLL
ret
.set CS_2D_ROLL_info, 0 | _compiling_bit | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "WHILE"
.balign bee_word_bytes, 0x20 
.word CS_2D_ROLL - .
.word WHILE_compilation
.word WHILE_info 
.global WHILE
WHILE:
calli IF - (2 * bee_word_bytes) + IF_compilation
pushi 1 
calli CS_2D_ROLL
ret
.set WHILE_compilation, (2 * bee_word_bytes)
.set WHILE_info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "REPEAT"
.balign bee_word_bytes, 0x20 
.word WHILE - .
.word REPEAT_compilation
.word REPEAT_info 
.global REPEAT
REPEAT:
calli AGAIN - (2 * bee_word_bytes) + AGAIN_compilation
calli THEN - (2 * bee_word_bytes) + THEN_compilation
ret
.set REPEAT_compilation, (2 * bee_word_bytes)
.set REPEAT_info, _immediate_bit | _compiling_bit | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "ELSE"
.balign bee_word_bytes, 0x20 
.word REPEAT - .
.word ELSE_compilation
.word ELSE_info 
.global ELSE
ELSE:
calli AHEAD - (2 * bee_word_bytes) + AHEAD_compilation
pushi 1 
calli CS_2D_ROLL
calli THEN - (2 * bee_word_bytes) + THEN_compilation
ret
.set ELSE_compilation, (2 * bee_word_bytes)
.set ELSE_info, _immediate_bit | _compiling_bit | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "'NODE"
.balign bee_word_bytes, 0x20 
.word ELSE - .
.word _27_NODE_compilation
.word _27_NODE_info 
.global _27_NODE
_27_NODE:
nop 
calli _27_NODE_doer
.balign bee_word_bytes
_27_NODE_body:
.ds.b 1 * bee_word_bytes
.set _27_NODE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set _27_NODE_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x5 
.ascii "'LOOP"
.balign bee_word_bytes, 0x20 
.word _27_NODE - .
.word _27_LOOP_compilation
.word _27_LOOP_info 
.global _27_LOOP
_27_LOOP:
nop 
calli _27_LOOP_doer
.balign bee_word_bytes
_27_LOOP_body:
.ds.b 1 * bee_word_bytes
.set _27_LOOP_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set _27_LOOP_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x8 
.ascii "NEW-NODE"
.balign bee_word_bytes, 0x20 
.word _27_LOOP - .
.word NEW_2D_NODE_compilation
.word NEW_2D_NODE_info 
.global NEW_2D_NODE
NEW_2D_NODE:
calli _27_NODE
load
calli _27_LOOP
load
calli HERE
word_bytes
neg
not
not
and
pushi 0 # 0x0 
dup
calli _27_LOOP
store
calli _27_NODE
store
ret
.set NEW_2D_NODE_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "TIE-NODE"
.balign bee_word_bytes, 0x20 
.word NEW_2D_NODE - .
.word TIE_2D_NODE_compilation
.word TIE_2D_NODE_info 
.global TIE_2D_NODE
TIE_2D_NODE:
calli _27_LOOP
load
calli FORWARD
calli _2E_LABEL_2D_DEF
calli _27_NODE
load
.L3857b:
pushi 0 # 0x0 
dup
calli _27_LOOP
load
calli _3C__3E_
jumpzi .L3862f
pushi 0 # 0x0 
dup
calli _40_BRANCH
pushi 0 # 0x0 
swap
calli THEN - (2 * bee_word_bytes) + THEN_compilation
jumpi .L3857b
.L3862f:
pop
calli _27_LOOP
store
calli _27_NODE
store
ret
.set TIE_2D_NODE_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "I"
.balign bee_word_bytes, 0x20 
.word TIE_2D_NODE - .
.word I_compilation
.word I_info 
.global I
I:
pushreli R_40_
calli _28_POSTPONE_29_
ret
.set I_compilation, (2 * bee_word_bytes)
.set I_info, _immediate_bit | _compiling_bit | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "LEAVE"
.balign bee_word_bytes, 0x20 
.word I - .
.word LEAVE_compilation
.word LEAVE_info 
.global LEAVE
LEAVE:
calli LEAVE_2C_
calli _27_LOOP
load
calli FORWARD
calli _2E_BRANCH
calli HERE
calli _27_NODE
pushi 0 # 0x0 
dup
load
calli HERE
calli BRANCH_2C_
pushi 0 # 0x0 
swap
calli _21_BRANCH
store
ret
.set LEAVE_compilation, (2 * bee_word_bytes)
.set LEAVE_info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "DO"
.balign bee_word_bytes, 0x20 
.word LEAVE - .
.word DO_compilation
.word DO_info 
.global DO
DO:
calli NEW_2D_NODE
calli DO_2C_
calli BEGIN - (2 * bee_word_bytes) + BEGIN_compilation
ret
.set DO_compilation, (2 * bee_word_bytes)
.set DO_info, _immediate_bit | _compiling_bit | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "?DO"
.balign bee_word_bytes, 0x20 
.word DO - .
.word _3F_DO_compilation
.word _3F_DO_info 
.global _3F_DO
_3F_DO:
calli NEW_2D_NODE
pushreli _32_DUP
calli _28_POSTPONE_29_
calli DO_2C_
pushreli _3D_
calli _28_POSTPONE_29_
calli IF - (2 * bee_word_bytes) + IF_compilation
calli LEAVE - (2 * bee_word_bytes) + LEAVE_compilation
calli THEN - (2 * bee_word_bytes) + THEN_compilation
calli BEGIN - (2 * bee_word_bytes) + BEGIN_compilation
ret
.set _3F_DO_compilation, (2 * bee_word_bytes)
.set _3F_DO_info, _immediate_bit | _compiling_bit | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "LOOP"
.balign bee_word_bytes, 0x20 
.word _3F_DO - .
.word LOOP_compilation
.word LOOP_info 
.global LOOP
LOOP:
calli LOOP_2C_
calli TIE_2D_NODE
calli END_2D_LOOP_2C_
ret
.set LOOP_compilation, (2 * bee_word_bytes)
.set LOOP_info, _immediate_bit | _compiling_bit | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "+LOOP"
.balign bee_word_bytes, 0x20 
.word LOOP - .
.word _2B_LOOP_compilation
.word _2B_LOOP_info 
.global _2B_LOOP
_2B_LOOP:
calli _2B_LOOP_2C_
calli TIE_2D_NODE
calli END_2D_LOOP_2C_
ret
.set _2B_LOOP_compilation, (2 * bee_word_bytes)
.set _2B_LOOP_info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "RECURSE"
.balign bee_word_bytes, 0x20 
.word _2B_LOOP - .
.word RECURSE_compilation
.word RECURSE_info 
.global RECURSE
RECURSE:
calli LAST
calli COMPILE_2C_
ret
.set RECURSE_compilation, (2 * bee_word_bytes)
.set RECURSE_info, _immediate_bit | _compiling_bit | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "CASE"
.balign bee_word_bytes, 0x20 
.word RECURSE - .
.word CASE_compilation
.word CASE_info 
.global CASE
CASE:
pushi 0 
ret
.set CASE_compilation, (2 * bee_word_bytes)
.set CASE_info, _immediate_bit | _compiling_bit | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "OF"
.balign bee_word_bytes, 0x20 
.word CASE - .
.word OF_compilation
.word OF_info 
.global OF
OF:
not
neg
pushs
pushreli OVER
calli _28_POSTPONE_29_
pushreli _3D_
calli _28_POSTPONE_29_
calli IF - (2 * bee_word_bytes) + IF_compilation
pushreli DROP
calli _28_POSTPONE_29_
pops
ret
.set OF_compilation, (2 * bee_word_bytes)
.set OF_info, _immediate_bit | _compiling_bit | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "ENDOF"
.balign bee_word_bytes, 0x20 
.word OF - .
.word ENDOF_compilation
.word ENDOF_info 
.global ENDOF
ENDOF:
pushs
calli ELSE - (2 * bee_word_bytes) + ELSE_compilation
pops
ret
.set ENDOF_compilation, (2 * bee_word_bytes)
.set ENDOF_info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "ENDCASE"
.balign bee_word_bytes, 0x20 
.word ENDOF - .
.word ENDCASE_compilation
.word ENDCASE_info 
.global ENDCASE
ENDCASE:
pushreli DROP
calli _28_POSTPONE_29_
pushi 0 
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L3991f
jumpi .L3987f
.L3991f:
.L3993b:
calli THEN - (2 * bee_word_bytes) + THEN_compilation
calli _28_LOOP_29_
jumpzi .L3993b
.L3987f:
.L3992f:
calli UNLOOP
ret
.set ENDCASE_compilation, (2 * bee_word_bytes)
.set ENDCASE_info, _immediate_bit | _compiling_bit | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "CLITERAL"
.balign bee_word_bytes, 0x20 
.word ENDCASE - .
.word CLITERAL_compilation
.word CLITERAL_info 
.global CLITERAL
CLITERAL:
pushreli _28_C_22__29_
calli _28_POSTPONE_29_
calli _22__2C_
pushi 0 
calli CALIGN
ret
.set CLITERAL_compilation, (2 * bee_word_bytes)
.set CLITERAL_info, _immediate_bit | _compiling_bit | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "SLITERAL"
.balign bee_word_bytes, 0x20 
.word CLITERAL - .
.word SLITERAL_compilation
.word SLITERAL_info 
.global SLITERAL
SLITERAL:
pushreli _28_S_22__29_
calli _28_POSTPONE_29_
calli _22__2C_
pushi 0 
calli CALIGN
ret
.set SLITERAL_compilation, (2 * bee_word_bytes)
.set SLITERAL_info, _immediate_bit | _compiling_bit | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "2@"
.balign bee_word_bytes, 0x20 
.word SLITERAL - .
.word _32__40__compilation
.word _32__40__info 
.global _32__40_
_32__40_:
pushi 0 # 0x0 
dup
calli CELL_2B_
load
pushi 0 # 0x0 
swap
load
ret
.set _32__40__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "2!"
.balign bee_word_bytes, 0x20 
.word _32__40_ - .
.word _32__21__compilation
.word _32__21__info 
.global _32__21_
_32__21_:
calli TUCK
store
calli CELL_2B_
store
ret
.set _32__21__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "2,"
.balign bee_word_bytes, 0x20 
.word _32__21_ - .
.word _32__2C__compilation
.word _32__2C__info 
.global _32__2C_
_32__2C_:
calli _2C_
calli _2C_
ret
.set _32__2C__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "BLANK"
.balign bee_word_bytes, 0x20 
.word _32__2C_ - .
.word BLANK_compilation
.word BLANK_info 
.global BLANK
BLANK:
calli BL
calli FILL
ret
.set BLANK_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "COMPARE"
.balign bee_word_bytes, 0x20 
.word BLANK - .
.word COMPARE_compilation
.word COMPARE_info 
.global COMPARE
COMPARE:
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushi 2 # 0x2 
swap
pushi 0 # 0x0 
swap
calli _32_OVER
calli MIN
pushi 0 # 0x0 
dup
calli _30__3E_
jumpzi .L4076f
pushi 0 
calli _32__3E_R
.L4079b:
pushi 1 # 0x1 
dup
load1
pushi 1 # 0x1 
dup
load1
calli _3C__3E_
jumpzi .L4086f
load1
pushi 0 # 0x0 
swap
load1
lt
neg
calli _32__2A_
not
calli NIP
calli NIP
calli UNLOOP
ret
.L4086f:
calli CHAR_2B_
pushi 0 # 0x0 
swap
calli CHAR_2B_
pushi 0 # 0x0 
swap
calli _28_LOOP_29_
jumpzi .L4079b
.L4078f:
calli UNLOOP
calli _32_DROP
calli _32_DUP
calli _3C__3E_
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
lt
neg
calli _32__2A_
not
and
jumpi .L4120f
.L4076f:
calli _32_DROP
calli _32_DROP
.L4120f:
ret
.set COMPARE_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "SEARCH"
.balign bee_word_bytes, 0x20 
.word COMPARE - .
.word SEARCH_compilation
.word SEARCH_info 
.global SEARCH
SEARCH:
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli _32_DUP
pushi 1 # 0x1 
dup
pushi 0 # 0x0 
swap
calli U_3E_
pushi 0 # 0x0 
swap
calli _30__3D_
or
jumpzi .L4142f
calli NIP
calli NIP
calli FALSE
ret
.L4142f:
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
calli _32_OVER
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushi 2 # 0x2 
swap
pushi 0 # 0x0 
swap
calli TUCK
calli _32__3E_R
neg
add
not
neg
pushi 1 # 0x1 
dup
add
pushi 0 # 0x0 
swap
calli _32_R_3E_
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushi 2 # 0x2 
swap
pushi 0 # 0x0 
swap
calli _32__3E_R
.L4181b:
calli _32_DUP
dups
pushi 1 # 0x1 
dup
calli COMPARE
calli _30__3D_
jumpzi .L4187f
calli _32_DROP
add
dups
calli TUCK
neg
add
calli TRUE
calli UNLOOP
ret
.L4187f:
calli _28_LOOP_29_
jumpzi .L4181b
.L4180f:
calli UNLOOP
calli _32_DROP
calli FALSE
ret
.set SEARCH_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "\"CASE"
.balign bee_word_bytes, 0x20 
.word SEARCH - .
.word _22_CASE_compilation
.word _22_CASE_info 
.global _22_CASE
_22_CASE:
calli CASE - (2 * bee_word_bytes) + CASE_compilation
ret
.set _22_CASE_compilation, (2 * bee_word_bytes)
.set _22_CASE_info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "\"OF"
.balign bee_word_bytes, 0x20 
.word _22_CASE - .
.word _22_OF_compilation
.word _22_OF_info 
.global _22_OF
_22_OF:
not
neg
pushs
pushreli _32_OVER
calli _28_POSTPONE_29_
pushreli COMPARE
calli _28_POSTPONE_29_
pushreli _30__3D_
calli _28_POSTPONE_29_
calli IF - (2 * bee_word_bytes) + IF_compilation
pushreli _32_DROP
calli _28_POSTPONE_29_
pops
ret
.set _22_OF_compilation, (2 * bee_word_bytes)
.set _22_OF_info, _immediate_bit | _compiling_bit | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "\"ENDOF"
.balign bee_word_bytes, 0x20 
.word _22_OF - .
.word _22_ENDOF_compilation
.word _22_ENDOF_info 
.global _22_ENDOF
_22_ENDOF:
calli ENDOF - (2 * bee_word_bytes) + ENDOF_compilation
ret
.set _22_ENDOF_compilation, (2 * bee_word_bytes)
.set _22_ENDOF_info, _immediate_bit | _compiling_bit | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "\"ENDCASE"
.balign bee_word_bytes, 0x20 
.word _22_ENDOF - .
.word _22_ENDCASE_compilation
.word _22_ENDCASE_info 
.global _22_ENDCASE
_22_ENDCASE:
pushreli _32_DROP
calli _28_POSTPONE_29_
pushi 0 
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L4245f
jumpi .L4241f
.L4245f:
.L4247b:
calli THEN - (2 * bee_word_bytes) + THEN_compilation
calli _28_LOOP_29_
jumpzi .L4247b
.L4241f:
.L4246f:
calli UNLOOP
ret
.set _22_ENDCASE_compilation, (2 * bee_word_bytes)
.set _22_ENDCASE_info, _immediate_bit | _compiling_bit | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "READ-LINE"
.balign bee_word_bytes, 0x20 
.word _22_ENDCASE - .
.word READ_2D_LINE_compilation
.word READ_2D_LINE_info 
.global READ_2D_LINE
READ_2D_LINE:
pushs
pushi 1 # 0x1 
dup
pushi 0 # 0x0 
swap
dups
calli READ_2D_FILE
calli _3F_DUP
jumpzi .L4265f
calli NIP
calli NIP
pushi 0 
calli FALSE
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
pops
pop
ret
.L4265f:
pushi 0 # 0x0 
dup
calli _30__3D_
jumpzi .L4280f
calli NIP
calli FALSE
pushi 0 
pops
pop
ret
.L4280f:
calli TUCK
calli EOL
calli SEARCH
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
pop
jumpzi .L4295f
calli TUCK
neg
add
pushi 0 # 0x0 
swap
calli EOL
calli NIP
neg
add
calli _3F_DUP
jumpzi .L4306f
dups
calli FILE_2D_POSITION
calli _3F_DUP
jumpzi .L4310f
pushs
calli _32_DROP
pop
calli FALSE
pops
pops
pop
ret
.L4310f:
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli U_3E_UD
calli D_2D_
dups
calli REPOSITION_2D_FILE
calli _3F_DUP
jumpzi .L4328f
calli FALSE
pushi 0 # 0x0 
swap
ret
.L4328f:
.L4306f:
jumpi .L4333f
.L4295f:
pop
.L4333f:
pops
pop
calli TRUE
pushi 0 
ret
.set READ_2D_LINE_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "WRITE-LINE"
.balign bee_word_bytes, 0x20 
.word READ_2D_LINE - .
.word WRITE_2D_LINE_compilation
.word WRITE_2D_LINE_info 
.global WRITE_2D_LINE
WRITE_2D_LINE:
pushs
dups
calli WRITE_2D_FILE
calli _3F_DUP
jumpzi .L4349f
pops
pop
ret
.L4349f:
calli EOL
pops
calli WRITE_2D_FILE
ret
.set WRITE_2D_LINE_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "SPACE"
.balign bee_word_bytes, 0x20 
.word WRITE_2D_LINE - .
.word SPACE_compilation
.word SPACE_info 
.global SPACE
SPACE:
calli BL
calli EMIT
ret
.set SPACE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "SPACES"
.balign bee_word_bytes, 0x20 
.word SPACE - .
.word SPACES_compilation
.word SPACES_info 
.global SPACES
SPACES:
pushi 0 
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L4373f
jumpi .L4369f
.L4373f:
.L4375b:
calli SPACE
calli _28_LOOP_29_
jumpzi .L4375b
.L4369f:
.L4374f:
calli UNLOOP
ret
.set SPACES_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "TYPE"
.balign bee_word_bytes, 0x20 
.word SPACES - .
.word TYPE_compilation
.word TYPE_info 
.global TYPE
TYPE:
pushi 1 # 0x1 
dup
add
pushi 0 # 0x0 
swap
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L4393f
jumpi .L4389f
.L4393f:
.L4395b:
dups
load1
calli EMIT
calli _2B_CHAR
calli _28__2B_LOOP_29_
jumpzi .L4395b
.L4389f:
.L4394f:
calli UNLOOP
ret
.set TYPE_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "-TRAILING"
.balign bee_word_bytes, 0x20 
.word TYPE - .
.word _2D_TRAILING_compilation
.word _2D_TRAILING_info 
.global _2D_TRAILING
_2D_TRAILING:
.L4408b:
pushi 0 # 0x0 
dup
jumpzi .L4410f
calli _32_DUP
neg
not
add
load1
calli BL
eq
neg
jumpi .L4419f
.L4410f:
calli FALSE
.L4419f:
jumpzi .L4421f
neg
not
jumpi .L4408b
.L4421f:
ret
.set _2D_TRAILING_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "/FILE-BUFFER"
.balign bee_word_bytes, 0x20 
.word _2D_TRAILING - .
.word _2F_FILE_2D_BUFFER_compilation
.word _2F_FILE_2D_BUFFER_info 
.global _2F_FILE_2D_BUFFER
_2F_FILE_2D_BUFFER:
pushi 1024 
ret
.set _2F_FILE_2D_BUFFER_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "#FILE-BUFFERS"
.balign bee_word_bytes, 0x20 
.word _2F_FILE_2D_BUFFER - .
.word _23_FILE_2D_BUFFERS_compilation
.word _23_FILE_2D_BUFFERS_info 
.global _23_FILE_2D_BUFFERS
_23_FILE_2D_BUFFERS:
pushi 16 
ret
.set _23_FILE_2D_BUFFERS_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "FILE-BUFFER#"
.balign bee_word_bytes, 0x20 
.word _23_FILE_2D_BUFFERS - .
.word FILE_2D_BUFFER_23__compilation
.word FILE_2D_BUFFER_23__info 
.global FILE_2D_BUFFER_23_
FILE_2D_BUFFER_23_:
nop 
calli FILE_2D_BUFFER_23__doer
.balign bee_word_bytes
FILE_2D_BUFFER_23__body:
.ds.b 1 * bee_word_bytes
.set FILE_2D_BUFFER_23__info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.set FILE_2D_BUFFER_23__doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0xA 
.ascii "FIRST-FILE"
.balign bee_word_bytes, 0x20 
.word FILE_2D_BUFFER_23_ - .
.word FIRST_2D_FILE_compilation
.word FIRST_2D_FILE_info 
.global FIRST_2D_FILE
FIRST_2D_FILE:
nop 
calli FIRST_2D_FILE_doer
.balign bee_word_bytes
FIRST_2D_FILE_body:
.ds.b 16384 
.set FIRST_2D_FILE_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.set FIRST_2D_FILE_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0xF 
.ascii "ALLOCATE-BUFFER"
.balign bee_word_bytes, 0x20 
.word FIRST_2D_FILE - .
.word ALLOCATE_2D_BUFFER_compilation
.word ALLOCATE_2D_BUFFER_info 
.global ALLOCATE_2D_BUFFER
ALLOCATE_2D_BUFFER:
calli FILE_2D_BUFFER_23_
load
pushi 0 # 0x0 
dup
calli _23_FILE_2D_BUFFERS
eq
neg
jumpzi .L6515f
pushi -1 
jumpi .L6517f
.L6515f:
pushi 0 # 0x0 
dup
not
neg
calli FILE_2D_BUFFER_23_
store
calli _2F_FILE_2D_BUFFER
mul
calli FIRST_2D_FILE
add
pushi 0 
.L6517f:
ret
.set ALLOCATE_2D_BUFFER_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "FREE-BUFFER"
.balign bee_word_bytes, 0x20 
.word ALLOCATE_2D_BUFFER - .
.word FREE_2D_BUFFER_compilation
.word FREE_2D_BUFFER_info 
.global FREE_2D_BUFFER
FREE_2D_BUFFER:
calli FILE_2D_BUFFER_23_
pushi 0 # 0x0 
dup
load
calli _30__3D_
jumpzi .L6540f
pop
pushi -1 
jumpi .L6543f
.L6540f:
pushi -1 
pushi 0 # 0x0 
swap
calli _2B__21_
pushi 0 
.L6543f:
ret
.set FREE_2D_BUFFER_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "ACCEPT"
.balign bee_word_bytes, 0x20 
.word FREE_2D_BUFFER - .
.word ACCEPT_compilation
.word ACCEPT_info 
.global ACCEPT
ACCEPT:
calli STDIN
calli READ_2D_LINE
calli _32_DROP
ret
.set ACCEPT_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii ">IN"
.balign bee_word_bytes, 0x20 
.word ACCEPT - .
.word _3E_IN_compilation
.word _3E_IN_info 
.global _3E_IN
_3E_IN:
nop 
calli _3E_IN_doer
.balign bee_word_bytes
_3E_IN_body:
.ds.b 1 * bee_word_bytes
.set _3E_IN_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.set _3E_IN_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x8 
.ascii "EVALUAND"
.balign bee_word_bytes, 0x20 
.word _3E_IN - .
.word EVALUAND_compilation
.word EVALUAND_info 
.global EVALUAND
EVALUAND:
nop 
calli EVALUAND_doer
.balign bee_word_bytes
EVALUAND_body:
.ds.b 1 * bee_word_bytes
.set EVALUAND_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.set EVALUAND_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x9 
.ascii "#EVALUAND"
.balign bee_word_bytes, 0x20 
.word EVALUAND - .
.word _23_EVALUAND_compilation
.word _23_EVALUAND_info 
.global _23_EVALUAND
_23_EVALUAND:
nop 
calli _23_EVALUAND_doer
.balign bee_word_bytes
_23_EVALUAND_body:
.ds.b 1 * bee_word_bytes
.set _23_EVALUAND_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.set _23_EVALUAND_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x4 
.ascii "#TIB"
.balign bee_word_bytes, 0x20 
.word _23_EVALUAND - .
.word _23_TIB_compilation
.word _23_TIB_info 
.global _23_TIB
_23_TIB:
nop 
calli _23_TIB_doer
.balign bee_word_bytes
_23_TIB_body:
.ds.b 1 * bee_word_bytes
.set _23_TIB_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.set _23_TIB_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x3 
.ascii "TIB"
.balign bee_word_bytes, 0x20 
.word _23_TIB - .
.word TIB_compilation
.word TIB_info 
.global TIB
TIB:
nop 
calli TIB_doer
.balign bee_word_bytes
TIB_body:
.ds.b 256 
.set TIB_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.set TIB_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x4 
.ascii "#FIB"
.balign bee_word_bytes, 0x20 
.word TIB - .
.word _23_FIB_compilation
.word _23_FIB_info 
.global _23_FIB
_23_FIB:
nop 
calli _23_FIB_doer
.balign bee_word_bytes
_23_FIB_body:
.ds.b 1 * bee_word_bytes
.set _23_FIB_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.set _23_FIB_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x3 
.ascii "FIB"
.balign bee_word_bytes, 0x20 
.word _23_FIB - .
.word FIB_compilation
.word FIB_info 
.global FIB
FIB:
nop 
calli FIB_doer
.balign bee_word_bytes
FIB_body:
.word 0 
.set FIB_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.set FIB_doer, VALUE_does
.balign bee_word_bytes
.byte 0x9 
.ascii "SOURCE-ID"
.balign bee_word_bytes, 0x20 
.word FIB - .
.word SOURCE_2D_ID_compilation
.word SOURCE_2D_ID_info 
.global SOURCE_2D_ID
SOURCE_2D_ID:
nop 
calli SOURCE_2D_ID_doer
.balign bee_word_bytes
SOURCE_2D_ID_body:
.word 0 
.set SOURCE_2D_ID_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.set SOURCE_2D_ID_doer, VALUE_does
.balign bee_word_bytes
.byte 0x6 
.ascii "SOURCE"
.balign bee_word_bytes, 0x20 
.word SOURCE_2D_ID - .
.word SOURCE_compilation
.word SOURCE_info 
.global SOURCE
SOURCE:
calli SOURCE_2D_ID
pushi -1 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L6658f
pop
calli EVALUAND
load
calli _23_EVALUAND
load
jumpi .L6664f
.L6658f:
pushi 0 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L6670f
pop
calli TIB
calli _23_TIB
load
jumpi .L6675f
.L6670f:
pushs
calli FIB
calli _23_FIB
load
pops
pop
.L6675f:
.L6664f:
ret
.set SOURCE_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "SAVE-INPUT"
.balign bee_word_bytes, 0x20 
.word SOURCE - .
.word SAVE_2D_INPUT_compilation
.word SAVE_2D_INPUT_info 
.global SAVE_2D_INPUT
SAVE_2D_INPUT:
calli _3E_IN
load
calli SOURCE_2D_ID
pushi 0 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L6696f
pop
pushi 0 
pushi 2 
jumpi .L6700f
.L6696f:
pushi -1 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L6706f
pop
calli EVALUAND
load
calli _23_EVALUAND
load
pushi -1 
pushi 4 
jumpi .L6714f
.L6706f:
pushs
calli FIB
calli _23_FIB
load
calli SOURCE_2D_ID
pushi 2 
pushi 5 
pops
pop
.L6714f:
.L6700f:
ret
.set SAVE_2D_INPUT_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "RESTORE-INPUT"
.balign bee_word_bytes, 0x20 
.word SAVE_2D_INPUT - .
.word RESTORE_2D_INPUT_compilation
.word RESTORE_2D_INPUT_info 
.global RESTORE_2D_INPUT
RESTORE_2D_INPUT:
pop
pushi 0 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L6736f
pop
pushi 0 
pushreli SOURCE_2D_ID_body
store
jumpi .L6741f
.L6736f:
pushi 2 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L6747f
pop
pushreli SOURCE_2D_ID_body
store
calli _23_FIB
store
pushreli FIB_body
store
jumpi .L6755f
.L6747f:
pushi -1 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L6761f
pop
calli _23_EVALUAND
store
calli EVALUAND
store
pushi -1 
pushreli SOURCE_2D_ID_body
store
jumpi .L6770f
.L6761f:
pop
.L6770f:
.L6755f:
.L6741f:
calli _3E_IN
store
calli FALSE
ret
.set RESTORE_2D_INPUT_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "'RETURN"
.balign bee_word_bytes, 0x20 
.word RESTORE_2D_INPUT - .
.word _27_RETURN_compilation
.word _27_RETURN_info 
.global _27_RETURN
_27_RETURN:
nop 
calli _27_RETURN_doer
.balign bee_word_bytes
_27_RETURN_body:
.ds.b 1 * bee_word_bytes
.set _27_RETURN_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set _27_RETURN_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0xC 
.ascii "SAVE-INPUT>R"
.balign bee_word_bytes, 0x20 
.word _27_RETURN - .
.word SAVE_2D_INPUT_3E_R_compilation
.word SAVE_2D_INPUT_3E_R_info 
.global SAVE_2D_INPUT_3E_R
SAVE_2D_INPUT_3E_R:
pops
calli _27_RETURN
store
calli SAVE_2D_INPUT
pushi 0 # 0x0 
dup
.L6794b:
calli _3F_DUP
jumpzi .L6795f
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
pushs
neg
not
jumpi .L6794b
.L6795f:
pushs
calli _27_RETURN
load
pushs
ret
.set SAVE_2D_INPUT_3E_R_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xF 
.ascii "R>RESTORE-INPUT"
.balign bee_word_bytes, 0x20 
.word SAVE_2D_INPUT_3E_R - .
.word R_3E_RESTORE_2D_INPUT_compilation
.word R_3E_RESTORE_2D_INPUT_info 
.global R_3E_RESTORE_2D_INPUT
R_3E_RESTORE_2D_INPUT:
pops
calli _27_RETURN
store
pops
pushi 0 # 0x0 
dup
.L6820b:
calli _3F_DUP
jumpzi .L6821f
pops
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
neg
not
jumpi .L6820b
.L6821f:
calli RESTORE_2D_INPUT
pop
calli _27_RETURN
load
pushs
ret
.set R_3E_RESTORE_2D_INPUT_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "SCAN-TEST"
.balign bee_word_bytes, 0x20 
.word R_3E_RESTORE_2D_INPUT - .
.word SCAN_2D_TEST_compilation
.word SCAN_2D_TEST_info 
.global SCAN_2D_TEST
SCAN_2D_TEST:
nop 
calli SCAN_2D_TEST_doer
.balign bee_word_bytes
SCAN_2D_TEST_body:
.word SCAN_2D_TEST_defer - .
.set SCAN_2D_TEST_defer, ABORT
.set SCAN_2D_TEST_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.set SCAN_2D_TEST_doer, DEFER_does
.balign bee_word_bytes
.byte 0x4 
.ascii "SCAN"
.balign bee_word_bytes, 0x20 
.word SCAN_2D_TEST - .
.word SCAN_compilation
.word SCAN_info 
.global SCAN
SCAN:
pushreli SCAN_2D_TEST
calli DEFER_21_
calli SOURCE
pushi 1 # 0x1 
dup
add
pushi 0 # 0x0 
swap
calli _3E_IN
load
add
pushi 0 # 0x0 
swap
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
pushi 1 # 0x1 
dup
pushi 3 
dup
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L6873f
jumpi .L6869f
.L6873f:
.L6875b:
pushi 0 # 0x0 
dup
dups
load1
calli SCAN_2D_TEST
jumpzi .L6880f
calli NIP
dups
pushi 0 # 0x0 
swap
jumpi .L6869f
.L6880f:
calli _2B_CHAR
calli _28__2B_LOOP_29_
jumpzi .L6875b
.L6869f:
.L6885f:
.L6874f:
calli UNLOOP
pop
pushi 1 # 0x1 
dup
neg
add
pushi 0 # 0x0 
dup
calli _3E_IN
calli _2B__21_
ret
.set SCAN_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "PARSE"
.balign bee_word_bytes, 0x20 
.word SCAN - .
.word PARSE_compilation
.word PARSE_info 
.global PARSE
PARSE:
pushreli _3D_
calli SCAN
calli _3E_IN
pushi 0 # 0x0 
dup
load
calli CHAR_2B_
calli SOURCE
calli NIP
calli MIN
pushi 0 # 0x0 
swap
store
ret
.set PARSE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "WORD"
.balign bee_word_bytes, 0x20 
.word PARSE - .
.word WORD_compilation
.word WORD_info 
.global WORD
WORD:
pushi 0 # 0x0 
dup
pushreli _3C__3E_
calli SCAN
calli _32_DROP
calli PARSE
calli TOKEN
calli _32_DUP
store1
calli CHAR_2B_
calli _32_DUP
add
calli BL
pushi 0 # 0x0 
swap
store1
pushi 0 # 0x0 
swap
calli CMOVE
calli TOKEN
ret
.set WORD_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii ".("
.balign bee_word_bytes, 0x20 
.word WORD - .
.word _2E__28__compilation
.word _2E__28__info 
.global _2E__28_
_2E__28_:
pushi 41 
calli PARSE
calli TYPE
ret
.set _2E__28__compilation, (2 * bee_word_bytes)
.L6951b:
.byte 0x6 
.ascii "pforth"
.set _2E__28__info, _immediate_bit | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "\"PROGRAM-NAME"
.balign bee_word_bytes, 0x20 
.word _2E__28_ - .
.word _22_PROGRAM_2D_NAME_compilation
.word _22_PROGRAM_2D_NAME_info 
.global _22_PROGRAM_2D_NAME
_22_PROGRAM_2D_NAME:
nop 
calli _22_PROGRAM_2D_NAME_doer
.balign bee_word_bytes
_22_PROGRAM_2D_NAME_body:
.word .L6951b - .
.set _22_PROGRAM_2D_NAME_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.set _22_PROGRAM_2D_NAME_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0xC 
.ascii "ERROR-PREFIX"
.balign bee_word_bytes, 0x20 
.word _22_PROGRAM_2D_NAME - .
.word ERROR_2D_PREFIX_compilation
.word ERROR_2D_PREFIX_info 
.global ERROR_2D_PREFIX
ERROR_2D_PREFIX:
calli _22_PROGRAM_2D_NAME
calli REL_40_
calli COUNT
calli TYPE
calli _28_S_22__29_
.byte 0x2 
.ascii ": "
.balign bee_word_bytes, 0x0 
calli TYPE
ret
.set ERROR_2D_PREFIX_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "C\""
.balign bee_word_bytes, 0x20 
.word ERROR_2D_PREFIX - .
.word C_22__compilation
.word C_22__info 
.global C_22_
C_22_:
pushi 34 
calli PARSE
calli CLITERAL - (2 * bee_word_bytes) + CLITERAL_compilation
ret
.set C_22__compilation, (2 * bee_word_bytes)
.set C_22__info, _immediate_bit | _compiling_bit | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "S\""
.balign bee_word_bytes, 0x20 
.word C_22_ - .
.word S_22__compilation
.word S_22__info 
.global S_22_
S_22_:
pushi 34 
calli PARSE
calli S_22_B
pushi 0 # 0x0 
swap
calli _32_DUP
calli _32__3E_R
calli CMOVE
calli _32_R_3E_
ret
.balign bee_word_bytes
.word 0 
.L6996n:
pushi 34 
calli PARSE
calli SLITERAL - (2 * bee_word_bytes) + SLITERAL_compilation
ret
.set S_22__compilation, .L6996n - (S_22_ - 2 * bee_word_bytes)
.set S_22__info, _immediate_bit | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii ".\""
.balign bee_word_bytes, 0x20 
.word S_22_ - .
.word _2E__22__compilation
.word _2E__22__info 
.global _2E__22_
_2E__22_:
calli S_22_ - (2 * bee_word_bytes) + S_22__compilation
pushreli TYPE
calli _28_POSTPONE_29_
ret
.set _2E__22__compilation, (2 * bee_word_bytes)
.set _2E__22__info, _immediate_bit | _compiling_bit | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "CHAR"
.balign bee_word_bytes, 0x20 
.word _2E__22_ - .
.word CHAR_compilation
.word CHAR_info 
.global CHAR
CHAR:
calli BL
calli WORD
calli CHAR_2B_
load1
ret
.set CHAR_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "[CHAR]"
.balign bee_word_bytes, 0x20 
.word CHAR - .
.word _5B_CHAR_5D__compilation
.word _5B_CHAR_5D__info 
.global _5B_CHAR_5D_
_5B_CHAR_5D_:
calli CHAR
calli LITERAL - (2 * bee_word_bytes) + LITERAL_compilation
ret
.set _5B_CHAR_5D__compilation, (2 * bee_word_bytes)
.set _5B_CHAR_5D__info, _immediate_bit | _compiling_bit | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "ABORT\""
.balign bee_word_bytes, 0x20 
.word _5B_CHAR_5D_ - .
.word ABORT_22__compilation
.word ABORT_22__info 
.global ABORT_22_
ABORT_22_:
calli C_22_ - (2 * bee_word_bytes) + C_22__compilation
pushreli _28_ABORT_22__29_
calli _28_POSTPONE_29_
ret
.set ABORT_22__compilation, (2 * bee_word_bytes)
.set ABORT_22__info, _immediate_bit | _compiling_bit | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "BASE"
.balign bee_word_bytes, 0x20 
.word ABORT_22_ - .
.word BASE_compilation
.word BASE_info 
.global BASE
BASE:
nop 
calli BASE_doer
.balign bee_word_bytes
BASE_body:
.ds.b 1 * bee_word_bytes
.set BASE_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.set BASE_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x4 
.ascii "HELD"
.balign bee_word_bytes, 0x20 
.word BASE - .
.word HELD_compilation
.word HELD_info 
.global HELD
HELD:
nop 
calli HELD_doer
.balign bee_word_bytes
HELD_body:
.ds.b 1 * bee_word_bytes
.set HELD_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.set HELD_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x7 
.ascii "DECIMAL"
.balign bee_word_bytes, 0x20 
.word HELD - .
.word DECIMAL_compilation
.word DECIMAL_info 
.global DECIMAL
DECIMAL:
pushi 10 
calli BASE
store
ret
.set DECIMAL_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "HEX"
.balign bee_word_bytes, 0x20 
.word DECIMAL - .
.word HEX_compilation
.word HEX_info 
.global HEX
HEX:
pushi 16 
calli BASE
store
ret
.set HEX_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "HOLD"
.balign bee_word_bytes, 0x20 
.word HEX - .
.word HOLD_compilation
.word HOLD_info 
.global HOLD
HOLD:
calli _2D_CHAR
calli HELD
calli _2B__21_
calli HELD
load
store1
ret
.set HOLD_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "SIGN"
.balign bee_word_bytes, 0x20 
.word HOLD - .
.word SIGN_compilation
.word SIGN_info 
.global SIGN
SIGN:
calli _30__3C_
jumpzi .L7078f
pushi 45 
calli HOLD
.L7078f:
ret
.set SIGN_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "<#"
.balign bee_word_bytes, 0x20 
.word SIGN - .
.word _3C__23__compilation
.word _3C__23__info 
.global _3C__23_
_3C__23_:
calli TOKEN_2D_END
calli HELD
store
ret
.set _3C__23__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "#>"
.balign bee_word_bytes, 0x20 
.word _3C__23_ - .
.word _23__3E__compilation
.word _23__3E__info 
.global _23__3E_
_23__3E_:
calli _32_DROP
calli HELD
load
calli TOKEN_2D_END
pushi 1 # 0x1 
dup
neg
add
ret
.set _23__3E__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "#"
.balign bee_word_bytes, 0x20 
.word _23__3E_ - .
.word _23__compilation
.word _23__info 
.global _23_
_23_:
calli BASE
load
calli U_3E_UD
calli UD_2F_MOD
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushi 2 # 0x2 
swap
pushi 0 # 0x0 
swap
calli UD_3E_U
pushi 0 # 0x0 
dup
pushi 10 
lt
neg
jumpzi .L7125f
pushi 48 
add
jumpi .L7128f
.L7125f:
pushi 55 
add
.L7128f:
calli HOLD
ret
.set _23__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "#S"
.balign bee_word_bytes, 0x20 
.word _23_ - .
.word _23_S_compilation
.word _23_S_info 
.global _23_S
_23_S:
.L7137b:
calli _23_
calli _32_DUP
calli D_30__3D_
jumpzi .L7137b
ret
.set _23_S_info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "D.R"
.balign bee_word_bytes, 0x20 
.word _23_S - .
.word D_2E_R_compilation
.word D_2E_R_info 
.global D_2E_R
D_2E_R:
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
calli TUCK
calli DABS
calli _3C__23_
calli _23_S
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli SIGN
calli _23__3E_
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
pushi 1 # 0x1 
dup
neg
add
pushi 0 
calli MAX
calli SPACES
calli TYPE
ret
.set D_2E_R_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "D."
.balign bee_word_bytes, 0x20 
.word D_2E_R - .
.word D_2E__compilation
.word D_2E__info 
.global D_2E_
D_2E_:
pushi 0 
calli D_2E_R
calli SPACE
ret
.set D_2E__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii ".R"
.balign bee_word_bytes, 0x20 
.word D_2E_ - .
.word _2E_R_compilation
.word _2E_R_info 
.global _2E_R
_2E_R:
pushi 0 # 0x0 
swap
calli S_3E_D
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli D_2E_R
ret
.set _2E_R_info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "."
.balign bee_word_bytes, 0x20 
.word _2E_R - .
.word _2E__compilation
.word _2E__info 
.global _2E_
_2E_:
pushi 0 
calli _2E_R
calli SPACE
ret
.set _2E__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "DEC."
.balign bee_word_bytes, 0x20 
.word _2E_ - .
.word DEC_2E__compilation
.word DEC_2E__info 
.global DEC_2E_
DEC_2E_:
calli BASE
load
pushi 0 # 0x0 
swap
calli DECIMAL
calli _2E_
calli BASE
store
ret
.set DEC_2E__info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "U.R"
.balign bee_word_bytes, 0x20 
.word DEC_2E_ - .
.word U_2E_R_compilation
.word U_2E_R_info 
.global U_2E_R
U_2E_R:
pushi 0 # 0x0 
swap
calli U_3E_UD
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli D_2E_R
ret
.set U_2E_R_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "U."
.balign bee_word_bytes, 0x20 
.word U_2E_R - .
.word U_2E__compilation
.word U_2E__info 
.global U_2E_
U_2E_:
pushi 0 
calli U_2E_R
calli SPACE
ret
.set U_2E__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "H."
.balign bee_word_bytes, 0x20 
.word U_2E_ - .
.word H_2E__compilation
.word H_2E__info 
.global H_2E_
H_2E_:
calli BASE
load
pushi 0 # 0x0 
swap
calli HEX
calli U_2E_
calli BASE
store
ret
.set H_2E__info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii ">NUMBER"
.balign bee_word_bytes, 0x20 
.word H_2E_ - .
.word _3E_NUMBER_compilation
.word _3E_NUMBER_info 
.global _3E_NUMBER
_3E_NUMBER:
pushi 0 # 0x0 
dup
jumpzi .L7255f
pushi 1 # 0x1 
dup
add
pushi 0 # 0x0 
swap
calli TUCK
pushi 1 # 0x1 
dup
pushs
calli _32__3E_R
.L7266b:
load1
pushi 0 # 0x0 
dup
pushi 65 
lt
neg
jumpzi .L7272f
pushi 48 
neg
add
jumpi .L7276f
.L7272f:
pushi 55 
neg
add
.L7276f:
pushi 0 # 0x0 
dup
calli BASE
load
neg
add
calli _30__3C_
not
pushi 1 # 0x1 
dup
calli _30__3C_
or
jumpzi .L7292f
pop
dups
jumpi .L7265f
.L7292f:
pushs
calli BASE
load
calli U_3E_UD
calli D_2A_
pops
calli M_2B_
dups
calli CHAR_2B_
calli _28_LOOP_29_
jumpzi .L7266b
.L7265f:
.L7295f:
calli UNLOOP
pushi 0 # 0x0 
dup
pops
calli _3E__2D__3C_
.L7255f:
ret
.set _3E_NUMBER_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "SKIP-CHAR"
.balign bee_word_bytes, 0x20 
.word _3E_NUMBER - .
.word SKIP_2D_CHAR_compilation
.word SKIP_2D_CHAR_info 
.global SKIP_2D_CHAR
SKIP_2D_CHAR:
neg
not
pushi 0 # 0x0 
swap
calli CHAR_2B_
pushi 0 # 0x0 
swap
ret
.set SKIP_2D_CHAR_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "NUMBER"
.balign bee_word_bytes, 0x20 
.word SKIP_2D_CHAR - .
.word NUMBER_compilation
.word NUMBER_info 
.global NUMBER
NUMBER:
pushi 0 # 0x0 
dup
pushs
pushi 0 
pushi 0 
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli COUNT
calli BASE
load
pushs
pushi 1 # 0x1 
dup
load1
pushi 35 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L7351f
pop
pushi 10 
calli BASE
store
calli SKIP_2D_CHAR
jumpi .L7357f
.L7351f:
pushi 36 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L7363f
pop
pushi 16 
calli BASE
store
calli SKIP_2D_CHAR
jumpi .L7369f
.L7363f:
pushi 37 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L7375f
pop
pushi 2 
calli BASE
store
calli SKIP_2D_CHAR
jumpi .L7381f
.L7375f:
pop
.L7381f:
.L7369f:
.L7357f:
pushi 1 # 0x1 
dup
load1
pushi 45 
eq
neg
pushi 0 # 0x0 
dup
pushs
jumpzi .L7392f
calli SKIP_2D_CHAR
.L7392f:
calli FALSE
pushs
.L7396b:
calli _3E_NUMBER
calli _3F_DUP
jumpzi .L7398f
pushi 1 # 0x1 
dup
load1
pushi 4 
calli _2F_
pushi 11 
calli _3C__3E_
jumpzi .L7406f
calli _32_R_3E_
calli _32_DROP
pops
calli BASE
store
pops
calli UNDEFINED
.L7406f:
pops
pop
calli TRUE
pushs
calli SKIP_2D_CHAR
jumpi .L7396b
.L7398f:
pop
calli _32_R_3E_
pushs
jumpzi .L7423f
calli DNEGATE
.L7423f:
dups
not
jumpzi .L7427f
calli D_3E_S
.L7427f:
pops
pops
calli BASE
store
pops
pop
ret
.set NUMBER_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "DEFINITIONS"
.balign bee_word_bytes, 0x20 
.word NUMBER - .
.word DEFINITIONS_compilation
.word DEFINITIONS_info 
.global DEFINITIONS
DEFINITIONS:
calli CONTEXT
load
calli SET_2D_CURRENT
ret
.set DEFINITIONS_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "GET-ORDER"
.balign bee_word_bytes, 0x20 
.word DEFINITIONS - .
.word GET_2D_ORDER_compilation
.word GET_2D_ORDER_info 
.global GET_2D_ORDER
GET_2D_ORDER:
calli _23_ORDER
load
pushi 0 # 0x0 
dup
jumpzi .L7454f
pushi 0 # 0x0 
dup
pushs
calli CELLS
calli CONTEXT
calli TUCK
add
calli CELL_2D_
calli _32__3E_R
.L7464b:
dups
load
calli _2D_CELL
calli _28__2B_LOOP_29_
jumpzi .L7464b
.L7463f:
calli UNLOOP
pops
.L7454f:
ret
.set GET_2D_ORDER_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "VISIBLE?"
.balign bee_word_bytes, 0x20 
.word GET_2D_ORDER - .
.word VISIBLE_3F__compilation
.word VISIBLE_3F__info 
.global VISIBLE_3F_
VISIBLE_3F_:
nop 
calli VISIBLE_3F__doer
.balign bee_word_bytes
VISIBLE_3F__body:
.word VISIBLE_3F__defer - .
.set VISIBLE_3F__defer, ABORT
.set VISIBLE_3F__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.set VISIBLE_3F__doer, DEFER_does
.balign bee_word_bytes
.byte 0xB 
.ascii "ALL-VISIBLE"
.balign bee_word_bytes, 0x20 
.word VISIBLE_3F_ - .
.word ALL_2D_VISIBLE_compilation
.word ALL_2D_VISIBLE_info 
.global ALL_2D_VISIBLE
ALL_2D_VISIBLE:
calli _32_DROP
pop
calli TRUE
ret
.set ALL_2D_VISIBLE_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "VET-WORDLIST"
.balign bee_word_bytes, 0x20 
.word ALL_2D_VISIBLE - .
.word VET_2D_WORDLIST_compilation
.word VET_2D_WORDLIST_info 
.global VET_2D_WORDLIST
VET_2D_WORDLIST:
pushi 0 # 0x0 
dup
pushs
.L7497b:
calli REL_40_
calli _3F_DUP
jumpzi .L7499f
pushi 0 # 0x0 
dup
calli _3E_NAME
calli _32_OVER
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli COUNT
calli COMPARE
calli _30__3D_
jumpzi .L7511f
pushi 0 # 0x0 
dup
calli _3E_INFO
load
calli SMUDGE_2D_BIT
and
calli _30__3D_
jumpzi .L7519f
dups
pushi 1 # 0x1 
dup
pushi 0 # 0x0 
dup
calli _3E_INFO
load
calli _30__3C_
calli _32__2A_
not
pushi 0 # 0x0 
dup
pushs
calli VISIBLE_3F_
jumpzi .L7534f
calli NIP
calli NIP
pops
pops
pop
ret
jumpi .L7541f
.L7534f:
pops
pop
.L7541f:
.L7519f:
.L7511f:
calli _3E_LINK
jumpi .L7497b
.L7499f:
calli _32_DROP
pops
pop
pushi 0 
ret
.set VET_2D_WORDLIST_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xF 
.ascii "SEARCH-WORDLIST"
.balign bee_word_bytes, 0x20 
.word VET_2D_WORDLIST - .
.word SEARCH_2D_WORDLIST_compilation
.word SEARCH_2D_WORDLIST_info 
.global SEARCH_2D_WORDLIST
SEARCH_2D_WORDLIST:
pushreli ALL_2D_VISIBLE
pushreli VISIBLE_3F_
calli DEFER_21_
calli VET_2D_WORDLIST
ret
.set SEARCH_2D_WORDLIST_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "SELECT"
.balign bee_word_bytes, 0x20 
.word SEARCH_2D_WORDLIST - .
.word SELECT_compilation
.word SELECT_info 
.global SELECT
SELECT:
pushreli VISIBLE_3F_
calli DEFER_21_
pushs
calli GET_2D_ORDER
pops
pushi 0 # 0x0 
swap
calli _3F_DUP
jumpzi .L7573f
pushi 1 
pushi 0 # 0x0 
swap
calli _32__3E_R
.L7578b:
calli TUCK
calli COUNT
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
calli VET_2D_WORDLIST
calli _3F_DUP
jumpzi .L7586f
dups
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
calli _32__3E_R
pushi 0 
calli _32__3E_R
.L7595b:
pop
calli _28_LOOP_29_
jumpzi .L7595b
.L7594f:
calli UNLOOP
calli _32_R_3E_
calli UNLOOP
ret
.L7586f:
pushi -1 
calli _28__2B_LOOP_29_
jumpzi .L7578b
.L7577f:
calli UNLOOP
.L7573f:
pushi 0 
ret
.set SELECT_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "FIND"
.balign bee_word_bytes, 0x20 
.word SELECT - .
.word FIND_compilation
.word FIND_info 
.global FIND
FIND:
pushreli ALL_2D_VISIBLE
calli SELECT
ret
.set FIND_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xF 
.ascii "CURRENT-LITERAL"
.balign bee_word_bytes, 0x20 
.word FIND - .
.word CURRENT_2D_LITERAL_compilation
.word CURRENT_2D_LITERAL_info 
.global CURRENT_2D_LITERAL
CURRENT_2D_LITERAL:
nop 
calli CURRENT_2D_LITERAL_doer
.balign bee_word_bytes
CURRENT_2D_LITERAL_body:
.word CURRENT_2D_LITERAL_defer - .
.set CURRENT_2D_LITERAL_defer, LITERAL
.set CURRENT_2D_LITERAL_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.set CURRENT_2D_LITERAL_doer, DEFER_does
.balign bee_word_bytes
.byte 0x18 
.ascii "CURRENT-RELATIVE-LITERAL"
.balign bee_word_bytes, 0x20 
.word CURRENT_2D_LITERAL - .
.word CURRENT_2D_RELATIVE_2D_LITERAL_compilation
.word CURRENT_2D_RELATIVE_2D_LITERAL_info 
.global CURRENT_2D_RELATIVE_2D_LITERAL
CURRENT_2D_RELATIVE_2D_LITERAL:
nop 
calli CURRENT_2D_RELATIVE_2D_LITERAL_doer
.balign bee_word_bytes
CURRENT_2D_RELATIVE_2D_LITERAL_body:
.word CURRENT_2D_RELATIVE_2D_LITERAL_defer - .
.set CURRENT_2D_RELATIVE_2D_LITERAL_defer, RELATIVE_2D_LITERAL
.set CURRENT_2D_RELATIVE_2D_LITERAL_info, 0 | 0 | 0 | (24 <<_name_length_bits) | 0x0 
.set CURRENT_2D_RELATIVE_2D_LITERAL_doer, DEFER_does
.balign bee_word_bytes
.byte 0x8 
.ascii "POSTPONE"
.balign bee_word_bytes, 0x20 
.word CURRENT_2D_RELATIVE_2D_LITERAL - .
.word POSTPONE_compilation
.word POSTPONE_info 
.global POSTPONE
POSTPONE:
calli BL
calli WORD
pushi 0 # 0x0 
dup
calli FIND
calli _3F_DUP
calli _30__3D_
jumpzi .L7645f
calli UNDEFINED
.L7645f:
calli _30__3E_
jumpzi .L7648f
calli _3E_COMPILE
calli REL_40_
calli CALL_2C_
calli _2E_CALL_2D_COMPILE_2D_METHOD
jumpi .L7653f
.L7648f:
calli PUSHREL_2C_
calli _2E_PUSHRELI_2D_SYMBOL
pushreli _28_POSTPONE_29_
calli CURRENT_2D_COMPILE_2C_
.L7653f:
ret
.set POSTPONE_compilation, (2 * bee_word_bytes)
.set POSTPONE_info, _immediate_bit | _compiling_bit | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "ISDIGIT"
.balign bee_word_bytes, 0x20 
.word POSTPONE - .
.word ISDIGIT_compilation
.word ISDIGIT_info 
.global ISDIGIT
ISDIGIT:
pushi 0 # 0x0 
dup
pushi 48 
lt
neg
not
pushi 0 # 0x0 
swap
pushi 57 
calli _3E_
not
and
ret
.set ISDIGIT_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "ISUPPER"
.balign bee_word_bytes, 0x20 
.word ISDIGIT - .
.word ISUPPER_compilation
.word ISUPPER_info 
.global ISUPPER
ISUPPER:
pushi 0 # 0x0 
dup
pushi 65 
lt
neg
not
pushi 0 # 0x0 
swap
pushi 90 
calli _3E_
not
and
ret
.set ISUPPER_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "ISLOWER"
.balign bee_word_bytes, 0x20 
.word ISUPPER - .
.word ISLOWER_compilation
.word ISLOWER_info 
.global ISLOWER
ISLOWER:
pushi 0 # 0x0 
dup
pushi 97 
lt
neg
not
pushi 0 # 0x0 
swap
pushi 122 
calli _3E_
not
and
ret
.set ISLOWER_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "ISALPHA"
.balign bee_word_bytes, 0x20 
.word ISLOWER - .
.word ISALPHA_compilation
.word ISALPHA_info 
.global ISALPHA
ISALPHA:
pushi 0 # 0x0 
dup
calli ISUPPER
pushi 0 # 0x0 
swap
calli ISLOWER
or
ret
.set ISALPHA_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "ISALNUM"
.balign bee_word_bytes, 0x20 
.word ISALPHA - .
.word ISALNUM_compilation
.word ISALNUM_info 
.global ISALNUM
ISALNUM:
pushi 0 # 0x0 
dup
calli ISDIGIT
pushi 0 # 0x0 
swap
calli ISALPHA
or
ret
.set ISALNUM_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "2.H"
.balign bee_word_bytes, 0x20 
.word ISALNUM - .
.word _32__2E_H_compilation
.word _32__2E_H_info 
.global _32__2E_H
_32__2E_H:
calli BASE
load
pushs
calli HEX
calli U_3E_UD
calli _3C__23_
calli _23_
calli _23_
calli _23__3E_
pops
calli BASE
store
calli TYPE
ret
.set _32__2E_H_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii ".MANGLE"
.balign bee_word_bytes, 0x20 
.word _32__2E_H - .
.word _2E_MANGLE_compilation
.word _2E_MANGLE_info 
.global _2E_MANGLE
_2E_MANGLE:
pushi 1 # 0x1 
dup
add
pushi 0 # 0x0 
swap
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L7765f
jumpi .L7761f
.L7765f:
.L7767b:
dups
load1
pushi 0 # 0x0 
dup
calli ISALPHA
jumpzi .L7772f
calli EMIT
jumpi .L7774f
.L7772f:
pushi 95 
calli EMIT
calli _32__2E_H
pushi 95 
calli EMIT
.L7774f:
calli _28_LOOP_29_
jumpzi .L7767b
.L7761f:
.L7766f:
calli UNLOOP
ret
.set _2E_MANGLE_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii ".NAME"
.balign bee_word_bytes, 0x20 
.word _2E_MANGLE - .
.word _2E_NAME_compilation
.word _2E_NAME_info 
.global _2E_NAME
_2E_NAME:
calli COUNT
calli _2E_MANGLE
ret
.set _2E_NAME_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii ".NAME-LABEL"
.balign bee_word_bytes, 0x20 
.word _2E_NAME - .
.word _2E_NAME_2D_LABEL_compilation
.word _2E_NAME_2D_LABEL_info 
.global _2E_NAME_2D_LABEL
_2E_NAME_2D_LABEL:
pushi 0 # 0x0 
dup
calli _28_S_22__29_
.byte 0x8 
.ascii ".global "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli CR
calli _2E_NAME
calli _28_S_22__29_
.byte 0x1 
.ascii ":"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set _2E_NAME_2D_LABEL_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "(.LINK)"
.balign bee_word_bytes, 0x20 
.word _2E_NAME_2D_LABEL - .
.word _28__2E_LINK_29__compilation
.word _28__2E_LINK_29__info 
.global _28__2E_LINK_29_
_28__2E_LINK_29_:
calli _28_S_22__29_
.byte 0x6 
.ascii ".word "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _3F_DUP
jumpzi .L7818f
calli _3E_NAME
calli _2E_NAME
jumpi .L7821f
.L7818f:
calli _28_S_22__29_
.byte 0x2 
.ascii ". "
.balign bee_word_bytes, 0x0 
calli TYPE
.L7821f:
calli _28_S_22__29_
.byte 0x4 
.ascii " - ."
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set _28__2E_LINK_29__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii ".LINK"
.balign bee_word_bytes, 0x20 
.word _28__2E_LINK_29_ - .
.word _2E_LINK_compilation
.word _2E_LINK_info 
.global _2E_LINK
_2E_LINK:
pushreli _28__2E_LINK_29_
calli TO_2D_ASMOUT
ret
.set _2E_LINK_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xE 
.ascii ".COMPILE-FIELD"
.balign bee_word_bytes, 0x20 
.word _2E_LINK - .
.word _2E_COMPILE_2D_FIELD_compilation
.word _2E_COMPILE_2D_FIELD_info 
.global _2E_COMPILE_2D_FIELD
_2E_COMPILE_2D_FIELD:
calli _28_S_22__29_
.byte 0x6 
.ascii ".word "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0xC 
.ascii "_compilation"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set _2E_COMPILE_2D_FIELD_info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii ".INFO-FIELD"
.balign bee_word_bytes, 0x20 
.word _2E_COMPILE_2D_FIELD - .
.word _2E_INFO_2D_FIELD_compilation
.word _2E_INFO_2D_FIELD_info 
.global _2E_INFO_2D_FIELD
_2E_INFO_2D_FIELD:
calli _28_S_22__29_
.byte 0x6 
.ascii ".word "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x6 
.ascii "_info "
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set _2E_INFO_2D_FIELD_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii ".INFO"
.balign bee_word_bytes, 0x20 
.word _2E_INFO_2D_FIELD - .
.word _2E_INFO_compilation
.word _2E_INFO_info 
.global _2E_INFO
_2E_INFO:
calli _28_S_22__29_
.byte 0x5 
.ascii ".set "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x7 
.ascii "_info, "
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 0 # 0x0 
dup
calli IMMEDIATE_2D_BIT
and
jumpzi .L7881f
calli _28_S_22__29_
.byte 0xE 
.ascii "_immediate_bit"
.balign bee_word_bytes, 0x0 
calli TYPE
jumpi .L7886f
.L7881f:
calli _28_S_22__29_
.byte 0x1 
.ascii "0"
.balign bee_word_bytes, 0x0 
calli TYPE
.L7886f:
calli IMMEDIATE_2D_BIT
not
and
calli _28_S_22__29_
.byte 0x3 
.ascii " | "
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 0 # 0x0 
dup
calli COMPILING_2D_BIT
and
jumpzi .L7900f
calli _28_S_22__29_
.byte 0xE 
.ascii "_compiling_bit"
.balign bee_word_bytes, 0x0 
calli TYPE
jumpi .L7905f
.L7900f:
calli _28_S_22__29_
.byte 0x1 
.ascii "0"
.balign bee_word_bytes, 0x0 
calli TYPE
.L7905f:
calli COMPILING_2D_BIT
not
and
calli _28_S_22__29_
.byte 0x3 
.ascii " | "
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 0 # 0x0 
dup
calli SMUDGE_2D_BIT
and
jumpzi .L7919f
calli _28_S_22__29_
.byte 0xB 
.ascii "_smudge_bit"
.balign bee_word_bytes, 0x0 
calli TYPE
jumpi .L7924f
.L7919f:
calli _28_S_22__29_
.byte 0x1 
.ascii "0"
.balign bee_word_bytes, 0x0 
calli TYPE
.L7924f:
calli SMUDGE_2D_BIT
not
and
calli _28_S_22__29_
.byte 0x3 
.ascii " | "
.balign bee_word_bytes, 0x0 
calli TYPE
calli CELL_2D_BITS
calli BYTE_2D_BITS
neg
add
calli _32_DUP
rshift
calli _28_S_22__29_
.byte 0x1 
.ascii "("
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_
calli _28_S_22__29_
.byte 0x2 
.ascii "<<"
.balign bee_word_bytes, 0x0 
calli TYPE
calli _28_S_22__29_
.byte 0x17 
.ascii "_name_length_bits) | 0x"
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 1 
pushi 0 # 0x0 
swap
lshift
neg
not
and
calli H_2E_
calli CR
ret
.set _2E_INFO_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xE 
.ascii ".PREVIOUS-INFO"
.balign bee_word_bytes, 0x20 
.word _2E_INFO - .
.word _2E_PREVIOUS_2D_INFO_compilation
.word _2E_PREVIOUS_2D_INFO_info 
.global _2E_PREVIOUS_2D_INFO
_2E_PREVIOUS_2D_INFO:
calli LAST
pushi 0 # 0x0 
dup
calli _3E_INFO
load
pushi 0 # 0x0 
swap
calli _3E_NAME
pushreli _2E_INFO
calli TO_2D_ASMOUT
ret
.set _2E_PREVIOUS_2D_INFO_info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii ".DOES"
.balign bee_word_bytes, 0x20 
.word _2E_PREVIOUS_2D_INFO - .
.word _2E_DOES_compilation
.word _2E_DOES_info 
.global _2E_DOES
_2E_DOES:
calli _28_S_22__29_
.byte 0x5 
.ascii ".set "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x7 
.ascii "_doer, "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x5 
.ascii "_does"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set _2E_DOES_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii ".DOES-LABEL"
.balign bee_word_bytes, 0x20 
.word _2E_DOES - .
.word _2E_DOES_2D_LABEL_compilation
.word _2E_DOES_2D_LABEL_info 
.global _2E_DOES_2D_LABEL
_2E_DOES_2D_LABEL:
calli _2E_NAME
calli _28_S_22__29_
.byte 0x6 
.ascii "_does:"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set _2E_DOES_2D_LABEL_info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii ".CREATED"
.balign bee_word_bytes, 0x20 
.word _2E_DOES_2D_LABEL - .
.word _2E_CREATED_compilation
.word _2E_CREATED_info 
.global _2E_CREATED
_2E_CREATED:
calli LAST
calli _3E_NAME
pushreli _2E_DOES
calli TO_2D_ASMOUT
ret
.set _2E_CREATED_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii ".SYMBOL"
.balign bee_word_bytes, 0x20 
.word _2E_CREATED - .
.word _2E_SYMBOL_compilation
.word _2E_SYMBOL_info 
.global _2E_SYMBOL
_2E_SYMBOL:
pushi 0 # 0x0 
dup
calli _3E_INFO
word_bytes
neg
not
add
load1
jumpzi .L8028f
calli _3E_NAME
calli _2E_NAME
jumpi .L8031f
.L8028f:
calli NONAME
calli _2E_LABEL
.L8031f:
ret
.set _2E_SYMBOL_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "HEADER"
.balign bee_word_bytes, 0x20 
.word _2E_SYMBOL - .
.word HEADER_compilation
.word HEADER_info 
.global HEADER
HEADER:
calli LAST
jumpzi .L8040f
calli _2E_PREVIOUS_2D_INFO
calli CREATED
load
calli _3F_DUP
jumpzi .L8045f
calli _2E_CREATED
.L8045f:
.L8040f:
calli FALSE
calli CREATED
store
pushi 0 # 0x0 
dup
pushs
calli ALIGN
pushi 0 # 0x0 
dup
load1
pushi 31 
calli MIN
pushi 1 # 0x1 
dup
store1
calli COUNT
calli _32_DUP
calli GET_2D_CURRENT
calli SEARCH_2D_WORDLIST
jumpzi .L8066f
pop
calli _32_DUP
calli TYPE
calli _28_S_22__29_
.byte 0xF 
.ascii " is not unique "
.balign bee_word_bytes, 0x0 
calli TYPE
.L8066f:
calli TUCK
calli _22__2C_
calli BL
calli CALIGN
calli LAST
calli _2E_LINK
calli LAST
calli RAW_2D_REL_2C_
dups
pushreli _2E_COMPILE_2D_FIELD
calli TO_2D_ASMOUT
pushi 0 
calli RAW_2C_
dups
pushreli _2E_INFO_2D_FIELD
calli TO_2D_ASMOUT
calli CELL_2D_BITS
calli BYTE_2D_BITS
neg
add
lshift
calli RAW_2C_
pops
pushreli _2E_NAME_2D_LABEL
calli TO_2D_ASMOUT
calli HERE
calli GET_2D_CURRENT
calli REL_21_
ret
.set HEADER_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "HANDLER"
.balign bee_word_bytes, 0x20 
.word HEADER - .
.word HANDLER_compilation
.word HANDLER_info 
.global HANDLER
HANDLER:
nop 
calli HANDLER_doer
.balign bee_word_bytes
HANDLER_body:
.ds.b 1 * bee_word_bytes
.set HANDLER_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set HANDLER_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x5 
.ascii "CATCH"
.balign bee_word_bytes, 0x20 
.word HANDLER - .
.word CATCH_compilation
.word CATCH_info 
.global CATCH
CATCH:
calli SP_40_
calli _2D_CELL
calli STACK_2D_DIRECTION
mul
add
pushs
calli HANDLER
load
pushs
calli SAVE_2D_INPUT_3E_R
get_sp
calli HANDLER
store
call
pops
.L8129b:
calli _3F_DUP
jumpzi .L8130f
pops
pop
neg
not
jumpi .L8129b
.L8130f:
pops
calli HANDLER
store
pops
pop
pushi 0 
ret
.set CATCH_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "FOREIGN?"
.balign bee_word_bytes, 0x20 
.word CATCH - .
.word FOREIGN_3F__compilation
.word FOREIGN_3F__info 
.global FOREIGN_3F_
FOREIGN_3F_:
pushi 2 
calli CELLS
add
load
pushi 1023 
calli _3E_
ret
.set FOREIGN_3F__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "LOCAL?"
.balign bee_word_bytes, 0x20 
.word FOREIGN_3F_ - .
.word LOCAL_3F__compilation
.word LOCAL_3F__info 
.global LOCAL_3F_
LOCAL_3F_:
calli NIP
pushi 1 
calli _3C__3E_
jumpzi .L8162f
calli STATE
load
calli GET_2D_CURRENT
calli FOREIGN_3F_
and
jumpzi .L8168f
calli GET_2D_CURRENT
eq
neg
ret
.L8168f:
.L8162f:
calli FOREIGN_3F_
not
ret
.set LOCAL_3F__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "NON-META?"
.balign bee_word_bytes, 0x20 
.word LOCAL_3F_ - .
.word NON_2D_META_3F__compilation
.word NON_2D_META_3F__info 
.global NON_2D_META_3F_
NON_2D_META_3F_:
calli NIP
pushi 1 
calli _3C__3E_
calli STATE
load
and
jumpzi .L8187f
pop
calli TRUE
jumpi .L8190f
.L8187f:
calli FOREIGN_3F_
not
.L8190f:
ret
.set NON_2D_META_3F__info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "'SELECTOR"
.balign bee_word_bytes, 0x20 
.word NON_2D_META_3F_ - .
.word _27_SELECTOR_compilation
.word _27_SELECTOR_info 
.global _27_SELECTOR
_27_SELECTOR:
nop 
calli _27_SELECTOR_doer
.balign bee_word_bytes
_27_SELECTOR_body:
.word LOCAL_3F_ - .
.set _27_SELECTOR_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.set _27_SELECTOR_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x9 
.ascii "INTERPRET"
.balign bee_word_bytes, 0x20 
.word _27_SELECTOR - .
.word INTERPRET_compilation
.word INTERPRET_info 
.global INTERPRET
INTERPRET:
.L8207b:
calli BL
calli WORD
pushi 0 # 0x0 
dup
load1
jumpzi .L8212f
calli _27_SELECTOR
calli REL_40_
calli SELECT
pushi 0 # 0x0 
dup
jumpzi .L8218f
calli STATE
load
calli _30__3D_
jumpzi .L8222f
pop
pushi 0 # 0x0 
dup
calli _3E_INFO
load
calli COMPILING_2D_BIT
and
jumpzi .L8230f
pushi -14 
calli THROW
.L8230f:
call
jumpi .L8234f
.L8222f:
calli _30__3E_
jumpzi .L8236f
calli _3E_COMPILE
calli REL_40_
call
jumpi .L8240f
.L8236f:
calli CURRENT_2D_COMPILE_2C_
.L8240f:
.L8234f:
jumpi .L8242f
.L8218f:
pop
calli NUMBER
calli STATE
load
jumpzi .L8247f
jumpzi .L8248f
pushi 0 # 0x0 
swap
calli CURRENT_2D_LITERAL
.L8248f:
calli CURRENT_2D_LITERAL
jumpi .L8253f
.L8247f:
pop
.L8253f:
.L8242f:
jumpi .L8207b
.L8212f:
pop
ret
.set INTERPRET_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "EVALUATE"
.balign bee_word_bytes, 0x20 
.word INTERPRET - .
.word EVALUATE_compilation
.word EVALUATE_info 
.global EVALUATE
EVALUATE:
calli SAVE_2D_INPUT_3E_R
pushi -1 
pushreli SOURCE_2D_ID_body
store
calli _23_EVALUAND
store
calli EVALUAND
store
pushi 0 
calli _3E_IN
store
calli INTERPRET
calli R_3E_RESTORE_2D_INPUT
ret
.set EVALUATE_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "REFILL"
.balign bee_word_bytes, 0x20 
.word EVALUATE - .
.word REFILL_compilation
.word REFILL_info 
.global REFILL
REFILL:
calli SOURCE_2D_ID
pushi 0 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8287f
pop
calli TIB
pushi 80 
calli ACCEPT
calli _23_TIB
store
pushi 0 
calli _3E_IN
store
calli TRUE
jumpi .L8298f
.L8287f:
pushi -1 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8304f
pop
calli FALSE
jumpi .L8307f
.L8304f:
pushs
calli FIB
calli _2F_FILE_2D_BUFFER
dups
calli READ_2D_LINE
calli _28_C_22__29_
.byte 0x1D 
.ascii "file read error during REFILL"
.balign bee_word_bytes, 0x0 
calli _28_ABORT_22__29_
pushi 0 # 0x0 
swap
calli _23_FIB
store
pushi 0 
calli _3E_IN
store
pops
pop
.L8307f:
.L8298f:
ret
.set REFILL_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "?STACK"
.balign bee_word_bytes, 0x20 
.word REFILL - .
.word _3F_STACK_compilation
.word _3F_STACK_info 
.global _3F_STACK
_3F_STACK:
calli DEPTH
calli _30__3C_
calli _28_C_22__29_
.byte 0xF 
.ascii "stack underflow"
.balign bee_word_bytes, 0x0 
calli _28_ABORT_22__29_
ret
.set _3F_STACK_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "REPL"
.balign bee_word_bytes, 0x20 
.word _3F_STACK - .
.word REPL_compilation
.word REPL_info 
.global REPL
REPL:
calli _5B_ - (2 * bee_word_bytes) + _5B__compilation
pushi 0 
pushreli SOURCE_2D_ID_body
store
.L8348b:
calli CR
calli REFILL
jumpzi .L8350f
calli INTERPRET
calli _3F_STACK
calli STATE
load
calli _30__3D_
jumpzi .L8356f
calli _28_S_22__29_
.byte 0x2 
.ascii "ok"
.balign bee_word_bytes, 0x0 
calli TYPE
.L8356f:
jumpi .L8348b
.L8350f:
calli TRUE
calli _28_C_22__29_
.byte 0x10 
.ascii "parse area empty"
.balign bee_word_bytes, 0x0 
calli _28_ABORT_22__29_
ret
.set REPL_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "HANDLE-ERROR"
.balign bee_word_bytes, 0x20 
.word REPL - .
.word HANDLE_2D_ERROR_compilation
.word HANDLE_2D_ERROR_info 
.global HANDLE_2D_ERROR
HANDLE_2D_ERROR:
pushi -1 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8378f
pop
jumpi .L8380f
.L8378f:
pushi -2 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8386f
pop
calli _27_THROWN
load
calli COUNT
calli TYPE
jumpi .L8392f
.L8386f:
pushi -9 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8398f
pop
pushi -9 
throw
jumpi .L8402f
.L8398f:
pushi -10 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8408f
pop
calli _28_S_22__29_
.byte 0x10 
.ascii "division by zero"
.balign bee_word_bytes, 0x0 
calli TYPE
jumpi .L8415f
.L8408f:
pushi -11 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8421f
pop
calli _28_S_22__29_
.byte 0x12 
.ascii "quotient too large"
.balign bee_word_bytes, 0x0 
calli TYPE
jumpi .L8428f
.L8421f:
pushi -13 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8434f
pop
calli _27_THROWN
load
calli COUNT
calli TYPE
calli _28_S_22__29_
.byte 0x2 
.ascii " ?"
.balign bee_word_bytes, 0x0 
calli TYPE
jumpi .L8443f
.L8434f:
pushi -14 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8449f
pop
calli _28_S_22__29_
.byte 0x10 
.ascii "compilation only"
.balign bee_word_bytes, 0x0 
calli TYPE
jumpi .L8456f
.L8449f:
pushi -20 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8462f
pop
calli _28_S_22__29_
.byte 0x1D 
.ascii "write to a read-only location"
.balign bee_word_bytes, 0x0 
calli TYPE
jumpi .L8470f
.L8462f:
pushi -23 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8476f
pop
pushi -23 
throw
jumpi .L8480f
.L8476f:
pushi -56 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8486f
pop
jumpi .L8488f
.L8486f:
pushi -512 
pushi 1 # 0x1 
dup
eq
neg
jumpzi .L8494f
pop
calli _28_S_22__29_
.byte 0xF 
.ascii "unknown option "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _27_THROWN
load
calli COUNT
calli TYPE
calli CR
pushi 1 
throw
jumpi .L8507f
.L8494f:
calli _28_S_22__29_
.byte 0xA 
.ascii "exception "
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 0 # 0x0 
dup
calli _2E_
calli _28_S_22__29_
.byte 0x6 
.ascii "raised"
.balign bee_word_bytes, 0x0 
calli TYPE
pop
.L8507f:
.L8488f:
.L8480f:
.L8470f:
.L8456f:
.L8443f:
.L8428f:
.L8415f:
.L8402f:
.L8392f:
.L8380f:
ret
.set HANDLE_2D_ERROR_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "(QUIT)"
.balign bee_word_bytes, 0x20 
.word HANDLE_2D_ERROR - .
.word _28_QUIT_29__compilation
.word _28_QUIT_29__info 
.global _28_QUIT_29_
_28_QUIT_29_:
.L8524b:
calli R_30_
set_sp
pushreli REPL
calli CATCH
pushi 0 # 0x0 
dup
calli HANDLE_2D_ERROR
pushi -56 
calli _3C__3E_
jumpzi .L8533f
calli S_30_
calli SP_21_
.L8533f:
jumpi .L8524b
ret
.set _28_QUIT_29__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "[ELSE]"
.balign bee_word_bytes, 0x20 
.word _28_QUIT_29_ - .
.word _5B_ELSE_5D__compilation
.word _5B_ELSE_5D__info 
.global _5B_ELSE_5D_
_5B_ELSE_5D_:
pushi 1 
.L8543b:
.L8543b:
calli BL
calli WORD
calli COUNT
pushi 0 # 0x0 
dup
jumpzi .L8548f
calli _32_DUP
calli _28_S_22__29_
.byte 0x4 
.ascii "[IF]"
.balign bee_word_bytes, 0x0 
calli COMPARE
calli _30__3D_
jumpzi .L8554f
calli _32_DROP
not
neg
jumpi .L8558f
.L8554f:
calli _32_DUP
calli _28_S_22__29_
.byte 0x6 
.ascii "[ELSE]"
.balign bee_word_bytes, 0x0 
calli COMPARE
calli _30__3D_
jumpzi .L8564f
calli _32_DROP
neg
not
pushi 0 # 0x0 
dup
jumpzi .L8570f
not
neg
.L8570f:
jumpi .L8573f
.L8564f:
calli _28_S_22__29_
.byte 0x6 
.ascii "[THEN]"
.balign bee_word_bytes, 0x0 
calli COMPARE
calli _30__3D_
jumpzi .L8578f
neg
not
.L8578f:
.L8573f:
.L8558f:
calli _3F_DUP
calli _30__3D_
jumpzi .L8583f
ret
.L8583f:
jumpi .L8543b
.L8548f:
calli _32_DROP
calli REFILL
calli _30__3D_
jumpzi .L8543b
pop
ret
.set _5B_ELSE_5D__compilation, (2 * bee_word_bytes)
.set _5B_ELSE_5D__info, _immediate_bit | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "[IF]"
.balign bee_word_bytes, 0x20 
.word _5B_ELSE_5D_ - .
.word _5B_IF_5D__compilation
.word _5B_IF_5D__info 
.global _5B_IF_5D_
_5B_IF_5D_:
calli _30__3D_
jumpzi .L8597f
calli _5B_ELSE_5D_ - (2 * bee_word_bytes) + _5B_ELSE_5D__compilation
.L8597f:
ret
.set _5B_IF_5D__compilation, (2 * bee_word_bytes)
.set _5B_IF_5D__info, _immediate_bit | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "[THEN]"
.balign bee_word_bytes, 0x20 
.word _5B_IF_5D_ - .
.word _5B_THEN_5D__compilation
.word _5B_THEN_5D__info 
.global _5B_THEN_5D_
_5B_THEN_5D_:
ret
.set _5B_THEN_5D__compilation, (2 * bee_word_bytes)
.set _5B_THEN_5D__info, _immediate_bit | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "DEFINED?"
.balign bee_word_bytes, 0x20 
.word _5B_THEN_5D_ - .
.word DEFINED_3F__compilation
.word DEFINED_3F__info 
.global DEFINED_3F_
DEFINED_3F_:
calli FIND
calli NIP
calli _30__3C__3E_
ret
.set DEFINED_3F__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "[DEFINED]"
.balign bee_word_bytes, 0x20 
.word DEFINED_3F_ - .
.word _5B_DEFINED_5D__compilation
.word _5B_DEFINED_5D__info 
.global _5B_DEFINED_5D_
_5B_DEFINED_5D_:
calli BL
calli WORD
calli DEFINED_3F_
ret
.set _5B_DEFINED_5D__compilation, (2 * bee_word_bytes)
.set _5B_DEFINED_5D__info, _immediate_bit | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "[UNDEFINED]"
.balign bee_word_bytes, 0x20 
.word _5B_DEFINED_5D_ - .
.word _5B_UNDEFINED_5D__compilation
.word _5B_UNDEFINED_5D__info 
.global _5B_UNDEFINED_5D_
_5B_UNDEFINED_5D_:
calli _5B_DEFINED_5D_ - (2 * bee_word_bytes) + _5B_DEFINED_5D__compilation
not
ret
.set _5B_UNDEFINED_5D__compilation, (2 * bee_word_bytes)
.set _5B_UNDEFINED_5D__info, _immediate_bit | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "COLON"
.balign bee_word_bytes, 0x20 
.word _5B_UNDEFINED_5D_ - .
.word COLON_compilation
.word COLON_info 
.global COLON
COLON:
calli HEADER
calli TRUE
calli SMUDGE
calli LINK_2C_
calli _5D_
ret
.set COLON_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii ":"
.balign bee_word_bytes, 0x20 
.word COLON - .
.word _3A__compilation
.word _3A__info 
.global _3A_
_3A_:
calli BL
calli WORD
calli COLON
ret
.set _3A__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "CURRENT?"
.balign bee_word_bytes, 0x20 
.word _3A_ - .
.word CURRENT_3F__compilation
.word CURRENT_3F__info 
.global CURRENT_3F_
CURRENT_3F_:
calli _32_DROP
calli GET_2D_CURRENT
eq
neg
ret
.set CURRENT_3F__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "PROVIDED?"
.balign bee_word_bytes, 0x20 
.word CURRENT_3F_ - .
.word PROVIDED_3F__compilation
.word PROVIDED_3F__info 
.global PROVIDED_3F_
PROVIDED_3F_:
pushreli CURRENT_3F_
calli SELECT
calli NIP
ret
.set PROVIDED_3F__info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "[PROVIDED]"
.balign bee_word_bytes, 0x20 
.word PROVIDED_3F_ - .
.word _5B_PROVIDED_5D__compilation
.word _5B_PROVIDED_5D__info 
.global _5B_PROVIDED_5D_
_5B_PROVIDED_5D_:
calli BL
calli WORD
calli PROVIDED_3F_
ret
.set _5B_PROVIDED_5D__compilation, (2 * bee_word_bytes)
.set _5B_PROVIDED_5D__info, _immediate_bit | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "PROVIDE:"
.balign bee_word_bytes, 0x20 
.word _5B_PROVIDED_5D_ - .
.word PROVIDE_3A__compilation
.word PROVIDE_3A__info 
.global PROVIDE_3A_
PROVIDE_3A_:
calli BL
calli WORD
pushi 0 # 0x0 
dup
calli PROVIDED_3F_
jumpzi .L8687f
pop
calli _5B_ELSE_5D_ - (2 * bee_word_bytes) + _5B_ELSE_5D__compilation
jumpi .L8690f
.L8687f:
calli COLON
.L8690f:
ret
.set PROVIDE_3A__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii ";"
.balign bee_word_bytes, 0x20 
.word PROVIDE_3A_ - .
.word _3B__compilation
.word _3B__info 
.global _3B_
_3B_:
calli UNLINK_2C_
calli _5B_ - (2 * bee_word_bytes) + _5B__compilation
calli FALSE
calli SMUDGE
ret
.set _3B__compilation, (2 * bee_word_bytes)
.set _3B__info, _immediate_bit | _compiling_bit | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii ":NONAME"
.balign bee_word_bytes, 0x20 
.word _3B_ - .
.word _3A_NONAME_compilation
.word _3A_NONAME_info 
.global _3A_NONAME
_3A_NONAME:
calli ALIGN
pushi 0 
calli _2C_
calli HERE
pushi 0 # 0x0 
dup
calli NONAME
calli _2E_LABEL_2D_DEF
calli LINK_2C_
calli _5D_
ret
.set _3A_NONAME_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii ";IMMEDIATE"
.balign bee_word_bytes, 0x20 
.word _3A_NONAME - .
.word _3B_IMMEDIATE_compilation
.word _3B_IMMEDIATE_info 
.global _3B_IMMEDIATE
_3B_IMMEDIATE:
calli _3B_ - (2 * bee_word_bytes) + _3B__compilation
calli SET_2D_IMMEDIATE
pushi 0 # 0x0 
dup
calli LAST
calli _3E_NAME
calli _2E_COMPILE_2D_METHOD
calli LAST
calli _3E_COMPILE
calli REL_21_
ret
.set _3B_IMMEDIATE_compilation, (2 * bee_word_bytes)
.set _3B_IMMEDIATE_info, _immediate_bit | _compiling_bit | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "("
.balign bee_word_bytes, 0x20 
.word _3B_IMMEDIATE - .
.word _28__compilation
.word _28__info 
.global _28_
_28_:
.L8737b:
pushi 41 
calli PARSE
calli _32_DROP
calli SOURCE_2D_ID
not
neg
pushi 2 
ult
neg
jumpzi .L8746f
ret
.L8746f:
calli _3E_IN
load
jumpzi .L8750f
calli SOURCE
pop
calli _3E_IN
load
neg
not
add
load1
pushi 41 
calli _3C__3E_
jumpi .L8761f
.L8750f:
calli TRUE
.L8761f:
jumpzi .L8763f
calli REFILL
calli _30__3D_
jumpzi .L8737b
.L8763f:
ret
.set _28__compilation, (2 * bee_word_bytes)
.set _28__info, _immediate_bit | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "\\"
.balign bee_word_bytes, 0x20 
.word _28_ - .
.word _5C__compilation
.word _5C__info 
.global _5C_
_5C_:
calli SOURCE
calli NIP
calli _3E_IN
store
ret
.set _5C__compilation, (2 * bee_word_bytes)
.set _5C__info, _immediate_bit | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "?"
.balign bee_word_bytes, 0x20 
.word _5C_ - .
.word _3F__compilation
.word _3F__info 
.global _3F_
_3F_:
load
calli _2E_
ret
.set _3F__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii ".S"
.balign bee_word_bytes, 0x20 
.word _3F_ - .
.word _2E_S_compilation
.word _2E_S_info 
.global _2E_S
_2E_S:
calli _3F_STACK
calli DEPTH
calli _3F_DUP
jumpzi .L8791f
neg
not
pushi 0 
pushi 0 # 0x0 
swap
calli _32__3E_R
.L8798b:
dups
dup
calli _2E_
pushi -1 
calli _28__2B_LOOP_29_
jumpzi .L8798b
.L8797f:
calli UNLOOP
jumpi .L8805f
.L8791f:
calli _28_S_22__29_
.byte 0xC 
.ascii "stack empty "
.balign bee_word_bytes, 0x0 
calli TYPE
.L8805f:
ret
.set _2E_S_info, 0 | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "INCLUDE-FILE"
.balign bee_word_bytes, 0x20 
.word _2E_S - .
.word INCLUDE_2D_FILE_compilation
.word INCLUDE_2D_FILE_info 
.global INCLUDE_2D_FILE
INCLUDE_2D_FILE:
calli SAVE_2D_INPUT_3E_R
pushreli SOURCE_2D_ID_body
store
calli ALLOCATE_2D_BUFFER
jumpzi .L8820f
calli SOURCE_2D_ID
pushi 11 # 0xB 
trap 0x0 
calli TRUE
calli _28_C_22__29_
.byte 0x14 
.ascii "no more file buffers"
.balign bee_word_bytes, 0x0 
calli _28_ABORT_22__29_
.L8820f:
pushreli FIB_body
store
calli REFILL
pushi 0 # 0x0 
dup
jumpzi .L8835f
calli _23_FIB
load
pushi 1 
calli _3E_
jumpzi .L8840f
calli FIB
load1
pushi 35 
eq
neg
calli FIB
not
neg
load1
pushi 33 
eq
neg
and
jumpzi .L8854f
pop
calli REFILL
.L8854f:
.L8840f:
.L8835f:
.L8857b:
jumpzi .L8857f
pushreli INTERPRET
calli CATCH
calli _3F_DUP
jumpzi .L8861f
calli SOURCE_2D_ID
pushi 11 # 0xB 
trap 0x0 
pop
calli FREE_2D_BUFFER
pop
calli THROW
.L8861f:
calli REFILL
jumpi .L8857b
.L8857f:
calli FREE_2D_BUFFER
calli _28_C_22__29_
.byte 0x16 
.ascii "no file buffer to free"
.balign bee_word_bytes, 0x0 
calli _28_ABORT_22__29_
calli R_3E_RESTORE_2D_INPUT
ret
.set INCLUDE_2D_FILE_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INCLUDED"
.balign bee_word_bytes, 0x20 
.word INCLUDE_2D_FILE - .
.word INCLUDED_compilation
.word INCLUDED_info 
.global INCLUDED
INCLUDED:
calli _32_DUP
pushi 5 # 0x5 
trap 0x0 
calli OPEN_2D_FILE
jumpzi .L8888f
pop
calli TRUE
calli _28_C_22__29_
.byte 0x16 
.ascii "file can't be INCLUDED"
.balign bee_word_bytes, 0x0 
calli _28_ABORT_22__29_
.L8888f:
pushs
calli _32_DROP
dups
calli INCLUDE_2D_FILE
pops
pushi 11 # 0xB 
trap 0x0 
calli _28_C_22__29_
.byte 0x16 
.ascii "error after INCLUDEing"
.balign bee_word_bytes, 0x0 
calli _28_ABORT_22__29_
ret
.set INCLUDED_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "INCLUDE\""
.balign bee_word_bytes, 0x20 
.word INCLUDED - .
.word INCLUDE_22__compilation
.word INCLUDE_22__info 
.global INCLUDE_22_
INCLUDE_22_:
pushi 34 
calli WORD
calli COUNT
calli INCLUDED
ret
.set INCLUDE_22__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x1 
.ascii "'"
.balign bee_word_bytes, 0x20 
.word INCLUDE_22_ - .
.word _27__compilation
.word _27__info 
.global _27_
_27_:
calli BL
calli WORD
calli FIND
calli _30__3D_
jumpzi .L8927f
calli UNDEFINED
.L8927f:
ret
.set _27__info, 0 | 0 | 0 | (1 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "[']"
.balign bee_word_bytes, 0x20 
.word _27_ - .
.word _5B__27__5D__compilation
.word _5B__27__5D__info 
.global _5B__27__5D_
_5B__27__5D_:
calli _27_
pushi 0 # 0x0 
dup
calli _3E_NAME
calli _2E_PUSHRELI_2D_SYMBOL
calli PUSHREL_2C_
ret
.set _5B__27__5D__compilation, (2 * bee_word_bytes)
.set _5B__27__5D__info, _immediate_bit | _compiling_bit | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "CREATE"
.balign bee_word_bytes, 0x20 
.word _5B__27__5D_ - .
.word CREATE_compilation
.word CREATE_info 
.global CREATE
CREATE:
calli BL
calli WORD
calli HEADER
calli CREATE_2C_
calli ALIGN
calli LAST
calli _2E_BODY_2D_LABEL_2D_DEF
ret
.set CREATE_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "DOES>"
.balign bee_word_bytes, 0x20 
.word CREATE - .
.word DOES_3E__compilation
.word DOES_3E__info 
.global DOES_3E_
DOES_3E_:
calli LAST
calli RELATIVE_2D_LITERAL - (2 * bee_word_bytes) + RELATIVE_2D_LITERAL_compilation
pushreli _28_DOES_3E__29_
calli _28_POSTPONE_29_
calli UNLINK_2C_
calli ALIGN
calli HERE
calli LAST
calli TUCK
neg
add
calli CELL_2F_
pushi 0 # 0x0 
swap
pushi 0 # 0x0 
dup
calli _3E_NAME
pushreli _2E_DOES_2D_LABEL
calli TO_2D_ASMOUT
calli _3E_INFO
pushi 0 # 0x0 
dup
load
pushi 65535 
not
and
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
swap
or
pushi 0 # 0x0 
swap
store
calli DOES_2D_LINK_2C_
ret
.set DOES_3E__compilation, (2 * bee_word_bytes)
.set DOES_3E__info, _immediate_bit | _compiling_bit | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "VARIABLE"
.balign bee_word_bytes, 0x20 
.word DOES_3E_ - .
.word VARIABLE_compilation
.word VARIABLE_info 
.global VARIABLE
VARIABLE:
calli CREATE
pushi 1 
calli ALLOT_2D_CELLS
ret
.set VARIABLE_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "CONSTANT"
.balign bee_word_bytes, 0x20 
.word VARIABLE - .
.word CONSTANT_compilation
.word CONSTANT_info 
.global CONSTANT
CONSTANT:
calli BL
calli WORD
calli HEADER
calli LINK_2C_
calli LITERAL - (2 * bee_word_bytes) + LITERAL_compilation
calli UNLINK_2C_
ret
.set CONSTANT_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "VALUE"
.balign bee_word_bytes, 0x20 
.word CONSTANT - .
.word VALUE_compilation
.word VALUE_info 
.global VALUE
VALUE:
calli CREATE
calli _2C_
pushreli VALUE
calli _28_DOES_3E__29_
ret
.balign bee_word_bytes
VALUE_does:
pops
load
ret
.set VALUE_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x5 
.balign bee_word_bytes
.byte 0xD 
.ascii ".BODY-LITERAL"
.balign bee_word_bytes, 0x20 
.word VALUE - .
.word _2E_BODY_2D_LITERAL_compilation
.word _2E_BODY_2D_LITERAL_info 
.global _2E_BODY_2D_LITERAL
_2E_BODY_2D_LITERAL:
calli _28_S_22__29_
.byte 0x9 
.ascii "pushreli "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x5 
.ascii "_body"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set _2E_BODY_2D_LITERAL_info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "BODY-LITERAL"
.balign bee_word_bytes, 0x20 
.word _2E_BODY_2D_LITERAL - .
.word BODY_2D_LITERAL_compilation
.word BODY_2D_LITERAL_info 
.global BODY_2D_LITERAL
BODY_2D_LITERAL:
pushi 0 # 0x0 
dup
calli FIND
calli _30__3D_
jumpzi .L9050f
calli UNDEFINED
.L9050f:
calli _3E_BODY
calli PUSHREL_2C_
pushreli _2E_BODY_2D_LITERAL
calli TO_2D_ASMOUT
ret
.set BODY_2D_LITERAL_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "TO"
.balign bee_word_bytes, 0x20 
.word BODY_2D_LITERAL - .
.word TO_compilation
.word TO_info 
.global TO
TO:
calli _27_
calli _3E_BODY
store
ret
.balign bee_word_bytes
.word 0 
.L9066n:
calli BL
calli WORD
calli BODY_2D_LITERAL
pushreli _21_
calli _28_POSTPONE_29_
ret
.set TO_compilation, .L9066n - (TO - 2 * bee_word_bytes)
.set TO_info, _immediate_bit | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xE 
.ascii ".DEFER-ADDRESS"
.balign bee_word_bytes, 0x20 
.word TO - .
.word _2E_DEFER_2D_ADDRESS_compilation
.word _2E_DEFER_2D_ADDRESS_info 
.global _2E_DEFER_2D_ADDRESS
_2E_DEFER_2D_ADDRESS:
calli _28_S_22__29_
.byte 0x6 
.ascii ".word "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0xA 
.ascii "_defer - ."
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set _2E_DEFER_2D_ADDRESS_info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "DEFER"
.balign bee_word_bytes, 0x20 
.word _2E_DEFER_2D_ADDRESS - .
.word DEFER_compilation
.word DEFER_info 
.global DEFER
DEFER:
calli CREATE
calli HERE
pushreli ABORT
calli _3E_REL
calli RAW_2C_
calli LAST
calli _3E_NAME
pushreli _2E_DEFER_2D_ADDRESS
calli TO_2D_ASMOUT
pushreli DEFER
calli _28_DOES_3E__29_
ret
.balign bee_word_bytes
DEFER_does:
pops
calli REL_40_
call
ret
.set DEFER_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0xC 
.balign bee_word_bytes
.byte 0x9 
.ascii "ACTION-OF"
.balign bee_word_bytes, 0x20 
.word DEFER - .
.word ACTION_2D_OF_compilation
.word ACTION_2D_OF_info 
.global ACTION_2D_OF
ACTION_2D_OF:
calli _27_
calli DEFER_40_
ret
.balign bee_word_bytes
.word 0 
.L9116n:
calli _5B__27__5D_ - (2 * bee_word_bytes) + _5B__27__5D__compilation
pushreli DEFER_40_
calli _28_POSTPONE_29_
ret
.set ACTION_2D_OF_compilation, .L9116n - (ACTION_2D_OF - 2 * bee_word_bytes)
.set ACTION_2D_OF_info, _immediate_bit | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii ".DEFER-LABEL"
.balign bee_word_bytes, 0x20 
.word ACTION_2D_OF - .
.word _2E_DEFER_2D_LABEL_compilation
.word _2E_DEFER_2D_LABEL_info 
.global _2E_DEFER_2D_LABEL
_2E_DEFER_2D_LABEL:
calli _28_S_22__29_
.byte 0x5 
.ascii ".set "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x8 
.ascii "_defer, "
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 0 # 0x0 
dup
calli _3E_INFO
word_bytes
neg
not
add
load1
jumpzi .L9141f
calli _3E_NAME
calli _2E_NAME
jumpi .L9144f
.L9141f:
calli NONAME
calli _2E_LABEL
.L9144f:
calli CR
ret
.set _2E_DEFER_2D_LABEL_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii ".DEFER-ABORT"
.balign bee_word_bytes, 0x20 
.word _2E_DEFER_2D_LABEL - .
.word _2E_DEFER_2D_ABORT_compilation
.word _2E_DEFER_2D_ABORT_info 
.global _2E_DEFER_2D_ABORT
_2E_DEFER_2D_ABORT:
calli _28_S_22__29_
.byte 0x5 
.ascii ".set "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0xD 
.ascii "_defer, ABORT"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set _2E_DEFER_2D_ABORT_info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x2 
.ascii "IS"
.balign bee_word_bytes, 0x20 
.word _2E_DEFER_2D_ABORT - .
.word IS_compilation
.word IS_info 
.global IS
IS:
calli _27_
calli _32_DUP
calli _3E_NAME
pushreli _2E_DEFER_2D_LABEL
calli TO_2D_ASMOUT
calli DEFER_21_
ret
.balign bee_word_bytes
.word 0 
.L9176n:
calli _5B__27__5D_ - (2 * bee_word_bytes) + _5B__27__5D__compilation
pushreli DEFER_21_
calli _28_POSTPONE_29_
ret
.set IS_compilation, .L9176n - (IS - 2 * bee_word_bytes)
.set IS_info, _immediate_bit | 0 | 0 | (2 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "DICTIONARY"
.balign bee_word_bytes, 0x20 
.word IS - .
.word DICTIONARY_compilation
.word DICTIONARY_info 
.global DICTIONARY
DICTIONARY:
calli CREATE
calli HERE
calli CELL_2B_
calli _2C_
calli ALLOT
pushreli DICTIONARY
calli _28_DOES_3E__29_
ret
.balign bee_word_bytes
DICTIONARY_does:
pops
pushreli DP_body
store
ret
.set DICTIONARY_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x8 
.balign bee_word_bytes
.byte 0x4 
.ascii "ROOT"
.balign bee_word_bytes, 0x20 
.word DICTIONARY - .
.word ROOT_compilation
.word ROOT_info 
.global ROOT
ROOT:
calli ROOTDP
pushreli DP_body
store
ret
.set ROOT_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "CHAIN"
.balign bee_word_bytes, 0x20 
.word ROOT - .
.word CHAIN_compilation
.word CHAIN_info 
.global CHAIN
CHAIN:
nop 
calli CHAIN_doer
.balign bee_word_bytes
CHAIN_body:
.L9211b:
.word 0 
.set CHAIN_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set CHAIN_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x8 
.ascii "WORDLIST"
.balign bee_word_bytes, 0x20 
.word CHAIN - .
.word WORDLIST_compilation
.word WORDLIST_info 
.global WORDLIST
WORDLIST:
calli ALIGN
calli HERE
pushi 0 
calli RAW_2C_
calli HERE
calli CHAIN
pushi 0 # 0x0 
dup
calli REL_40_
calli RAW_2D_REL_2C_
pushi 0 
calli _2E_WORD
calli REL_21_
pushi 0 
calli _2C_
ret
.set WORDLIST_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "VOCABULARY"
.balign bee_word_bytes, 0x20 
.word WORDLIST - .
.word VOCABULARY_compilation
.word VOCABULARY_info 
.global VOCABULARY
VOCABULARY:
calli WORDLIST
calli CREATE
calli REL_2C_
pushreli VOCABULARY
calli _28_DOES_3E__29_
ret
.balign bee_word_bytes
VOCABULARY_does:
pops
calli _23_ORDER
load
calli _30__3D_
jumpzi .L9248f
pushi 1 
calli _23_ORDER
calli _2B__21_
.L9248f:
calli REL_40_
calli CONTEXT
store
ret
.L9256b:
.word last_word - .
.balign bee_word_bytes
.word 0 
.word 0 
.set VOCABULARY_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x6 
.balign bee_word_bytes
.byte 0x5 
.ascii "FORTH"
.balign bee_word_bytes, 0x20 
.word VOCABULARY - .
.word FORTH_compilation
.word FORTH_info 
.global FORTH
FORTH:
nop 
calli FORTH_doer
.balign bee_word_bytes
FORTH_body:
.word .L9256b - .
.set FORTH_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.set FORTH_doer, VOCABULARY_does
.balign bee_word_bytes
.byte 0xE 
.ascii "FORTH-WORDLIST"
.balign bee_word_bytes, 0x20 
.word FORTH - .
.word FORTH_2D_WORDLIST_compilation
.word FORTH_2D_WORDLIST_info 
.global FORTH_2D_WORDLIST
FORTH_2D_WORDLIST:
pushreli FORTH
calli _3E_BODY
calli REL_40_
ret
.set FORTH_2D_WORDLIST_info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "ALSO"
.balign bee_word_bytes, 0x20 
.word FORTH_2D_WORDLIST - .
.word ALSO_compilation
.word ALSO_info 
.global ALSO
ALSO:
calli CONTEXT
pushi 0 # 0x0 
dup
calli CELL_2B_
calli _23_ORDER
load
calli CELLS
calli MOVE
pushi 1 
calli _23_ORDER
calli _2B__21_
ret
.set ALSO_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "ONLY"
.balign bee_word_bytes, 0x20 
.word ALSO - .
.word ONLY_compilation
.word ONLY_info 
.global ONLY
ONLY:
calli FORTH
pushi 1 
calli _23_ORDER
store
ret
.set ONLY_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "FOREIGN"
.balign bee_word_bytes, 0x20 
.word ONLY - .
.word FOREIGN_compilation
.word FOREIGN_info 
.global FOREIGN
FOREIGN:
calli CONTEXT
load
pushi 2 
calli CELLS
add
pushi 0 # 0x0 
dup
load
pushi 1024 
or
pushi 0 # 0x0 
swap
store
ret
.set FOREIGN_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "NATIVE"
.balign bee_word_bytes, 0x20 
.word FOREIGN - .
.word NATIVE_compilation
.word NATIVE_info 
.global NATIVE
NATIVE:
calli CONTEXT
load
pushi 2 
calli CELLS
add
pushi 0 # 0x0 
dup
load
pushi 1023 
and
pushi 0 # 0x0 
swap
store
ret
.set NATIVE_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "SET-ORDER"
.balign bee_word_bytes, 0x20 
.word NATIVE - .
.word SET_2D_ORDER_compilation
.word SET_2D_ORDER_info 
.global SET_2D_ORDER
SET_2D_ORDER:
pushi 0 # 0x0 
dup
pushi -1 
eq
neg
jumpzi .L9346f
calli ONLY
jumpi .L9348f
.L9346f:
pushi 0 # 0x0 
dup
calli _23_ORDER
store
calli CELLS
calli CONTEXT
calli TUCK
add
pushi 0 # 0x0 
swap
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L9363f
jumpi .L9359f
.L9363f:
.L9365b:
dups
store
word_bytes
calli _28__2B_LOOP_29_
jumpzi .L9365b
.L9359f:
.L9364f:
calli UNLOOP
.L9348f:
ret
.set SET_2D_ORDER_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "PREVIOUS"
.balign bee_word_bytes, 0x20 
.word SET_2D_ORDER - .
.word PREVIOUS_compilation
.word PREVIOUS_info 
.global PREVIOUS
PREVIOUS:
calli GET_2D_ORDER
pushi 0 # 0x0 
dup
calli _30__3E_
jumpzi .L9381f
calli NIP
neg
not
.L9381f:
calli SET_2D_ORDER
ret
.set PREVIOUS_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "ORDER"
.balign bee_word_bytes, 0x20 
.word PREVIOUS - .
.word ORDER_compilation
.word ORDER_info 
.global ORDER
ORDER:
calli _28_S_22__29_
.byte 0x9 
.ascii "CONTEXT: "
.balign bee_word_bytes, 0x0 
calli TYPE
calli GET_2D_ORDER
pushi 0 
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L9401f
jumpi .L9397f
.L9401f:
.L9403b:
calli H_2E_
calli _28_LOOP_29_
jumpzi .L9403b
.L9397f:
.L9402f:
calli UNLOOP
calli CR
calli _28_S_22__29_
.byte 0x9 
.ascii "CURRENT: "
.balign bee_word_bytes, 0x0 
calli TYPE
calli GET_2D_CURRENT
calli H_2E_
ret
.set ORDER_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "(FORGET)"
.balign bee_word_bytes, 0x20 
.word ORDER - .
.word _28_FORGET_29__compilation
.word _28_FORGET_29__info 
.global _28_FORGET_29_
_28_FORGET_29_:
calli _3E_NAME
calli DP
store
calli CHAIN
pushi 0 # 0x0 
dup
.L9426b:
load
pushi 0 # 0x0 
dup
calli HERE
lt
neg
jumpzi .L9426b
pushi 1 # 0x1 
dup
store
.L9436b:
load
calli _3F_DUP
jumpzi .L9438f
pushi 0 # 0x0 
dup
calli CELL_2D_
pushi 0 # 0x0 
dup
load
.L9445b:
pushi 0 # 0x0 
dup
calli HERE
lt
neg
not
jumpzi .L9451f
calli _3E_LINK
calli REL_40_
jumpi .L9445b
.L9451f:
pushi 0 # 0x0 
swap
calli REL_21_
jumpi .L9436b
.L9438f:
ret
.set _28_FORGET_29__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "FORGET"
.balign bee_word_bytes, 0x20 
.word _28_FORGET_29_ - .
.word FORGET_compilation
.word FORGET_info 
.global FORGET
FORGET:
calli _27_
calli _28_FORGET_29_
ret
.set FORGET_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "MARKER"
.balign bee_word_bytes, 0x20 
.word FORGET - .
.word MARKER_compilation
.word MARKER_info 
.global MARKER
MARKER:
calli CREATE
calli GET_2D_ORDER
pushi 0 # 0x0 
dup
calli _2C_
pushi 0 
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L9481f
jumpi .L9477f
.L9481f:
.L9483b:
calli _2C_
calli _28_LOOP_29_
jumpzi .L9483b
.L9477f:
.L9482f:
calli UNLOOP
calli LAST
calli _2C_
calli DP
calli _2C_
pushreli MARKER
calli _28_DOES_3E__29_
ret
.balign bee_word_bytes
MARKER_does:
pops
pushi 0 # 0x0 
dup
load
pushi 0 # 0x0 
dup
pushs
calli CELLS
calli _32_DUP
add
calli CELL_2B_
calli DP
pushs
pushi 0 # 0x0 
dup
calli CELL_2B_
load
pushreli DP_body
store
load
calli _28_FORGET_29_
pops
pushreli DP_body
store
pushi 1 # 0x1 
dup
calli CELL_2B_
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
add
calli _32__3E_R
.L9527b:
dups
load
calli _2D_CELL
calli _28__2B_LOOP_29_
jumpzi .L9527b
.L9526f:
calli UNLOOP
pops
calli SET_2D_ORDER
ret
.set MARKER_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x17 
.balign bee_word_bytes
.byte 0x7 
.ascii "CURSORX"
.balign bee_word_bytes, 0x20 
.word MARKER - .
.word CURSORX_compilation
.word CURSORX_info 
.global CURSORX
CURSORX:
nop 
calli CURSORX_doer
.balign bee_word_bytes
CURSORX_body:
.ds.b 1 * bee_word_bytes
.set CURSORX_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.set CURSORX_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0x7 
.ascii "ADVANCE"
.balign bee_word_bytes, 0x20 
.word CURSORX - .
.word ADVANCE_compilation
.word ADVANCE_info 
.global ADVANCE
ADVANCE:
calli CURSORX
calli _2B__21_
ret
.set ADVANCE_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "WRAP?"
.balign bee_word_bytes, 0x20 
.word ADVANCE - .
.word WRAP_3F__compilation
.word WRAP_3F__info 
.global WRAP_3F_
WRAP_3F_:
calli CURSORX
load
add
calli WIDTH
lt
neg
not
ret
.set WRAP_3F__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "NEWLINE"
.balign bee_word_bytes, 0x20 
.word WRAP_3F_ - .
.word NEWLINE_compilation
.word NEWLINE_info 
.global NEWLINE
NEWLINE:
pushi 0 
calli CURSORX
store
calli CR
ret
.set NEWLINE_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "GAP"
.balign bee_word_bytes, 0x20 
.word NEWLINE - .
.word GAP_compilation
.word GAP_info 
.global GAP
GAP:
pushi 3 
ret
.set GAP_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xE 
.ascii "WORDLIST-WORDS"
.balign bee_word_bytes, 0x20 
.word GAP - .
.word WORDLIST_2D_WORDS_compilation
.word WORDLIST_2D_WORDS_info 
.global WORDLIST_2D_WORDS
WORDLIST_2D_WORDS:
calli NEWLINE
.L9583b:
calli REL_40_
calli _3F_DUP
jumpzi .L9585f
pushi 0 # 0x0 
dup
calli _3E_NAME
calli COUNT
pushi 0 # 0x0 
dup
calli WRAP_3F_
jumpzi .L9593f
calli NEWLINE
.L9593f:
pushi 0 # 0x0 
dup
calli ADVANCE
calli TYPE
calli GAP
calli WRAP_3F_
jumpzi .L9601f
calli NEWLINE
jumpi .L9603f
.L9601f:
calli GAP
pushi 0 # 0x0 
dup
calli SPACES
calli ADVANCE
.L9603f:
calli _3E_LINK
jumpi .L9583b
.L9585f:
calli CURSORX
load
jumpzi .L9613f
calli NEWLINE
.L9613f:
ret
.set WORDLIST_2D_WORDS_info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "WORDS"
.balign bee_word_bytes, 0x20 
.word WORDLIST_2D_WORDS - .
.word WORDS_compilation
.word WORDS_info 
.global WORDS
WORDS:
calli CONTEXT
load
calli WORDLIST_2D_WORDS
ret
.set WORDS_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "ALL-WORDS"
.balign bee_word_bytes, 0x20 
.word WORDS - .
.word ALL_2D_WORDS_compilation
.word ALL_2D_WORDS_info 
.global ALL_2D_WORDS
ALL_2D_WORDS:
calli GET_2D_ORDER
pushi 0 
calli _32_DUP
calli _32__3E_R
eq
neg
jumpzi .L9635f
jumpi .L9631f
.L9635f:
.L9637b:
calli WORDLIST_2D_WORDS
calli _28_LOOP_29_
jumpzi .L9637b
.L9631f:
.L9636f:
calli UNLOOP
ret
.set ALL_2D_WORDS_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "ENVIRONMENT?"
.balign bee_word_bytes, 0x20 
.word ALL_2D_WORDS - .
.word ENVIRONMENT_3F__compilation
.word ENVIRONMENT_3F__info 
.global ENVIRONMENT_3F_
ENVIRONMENT_3F_:
calli _28_S_22__29_
.byte 0xF 
.ascii "/COUNTED-STRING"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9653f
calli _32_DROP
pushi 255 
jumpi .L9656f
.L9653f:
calli _28_S_22__29_
.byte 0x5 
.ascii "/HOLD"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9662f
calli _32_DROP
pushi 256 
jumpi .L9665f
.L9662f:
calli _28_S_22__29_
.byte 0x4 
.ascii "/PAD"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9671f
calli _32_DROP
pushi 256 
jumpi .L9674f
.L9671f:
calli _28_S_22__29_
.byte 0x11 
.ascii "ADDRESS-UNIT-BITS"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9682f
calli _32_DROP
pushi 8 
jumpi .L9685f
.L9682f:
calli _28_S_22__29_
.byte 0x5 
.ascii "BLOCK"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9691f
calli _32_DROP
calli FALSE
jumpi .L9694f
.L9691f:
calli _28_S_22__29_
.byte 0x9 
.ascii "BLOCK-EXT"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9701f
calli _32_DROP
calli FALSE
jumpi .L9704f
.L9701f:
calli _28_S_22__29_
.byte 0x4 
.ascii "CORE"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9710f
calli _32_DROP
calli TRUE
jumpi .L9713f
.L9710f:
calli _28_S_22__29_
.byte 0x8 
.ascii "CORE-EXT"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9720f
calli _32_DROP
calli FALSE
jumpi .L9723f
.L9720f:
calli _28_S_22__29_
.byte 0x6 
.ascii "DOUBLE"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9729f
calli _32_DROP
calli FALSE
jumpi .L9732f
.L9729f:
calli _28_S_22__29_
.byte 0xA 
.ascii "DOUBLE-EXT"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9739f
calli _32_DROP
calli FALSE
jumpi .L9742f
.L9739f:
calli _28_S_22__29_
.byte 0x9 
.ascii "EXCEPTION"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9749f
calli _32_DROP
calli TRUE
jumpi .L9752f
.L9749f:
calli _28_S_22__29_
.byte 0xD 
.ascii "EXCEPTION-EXT"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9759f
calli _32_DROP
calli TRUE
jumpi .L9762f
.L9759f:
calli _28_S_22__29_
.byte 0x8 
.ascii "FACILITY"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9769f
calli _32_DROP
calli FALSE
jumpi .L9772f
.L9769f:
calli _28_S_22__29_
.byte 0xC 
.ascii "FACILITY-EXT"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9779f
calli _32_DROP
calli FALSE
jumpi .L9782f
.L9779f:
calli _28_S_22__29_
.byte 0x4 
.ascii "FILE"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9788f
calli _32_DROP
calli TRUE
jumpi .L9791f
.L9788f:
calli _28_S_22__29_
.byte 0x8 
.ascii "FILE-EXT"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9798f
calli _32_DROP
calli TRUE
jumpi .L9801f
.L9798f:
calli _28_S_22__29_
.byte 0x7 
.ascii "FLOORED"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9807f
calli _32_DROP
calli TRUE
jumpi .L9810f
.L9807f:
calli _28_S_22__29_
.byte 0x8 
.ascii "MAX-CHAR"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9817f
calli _32_DROP
pushi 255 
jumpi .L9820f
.L9817f:
calli _28_S_22__29_
.byte 0x5 
.ascii "MAX-D"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9826f
calli _32_DROP
pushi -1 
pushi 1 
rshift
calli S_3E_D
jumpi .L9832f
.L9826f:
calli _28_S_22__29_
.byte 0x5 
.ascii "MAX-N"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9838f
calli _32_DROP
pushi -1 
pushi 1 
rshift
jumpi .L9843f
.L9838f:
calli _28_S_22__29_
.byte 0x5 
.ascii "MAX-U"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9849f
calli _32_DROP
pushi -1 
jumpi .L9852f
.L9849f:
calli _28_S_22__29_
.byte 0x6 
.ascii "MAX-UD"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9858f
calli _32_DROP
pushi -1 
pushi 0 
jumpi .L9862f
.L9858f:
calli _28_S_22__29_
.byte 0x12 
.ascii "RETURN-STACK-CELLS"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9870f
calli _32_DROP
calli RETURN_2D_STACK_2D_CELLS
jumpi .L9873f
.L9870f:
calli _28_S_22__29_
.byte 0xC 
.ascii "SEARCH-ORDER"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9880f
calli _32_DROP
calli TRUE
jumpi .L9883f
.L9880f:
calli _28_S_22__29_
.byte 0x10 
.ascii "SEARCH-ORDER-EXT"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9891f
calli _32_DROP
calli TRUE
jumpi .L9894f
.L9891f:
calli _28_S_22__29_
.byte 0xB 
.ascii "STACK-CELLS"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9901f
calli _32_DROP
calli STACK_2D_CELLS
jumpi .L9904f
.L9901f:
calli _28_S_22__29_
.byte 0x6 
.ascii "STRING"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9910f
calli _32_DROP
calli TRUE
jumpi .L9913f
.L9910f:
calli _28_S_22__29_
.byte 0xA 
.ascii "STRING-EXT"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9920f
calli _32_DROP
calli TRUE
jumpi .L9923f
.L9920f:
calli _28_S_22__29_
.byte 0x5 
.ascii "TOOLS"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9929f
calli _32_DROP
calli FALSE
jumpi .L9932f
.L9929f:
calli _28_S_22__29_
.byte 0x9 
.ascii "TOOLS-EXT"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9939f
calli _32_DROP
calli FALSE
jumpi .L9942f
.L9939f:
calli _28_S_22__29_
.byte 0x9 
.ascii "WORDLISTS"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L9949f
calli _32_DROP
pushi 8 
jumpi .L9952f
.L9949f:
calli _32_DROP
calli FALSE
ret
calli _32_DROP
.L9952f:
.L9942f:
.L9932f:
.L9923f:
.L9913f:
.L9904f:
.L9894f:
.L9883f:
.L9873f:
.L9862f:
.L9852f:
.L9843f:
.L9832f:
.L9820f:
.L9810f:
.L9801f:
.L9791f:
.L9782f:
.L9772f:
.L9762f:
.L9752f:
.L9742f:
.L9732f:
.L9723f:
.L9713f:
.L9704f:
.L9694f:
.L9685f:
.L9674f:
.L9665f:
.L9656f:
calli TRUE
ret
.set ENVIRONMENT_3F__info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "(THROW)"
.balign bee_word_bytes, 0x20 
.word ENVIRONMENT_3F_ - .
.word _28_THROW_29__compilation
.word _28_THROW_29__info 
.global _28_THROW_29_
_28_THROW_29_:
calli _3F_DUP
jumpzi .L9964f
calli HANDLER
load
calli _3F_DUP
jumpzi .L9968f
set_sp
calli R_3E_RESTORE_2D_INPUT
pops
calli HANDLER
store
pops
pushi 0 # 0x0 
swap
pushs
calli SP_21_
pops
jumpi .L9980f
.L9968f:
calli ERROR_2D_PREFIX
pushi 0 # 0x0 
dup
calli HANDLE_2D_ERROR
calli CR
throw
.L9980f:
.L9964f:
ret
.set _28_THROW_29__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "AT-XY"
.balign bee_word_bytes, 0x20 
.word _28_THROW_29_ - .
.word AT_2D_XY_compilation
.word AT_2D_XY_info 
.global AT_2D_XY
AT_2D_XY:
pushi 27 
calli EMIT
pushi 91 
calli EMIT
pushi 0 # 0x0 
swap
pushi 0 
calli _2E_R
pushi 59 
calli EMIT
pushi 0 
calli _2E_R
pushi 72 
calli EMIT
ret
.set AT_2D_XY_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "(.ALIGN)"
.balign bee_word_bytes, 0x20 
.word AT_2D_XY - .
.word _28__2E_ALIGN_29__compilation
.word _28__2E_ALIGN_29__info 
.global _28__2E_ALIGN_29_
_28__2E_ALIGN_29_:
calli _28_S_22__29_
.byte 0x16 
.ascii ".balign bee_word_bytes"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10020n:
pushreli _28__2E_ALIGN_29_
calli TO_2D_ASMOUT
ret
.set _2E_ALIGN_defer, .L10020n
.set _28__2E_ALIGN_29__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "(.CALIGN)"
.balign bee_word_bytes, 0x20 
.word _28__2E_ALIGN_29_ - .
.word _28__2E_CALIGN_29__compilation
.word _28__2E_CALIGN_29__info 
.global _28__2E_CALIGN_29_
_28__2E_CALIGN_29_:
calli _28_S_22__29_
.byte 0x1A 
.ascii ".balign bee_word_bytes, 0x"
.balign bee_word_bytes, 0x0 
calli TYPE
calli H_2E_
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10038n:
pushreli _28__2E_CALIGN_29_
calli TO_2D_ASMOUT
ret
.set _2E_CALIGN_defer, .L10038n
.set _28__2E_CALIGN_29__info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xD 
.ascii "(.REL-OFFSET)"
.balign bee_word_bytes, 0x20 
.word _28__2E_CALIGN_29_ - .
.word _28__2E_REL_2D_OFFSET_29__compilation
.word _28__2E_REL_2D_OFFSET_29__info 
.global _28__2E_REL_2D_OFFSET_29_
_28__2E_REL_2D_OFFSET_29_:
calli _28_S_22__29_
.byte 0x6 
.ascii ".word "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _3F_DUP
jumpzi .L10050f
calli BACKWARD
calli _2E_LABEL
calli _28_S_22__29_
.byte 0x4 
.ascii " - ."
.balign bee_word_bytes, 0x0 
calli TYPE
jumpi .L10056f
.L10050f:
calli _28_S_22__29_
.byte 0x1 
.ascii "0"
.balign bee_word_bytes, 0x0 
calli TYPE
.L10056f:
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10063n:
pushreli _28__2E_REL_2D_OFFSET_29_
calli TO_2D_ASMOUT
ret
.set _2E_REL_2D_OFFSET_defer, .L10063n
.set _28__2E_REL_2D_OFFSET_29__info, 0 | 0 | 0 | (13 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "(.NOP)"
.balign bee_word_bytes, 0x20 
.word _28__2E_REL_2D_OFFSET_29_ - .
.word _28__2E_NOP_29__compilation
.word _28__2E_NOP_29__info 
.global _28__2E_NOP_29_
_28__2E_NOP_29_:
calli _28_S_22__29_
.byte 0x4 
.ascii "nop "
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10076n:
pushreli _28__2E_NOP_29_
calli TO_2D_ASMOUT
ret
.set _2E_NOP_defer, .L10076n
.set _28__2E_NOP_29__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "(.ALLOT)"
.balign bee_word_bytes, 0x20 
.word _28__2E_NOP_29_ - .
.word _28__2E_ALLOT_29__compilation
.word _28__2E_ALLOT_29__info 
.global _28__2E_ALLOT_29_
_28__2E_ALLOT_29_:
calli _28_S_22__29_
.byte 0x6 
.ascii ".ds.b "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10091n:
pushreli _28__2E_ALLOT_29_
calli TO_2D_ASMOUT
ret
.set _2E_ALLOT_defer, .L10091n
.set _28__2E_ALLOT_29__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xE 
.ascii "(.ALLOT-CELLS)"
.balign bee_word_bytes, 0x20 
.word _28__2E_ALLOT_29_ - .
.word _28__2E_ALLOT_2D_CELLS_29__compilation
.word _28__2E_ALLOT_2D_CELLS_29__info 
.global _28__2E_ALLOT_2D_CELLS_29_
_28__2E_ALLOT_2D_CELLS_29_:
calli _28_S_22__29_
.byte 0x6 
.ascii ".ds.b "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_
calli _28_S_22__29_
.byte 0x10 
.ascii "* bee_word_bytes"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10111n:
pushreli _28__2E_ALLOT_2D_CELLS_29_
calli TO_2D_ASMOUT
ret
.set _2E_ALLOT_2D_CELLS_defer, .L10111n
.set _28__2E_ALLOT_2D_CELLS_29__info, 0 | 0 | 0 | (14 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "(.WORD)"
.balign bee_word_bytes, 0x20 
.word _28__2E_ALLOT_2D_CELLS_29_ - .
.word _28__2E_WORD_29__compilation
.word _28__2E_WORD_29__info 
.global _28__2E_WORD_29_
_28__2E_WORD_29_:
calli _28_S_22__29_
.byte 0x6 
.ascii ".word "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10125n:
pushreli _28__2E_WORD_29_
calli TO_2D_ASMOUT
ret
.set _2E_WORD_defer, .L10125n
.set _28__2E_WORD_29__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "(.BYTE)"
.balign bee_word_bytes, 0x20 
.word _28__2E_WORD_29_ - .
.word _28__2E_BYTE_29__compilation
.word _28__2E_BYTE_29__info 
.global _28__2E_BYTE_29_
_28__2E_BYTE_29_:
calli _28_S_22__29_
.byte 0x8 
.ascii ".byte 0x"
.balign bee_word_bytes, 0x0 
calli TYPE
calli H_2E_
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10140n:
pushreli _28__2E_BYTE_29_
calli TO_2D_ASMOUT
ret
.set _2E_BYTE_defer, .L10140n
.set _28__2E_BYTE_29__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "(.STRING)"
.balign bee_word_bytes, 0x20 
.word _28__2E_BYTE_29_ - .
.word _28__2E_STRING_29__compilation
.word _28__2E_STRING_29__info 
.global _28__2E_STRING_29_
_28__2E_STRING_29_:
calli _28_S_22__29_
.byte 0x7 
.ascii ".ascii "
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 34 
pushi 0 # 0x0 
dup
calli EMIT
pushi 1 # 0x1 
swap
pushi 0 # 0x0 
swap
pushi 1 # 0x1 
dup
add
pushi 0 # 0x0 
swap
calli _32__3E_R
.L10165b:
dups
load1
pushi 0 # 0x0 
dup
pushi 34 
eq
neg
pushi 1 # 0x1 
dup
pushi 92 
eq
neg
or
jumpzi .L10178f
pushi 92 
calli EMIT
.L10178f:
calli EMIT
calli _28_LOOP_29_
jumpzi .L10165b
.L10164f:
calli UNLOOP
calli EMIT
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10189n:
pushreli _28__2E_STRING_29_
calli TO_2D_ASMOUT
ret
.set _2E_STRING_defer, .L10189n
.set _28__2E_STRING_29__info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "(.PUSHI)"
.balign bee_word_bytes, 0x20 
.word _28__2E_STRING_29_ - .
.word _28__2E_PUSHI_29__compilation
.word _28__2E_PUSHI_29__info 
.global _28__2E_PUSHI_29_
_28__2E_PUSHI_29_:
calli _28_S_22__29_
.byte 0x6 
.ascii "pushi "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10204n:
pushreli _28__2E_PUSHI_29_
calli TO_2D_ASMOUT
ret
.set _2E_PUSHI_defer, .L10204n
.set _28__2E_PUSHI_29__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xB 
.ascii "(.PUSHRELI)"
.balign bee_word_bytes, 0x20 
.word _28__2E_PUSHI_29_ - .
.word _28__2E_PUSHRELI_29__compilation
.word _28__2E_PUSHRELI_29__info 
.global _28__2E_PUSHRELI_29_
_28__2E_PUSHRELI_29_:
calli _28_S_22__29_
.byte 0x9 
.ascii "pushreli "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_SYMBOL
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10220n:
pushreli _28__2E_PUSHRELI_29_
calli TO_2D_ASMOUT
ret
.set _2E_PUSHRELI_defer, .L10220n
.set _28__2E_PUSHRELI_29__info, 0 | 0 | 0 | (11 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "(.PUSH)"
.balign bee_word_bytes, 0x20 
.word _28__2E_PUSHRELI_29_ - .
.word _28__2E_PUSH_29__compilation
.word _28__2E_PUSH_29__info 
.global _28__2E_PUSH_29_
_28__2E_PUSH_29_:
calli HERE
calli _28_S_22__29_
.byte 0x6 
.ascii "calli "
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 0 # 0x0 
dup
calli FORWARD
calli _2E_LABEL
calli CR
pushi 0 # 0x0 
swap
calli _28__2E_WORD_29_
calli FORWARD
calli _2E_LABEL_2D_DEF
calli _28_S_22__29_
.byte 0x4 
.ascii "pops"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli _28_S_22__29_
.byte 0x4 
.ascii "load"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10251n:
pushreli _28__2E_PUSH_29_
calli TO_2D_ASMOUT
ret
.set _2E_PUSH_defer, .L10251n
.set _28__2E_PUSH_29__info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "(.LABEL)"
.balign bee_word_bytes, 0x20 
.word _28__2E_PUSH_29_ - .
.word _28__2E_LABEL_29__compilation
.word _28__2E_LABEL_29__info 
.global _28__2E_LABEL_29_
_28__2E_LABEL_29_:
pushi 0 # 0x0 
swap
calli _28_S_22__29_
.byte 0x2 
.ascii ".L"
.balign bee_word_bytes, 0x0 
calli TYPE
calli ADDR_3E_LABEL
pushi 0 
calli U_2E_R
calli EMIT
ret
.balign bee_word_bytes
.word 0 
.L10270n:
pushreli _28__2E_LABEL_29_
calli TO_2D_ASMOUT
ret
.set _2E_LABEL_defer, .L10270n
.set _28__2E_LABEL_29__info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xC 
.ascii "(.LABEL-DEF)"
.balign bee_word_bytes, 0x20 
.word _28__2E_LABEL_29_ - .
.word _28__2E_LABEL_2D_DEF_29__compilation
.word _28__2E_LABEL_2D_DEF_29__info 
.global _28__2E_LABEL_2D_DEF_29_
_28__2E_LABEL_2D_DEF_29_:
calli _2E_LABEL
calli _28_S_22__29_
.byte 0x1 
.ascii ":"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10285n:
pushreli _28__2E_LABEL_2D_DEF_29_
calli TO_2D_ASMOUT
ret
.set _2E_LABEL_2D_DEF_defer, .L10285n
.set _28__2E_LABEL_2D_DEF_29__info, 0 | 0 | 0 | (12 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x11 
.ascii "(.BODY-LABEL-DEF)"
.balign bee_word_bytes, 0x20 
.word _28__2E_LABEL_2D_DEF_29_ - .
.word _28__2E_BODY_2D_LABEL_2D_DEF_29__compilation
.word _28__2E_BODY_2D_LABEL_2D_DEF_29__info 
.global _28__2E_BODY_2D_LABEL_2D_DEF_29_
_28__2E_BODY_2D_LABEL_2D_DEF_29_:
calli _3E_NAME
calli _2E_NAME
calli _28_S_22__29_
.byte 0x6 
.ascii "_body:"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10302n:
pushreli _28__2E_BODY_2D_LABEL_2D_DEF_29_
calli TO_2D_ASMOUT
ret
.set _2E_BODY_2D_LABEL_2D_DEF_defer, .L10302n
.set _28__2E_BODY_2D_LABEL_2D_DEF_29__info, 0 | 0 | 0 | (17 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "(.BRANCH)"
.balign bee_word_bytes, 0x20 
.word _28__2E_BODY_2D_LABEL_2D_DEF_29_ - .
.word _28__2E_BRANCH_29__compilation
.word _28__2E_BRANCH_29__info 
.global _28__2E_BRANCH_29_
_28__2E_BRANCH_29_:
calli _28_S_22__29_
.byte 0x6 
.ascii "jumpi "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_LABEL
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10317n:
pushreli _28__2E_BRANCH_29_
calli TO_2D_ASMOUT
ret
.set _2E_BRANCH_defer, .L10317n
.set _28__2E_BRANCH_29__info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "(.IF)"
.balign bee_word_bytes, 0x20 
.word _28__2E_BRANCH_29_ - .
.word _28__2E_IF_29__compilation
.word _28__2E_IF_29__info 
.global _28__2E_IF_29_
_28__2E_IF_29_:
calli _28_S_22__29_
.byte 0x7 
.ascii "jumpzi "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_LABEL
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10331n:
pushreli _28__2E_IF_29_
calli TO_2D_ASMOUT
ret
.set _2E_IF_defer, .L10331n
.set _28__2E_IF_29__info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "(.RET)"
.balign bee_word_bytes, 0x20 
.word _28__2E_IF_29_ - .
.word _28__2E_RET_29__compilation
.word _28__2E_RET_29__info 
.global _28__2E_RET_29_
_28__2E_RET_29_:
calli _28_S_22__29_
.byte 0x3 
.ascii "ret"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10344n:
pushreli _28__2E_RET_29_
calli TO_2D_ASMOUT
ret
.set _2E_RET_defer, .L10344n
.set _28__2E_RET_29__info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x13 
.ascii "(.IMMEDIATE-METHOD)"
.balign bee_word_bytes, 0x20 
.word _28__2E_RET_29_ - .
.word _28__2E_IMMEDIATE_2D_METHOD_29__compilation
.word _28__2E_IMMEDIATE_2D_METHOD_29__info 
.global _28__2E_IMMEDIATE_2D_METHOD_29_
_28__2E_IMMEDIATE_2D_METHOD_29_:
calli _28_S_22__29_
.byte 0x5 
.ascii ".set "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x22 
.ascii "_compilation, (2 * bee_word_bytes)"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10367n:
pushreli _28__2E_IMMEDIATE_2D_METHOD_29_
calli TO_2D_ASMOUT
ret
.set _2E_IMMEDIATE_2D_METHOD_defer, .L10367n
.set _28__2E_IMMEDIATE_2D_METHOD_29__info, 0 | 0 | 0 | (19 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x11 
.ascii "(.COMPILE-METHOD)"
.balign bee_word_bytes, 0x20 
.word _28__2E_IMMEDIATE_2D_METHOD_29_ - .
.word _28__2E_COMPILE_2D_METHOD_29__compilation
.word _28__2E_COMPILE_2D_METHOD_29__info 
.global _28__2E_COMPILE_2D_METHOD_29_
_28__2E_COMPILE_2D_METHOD_29_:
calli _28_S_22__29_
.byte 0x5 
.ascii ".set "
.balign bee_word_bytes, 0x0 
calli TYPE
calli TUCK
calli _2E_NAME
calli _28_S_22__29_
.byte 0xE 
.ascii "_compilation, "
.balign bee_word_bytes, 0x0 
calli TYPE
calli NONAME
calli _2E_LABEL
calli _28_S_22__29_
.byte 0x4 
.ascii " - ("
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x16 
.ascii " - 2 * bee_word_bytes)"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10399n:
pushreli _28__2E_COMPILE_2D_METHOD_29_
calli TO_2D_ASMOUT
ret
.set _2E_COMPILE_2D_METHOD_defer, .L10399n
.set _28__2E_COMPILE_2D_METHOD_29__info, 0 | 0 | 0 | (17 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x16 
.ascii "(.CALL-COMPILE-METHOD)"
.balign bee_word_bytes, 0x20 
.word _28__2E_COMPILE_2D_METHOD_29_ - .
.word _28__2E_CALL_2D_COMPILE_2D_METHOD_29__compilation
.word _28__2E_CALL_2D_COMPILE_2D_METHOD_29__info 
.global _28__2E_CALL_2D_COMPILE_2D_METHOD_29_
_28__2E_CALL_2D_COMPILE_2D_METHOD_29_:
calli _28_S_22__29_
.byte 0x6 
.ascii "calli "
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 0 # 0x0 
dup
calli _2E_NAME
calli _28_S_22__29_
.byte 0x1A 
.ascii " - (2 * bee_word_bytes) + "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0xC 
.ascii "_compilation"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10428n:
pushreli _28__2E_CALL_2D_COMPILE_2D_METHOD_29_
calli TO_2D_ASMOUT
ret
.set _2E_CALL_2D_COMPILE_2D_METHOD_defer, .L10428n
.set _28__2E_CALL_2D_COMPILE_2D_METHOD_29__info, 0 | 0 | 0 | (22 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xF 
.ascii "(.INLINE-COUNT)"
.balign bee_word_bytes, 0x20 
.word _28__2E_CALL_2D_COMPILE_2D_METHOD_29_ - .
.word _28__2E_INLINE_2D_COUNT_29__compilation
.word _28__2E_INLINE_2D_COUNT_29__info 
.global _28__2E_INLINE_2D_COUNT_29_
_28__2E_INLINE_2D_COUNT_29_:
calli _28_S_22__29_
.byte 0x5 
.ascii ".set "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x9 
.ascii "_inline, "
.balign bee_word_bytes, 0x0 
calli TYPE
pushi 0 
calli U_2E_R
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10449n:
pushreli _28__2E_INLINE_2D_COUNT_29_
calli TO_2D_ASMOUT
ret
.set _2E_INLINE_2D_COUNT_defer, .L10449n
.set _28__2E_INLINE_2D_COUNT_29__info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xF 
.ascii "(.CREATED-CODE)"
.balign bee_word_bytes, 0x20 
.word _28__2E_INLINE_2D_COUNT_29_ - .
.word _28__2E_CREATED_2D_CODE_29__compilation
.word _28__2E_CREATED_2D_CODE_29__info 
.global _28__2E_CREATED_2D_CODE_29_
_28__2E_CREATED_2D_CODE_29_:
calli _28_S_22__29_
.byte 0x6 
.ascii "calli "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli _28_S_22__29_
.byte 0x5 
.ascii "_doer"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10467n:
pushreli _28__2E_CREATED_2D_CODE_29_
calli TO_2D_ASMOUT
ret
.set _2E_CREATED_2D_CODE_defer, .L10467n
.set _28__2E_CREATED_2D_CODE_29__info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x12 
.ascii "(.PUSHRELI-SYMBOL)"
.balign bee_word_bytes, 0x20 
.word _28__2E_CREATED_2D_CODE_29_ - .
.word _28__2E_PUSHRELI_2D_SYMBOL_29__compilation
.word _28__2E_PUSHRELI_2D_SYMBOL_29__info 
.global _28__2E_PUSHRELI_2D_SYMBOL_29_
_28__2E_PUSHRELI_2D_SYMBOL_29_:
calli _28_S_22__29_
.byte 0x9 
.ascii "pushreli "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _2E_NAME
calli CR
ret
.balign bee_word_bytes
.word 0 
.L10484n:
pushreli _28__2E_PUSHRELI_2D_SYMBOL_29_
calli TO_2D_ASMOUT
ret
.set _2E_PUSHRELI_2D_SYMBOL_defer, .L10484n
.set _28__2E_PUSHRELI_2D_SYMBOL_29__info, 0 | 0 | 0 | (18 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "ARGC"
.balign bee_word_bytes, 0x20 
.word _28__2E_PUSHRELI_2D_SYMBOL_29_ - .
.word ARGC_compilation
.word ARGC_info 
.global ARGC
ARGC:
nop 
calli ARGC_doer
.balign bee_word_bytes
ARGC_body:
.ds.b 1 * bee_word_bytes
.set ARGC_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.set ARGC_doer, _28_CREATE_29__does
.balign bee_word_bytes
.byte 0xF 
.ascii "INITIALIZE-ARGS"
.balign bee_word_bytes, 0x20 
.word ARGC - .
.word INITIALIZE_2D_ARGS_compilation
.word INITIALIZE_2D_ARGS_info 
.global INITIALIZE_2D_ARGS
INITIALIZE_2D_ARGS:
pushi 256 # 0x100 
trap 0x0 
calli ARGC
store
ret
.set INITIALIZE_2D_ARGS_info, 0 | 0 | 0 | (15 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x3 
.ascii "ARG"
.balign bee_word_bytes, 0x20 
.word INITIALIZE_2D_ARGS - .
.word ARG_compilation
.word ARG_info 
.global ARG
ARG:
pushi 256 # 0x100 
trap 0x0 
calli ARGC
load
neg
add
add
calli ABSOLUTE_2D_ARG
ret
.set ARG_info, 0 | 0 | 0 | (3 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "SHIFT-ARGS"
.balign bee_word_bytes, 0x20 
.word ARG - .
.word SHIFT_2D_ARGS_compilation
.word SHIFT_2D_ARGS_info 
.global SHIFT_2D_ARGS
SHIFT_2D_ARGS:
calli ARGC
load
neg
not
pushi 0 
calli MAX
calli ARGC
store
ret
.set SHIFT_2D_ARGS_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x8 
.ascii "NEXT-ARG"
.balign bee_word_bytes, 0x20 
.word SHIFT_2D_ARGS - .
.word NEXT_2D_ARG_compilation
.word NEXT_2D_ARG_info 
.global NEXT_2D_ARG
NEXT_2D_ARG:
pushi 0 
calli ARG
calli SHIFT_2D_ARGS
ret
.set NEXT_2D_ARG_info, 0 | 0 | 0 | (8 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x7 
.ascii "VERSION"
.balign bee_word_bytes, 0x20 
.word NEXT_2D_ARG - .
.word VERSION_compilation
.word VERSION_info 
.global VERSION
VERSION:
calli _28_S_22__29_
.byte 0x4 
.ascii "0.82"
.balign bee_word_bytes, 0x0 
ret
.set VERSION_info, 0 | 0 | 0 | (7 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "\"PLATFORM"
.balign bee_word_bytes, 0x20 
.word VERSION - .
.word _22_PLATFORM_compilation
.word _22_PLATFORM_info 
.global _22_PLATFORM
_22_PLATFORM:
calli _28_S_22__29_
.byte 0x3 
.ascii "Bee"
.balign bee_word_bytes, 0x0 
ret
.set _22_PLATFORM_info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x4 
.ascii "HELP"
.balign bee_word_bytes, 0x20 
.word _22_PLATFORM - .
.word HELP_compilation
.word HELP_info 
.global HELP
HELP:
calli _28_S_22__29_
.byte 0x7 
.ascii "Usage: "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _22_PROGRAM_2D_NAME
calli REL_40_
calli COUNT
calli TYPE
calli _28_S_22__29_
.byte 0x1A 
.ascii " [OPTION...] [FILENAME...]"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli CR
calli _28_S_22__29_
.byte 0xB 
.ascii "Run pForth."
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli CR
calli _28_S_22__29_
.byte 0x38 
.ascii "--interact       enter interactive loop after evaluating"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli _28_S_22__29_
.byte 0x27 
.ascii "                 command-line arguments"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli _28_S_22__29_
.byte 0x28 
.ascii "--evaluate TEXT  evaluate the given text"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli _28_S_22__29_
.byte 0x33 
.ascii "--help           display this help message and exit"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli _28_S_22__29_
.byte 0x35 
.ascii "--version        display version information and exit"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli _28_S_22__29_
.byte 0x1E 
.ascii "FILE             evaluate FILE"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli CR
calli _28_S_22__29_
.byte 0x1C 
.ascii "Report bugs to rrt@sc3d.org."
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set HELP_info, 0 | 0 | 0 | (4 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x6 
.ascii "BANNER"
.balign bee_word_bytes, 0x20 
.word HELP - .
.word BANNER_compilation
.word BANNER_info 
.global BANNER
BANNER:
calli _28_S_22__29_
.byte 0x8 
.ascii "pForth v"
.balign bee_word_bytes, 0x0 
calli TYPE
calli VERSION
calli TYPE
calli _28_S_22__29_
.byte 0xC 
.ascii " (platform: "
.balign bee_word_bytes, 0x0 
calli TYPE
calli _22_PLATFORM
calli TYPE
calli _28_S_22__29_
.byte 0x1 
.ascii ")"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
calli _28_S_22__29_
.byte 0x1B 
.ascii "(c) Reuben Thomas 1991-2021"
.balign bee_word_bytes, 0x0 
calli TYPE
calli CR
ret
.set BANNER_info, 0 | 0 | 0 | (6 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x9 
.ascii "INTERACT?"
.balign bee_word_bytes, 0x20 
.word BANNER - .
.word INTERACT_3F__compilation
.word INTERACT_3F__info 
.global INTERACT_3F_
INTERACT_3F_:
nop 
calli INTERACT_3F__doer
.balign bee_word_bytes
INTERACT_3F__body:
.word 0 
.set INTERACT_3F__info, 0 | 0 | 0 | (9 <<_name_length_bits) | 0x0 
.set INTERACT_3F__doer, VALUE_does
.balign bee_word_bytes
.byte 0x10 
.ascii "DO-START-OPTIONS"
.balign bee_word_bytes, 0x20 
.word INTERACT_3F_ - .
.word DO_2D_START_2D_OPTIONS_compilation
.word DO_2D_START_2D_OPTIONS_info 
.global DO_2D_START_2D_OPTIONS
DO_2D_START_2D_OPTIONS:
calli ARGC
load
jumpzi .L10688f
calli HERE
calli _22_PROGRAM_2D_NAME
calli REL_21_
calli NEXT_2D_ARG
calli _22__2C_
.L10688f:
calli ARGC
load
jumpzi .L10696f
.L10697b:
calli NEXT_2D_ARG
pushi 1 # 0x1 
dup
jumpzi .L10700f
pushi 1 # 0x1 
dup
load1
pushi 45 
eq
neg
jumpzi .L10707f
calli _28_S_22__29_
.byte 0x6 
.ascii "--help"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L10713f
calli _32_DROP
calli HELP
calli BYE
jumpi .L10717f
.L10713f:
calli _28_S_22__29_
.byte 0x9 
.ascii "--version"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L10724f
calli _32_DROP
calli BANNER
calli BYE
jumpi .L10728f
.L10724f:
calli _28_S_22__29_
.byte 0xA 
.ascii "--evaluate"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L10735f
calli _32_DROP
calli NEXT_2D_ARG
calli EVALUATE
jumpi .L10739f
.L10735f:
calli _28_S_22__29_
.byte 0xA 
.ascii "--interact"
.balign bee_word_bytes, 0x0 
calli _32_OVER
calli COMPARE
calli _30__3D_
jumpzi .L10746f
calli _32_DROP
calli TRUE
pushreli INTERACT_3F__body
store
jumpi .L10751f
.L10746f:
calli HERE
calli _27_THROWN
store
calli _22__2C_
pushi -512 
calli THROW
calli _32_DROP
.L10751f:
.L10739f:
.L10728f:
.L10717f:
jumpi .L10759f
.L10707f:
calli INCLUDED
.L10759f:
jumpi .L10697b
.L10700f:
calli _32_DROP
calli INTERACT_3F_
not
jumpzi .L10765f
calli BYE
.L10765f:
jumpi .L10767f
.L10696f:
calli BANNER
.L10767f:
calli _28_QUIT_29_
ret
.set DO_2D_START_2D_OPTIONS_info, 0 | 0 | 0 | (16 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x12 
.ascii "PARSE-COMMAND-LINE"
.balign bee_word_bytes, 0x20 
.word DO_2D_START_2D_OPTIONS - .
.word PARSE_2D_COMMAND_2D_LINE_compilation
.word PARSE_2D_COMMAND_2D_LINE_info 
.global PARSE_2D_COMMAND_2D_LINE
PARSE_2D_COMMAND_2D_LINE:
ret
.set PARSE_2D_COMMAND_2D_LINE_info, 0 | 0 | 0 | (18 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0x5 
.ascii "START"
.balign bee_word_bytes, 0x20 
.word PARSE_2D_COMMAND_2D_LINE - .
.word START_compilation
.word START_info 
.global START
START:
calli ROOTDP
store
pushreli _28_THROW_29_
calli _27_THROW_21_
pushi 0 
calli HANDLER
store
pushi 0 
calli FILE_2D_BUFFER_23_
store
calli ROOT
calli ONLY
calli FORTH
calli DEFINITIONS
calli DECIMAL
calli PARSE_2D_COMMAND_2D_LINE
calli INITIALIZE_2D_ARGS
calli INITIALIZE_2D_TERMINAL
calli DO_2D_START_2D_OPTIONS
ret
.set START_info, 0 | 0 | 0 | (5 <<_name_length_bits) | 0x0 
.balign bee_word_bytes
.byte 0xA 
.ascii "INITIALIZE"
.balign bee_word_bytes, 0x20 
.word START - .
.word INITIALIZE_compilation
.word INITIALIZE_info 
.global INITIALIZE
INITIALIZE:
pops
calli CELL_2D_
pushreli _27_FORTH_body
store
pushreli END_OF_IMAGE
calli START
ret
.balign bee_word_bytes
END_OF_IMAGE:
.set last_word, INITIALIZE
.set INITIALIZE_info, 0 | 0 | 0 | (10 <<_name_length_bits) | 0x0 
