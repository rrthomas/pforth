# pForth

https://github.com/rrthomas/pforth  

by Reuben Thomas <rrt@sc3d.org>  

pForth is a simple ANS Forth compiler, intended for portability and study.
It has been principally used as an environment for building other Forth
compilers: metacompiling itself for the
[Bee](https://github.com/rrthomas/bee),
[Beetle](https://github.com/rrthomas/beetle) and
[Mit](https://github.com/rrthomas/mit) portable virtual machines and
the ARM-based [RISC OS](https://www.riscosopen.org/); compiling a cut-down
version called mForth (now defunct) for RISC OS and Beetle, and building
[Machine Forth](https://rrt.sc3d.org/Software/Forth) systems.

pForth is released purely in the hope that it might be interesting or useful
to someone.

(I am aware that there are other Forth compilers called pForth; the
duplication was unintentional.)

pForth comes pre-compiled for Bee (`src/bee/pforth`), Beetle
(`src/beetle/pforth`), the Mit (`src/mit/pforth`), and for RISC OS 3
(`!pForth`).

See `doc/pforth.pdf` for ANSI conformance information.


## Copyright and Disclaimer

The package is distributed under the GNU Public License version 3, or, at
your option, any later version. See the file COPYING.

THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER'S RISK.


## Installation

Bee, Beetle, Mit or RISC OS is required (see above). If the native build
architecture is not supported, Bee, Beetle and Mit are automatically
searched for, in that order.

To choose the host and/or build system manually, pass the `--host=ARCH` or
`--build=ARCH` arguments to `configure`; to choose the flavour (e.g. `mit-threaded`), pass the `HOST_FLAVOUR` or `BUILD_FLAVOUR` variable.

To give the path to a VM executor, set the `HOST_EXECUTOR` or
`BUILD_EXECUTOR` variables.

See `./configure --help` for more information.

To build the documentation, a comprehensive TeX system such as TeXLive is
required.

### Building from a release tarball

(For building from git, first see below.)

From an unpacked release tarball, run:

```
./configure && make && make check && [sudo] make install
```

See the file `INSTALL` or the output of `./configure --help` for more
information.

### Building from git

To build from a git checkout, GNU autotools (autoconf and automake) are also
required. Run:

```
git submodule update --init --recursive
autoreconf -fi
```

and then proceed as above for a release build.


## Acknowledgements

Thanks to Hanno Schwalm for improved 0=, U< and U> primitives for the ARM
version, and to the authors of RISC Forth, the first Forth system I studied
closely, which inspired me to write pForth.


## Bugs and comments

Please file bug reports and make comments on
[GitHub](https://github.com/rrthomas/pforth/issues), or by email (see
above).

I will probably fix any bugs. Any future development is likely to involve a
total rewrite; I'm particularly interested in rewriting pForth in a more
Forth-like manner (more decomposed, rather than implementing each word as a
single word), and perhaps using object orientation. See `doc/ToDo`.
