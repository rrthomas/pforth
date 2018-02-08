# Source Makefile.am

bin_SCRIPTS = pforth
data_DATA = beetle/pforth.img
noinst_DATA = riscos/pforth.img

EXTRA_DIST = $(portable_SRCS) $(riscos_SRCS) $(beetle_SRCS) \
	pforth.in beetle/pforth.img riscos/pforth.img
CLEANFILES = pforth beetle/pforth.img riscos/pforth.img

beetle_SRCS = beetle/assembler.fs beetle/bracket-does.fs			\
	beetle/compiler1.fs beetle/compiler.fs beetle/compiler-postpone.fs	\
	beetle/does-resolver.fs beetle/fileio.fs beetle/initialize.fs		\
	beetle/os-compiler.fs beetle/os.fs beetle/platform.fs			\
	beetle/primitives.fs beetle/save.fs beetle/target-forth.fs		\
	beetle/target-util.fs beetle/terminal.fs beetle/throw-contents.fs	\
	beetle/util.fs

riscos_SRCS = riscos/assembler.fs riscos/bracket-does.fs			\
	riscos/compiler1.fs riscos/compiler.fs riscos/compiler-postpone.fs	\
	riscos/does-resolver.fs riscos/fileio.fs riscos/initialize.fs		\
	riscos/os-compiler.fs riscos/os.fs riscos/platform.fs			\
	riscos/primitives.fs riscos/save.fs riscos/target-forth.fs		\
	riscos/target-util.fs riscos/terminal.fs riscos/throw-contents.fs	\
	riscos/util.fs

portable_SRCS = compiler2.fs compiler4.fs compiler5.fs control1.fs	\
	control2.fs defining.fs does.fs highlevel.fs interpreter4.fs	\
	make.fs resolver.fs vocabulary.fs

do_build = \
	if test $(srcdir) != $(builddir); then \
	    for i in $^; do \
		$(LN_S) "$$i" . ; \
	    done; \
	else \
	    cd $(abs_builddir)/$$flavour; \
	fi; \
	$(BEETLE) $(abs_srcdir)/beetle/pforth.img make.fs

beetle/pforth.img: $(portable_SRCS) $(beetle_SRCS)
	export flavour=beetle; $(do_build) && \
	mv pforth-new pforth-new-0 && \
	$(BEETLE) $(abs_srcdir)/beetle/pforth-new-0 make.fs && \
	cmp pforth-new pforth-new-0 && \
	mv pforth-new pforth.img && \
	rm pforth-new-0

riscos/pforth.img: $(portable_SRCS) $(riscos_SRCS)
	export flavour=riscos; $(do_build) && \
	mv pforth-new pforth.img

do_subst = sed -e 's,[@]datadir[@],$(datadir),g' \
	-e 's,[@]BEETLE[@],$(BEETLE),g'

pforth: pforth.in Makefile
	$(do_subst) < $(srcdir)/pforth.in > pforth

# FIXME
#check-local: libbeetle.la

CLOC = cloc --force-lang="Forth",fs

loc:
	$(CLOC) $(portable_SRCS) $(beetle_SRCS) $(riscos_SRCS)
