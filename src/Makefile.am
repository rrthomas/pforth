# Source Makefile.am
#
# (c) Reuben Thomas 2018-2022
#
# The package is distributed under the GNU GPL version 3, or, at your
# option, any later version.
#
# THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
# RISK.

SUBDIRS = bee

bin_SCRIPTS = pforth

include $(srcdir)/host_src.am

# Forth image
pforth: $(host)/pforth
	echo "#!$(HOST_EXECUTOR) $(HOST_EXECUTOR_FLAGS)" | cat - $(host)/pforth > $@
	chmod +x $@

EXTRA_DIST = $(portable_SRCS)
CLEANFILES = pforth
DISTCLEANFILES =

if HAVE_RLWRAP
install-exec-hook:
	export pforth_name=`echo pforth | sed '$(transform)'`; \
	sed 's,@PFORTH@,'$$pforth_name',' < pforthi > pforthi.tmp; \
	$(INSTALL_PROGRAM) pforthi.tmp $(DESTDIR)$(bindir)/`echo pforthi | sed '$(transform)'`

uninstall-hook:
	rm $(DESTDIR)$(bindir)/`echo pforthi | sed '$(transform)'`

DISTCLEANFILES += $(host)/pforthi pforthi.tmp
endif

loc-local:
	cd $(srcdir) && $(CLOC) $(CLOC_OPTS) $(portable_SRCS) $(abs_builddir)/highlevel.fs

# Error code 243 below is 256 + (-13) (Forth error code for "unknown word")
check-local: pforth
	cd $(top_srcdir)/extras && \
	echo foobar | $(abs_builddir)/pforth tests.fs
	if test "$(host)" = "$(build)" -a "$(target)" = "$(host)"; then \
	    ./pforth --evaluate FOO || test $? = 243 ; \
	fi
