package org.sc3d.apt.gnat;

/** Represents the state of the Gnat virtual machine, including the values of all the registers. Instances are mutable, and the values of the registers change during execution. */
public class State {
  /** Constructs a new State, given values for its fields. */
  public State(Value.Hash e, Value.Code p) {
    this.e = e; this.p = p;
  }
  
  /** The E register. 'E' stands for 'environment'. The value of the E register is a Value.Hash used to store local variables. */
  public Value.Hash e;
  
  /** The P register. 'P' stands for 'program'. The value of the P register is a Value.Code representing the next instruction to execute. */
  public Value.Code p;
  
  /** Executes some instructions, updating the values of the registers.
   * @param count the number of instructions to execute.
   */
  public void execute(int count) {
    while (--count>=0) this.p.execute(this);
  }
}
