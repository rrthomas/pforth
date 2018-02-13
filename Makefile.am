# Top-level Makefile.am

SUBDIRS = lib src doc extras

ACLOCAL_AMFLAGS = -I m4

EXTRA_DIST = README.md m4/gnulib-cache.m4

release: distcheck
	git diff --exit-code && \
	git tag -a -m "Release tag" "v$(VERSION)" && \
	git push && git push --tags && \
	woger github \
		github_user=rrthomas \
		package=pforth \
		version=$(VERSION) \
		dist_type=tar.gz
