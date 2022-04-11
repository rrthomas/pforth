import sys
print (sys.path)

from pymeta.grammar import OMeta

grammar = """
number:n = -> (n)
"""

code = """42"""

parser = OMeta.makeGrammar(grammar, {})
print(parser(code).apply("number"))
