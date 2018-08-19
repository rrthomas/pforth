# Docs Makefile.am

if GNU_MAKE
BIBINPUTS = $(abs_srcdir)/bibtex/bib
export BIBINPUTS
endif

LATEX_SRCS = \
	$(srcdir)/pforth.tex \
	$(srcdir)/bibtex/bib/vm.bib

LATEX_PDFS = \
	pforth.pdf

all: pdf
LATEXMK_OPTS = -bibtex -pdf


pdf-local: $(LATEX_PDFS)

MAINTAINERCLEANFILES = $(LATEX_PDFS)

clean-local:
	$(LATEXMK) $(LATEXMK_OPTS) -C $(LATEX_SRCS)

EXTRA_DIST = $(LATEX_SRCS) $(LATEX_PDFS) \
	InputStack OOF PartialEval Threading ToDo

.tex.pdf:
	$(LATEXMK) $(LATEXMK_OPTS) $<
