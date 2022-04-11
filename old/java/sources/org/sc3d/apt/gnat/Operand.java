package org.sc3d.apt.gnat;

/** Represents an operand of a Gnat instruction, before it has gone through the JIT compiler. */
public abstract class Operand {
  /** Don't want any subclasses except 'P', 'E', 'Imm', 'Code', 'New', 'Get'. */
  private Operand(int type, Session session) {
    this.type = type;
    this.session = session;
  }
  
  /** One of the 'TYPE_XXX' values. */
  public final int type;
  
  /** One of the possible values of the 'type' field. */
  public static final int
    TYPE_P = 1,
    TYPE_E = 2,
    TYPE_IMM = 3,
    TYPE_CODE = 4,
    TYPE_NEW = 5,
    TYPE_GET = 6;
  
  /** The compiler Session during which this Operand was compiled. This field can be null if this Operand does not contain any unresolved code constants. */
  public final Session session;
  
  /** Returns a source-code representation of this Operand, if possible. */
  public abstract String toString();
  
  ////////////////////////////////////////////////////////////////////////////

  /** The current value of the program counter register. */
  public static class P extends Operand {
    /** Constructs a P. */
    public P() { super(TYPE_P, null); }
    
    /** Returns "P". */
    public String toString() { return "P"; }
  }  
  
  ////////////////////////////////////////////////////////////////////////////

  /** The current value of the environment register. */
  public static class E extends Operand {
    /** Constructs a E. */
    public E() { super(TYPE_E, null); }
    
    /** Returns "E". */
    public String toString() { return "E"; }
  }
  
  ////////////////////////////////////////////////////////////////////////////

  /** An immediate constant of type INT or STR. */
  public static class Imm extends Operand {
    /** Constructs an Imm, given its integer value. */
    public Imm(int value) {
      super(TYPE_IMM, null);
      this.value = new Value.Int(value);
    }
    
    /** Constructs an Imm, given its string value. */
    public Imm(String value) {
      super(TYPE_IMM, null);
      this.value = new Value.Str(value);
    }
    
    /** The value. */
    public final Value value;
    
    /** Returns 'value.toString()'. */
    public String toString() { return this.value.toString(); }
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** An immediate constant of type Code. Such a value is initially unresolved, meaning that its value is not at first defined. Define the value later by calling the 'resolve()' method exactly once. This facility is provided to enable you to construct loops and other cyclic structures. */
  public static class Code extends Operand {
    /** Constructs an unresolved Code.
     * @param session a Session used to count how many Codes need to be resolved.
     */
    public Code(Session session) {
      super(TYPE_CODE, session);
      this.value = null;
      this.session.count++;
    }
    
    /** Returns the value of this Code constant, or 'null' if it has not yet been resolved. */
    public Instruction get() { return this.value; }
    
    /** Resolves this Code constant. This method must be called exactly once. */
    public void resolve(Instruction value) {
      if (this.value!=null)
        throw new IllegalArgumentException("Already resolved");
      // TODO: Check that the Session is the same as 'this.session'.
      this.value = value;
      this.session.count--;
    }
    
    /** Returns the string "(code)". */
    public String toString() { return "(code)"; }
    
    /** The private copy of the value. Outsiders can access this only via the 'get()' and 'resolve()' methods. */
    private Instruction value;
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** A fresh empty hash. */
  public static class New extends Operand {
    /** Constructs a New. */
    public New() { super(TYPE_NEW, null); }
    
    /** Returns "NEW". */
    public String toString() { return "NEW"; }
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** A value read from a hashtable. The key and hashtable are themselves Operands. */
  public static class Get extends Operand {
    /** Constructs a Get. The key and hash must belong to the same Session, if they belong to a Session at all.
     * @param h the hash.
     * @param k the key.
     */
    public Get(Operand h, Operand k) {
      super(TYPE_GET, mergeSessions(h.session, k.session));
      this.h = h; this.k = k;
    }
    
    /** The Operand that represents the hash-table. */
    public final Operand h;
    
    /** The operand that represents the key. */
    public final Operand k;
    
    /** Returns a string of the form "h[k]" where "h" and "k" are the operands. */
    public String toString() {
      return this.h+"["+this.k+"]";
    }
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /* Private. */
  
  /** Checks that 'x' and 'y' are the same Session, but permits 'null' as a wild-card. Returns the Session, or throws an IllegalArgumentException. */
  private static Session mergeSessions(Session x, Session y) {
    if (x==null) return y;
    if (y==null) return x;
    if (x!=y) throw new IllegalArgumentException();
    return x;
  }
}
