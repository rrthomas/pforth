dnl Process this file with autoconf to produce a configure script

AC_INIT(pForth, 0.81, rrt@sc3d.org)
AC_CONFIG_AUX_DIR([build-aux])
AM_INIT_AUTOMAKE([-Wall -Werror foreign])

dnl Check for programs
AC_PROG_LN_S
AC_PATH_PROG(LATEXMK, latexmk, true)
AM_CONDITIONAL([HAVE_LATEXMK], [test "$ac_cv_path_LATEXMK" != "true"])

dnl Check build and host architectures (native or VM)
AC_DEFUN([PFORTH_CHECK_EXECUTOR],
 [AC_PATH_PROG([$1], [$2])
  AS_IF([test "$ac_cv_path_BUILD_EXECUTOR" = ""],
    AC_MSG_ERROR([Could not find executor $2]))])

AS_IF([test "$BUILD_EXECUTOR" = ""],
  [AS_CASE([$build],
    [arm*-riscos], [BUILD_EXECUTOR=""; build=riscos],
    [beetle*], [PFORTH_CHECK_EXECUTOR([BUILD_EXECUTOR], [beetle])],
    [mit*], [PFORTH_CHECK_EXECUTOR([BUILD_EXECUTOR], [mit])
    BUILD_EXECUTOR="$BUILD_EXECUTOR --core-dump"],
    [AS_IF([test "$build" != ""],
       AC_MSG_ERROR([pForth does not support building with $build]))
     AC_PATH_PROGS(BUILD_EXECUTOR, [beetle mit])
     AS_IF([test "$ac_cv_path_BUILD_EXECUTOR" = ""],
       AC_MSG_ERROR([No build executor found for pForth]))
     build=`basename $BUILD_EXECUTOR`])],
  [AS_IF([test "$build" = ""],
    [AC_MSG_ERROR([BUILD_EXECUTOR is set, but --build is not!])])
   PFORTH_CHECK_EXECUTOR([BUILD_EXECUTOR], [$BUILD_EXECUTOR])])
AS_IF([! $BUILD_EXECUTOR $srcdir/src/$build/pforth-base --evaluate BYE],
  [AC_MSG_ERROR([$BUILD_EXECUTOR does not work!])])
AC_SUBST([build])

AS_IF([test "$HOST_EXECUTOR" = ""],
  [AS_CASE([$host],
    [""], [HOST_EXECUTOR=$BUILD_EXECUTOR; host=$build],
    [arm*-riscos], [HOST_EXECUTOR=""],
    [beetle*], [PFORTH_CHECK_EXECUTOR([HOST_EXECUTOR], [beetle])],
    [mit*], [PFORTH_CHECK_EXECUTOR([HOST_EXECUTOR], [mit])],
    [AC_MSG_ERROR([pForth does not support host $host])])],
  [AS_IF([test "$host" = ""],
   [AC_MSG_ERROR([HOST_EXECUTOR is set, but --host is not!])],
   [AS_IF([test ! -x "$HOST_EXECUTOR"],
     [AC_MSG_WARN([Cannot find HOST_EXECUTOR on this system])])])])
AC_SUBST([host])
AC_SUBST([HOST_EXECUTOR])

AC_MSG_NOTICE([Building system: $build])
AS_IF([test "$BUILD_EXECUTOR" != ""],
  AC_MSG_NOTICE([using VM: $BUILD_EXECUTOR]))
AC_MSG_NOTICE([Building to install on host: $host])
AS_IF([test "$HOST_EXECUTOR" != ""],
  AC_MSG_NOTICE([using VM: $HOST_EXECUTOR]))

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
  src/beetle/Makefile
  src/riscos/Makefile
  src/mit/Makefile
  src/mit-threaded/Makefile
  src/highlevel.fs
  doc/Makefile
  extras/Makefile
])
AC_OUTPUT
