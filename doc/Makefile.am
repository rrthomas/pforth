# Docs Makefile.am

LATEX_SRCS = \
	$(srcdir)/pforth.tex \
	$(srcdir)/bibtex/bib/vm.bib

LATEX_PDFS = \
	pforth.pdf

if HAVE_LATEXMK
dist_doc_DATA = $(LATEX_PDFS)
endif

all: pdf

pdf-local: $(LATEX_PDFS)

MAINTAINERCLEANFILES = $(LATEX_PDFS)

LATEXMK_OPTS = -bibtex -pdf

clean-local:
	$(LATEXMK) $(LATEXMK_OPTS) -C $(LATEX_SRCS)

EXTRA_DIST = $(LATEX_SRCS) $(LATEX_PDFS) \
	InputStack OOF PartialEval Threading ToDo

.tex.pdf:
	env BIBINPUTS=$(abs_srcdir)/bibtex/bib $(LATEXMK) $(LATEXMK_OPTS) $<
