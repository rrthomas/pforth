# Source Makefile.am
#
# (c) Reuben Thomas 2018-2020
#
# The package is distributed under the GNU GPL version 3, or, at your
# option, any later version.
#
# THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
# RISK.

SUBDIRS = beetle riscos mit mit-threaded

bin_SCRIPTS = pforth

include $(srcdir)/host_src.am

all-local: !pForth/!TestBuild,feb

!pForth/!TestBuild,feb: Makefile.am
	echo "Set TestBuild\$$ExitCode 0" > $@; \
	echo "Dir src" >> $@; \
	echo "" >> $@; \
	for i in $(SUBDIRS); do \
		echo "Dir $$i" >> $@; \
		echo "pforth make.fs" >> $@; \
		echo "If <Sys\$$ReturnCode> <> 0 Then Set TestBuild\$$ExitCode <Sys\$$ReturnCode>" >> $@; \
		echo "diff pforth-new pforth" >> $@; \
		echo "If <Sys\$$ReturnCode> <> 0 Then Set TestBuild\$$ExitCode <Sys\$$ReturnCode>" >> $@; \
		echo "Dir ^" >> $@; \
		echo "" >> $@; \
	done; \
	echo "Dir riscos" >> $@; \
	echo "Copy pforth-new pforth-new-0 ~C F" >> $@; \
	echo "Delete pforth-new" >> $@; \
	echo "IfType pforth-new-0 &ff8 Then Run pforth-new-0 make.fs" >> $@; \
	echo "If <Sys\$$ReturnCode> <> 0 Then Set TestBuild\$$ExitCode <Sys\$$ReturnCode>" >> $@; \
	echo "diff pforth-new pforth" >> $@; \
	echo "If <Sys\$$ReturnCode> <> 0 Then Set TestBuild\$$ExitCode <Sys\$$ReturnCode>" >> $@; \
	echo "IfType pforth-new &ff8 Then Echo Filetype OK Else Set TestBuild$ExitCode 3" >> $@; \
	echo "Dir ^" >> $@; \
	echo "" >> $@; \
	echo "Dir ^" >> $@

# Forth image
pforth: $(host)/pforth
	echo "#!$(HOST_EXECUTOR) $(HOST_EXECUTOR_FLAGS)" | cat - $(host)/pforth > $@
	chmod +x $@

EXTRA_DIST = $(portable_SRCS)							\
	!pForth/!TestBuild,feb
CLEANFILES = pforth
DISTCLEANFILES = $(host)/pforth

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
	$(CLOC) $(CLOC_OPTS) $(portable_SRCS)

# Error code 243 below is 256 + (-13) (Forth error code for "unknown word")
check-local: pforth
	cd $(top_srcdir)/extras && \
	echo foobar | $(abs_builddir)/pforth tests.fs
	if test "$(host)" = "$(build)" -a "$(target)" = "$(host)"; then \
	    ./pforth --evaluate FOO || test $? = 243 ; \
	fi
