# Source Makefile.am

bin_SCRIPTS = pforth
data_DATA = beetle/pforth.img
noinst_DATA = riscos/pforth-base.img

EXTRA_DIST = $(portable_SRCS) $(riscos_SRCS) $(beetle_SRCS)                     \
	make.fs beetle/make.fs							\
	pforth.in beetle/pforth-base.img riscos/pforth-base.img
CLEANFILES = pforth
DISTCLEANFILES = beetle/pforth-base.img beetle/pforth.img riscos/pforth-base.img

beetle/highlevel.fs riscos/highlevel.fs: highlevel.fs

beetle_SRCS = beetle/accept.fs beetle/compiler2.fs beetle/compiler4.fs		\
	beetle/compiler5.fs beetle/control1.fs beetle/control2.fs		\
	beetle/defer-fetch-store.fs beetle/defining.fs beetle/does.fs		\
	beetle/highlevel.fs beetle/interpreter3.fs beetle/make-base.fs		\
	beetle/resolver.fs beetle/vocabulary.fs beetle/assembler.fs		\
	beetle/bracket-does.fs beetle/compiler1.fs beetle/compiler.fs		\
	beetle/compiler-postpone.fs beetle/does-resolver.fs			\
	beetle/fileio.fs beetle/initialize.fs beetle/os-compiler.fs		\
	beetle/os.fs beetle/platform.fs beetle/primitives.fs beetle/save.fs	\
	beetle/target-forth.fs beetle/target-util.fs beetle/terminal.fs		\
	beetle/throw-contents.fs beetle/util.fs

riscos_SRCS = riscos/accept.fs riscos/compiler2.fs riscos/compiler4.fs		\
	riscos/compiler5.fs riscos/control1.fs riscos/control2.fs		\
	riscos/defer-fetch-store.fs riscos/defining.fs riscos/does.fs		\
	riscos/highlevel.fs riscos/interpreter3.fs riscos/make-base.fs		\
	riscos/resolver.fs riscos/vocabulary.fs riscos/assembler.fs		\
	riscos/bracket-does.fs riscos/compiler1.fs riscos/compiler.fs		\
	riscos/compiler-postpone.fs riscos/does-resolver.fs			\
	riscos/fileio.fs riscos/initialize.fs riscos/os-compiler.fs		\
	riscos/os.fs riscos/platform.fs riscos/primitives.fs riscos/save.fs	\
	riscos/target-forth.fs riscos/target-util.fs riscos/terminal.fs		\
	riscos/throw-contents.fs riscos/util.fs

portable_SRCS = compiler2.fs compiler4.fs compiler5.fs control1.fs		\
	control2.fs defer-fetch-store.fs defining.fs does.fs highlevel.fs	\
	interpreter3.fs make-base.fs resolver.fs vocabulary.fs

do_build_prep = \
	if test $(srcdir) != $(builddir); then \
	    $(MKDIR_P) $$flavour; \
	    cd $$flavour; \
	    for i in $$srcs; do \
		$(LN_S) -f $(abs_srcdir)/"$$i" . ; \
	    done; \
	else \
	    cd $(abs_srcdir)/$$flavour; \
	fi

beetle/pforth.img: beetle/pforth-base.img $(portable_SRCS) $(beetle_SRCS) make.fs
	export flavour=beetle srcs="$(beetle_SRCS) beetle/make.fs"; $(do_build_prep) && \
	$(BEETLE) $(abs_srcdir)/beetle/pforth-base.img make.fs

beetle/pforth-base.img: $(portable_SRCS) $(beetle_SRCS)
	export flavour=beetle srcs="$(beetle_SRCS)"; $(do_build_prep) && \
	$(BEETLE) $(abs_srcdir)/beetle/pforth-base.img make-base.fs && \
	mv pforth-new pforth-new-0 && \
	$(BEETLE) pforth-new-0 make-base.fs && \
	cmp pforth-new pforth-new-0 && \
	mv pforth-new pforth-base.img && \
	rm pforth-new-0

# FIXME: Do idempotency test
riscos/pforth-base.img: $(portable_SRCS) $(riscos_SRCS) beetle/pforth-base.img
	export flavour=riscos srcs="$(riscos_SRCS)"; $(do_build_prep) && \
	$(BEETLE) $(abs_srcdir)/beetle/pforth-base.img make-base.fs && \
	mv pforth-new pforth-base.img

# FIXME: Build extended RISC OS image

do_subst = sed -e 's,[@]datadir[@],$(datadir),g' \
	-e 's,[@]BEETLE[@],$(BEETLE),g'

pforth: pforth.in Makefile
	$(do_subst) < $(srcdir)/pforth.in > pforth

check-local: beetle/pforth.img
	cd $(top_srcdir)/extras && \
	echo foobar | $(BEETLE) $(abs_builddir)/beetle/pforth.img tests.fs

CLOC = cloc --force-lang="Forth",fs

loc:
	$(CLOC) $(portable_SRCS) $(beetle_SRCS) $(riscos_SRCS)
