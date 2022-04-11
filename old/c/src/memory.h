#define zmalloc(size) \
  zrealloc(NULL, 0, (size))

void *zrealloc(void *ptr, size_t oldsize, size_t newsize);
