/* Vectors (auto-extending arrays) */

/* Unitialised vector elements are zeroed */

#ifndef VECTOR_H
#define VECTOR_H

#include <stddef.h>

typedef struct {
  size_t itemsize;              /* size of each item in bytes */
  size_t items;                 /* number of items used */
  size_t size;                  /* number of items available */
  void *array;                  /* the array of contents */
} Vector;

Vector *vec_new(size_t itemsize);
void vec_free(Vector *v);
void *vec_toarray(Vector *v);
void *vec_index(Vector *v, size_t idx);

#define vec_itemsize(v) (v)->itemsize
#define vec_items(v)    (v)->items


/* Use macros so that memcpy is inlined */

#define vec_get(v, idx, res) \
   memcpy((res), vec_index((v), (idx)), sizeof(res))

#define vec_set(v, idx, val) \
   memcpy(vec_index((v), (idx)), (val), sizeof(val))


#endif
