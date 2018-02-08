dnl Process this file with autoconf to produce a configure script

AC_INIT(pForth, 0.79, rrt@sc3d.org)
AC_CONFIG_AUX_DIR([build-aux])
AM_INIT_AUTOMAKE([-Wall foreign])

dnl Check for programs
AC_PROG_MKDIR_P
AC_PROG_LN_S
AC_PATH_PROG(BEETLE, beetle, AC_MSG_ERROR([Beetle not found]))

dnl Generate output files
AC_CONFIG_FILES([Makefile src/Makefile])
AC_OUTPUT
