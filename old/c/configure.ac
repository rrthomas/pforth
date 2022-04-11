dnl configure.ac

dnl Initialise autoconf and automake
AC_INIT(gnat, 0.0, rrt@sc3d.org)
AM_INIT_AUTOMAKE([-Wall -Werror std-options])

dnl Set up C compiler
AC_GNU_SOURCE
AC_PROG_CC
AX_CFLAGS_WARN_ALL
if test "$GCC" = "yes"; then
  CFLAGS="$CFLAGS -W -Wmissing-prototypes -Wstrict-prototypes -D_FORTIFY_SOURCE=2"
fi
gl_EARLY
AM_PROG_CC_C_O

dnl Valgrind
AX_WITH_PROG(VALGRIND, valgrind)
if test -n "$VALGRIND"; then
  VALGRIND="$VALGRIND --error-exitcode=1 --leak-check=full"
fi

dnl Checks for functions and headers
gl_INIT
AC_HEADER_STDC

dnl Generate output
AC_CONFIG_HEADER(config.h)
AC_CONFIG_FILES(Makefile lib/Makefile src/Makefile)
AC_OUTPUT
