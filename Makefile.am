# Top-level Makefile.am
#
# (c) Reuben Thomas 2018-2020
#
# The package is distributed under the GNU GPL version 3, or, at your
# option, any later version.
#
# THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
# RISK.

SUBDIRS = src doc extras

EXTRA_DIST = README.md

release: distcheck
	git diff --exit-code && \
	git tag -a -m "Release tag" "v$(VERSION)" && \
	git push && git push --tags && \
	woger github \
		github_user=rrthomas \
		package=pforth \
		version=$(VERSION) \
		dist_type=tar.gz

distcheck-hook:
	touch src/highlevel.fs.in
