/* Auto-extending buffers */

#ifndef BUFFER_H
#define BUFFER_H

#include <stddef.h>
#include <stdint.h>

#include "vector.h"

typedef Vector Buffer;

Buffer *buf_new(void);
void buf_free(Buffer *b);
uint8_t *buf_toarray(Buffer *b);
void buf_addblk(Buffer *b, size_t n, const uint8_t *d);
size_t buf_used(Buffer *b);
void buf_align(Buffer *b, size_t n);

/* Add an object d of type t to a buffer b */
#define buf_add(b, t, d) \
  (void)vec_index((Vector *)b, buf_used(b) + sizeof(t) - 1), \
  *(t *)vec_index((Vector *)b, buf_used(b) - sizeof(t)) = (d)


#endif
