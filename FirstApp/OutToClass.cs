using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp
{
  public class OutToClass
  {
    private int[] data;

    public OutToClass( int[] data )
    {
      this.data = data;
    }
    public void StartChangeData()
    {
      ChangeData( data, 200 );
    }
    public void ChangeData( int[] val, int newval )
    {
      for( int ii = 0; ii < val.Length; ii++ )
        val[ ii ] = newval;
    }
  }

  public class Parent
  {
    public Child1 ChildName { get; set; }
    public Child2 ChildAge { get; set; }
  }
  public class Child1
  {
    public string Name { get; set; }
  }

  public class Child2
  {
    public double Age { get; set; }
  }
}
