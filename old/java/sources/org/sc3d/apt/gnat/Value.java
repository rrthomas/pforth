package org.sc3d.apt.gnat;

import java.util.*;

public abstract class Value {
  /** Don't want any subclasses except Int, Str, Hash, Code and Ext. */
  private Value(int type) { this.type = type; }
  
  /** The type of this Value, which must be one of the 'TYPE_XXX' values. */
  public final int type;
  
  /** One of the values that the 'type' field can take. */
  public static final int
    TYPE_INT = 1,
    TYPE_STR = 2,
    TYPE_HASH = 3,
    TYPE_CODE = 4,
    TYPE_EXT = 5;
  
  /** Returns a source-code representation of this Value, if possible. */
  public abstract String toString();
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** The subclass of Value that represents an integer. */
  public static class Int extends Value {
    /** Constructs an Int. */
    public Int(int value) {
      super(TYPE_INT);
      this.value = value;
    }
    
    /** The Java int that stores the value of this Int. */
    public final int value;
    
    public String toString() { return ""+this.value; }
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** The subclass of Value that represents a string. */
  public static class Str extends Value {
    /** Constructs a Str. */
    public Str(String value) {
      super(TYPE_STR);
      this.value = value;
    }
    
    /** The Java String that stores the value of this Int. */
    public final String value;
    
    public String toString() { return '"'+this.value+'"'; }
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** The subclass of Value that represents a hash. */
  public static class Hash extends Value {
    /** Constructs a Hash. */
    public Hash() {
      super(TYPE_HASH);
      this.value = new HashMap();
    }
    
    /** The Java Map that stores the value of this Int. */
    public final HashMap value;
    
    public String toString() {
      final StringBuffer sb = new StringBuffer('{');
      final Iterator it = this.value.keySet().iterator();
      if (it.hasNext()) while (true) {
        final Value key = (Value)it.next();
        final Value value = (Value)this.value.get(key);
        sb.append(key).append("=").append(value);
        if (!it.hasNext()) break;
        sb.append(", ");
      }
      return sb.append('}').toString();
    }
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** The subclass of Value that represents executable code, after it has gone through the JIT compiler. This is an abstract superclass: subclasses can define their execution behaviour by overriding the 'execute()' method. */
  public static abstract class Code extends Value {
    /** Constructs a Code. */
    public Code() { super(TYPE_CODE); }
    
    /** Executes this Code value.
     * @param state the state of the virtual machine (updated). This includes the register P. Call this method when P is a reference to this Value.Code. This method changes the value of P to point to the next Value.Code.
     */
    public abstract void execute(State state);
  }
  
  ////////////////////////////////////////////////////////////////////////////
  
  /** The subclass of Value that represents an opaque value that can only be manipulated by libraries not written in Gnat. */
  public static class Ext extends Value {
    /** Constructs an Ext given its value. */
    public Ext(Object value) {
      super(TYPE_EXT);
      this.value = value;
    }
    
    /** The Java object that represents the value of this Ext. */
    public final Object value;
    
    /** Returns the string "(opaque value)". */
    public String toString() { return "(opaque value)"; }
  }
  
  ////////////////////////////////////////////////////////////////////////////
}
