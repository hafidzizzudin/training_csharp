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
}
