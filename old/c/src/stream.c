/* Stream utilities */

#include <stdio.h>

#include "memory.h"
#include "stream.h"


char *
getln(FILE *fp)
{
  size_t len = 256;
  int c;
  char *l = zmalloc(len), *s = l;

  for (c = getc(fp); c != '\n' && c != EOF; c = getc(fp)) {
    if (s == l + len) {
      l = zrealloc(l, len, len * 2);
      len *= 2;
    }
    *s++ = c;
  }
  if (s == l + len)
    l = zrealloc(l, len, len + 1);
  *s++ = '\0';

  return zrealloc(l, len, s - l);
}
