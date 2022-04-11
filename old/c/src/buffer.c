/* Auto-extending buffers */

#include <stdint.h>
#include <string.h>

#include "memory.h"
#include "vector.h"
#include "buffer.h"


/* Create a buffer */
Buffer *
buf_new(void)
{
  return (Buffer *)vec_new(sizeof(uint8_t));
}

/* Free a buffer b */
void
buf_free(Buffer *b)
{
  vec_free((Vector *)b);
}

/* Convert a buffer to a byte array */
uint8_t *
buf_toarray(Buffer *b)
{
  return (uint8_t *)vec_toarray((Vector *)b);
}

/* Add an n-byte block of data d of to a buffer b */
void
buf_addblk(Buffer *b, size_t n, const uint8_t *d)
{
  vec_index((Vector *)b, buf_used(b) + n - 1);
  memcpy(vec_index((Vector *)b, buf_used(b) - n), d, n);
}

/* Return number of bytes used in the buffer */
size_t
buf_used(Buffer *b)
{
  return vec_items((Vector *)b);
}

/* Align buffer buf_used(b) to the nearest n bytes (n a power of 2) */
void
buf_align(Buffer *b, size_t n)
{
  vec_index((Vector *)b, ((buf_used(b) + (n) - 1) & ~((n) - 1)) - 1);
}
