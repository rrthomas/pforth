/* Gnat: a tiny language
   (c) Reuben Thomas 2004 */

/* TODO: Plug leaks with valgrind */

#include <assert.h>
#include <stdio.h>
#include <errno.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

#include <memory.h>
#include <buffer.h>
#include <slack/config.h>
#include <slack/std.h>
#include <slack/map.h>
#include <slack/list.h>
#include <stream.h>

#include "gnat.h"


char *progname = "gnat";


/* Make a variadic wrapper for an exception routine */
#define unvify(vf, f) \
  void\
  f (const char *fmt, ...) \
  { \
    va_list ap; \
    va_start(ap, fmt); \
    vf(fmt, ap); \
    va_end(ap); \
  }

/* Write a warning to stderr of the form:

   progname:exc_file:exc_pos other arguments\n

   progname and exc_file are not displayed if they are NULL, and
   exc_pos isn't displayed if it is 0. */
void
vwarn(const char *fmt, va_list arg)
{
  if (progname)
    fprintf(stderr, "%s:", progname);
  if (progname)
    putc(' ', stderr);
  vfprintf(stderr, fmt, arg);
  va_end(arg);
  putc('\n', stderr);
}
unvify(vwarn, warn)

/* Like vwarn, but calls exit() after the warning message has been
   written. */
void
vdie(const char *fmt, va_list arg)
{
  vwarn(fmt, arg);
  exit(EXIT_FAILURE);
}
unvify(vdie, die)

/*
 * Resize an allocated memory area. Bytes that are no longer needed are zeroed
 * before being freed. Bytes that are newly needed are zeroed after being
 * allocated.
 */
void *zrealloc(void *ptr, size_t oldsize, size_t newsize)
{
  if (newsize < oldsize)
    memset((char *)ptr + newsize, 0, oldsize - newsize);

  // Behaviour in this case is unspecified for malloc and GC_REALLOC
  if (newsize == 0)
    return NULL;

  ptr = realloc(ptr, newsize);

  if (ptr == NULL)
    exit(2);

  if (newsize > oldsize)
    memset((char *)ptr + oldsize, 0, newsize - oldsize);

  return ptr;
}


/* Stack actions */

static void
gnat_dpush(GState *state, GObj obj)
{
  GObj *p = zmalloc(sizeof(GObj));
  *p = obj;
  list_prepend(state->dstack, p);
}

static GObj
gnat_dpop(GState *state)
{
  GObj *p, obj;
  assert(list_length(state->dstack) > 0);
  p = list_shift(state->dstack);
  obj = *p;
  free(p);
  return obj;
}

static GObj
gnat_dpopty(GState *state, GType type)
{
  GObj obj = gnat_dpop(state);
  assert(obj.ty == type);
  return obj;
}


/* Actions */

static void
gdo(GState *state)
{
  GObj code = gnat_dpopty(state, GTyCode);
  GAction *act, **savepc = state->pc;
  Map *saveenv = state->env;
  state->pc = code.val.code->code;
  state->env = map_create(NULL);
  map_put(state->env, "_up", saveenv);
  while ((act = *state->pc++))
    act(state);
  state->pc = savepc;
  map_destroy(&state->env);
  state->env = saveenv;
  free(code.val.code->code);
  free(code.val.code);
}

static void
gif(GState *state)
{
  GObj gelse = gnat_dpopty(state, GTyCode);
  GObj gthen = gnat_dpopty(state, GTyCode);
  GObj cond = gnat_dpopty(state, GTyInt);
  if (cond.val.intg)
    gnat_dpush(state, gthen);
  else
    gnat_dpush(state, gelse);
  gdo(state);
}

static void
fetch(GState *state)
{
  GObj name = gnat_dpopty(state, GTyString);
  GObj val;
  Map *env;
  int noval = 1, noup = 0;
  for (env = state->env; noval != 0 && noup == 0;
       (noup = map_get(env, "_up") == NULL))
    noval = map_get(env, name.val.str.p) == NULL;
  assert(!noval);
  gnat_dpush(state, val);
}

static void
store(GState *state)
{
  GObj name = gnat_dpopty(state, GTyString);
  GObj *valp = zmalloc(sizeof(GObj));
  *valp = gnat_dpop(state);
  map_put(state->env, name.val.str.p, valp);
}

static void
drop(GState *state)
{
  gnat_dpop(state);
}

#define dyad(name, op) \
  static void \
  name(GState *state) \
  { \
    GObj res; \
    GObj num2 = gnat_dpopty(state, GTyInt); \
    GObj num1 = gnat_dpopty(state, GTyInt); \
    res.ty = GTyInt; \
    res.val.intg = num1.val.intg op num2.val.intg; \
    gnat_dpush(state, res); \
  }

dyad(plus, +)
dyad(times, *)
dyad(eq, ==)
dyad(gt, >)

static void
print(GState *state)
{
  GObj obj = gnat_dpop(state);
  switch (obj.ty) {
  case GTyCode:
    printf("code %p\n", obj.val.code);
    break;
  case GTyInt:
    printf("%d\n", obj.val.intg);
    break;
  case GTyString:
    printf("%.*s\n", obj.val.str.len, obj.val.str.p);
    break;
  default:
    /* TODO That this case works is a kludge based on the fact that
       the ty field of GObj is the first field, and is a small
       integer */
    printf("primitive %d\n", obj.ty);
  }
}

static void
pushnum(GState *state)
{
  const GInt num = (GInt)(*state->pc++);
  GObj obj;
  obj.ty = GTyInt;
  obj.val.intg = num;
  gnat_dpush(state, obj);
}

static void
pushstr(GState *state)
{
  size_t len = (size_t)(*state->pc++);
  char *str = (char *)(state->pc);
  GObj obj;
  state->pc = align(str + len + 1);
  obj.ty = GTyString;
  obj.val.str.p = str;
  obj.val.str.len = len;
  gnat_dpush(state, obj);
}

static void
pushcode(GState *state)
{
  GCode *code = (GCode *)(*state->pc++);
  GObj obj;
  obj.ty = GTyCode;
  obj.val.code = code;
  gnat_dpush(state, obj);
}


/* Compiler */

static void
compnum(Buffer *buf, GInt num)
{
  buf_add(buf, GAction *, pushnum);
  buf_add(buf, GInt, num);
}

static void
compstr(Buffer *buf, char *str, size_t len)
{
  buf_add(buf, GAction *, pushstr);
  buf_add(buf, size_t, len);
  buf_addblk(buf, len, (unsigned char *)str);
  buf_add(buf, char, '\0');
  buf_align(buf, sizeof(GAction *));
}

static char **
compile(Map *env, char **token, GCode **ret)
{
  Buffer *buf;
  char *sym;
  buf = buf_new();
  for (sym = *token; sym; sym = *++token) {
    GAction *act;
    GInt num;
    char *endp;
    int len = strlen(sym);
    if (strcmp(sym, "{") == 0) {
      GCode *code;
      token = compile(env, ++token, &code);
      buf_add(buf, GAction *, pushcode);
      buf_add(buf, GCode *, code);
    } else if (strcmp(sym, "}") == 0)
      break;
    else if ((act = (GAction *)map_get(env, sym)))
      buf_add(buf, GAction *, act);
    else if (errno = 0, (num = strtol(sym, &endp, 10)),
             *endp == '\0' && errno == 0)
      compnum(buf, num);
    else if (*sym == '"' && sym[len - 1] == '"')
      compstr(buf, (GChar *)(sym + 1), len - 2);
  }
  buf_add(buf, GAction *, NULL);
  *ret = zmalloc(sizeof(GCode));
  (*ret)->len = buf_used(buf) / sizeof(GAction *);
  (*ret)->code = (GAction **)buf_toarray(buf);
  return token;
}


/* Main program */

static Map *
initrootenv(void)
{
  Map *root = map_create(NULL);
  map_put(root, "do", gdo);
  map_put(root, "if", gif);
  map_put(root, "@", fetch);
  map_put(root, "!", store);
  map_put(root, "drop", drop);
  map_put(root, "+", plus);
  map_put(root, "*", times);
  map_put(root, "=", eq);
  map_put(root, ">", gt);
  map_put(root, "print", print);
  return root;
}

/* TODO when saving precede the actual code by a list of identifiers
   from the root environment; the position in the list gives the
   number of the corresponding token, which should be used instead of
   code addresses */
void
save(const char *file, GCode *code)
{
  FILE *fh;
  fh = fopen(file, "wb");
  if (fh == NULL)
    die("could not open `%s': %s", file, strerror(errno));
  fwrite(code->code, sizeof(GAction), code->len, fh);
  fclose(fh);
  if (ferror(fh))
    die("error saving `%s': %s", file, strerror(errno));
}

GState *
gnat_new(Map *env)
{
  GState *state = zmalloc(sizeof(GState));
  state->dstack = list_create(NULL);
  state->env = env;
  return state;
}

void
gnat_free(GState *state)
{
  list_destroy(&state->dstack);
  free(state);
}

static void
run(GState *state, GCode *code)
{
  GObj obj;
  obj.ty = GTyCode;
  obj.val.code = code;
  gnat_dpush(state, obj);
  gdo(state);
}

/* Discard leading white space and comments */
static char *
skipspace(char *p)
{
  while (*p && (isspace(*p) || *p == '#')) {
    if (*p == '#')
      do p++;
      while (*p && *p != '\n');
    p++;
  }
  return p;
}

/* Read next token consisting graphic characters, advancing p past it,
   and discarding leading white space characters and comments; return
   pointer to token in *tok, length in *len, and new value of p as
   return value */
static char *
gettok(char *p, char **tok, int *len)
{
  p = skipspace(p);
  *tok = p;
  while (isgraph(*p))
    p++;
  *p = '\0';
  *len = p++ - *tok;
  return p;
}

int
main(void)
{
  Map *root = initrootenv();
  GState *state = gnat_new(root);

  while (!feof(stdin)) {
    char *line = getln(stdin);
    GCode *code;
    char *token[256];
    int i = 0;
    char *p = line;
    int linelen = strlen(line);
    while (p < line + linelen) {
      char *tok;
      int toklen;
      p = gettok(p, &tok, &toklen);
      if (toklen > 0) {
        tok[toklen] = '\0';
        token[i++] = tok;
      }
    }
    token[i] = NULL;
    if (*compile(root, token, &code) != '\0')
      die("extraneous tokens at end of line");
    free(line);
    run(state, code);
  }

  gnat_free(state);
  map_destroy(&root);
  return 0;
}
