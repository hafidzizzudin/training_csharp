using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EventDelegateExample
{
  public class CustomEventArgs : EventArgs
  {
    public string Title { get; set; }
  }

  public class EventClass
  {
    // 1. create delegate
    // 2. create event
    // 3. raise event
    //public delegate void BaseEvenDelegate( object source, EventArgs eventArgs ); // default
    public delegate void BaseEventDelegate( object source, CustomEventArgs eventArgs ); // custom eventArgs version 1
    public event BaseEventDelegate SpacePressed;
    public event EventHandler<bool> TestEventHandler;

    //public event EventHandler<CustomEventArgs> SpacePressed; // custom eventArgs version 2

    public string ClassName { get; set; } = "Default value class name";

    public void KeyboardInputHandler( string msg )
    {
      Console.WriteLine( "On process..." );
      Thread.Sleep( 3000 );

      OnSpacePressed( msg );
    }

    protected virtual void OnSpacePressed( string msg )
    {
      SpacePressed?.Invoke( this, new CustomEventArgs() { Title = msg } );
      TestEventHandler(this, true);
    }
  }
}

namespace MethodExtensionExample
{
  using EventDelegateExample;
  public static class ExtendEventDelegateClass
  {
    public static string AddHello( this EventClass eventClass )
    {
      return eventClass.ClassName + " Hello!!!";
    }
  }
}
