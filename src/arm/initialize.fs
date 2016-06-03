\ Initialization code
\ Reuben Thomas   15/4/96-15/3/99

CODE INITIALIZE
SWI," OS_GetEnv"
R2 PC 12 #+@ LDR,
R1 R2 0@ STR,
RP R1 MOV,
SP RP 256 # SUB,
' START 4 + B,
' LIMIT 8 + <M0 ,
END-CODE