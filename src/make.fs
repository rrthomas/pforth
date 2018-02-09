#!/usr/bin/env pforth
\ Create pForth distribution image
\ Reuben Thomas   started 15/4/96

INCLUDE" assembler.fs"

S" pforth-img" SAVE-IMAGE   \ write system image
