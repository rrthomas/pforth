dnl Process this file with autoconf to produce a configure script

AC_INIT(pForth, 0.79, rrt@sc3d.org)
AC_CONFIG_AUX_DIR([build-aux])
AM_INIT_AUTOMAKE([-Wall -Werror foreign])
AC_CONFIG_MACRO_DIR(m4)

dnl Check for programs
gl_EARLY
AC_PROG_MKDIR_P
AC_PROG_LN_S
AC_PATH_PROG(BEETLE, beetle, AC_MSG_ERROR([Beetle not found]))
AC_PATH_PROG(BEETLEI, beetlei, AC_MSG_ERROR([beetlei not found]))
AC_PATH_PROG(LATEXMK, latexmk, true)
gl_INIT

dnl Generate output files
AC_CONFIG_FILES([Makefile lib/Makefile src/Makefile src/highlevel.fs doc/Makefile extras/Makefile])
AC_OUTPUT
