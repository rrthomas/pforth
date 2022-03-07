# configure.ac
#
# (c) Reuben Thomas 2018-2022
#
# The package is distributed under the GNU GPL version 3, or, at your
# option, any later version.
#
# THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
# RISK.

AC_INIT(pForth, 0.82, rrt@sc3d.org)
AC_CONFIG_AUX_DIR([build-aux])
AM_INIT_AUTOMAKE([-Wall foreign])

dnl Check for programs
AC_PROG_LN_S
AC_PATH_PROG(LATEXMK, latexmk, true)
AM_CONDITIONAL([HAVE_LATEXMK], [test "$ac_cv_path_LATEXMK" != "true"])

dnl Check for Bee and its binutils
AC_PROG_GREP
AC_PATH_PROG([BEE], [bee])
AS_IF([test "$ac_cv_path_BEE" = ""],
    AC_MSG_ERROR([Could not find Bee]))
  AS_IF([$ac_cv_path_BEE --version | $GREP -q 64-bit], [bee_word_bits=64], [bee_word_bits=32])
  AS_IF([! $ac_cv_path_BEE $srcdir/src/pforth-$bee_word_bits --evaluate BYE],
    [AC_MSG_ERROR([$ac_cv_path_BEE does not work!])])
AC_SUBST([bee_word_bits])
AC_PATH_PROG([AS], [bee-as])
AS_IF([test "$ac_cv_path_AS" = ""],
    AC_MSG_ERROR([Could not find Bee as]))
AC_PATH_PROG([OBJCOPY], [bee-objcopy])
AS_IF([test "$ac_cv_path_OBJCOPY" = ""],
    AC_MSG_ERROR([Could not find Bee objcopy]))
AS_IF([$ac_cv_path_OBJCOPY --info | grep -q "^elf$bee_word_bits-(little|big)bee"],
    AC_MSG_ERROR([objcopy does not appear to be for bee])
)

dnl Readline wrapper
AC_PATH_PROG(RLWRAP, rlwrap)
AM_CONDITIONAL([HAVE_RLWRAP], [test -n "$ac_cv_path_RLWRAP"])

dnl Code counting
AM_EXTRA_RECURSIVE_TARGETS([loc])
AC_PATH_PROG(CLOC, cloc, true)
CLOC_OPTS=--force-lang="Forth",fs
AC_SUBST([CLOC_OPTS])

dnl Generate output files
AC_CONFIG_FILES([
  Makefile
  src/Makefile
  src/pforthi
  src/highlevel.fs
  doc/Makefile
  extras/Makefile
])
AC_OUTPUT
