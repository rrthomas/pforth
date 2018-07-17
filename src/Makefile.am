# Source Makefile.am

SUBDIRS = beetle riscos smite

bin_SCRIPTS = pforth
if HAVE_RLWRAP
bin_SCRIPTS += pforthi
endif
data_DATA = $(host)/pforth.img
BUILD_PFORTH = $(BUILD_EXECUTOR) $(abs_srcdir)/$(build)/pforth-base.img

include $(srcdir)/host_src.am

# Extended Forth image (if building on host)
$(host)/pforth.img: $(host)/pforth-base.img $(host)/make.fs
	if test "$(host)" = "$(build)"; then \
		cd $(host); \
		export srcs="$(host_SRCS) make.fs"; $(do_build_prep) && \
		$(BUILD_PFORTH) make.fs; \
	else \
		cp $< $@; \
	fi

EXTRA_DIST = $(portable_SRCS)							\
	make.fs pforth.in
CLEANFILES = pforth
DISTCLEANFILES = $(host)/pforth.img

portable_SRCS = compiler2.fs compiler4.fs compiler5.fs control1.fs		\
	control2.fs defer-fetch-store.fs defining.fs does.fs highlevel.fs	\
	interpreter3.fs make-base.fs resolver.fs vocabulary.fs

# FIXME: Add support for HOST_EXECUTORI
do_subst = sed -e 's,[@]datadir[@],$(datadir),g' \
	-e 's,[@]HOST_EXECUTOR[@],$(HOST_EXECUTOR),g'

pforth: pforth.in Makefile
	$(do_subst) < $(srcdir)/pforth.in > pforth

loc-local:
	$(CLOC) $(CLOC_OPTS) $(portable_SRCS)

check-local: $(host)/pforth.img
	cd $(top_srcdir)/extras && \
	echo foobar | $(HOST_EXECUTOR) $(abs_builddir)/$(host)/pforth.img tests.fs
