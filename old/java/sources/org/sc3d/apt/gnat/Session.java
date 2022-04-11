package org.sc3d.apt.gnat;

/** Represents a compiler session. The purpose of a session is to count the number of unresolved branch instructions (including IFs and Bs) so as to know when they are all resolved. */
public class Session {
  /** Constructs a new Session with the counter set to '0'. */
  public Session() { this.count = 0; }
  
  /** The counter. */
  public int count;
  
  /** Returns a string of the form "Session[17]". */
  public String toString() { return "Session["+this.count+"]"; }
}
