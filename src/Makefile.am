# Source Makefile.am
#
# (c) Reuben Thomas 2018-2022
#
# The package is distributed under the GNU GPL version 3, or, at your
# option, any later version.
#
# THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
# RISK.

bin_SCRIPTS = pforth

noinst_DATA = pforth-32.bin pforth-64.bin
EXTRA_DIST =  $(portable_SRCS) $(host_SRCS) pforth pforth-32.bin pforth-64.bin
DISTCLEANFILES = highlevel.fs pforth.s

portable_SRCS = compiler2.fs compiler4.fs compiler5.fs control1.fs		\
	control2.fs control3.fs defer-fetch-store.fs defining.fs does.fs	\
	interpreter3.fs make.fs vocabulary.fs

BUILD_PFORTH = $(BEE) $(abs_top_srcdir)/src/pforth-$(bee_word_bits).bin

host_SRCS = $(portable_SRCS) \
	accept.fs assembler.fs call-cells.fs code.fs compiler1.fs		\
	compiler.fs compiler-asm.fs compiler-defer.fs compiler-postpone.fs	\
	extra-primitives.fs fileio.fs init-space.fs initialize.fs mangle.fs	\
	native-call.fs opcodes.fs os-compiler.fs os.fs parse-command-line.fs	\
	platform.fs primitives.fs resolver-branch.fs save.fs strings2a.fs	\
	strings2b.fs system-params.fs terminal.fs util.fs

ETAGS_ARGS = --language forth $(portable_SRCS) $(host_SRCS) highlevel.fs.in

do_build = \
    if test $(srcdir) != $(builddir); then \
	for i in $(host_SRCS); do \
	$(LN_S) -f $(abs_top_srcdir)/src/"$$i" . ; \
	done; \
    fi && \
    $(BUILD_PFORTH) --evaluate "$$MINIMAL_PRIMITIVES" make.fs

# Build with triple test
pforth-$(bee_word_bits).bin: $(host_SRCS) highlevel.fs
	$(do_build) && \
	mv pforth-new pforth-new-0 && \
	$(BEE) pforth-new-0 --evaluate "$$MINIMAL_PRIMITIVES" make.fs && \
	cmp pforth-new pforth-new-0 && \
	rm pforth-new-0 && \
	mv pforth-new $@

pforth.s: pforth-$(bee_word_bits).bin
	$(BUILD_PFORTH) make.fs 2> $@

%-32.o: %.s
	$(AS) -m32 -R -o $@ $<

%-64.o: %.s
	$(AS) -m64 -R -o $@ $<

.o.bin:
	$(OBJCOPY) -O binary $< $@

loc-local:
	cd $(srcdir) && $(CLOC) $(CLOC_OPTS) $(host_SRCS) $(portable_SRCS) $(abs_builddir)/highlevel.fs

# Forth executable
pforth: pforth-$(bee_word_bits).bin
	echo "#!$(BEE)" | cat - pforth-$(bee_word_bits).bin > $@
	chmod +x $@

CLEANFILES = pforth

if HAVE_RLWRAP
install-exec-hook:
	export pforth_name=`echo pforth | sed '$(transform)'`; \
	sed 's,@PFORTH@,'$$pforth_name',' < pforthi > pforthi.tmp; \
	$(INSTALL_PROGRAM) pforthi.tmp $(DESTDIR)$(bindir)/`echo pforthi | sed '$(transform)'`

uninstall-hook:
	rm $(DESTDIR)$(bindir)/`echo pforthi | sed '$(transform)'`

DISTCLEANFILES += pforthi pforthi.tmp
endif

# Error code 243 below is 256 + (-13) (Forth error code for "unknown word")
check-local: pforth
	$(do_build) && \
	cmp pforth-$(bee_word_bits).bin pforth-new && \
	rm pforth-new && \
	./pforth --evaluate FOO || test $$? = 243
