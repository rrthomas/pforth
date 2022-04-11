/* Gnat: a tiny language
   (c) Reuben Thomas 2004 */

#include <slack/config.h>
#include <slack/std.h>
#include <slack/map.h>
#include <slack/list.h>


/* Primitive types */
typedef ptrdiff_t GInt;
typedef char GChar;

/* Predeclare structs as typedefs */
typedef struct GState GState;
typedef struct GCode GCode;
typedef struct GObj GObj;
typedef struct GString GString;

/* Action routine */
typedef void (GAction)(GState *state);

/* Code block */
struct GCode {
  size_t len;
  GAction **code;
};

/* String */
struct GString {
  size_t len;
  GChar *p;
};

/* Built-in object types */
typedef enum {
  GTyCode, GTyInt, GTyString,
} GType;

/* Data object */
struct GObj {
  GType ty;
  union {
    GCode *code;
    GInt intg;
    GString str;
  } val;
};

/* Execution state */
struct GState {
  GAction **pc; /* pointer to next code word to execute */
  Map *env;     /* code block environment */
  List *dstack; /* data stack */
};

#define align(pc) \
  (GAction **)((size_t)((uint8_t *)(pc) + sizeof(size_t) - 1) \
               & ~(sizeof(size_t) - 1))
