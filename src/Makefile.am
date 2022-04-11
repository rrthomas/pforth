# Source Makefile.am
#
# (c) Reuben Thomas 2018-2022
#
# The package is distributed under the GNU GPL version 3, or, at your
# option, any later version.
#
# THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
# RISK.

bin_SCRIPTS = gnat

noinst_DATA = gnat-32.bin gnat-64.bin
EXTRA_DIST =  $(portable_SRCS) $(host_SRCS) gnat gnat-32.bin gnat-64.bin
DISTCLEANFILES = highlevel.fs

portable_SRCS = make.fs

BUILD_GNAT = $(BEE) $(abs_top_srcdir)/src/gnat-$(bee_word_bits).bin

host_SRCS = $(portable_SRCS) \
	assembler.fs compiler-asm.fs extra-primitives.fs fileio.fs		\
	opcodes.fs platform.fs primitives.fs system-params.fs terminal.fs

ETAGS_ARGS = --language forth $(portable_SRCS) $(host_SRCS) highlevel.fs.in

do_build = $(BUILD_GNAT) --evaluate "$$MINIMAL_PRIMITIVES" make.fs

gnat.s: $(host_SRCS) highlevel.fs
	$(do_build) 2> $@

%-32.o: %.s
	$(AS) -m32 -R -o $@ $<

%-64.o: %.s
	$(AS) -m64 -R -o $@ $<

.o.s:
	$(AS) -R -o $@ $<

.o.bin:
	$(OBJCOPY) -O binary $< $@

loc-local:
	cd $(srcdir) && $(CLOC) $(CLOC_OPTS) $(host_SRCS) $(portable_SRCS) $(abs_builddir)/highlevel.fs

# Forth executable
gnat: gnat-$(bee_word_bits).bin
	echo "#!$(BEE)" | cat - gnat-$(bee_word_bits).bin > $@
	chmod +x $@

CLEANFILES = gnat

if HAVE_RLWRAP
install-exec-hook:
	export gnat_name=`echo gnat | sed '$(transform)'`; \
	sed 's,@GNAT@,'$$gnat_name',' < gnati > gnati.tmp; \
	$(INSTALL_PROGRAM) gnati.tmp $(DESTDIR)$(bindir)/`echo gnati | sed '$(transform)'`

uninstall-hook:
	rm $(DESTDIR)$(bindir)/`echo gnati | sed '$(transform)'`

DISTCLEANFILES += gnati gnati.tmp
endif

# Check we can run gnat, and do a triple-test build
# Error code 243 below is 256 + (-13) (Forth error code for "unknown word")
check-local: gnat
	( ./gnat --evaluate FOO || test $$? = 243 ) && \
	./gnat make.fs 2> gnat-new.s && \
	$(MAKE) gnat-new.bin && \
	./gnat make.fs 2> gnat-new-0.s && \
	diff gnat-new.s gnat-new-0.s && \
	rm gnat-new.s gnat-new-0.s gnat-new.bin
