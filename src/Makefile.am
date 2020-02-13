# Source Makefile.am

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
		echo "pforth make-base.fs" >> $@; \
		echo "If <Sys\$$ReturnCode> <> 0 Then Set TestBuild\$$ExitCode <Sys\$$ReturnCode>" >> $@; \
		echo "diff pforth-new pforth-base" >> $@; \
		echo "If <Sys\$$ReturnCode> <> 0 Then Set TestBuild\$$ExitCode <Sys\$$ReturnCode>" >> $@; \
		echo "Dir ^" >> $@; \
		echo "" >> $@; \
	done; \
	echo "Dir riscos" >> $@; \
	echo "Copy pforth-new pforth-new-0 ~C F" >> $@; \
	echo "Run pforth-new-0 make-base.fs" >> $@; \
	echo "If <Sys\$$ReturnCode> <> 0 Then Set TestBuild\$$ExitCode <Sys\$$ReturnCode>" >> $@; \
	echo "diff pforth-new pforth-base" >> $@; \
	echo "If <Sys\$$ReturnCode> <> 0 Then Set TestBuild\$$ExitCode <Sys\$$ReturnCode>" >> $@; \
	echo "Dir ^" >> $@; \
	echo "" >> $@; \
	echo "Dir ^" >> $@

# Forth image
pforth: $(host)/pforth-base
	echo "#!$(HOST_EXECUTOR) $(HOST_EXECUTOR_FLAGS)" | cat - $(host)/pforth-base > $@

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

check-local: $(host)/pforth-base
	cd $(top_srcdir)/extras && \
	echo foobar | $(HOST_EXECUTOR) $(HOST_EXECUTOR_FLAGS) $(abs_builddir)/$(host)/pforth-base tests.fs
