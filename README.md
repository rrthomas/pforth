# pForth

https://github.com/rrthomas/pforth  

by Reuben Thomas <rrt@sc3d.org>  

pForth is a simple ANS Forth compiler, intended for portability and study.
It has been principally used as an environment for building other Forth
compilers: metacompiling itself for the
[Beetle](https://github.com/rrthomas/beetle) portable virtual machine and
the ARM-based [RISC OS](https://www.riscosopen.org/); compiling a cut-down
version called mForth (now defunct) for RISC OS and Beetle, and building
[Machine Forth](https://rrt.sc3d.org/Software/Forth) systems.

pForth is released purely in the hope that it might be interesting or useful
to someone.

(I am aware that there are other Forth compilers called pForth; the
duplication was unintentional.)

pForth comes pre-compiled for the Beetle VM (`src/beetle/pforth`)
and for RISC OS 3 (`!pForth`). Beetle is a portable C program.


## Copyright and Disclaimer

The package is distributed under the GNU Public License version 3, or, at
your option, any later version. A copy is available from my web page (address
above).

THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER'S RISK.


## Installation

Beetle is required (see above).

### Building a release

From an unpacked release tarball, run:

```
./configure && make && [sudo] make install
```

See `./configure --help` for more options.

### Building from git

To build from a git checkout, GNU autotools (autoconf and automake) are also required. Run:

```
git submodule update --init --recursive
autoreconf -i
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
