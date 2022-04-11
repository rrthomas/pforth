/* Vectors (auto-extending arrays) */

#include <stddef.h>
#include <stdint.h>
#include <stdlib.h>

#include "memory.h"
#include "vector.h"


/* Create a vector whose items' size is size */
Vector *
vec_new(size_t itemsize)
{
  Vector *v = zmalloc(sizeof(Vector));
  vec_itemsize(v) = itemsize;
  vec_items(v) = 0;
  v->size = 0;
  v->array = NULL;
  return v;
}

/* Free a vector v */
void
vec_free(Vector *v)
{
  free(v->array);
  free(v);
}

/* Resize a vector v to items elements */
static void
resize(Vector *v, size_t items)
{
  v->array = zrealloc(v->array, v->size * vec_itemsize(v), items * vec_itemsize(v));
  v->size = items;
}

/* Convert a vector to an array */
void *
vec_toarray(Vector *v) {
  void *a;
  resize(v, vec_items(v));
  a = v->array;
  free(v);
  return a;
}

/* Return the address of a vector element, growing the array if
   needed */
void *
vec_index(Vector *v, size_t idx)
{
  if (idx >= v->size)
    resize(v, idx >= v->size * 2 ? idx + 1 : v->size * 2);
  if (idx >= vec_items(v))
    vec_items(v) = idx + 1;
  return (void *)((uint8_t *)v->array + idx * vec_itemsize(v));
}
