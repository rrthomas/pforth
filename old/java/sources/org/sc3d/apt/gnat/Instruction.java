package org.sc3d.apt.gnat;

/** Represents a Gnat instruction, before it has been through the JIT compiler. */
public abstract class Instruction {
  /** Don't want any subclasses except Java, B, If and Set. */
  private Instruction(int type) { this.type = type; }
  
  /** One of the 'TYPE_XXX' values. */
  public final int type;
  
  /** One of the values that the 'type' field can take. */
  public static final int
    TYPE_JAVA = 1,
    TYPE_B = 2,
    TYPE_IF = 3,
    TYPE_SET = 4;
  
  /** Returns a source-code representation of this Instruction, if possible. */
  public abstract String toString();
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** The subclass of Instruction that calls a Java method. */
  public static abstract class Java extends Instruction {
    /** Constructs a Java.
     * @param p an Operand representing the next Instruction to execute.
     */
    public Java(Operand p) {
      super(TYPE_JAVA);
      this.p = p;
    }
    
    /** The Operand. */
    public final Operand p;
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** The subclass of Instruction that represents a B instruction. A B instruction replaces the environment (sets the E register to a new value). */
  public static class B extends Instruction {
    /** Constructs a B.
     * @param e an Operand representing the new value of the E register.
     * @param p an Operand representing the next Instruction (the new value of the P register).
     */
    public B(Operand e, Operand p) {
      super(TYPE_B);
      this.e = e; this.p = p;
    }
    
    /** One of the Operands. */
    public final Operand e, p;
    
    /** Returns "B e" where "e" is the 'e' operand. */
    public String toString() { return "B "+this.e+" "+this.p; }
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** The subclass of Instruction that represents an IF instruction. An IF instruction evaluates a condition, and sets the program counter to one of two values depending on the outcome. */
  public static class If extends Instruction {
    /** Constructs an If.
     * @param c an Operand representing the condition.
     * @param t the 'then' clause: an Operand representing the value to write to the P register if the condition evaluates to anything but '0'.
     * @param e the 'else' clause: an Operand representing the value to write to the P register if the condition evaluates to '0'.
     */
    public If(Operand c, Operand t, Operand e) {
      super(TYPE_IF);
      this.c = c; this.t = t; this.e = e;
    }
    
    /** One of the Operands. */
    public final Operand c, t, e;
    
    /** Returns "IF c t e" where "c", "t" and "e" are the operands. */
    public String toString() { return "IF "+this.c+" "+this.t+" "+this.e; }
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** The subclass of Instruction that represents a SET instruction. A SET instruction stores a value in a hash-table at a key. */
  public static class Set extends Instruction {
    /** Constructs an Set.
     * @param h an Operand representing the hash-table.
     * @param k an Operand representing the key.
     * @param v an Operand representing the value.
     * @param p an Operand representing the next instruction to execute.
     */
    public Set(Operand h, Operand k, Operand v, Operand p) {
      super(TYPE_SET);
      this.h = h; this.k = k; this.v = v; this.p = p;
    }
    
    /** One of the Operands. */
    public final Operand h, k, v, p;
    
    /** Returns "SET h k v" where "h", "k" and "v" are the first three operands. */
    public String toString() { return "SET "+this.h+" "+this.k+" "+this.v; }
  }
  
  ////////////////////////////////////////////////////////////////////////////
}
