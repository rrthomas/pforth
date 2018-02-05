# Maintainer Makefile for pForth
# FIXME: autoconfiscate (or nancify?)

VERSION=0.79

# FIXME: should depend on some sort of distcheck
release:
	git diff --exit-code && \
	git tag -a -m "Release tag" "v$(VERSION)" && \
	git push && git push --tags && \
	woger github \
		github_user=rrthomas \
		package=pforth \
		version=$(VERSION) \
		dist_type=tar.gz
