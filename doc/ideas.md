This file is (c) Reuben Thomas 1995-2020, and is in the public domain.

# Input stack

Could generalise the input stream to be a stack. Have a specification for
each stream giving its handle (e.g. a filename, file handle+ptr+length,
address+length &c.). Also have a flag specifying whether the end of the
stream counts as EOL or not. Then can splice into the middle of a source by
copying its specifiers and changing the length and starting address (to do
this, need to have a known start address and length for the source that is to
be split).

# Object-oriented Forth syntax

CREATE[ ... ] for per-instance declarations. Need non-parsing versions of
VARIABLE, VALUE etc. Then can say e.g.

    CREATE[ S" FOO" VARIABLE 42 S" BAR" VALUE ]

DOES[ ... ] for methods. Go into interpretation mode after DOES[ and compile
into a (per-class) private dictionary.

DOES> and CREATE work as normal: CREATE is effectively CREATE[ ] and DOES> is
DOES[ : DOES-METHOD ... ; DEFAULT ].

DEFAULT after a declaration in CREATE[ ... ] or DOES[ ... ] makes that method
the default.

Modify the parser to allow the following syntax:

object		executes default method of object
object.method	executes method of object
class_method	executes method of class on the object on the stack
O' object	returns the object pointer of object.

Can parse the top three after attempting to parse a token as a number (should
be before for efficiency, but after for ANS compatibility; and yet not, as if
no non-ANSI object words are created, this effect will not occur).

# Partial evaluation

Newsgroups: comp.lang.forth  
Subject: Partial evaluation: a code generation mechanism (long)  
Organization: University of Cambridge, England  

Optimising compilers, particularly of functional languages, are often seen
as partial evaluators, and it occurred to me a little while ago that this
idea could be applied to Forth. More recently, it occurred to me that this
would not necessarily change the semantics of the language at all (at
least, not of ANS Standard Forth).

The scheme goes like this:

Instead of the traditional distinction between interpretation and
compilation, the distinction is made between full and partial evaluation.
These work as follows:

* With full evaluation, all code entered is compiled and then executed.
Hence the phrase A B ... Z is treated as if it had been entered

   :NONAME A B ... Z ; EXECUTE

in a Standard system. Code is executed as soon as it can be, so that for
example

   4 2 +

causes "4" then "2" then "+" to be executed, while

   TRUE IF 15  ELSE 14  THEN

causes "TRUE IF 15  ELSE 14  THEN" to be executed, as the control
structure can only be executed when it has been terminated (of course, if
we were being really clever, we could execute "15" as soon as we'd found
"TRUE IF" and simply discard "ELSE 14  THEN").

* Partial evaluation is the same, except that whenever a non-manifest
quantity is referred to evaluation stops and the code that has been
compiled so far is added to the dictionary plus the offending reference to
a non-manifest quantity. Partial evaluation then starts afresh. A
non-manifest quantity is one that is not known at compile-time.  This can
be:

   1. A stack location whose contents was not put there by the code being
      compiled
   2. A memory location whose contents was not stored by the code being
      compiled
   3. The result of an I/O operation

The final result of the partial evaluation is also compiled. Hence, the
phrase

   4 2 + SWAP  1 3 *

would cause the code "4" "2" and "+" to be executed, then "6 SWAP" to be
compiled when "SWAP" is found, which refers to a stack item not put there
by the code so far. Finally, "1" "3" and "*" are executed and "3" is
compiled. This is rather like the phrase

   [ 4 2 + ] LITERAL SWAP  [ 1 3 * ] LITERAL

in Standard Forth.

The simplest implementation of this evaluation mechanism is a table of
known memory locations. Whenever a location is stored to, it is added to
the table, or altered if it is already held there. This requires a few
words such as ! and +! to be trapped. Whenever a location is read (@ &c.)
the list is scanned to see if it is known. Whenever an unknown location is
read from, partial evaluation halts.

A special stack could also be used, with markers to represent unknown
quantities. Stack operators such as OVER and SWAP do not stop partial
evaluation; only words that use the value, such as +, cause it to halt. At
that point, code must be compiled to put the unknown quantities at the
correct positions on the stack. This requires far more words to be trapped
than simply memory references, but produces more efficient code,
especially on systems in which the stack's address is not fixed. 

: switches into partial evaluation mode, and ; into full evaluation mode.
Hence, the definition

   : FOO  4 2 + * ;

causes "6 *" to be compiled. In a Standard system we might have written

   : FOO [ 4 2 + ] LITERAL * ;

to get the same effect, but here we don't have to. The beauty of the new
system is that all values that can be reduced to literals are, without the
programmer having to specify them, and without compromising readability.
However, this is only the beginning: a word such as

   : FACT5*   1 5  BEGIN ?DUP WHILE  TUCK *  SWAP 1-  REPEAT  * ;

would be compiled as "120 *". Arbitrary control structures can be used
without the awkwardness or waste of memory of the usual circumlocution:

   : FACT5   1 5  BEGIN ?DUP WHILE  TUCK *  SWAP 1- REPEAT ;
   : FACT5*   [ FACT5 ] LITERAL * ;

Also, if the stack is modelled, many stack overheads disappear; the word

   'BUFFER COUNT

might be compiled as if written

   [ 'BUFFER CHAR+ ] LITERAL  [ 'BUFFER ] LITERAL C@

if COUNT were defined

   : COUNT   DUP CHAR+ SWAP C@ ;

In some ways, this is worse code, but if we take clever code generation a
stage further and assume we are generating native machine code, it is easy
to see how positions on the stack could be mapped on to machine registers
and quite efficient code generated.

Although partial evaluation does not give particularly good optimisation
for typical code which makes heavy use of non-manifest quantities, it
nevertheless performs some useful inter-word optimisations, and
additionally can be used to aid native code generation for register
machines, because it turns stack operations into memory stores. Also,
because it is so simple, uniform and low-level, it might be a good
mechanism for object-oriented systems, where different degrees of opacity
could be specified for partial evaluation. For example, method calls could
be treated as non-manifest to allow dynamic binding, or as manifest, so
that more efficient code could be generated.

Finally, introduced with care, it could be used in an ANS Standard system,
thanks to the Standard's being specified in semantic terms (though this
extensional approach grates with the simpler intensional stance of the
traditional Forth model, it is one of the Standard's greatest strengths). 
Such a compiler would extend the Standard, for example by allowing control
structures to be used in interpretive mode, without breaching it.

--{End of message}--

Heuristic for stopping unbounded code expansion: set some factor (e.g. 2)
above which code will not be expanded over the unevaluated version.

# Direct threading

Use 4-byte addresses, with the bottom two bits as follows:

00 - next word is code
01 - next word is data
10 - string follows (length in count byte)
11 - end of code (EXIT) or native code/data follows

Could use relative addressing.
