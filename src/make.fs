#!/usr/bin/env pforth
\ Create pForth distribution image

INCLUDE" assembler.fs"

S" pforth.img" SAVE-IMAGE   \ write system image
