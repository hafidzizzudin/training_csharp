﻿using EventDelegateExample;
using FirstLibrary.Testing;
using MethodExtensionExample;
using Newtonsoft.Json;
using System;
using System.Collections.Generic; // List
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FirstApp
{
  class Program
  {
    //[Conditional("DEBUG")]
    static void Main( string[] args )
    {
      var watch = new System.Diagnostics.Stopwatch();
      watch.Start();

      try
      {
        // 1. Value or Reference
        //ExCopyByValue();
        //ExCopyByReference();

        // 2. Nullable
        //NullableExample();

        // 3. String
        //ExString();

        // 4. Array
        //ExArray();

        // 5. Paramaters pass by ref
        //int passRef = 5;
        //ExPassByref(ref passRef); // Value type needs ref keyword to pass by ref
        //var test = new ParentClass( "Yanto" ); // Referenced type auto pass by ref
        //ExPassByRefNoKeyword( test );
        //Console.WriteLine( test.Name );

        // 6. Arguments in function
        //PrintArguments(x: 21);

        // 7. Attributes
        //Console.WriteLine("asdad");

        // 8. Properties
        //ExProperties();

        // 9. Generics (similar to template in C++)

        // 10. Loop
        //ExLoop();

        // 11. External library
        //ExLib();
        //ExLib("Test overloading");

        // 12. Object intializers
        //var newCar = new Car<int>{hidden = 2400, number:4};

        // 13. Partial class, must be on the same assembly
        //var newPartialClass = new PartialClass();

        // 14. Upcast n Downcast
        //ExInheritanceCasting();

        // 15. virtual, override, new (overloading, overriding, hiding)
        //ExVirtOverHide();

        //16. Interface and Abstract
        //ExInterfaceAbstract();

        //17. Threading, Some executions run concurently
        //ExThreading();

        //18. Async await
        //ExAsyncAwait();
        //Console.ReadKey();

        //19. Async await with Progress
        //ExAsyncAwaitProgress();
        //Console.ReadKey();

        //20. http request
        //DelegateTask task = ExHttpRequest;
        //task(); //version 1
        //task.Invoke(); //version 2
        //Console.ReadKey();

        //DelegateVoid t = WriteY;
        //21. Delegate
        //t();

        //22. Event and Delegate
        //ExEventDelegate();

        //23. LINQ
        //ExLINQ();
        //Console.ReadKey();

        //24. Method Extension Class
        //ExMethodExtensionClass();

        //25. Nameof syntax
        //ExNameOf();

        //26. Parsing Double
        //ExDoubleParsing();

        //27. File and Directory Path
        //ExFileDirectoryPath();

        //28. Get Class from String
        //ExGetClassFromString();

        // 29. Enum 
        //ExTestEnumFunc();

        // 30. Check Out
        //ExCheckOutParam();

        //31. Reverse search
        ExReverseSearchList();


      }
      catch( Exception e )
      {
        Console.WriteLine( "Error: " + e.Message );
      }

      watch.Stop();

      Console.WriteLine( $"\nTotal execution time: {watch.ElapsedMilliseconds}ms" );
    }

    private static void ExReverseSearchList()
    {
      var listTemp = new List<int>() { 1, 4, 5, 2, 5, 1, 2, 6, 1, 4 };
      var trueIdx = listTemp.FindLastIndex( ( val ) => val == 1 );
      var falseIdx = listTemp.FindLastIndex( ( val ) => val == 0 );
      Console.WriteLine( $"true {trueIdx}, false {falseIdx}" );
      var listAdvTemp = new List<List<int>>() { new List<int>() { 1, 5, 6 }, new List<int>() { 0, 5, 6 }, new List<int>() { 1, 0, 6 }, new List<int>() { 1, 2, 4 } };
      var trueIdxAdv = listAdvTemp.FindLastIndex( ( listInt ) => listInt.Contains( 0 ) );
      var falseIdxAdv = listAdvTemp.FindLastIndex( ( listInt ) => listInt.Contains( 8 ) );
      Console.WriteLine( $"true {trueIdxAdv}, false {falseIdxAdv}" );
    }

    private static void ExCheckOutParam()
    {
      int[] data = new int[] { 1, 2, 4, 5, 6 };
      var outTest = new OutToClass( data );
      foreach( var val in data )
      {
        Console.WriteLine( val );
      }

      outTest.StartChangeData();

      foreach( var val in data )
      {
        Console.WriteLine( val );
      }

    }

    private static void ExTestEnumFunc()
    {
      Console.WriteLine( "If int is not in enum, It will have type as enum but without any mapping\n" );

      TestEnum testSuccess = (TestEnum) 1;
      TestEnum testFail = (TestEnum) 3;
      int intSuccess = (int) testSuccess;
      int intFail = (int) testFail;

      Console.WriteLine( $"SUCCESS enum: {testSuccess}, int: {intSuccess}" );
      Console.WriteLine( $"FAILED  enum: {testFail}, int: {intFail}" );

      string error = "Error";
      Enum.TryParse( error, out TestEnum result );
      Console.WriteLine( $"SUCCESS enum: {result}, int: {(int) result}" );
      Enum.TryParse( "sadsa", out result );
      Console.WriteLine( $"FAILED  enum: {result}, int: {(int) result}" );

    }

    enum TestEnum
    {
      Info = 0,
      Warning = 1,
      Error = 2
    };

    private static void ExGetClassFromString()
    {
      string className = "EventClass";
      Type type = Type.GetType( className );
      object eventClass = Activator.CreateInstance( type );
      //Console.WriteLine( eventClass?.GetType() ?? "null value" );
    }

    private static void ExFileDirectoryPath()
    {
      string[] listdata = { "lalal", "dididi", "tototo" };
      Console.WriteLine( "test list data {0}", JsonConvert.SerializeObject( listdata ) );

      var currentEnvDirectory = Environment.CurrentDirectory;
      var currentDirDirectory = Directory.GetCurrentDirectory();
      var currentAsmEntryDirectory = Path.GetDirectoryName( Assembly.GetEntryAssembly().Location );
      var currentAsmExecDirectory = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location );

      Console.WriteLine( currentEnvDirectory );
      Console.WriteLine( currentDirDirectory );
      Console.WriteLine( currentAsmEntryDirectory );
      Console.WriteLine( currentAsmExecDirectory );

      var configFolderPath = Path.Combine( Directory.GetParent( currentEnvDirectory ).Parent.Parent.ToString(), "Configs" );
      Console.WriteLine( configFolderPath );
      var listFiles = Directory.GetFiles( configFolderPath );
      foreach( var file in listFiles )
      {
        Console.WriteLine( file );
        using( var streamReader = new StreamReader( file ) )
        {
          var content = streamReader.ReadToEnd();
          Console.WriteLine( content );
          Dictionary<string, dynamic> contentData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>( content );
          foreach( var entry in contentData )
          {
            Console.WriteLine( entry.Key + " " + entry.Value );
          }

          streamReader.Close();
          contentData[ "code" ] = 404;
          string json = JsonConvert.SerializeObject( contentData );
          File.WriteAllText( file, json );
        }
      }
    }

    private static void ExNameOf()
    {
      //throw new NotImplementedException();
      Console.WriteLine( $"{nameof( ExNameOf )} is called" );
    }

    private static void ExMethodExtensionClass()
    {
      Console.WriteLine( "===EXAMPLE CLASS EXTENSION===" );
      Console.WriteLine( new EventClass().AddHello() );
      Console.WriteLine();
    }

    private static async void ExLINQ()
    {
      Console.WriteLine( "===EXAMPLE LINQ===" );
      try
      {
        client.DefaultRequestHeaders.Add( "app-id", "5fe1440be2a1ce246c33047d" );

        var jsonString = await client.GetStringAsync( "https://dummyapi.io/data/api/user?limit=10" );

        Console.WriteLine( "Success" );

        QuickType.UserModelWrapper responseObj = JsonConvert.DeserializeObject<QuickType.UserModelWrapper>( jsonString );
        Console.WriteLine( responseObj.Limit );

        foreach( var user in responseObj.Data )
          user.PrintData();

        IEnumerable<string> listUser = responseObj.Data
          .Where( user => user.Title == "miss" )
          .OrderBy( user => user.FirstName ).ThenBy( user => user.LastName )
          .Select( user => user.FirstName + " " + user.LastName );

        Console.WriteLine( "\n\nFILTERED DATA" );
        foreach( var user in listUser )
          Console.WriteLine( user );
        ;
      }
      catch( Exception e )
      {
        Console.WriteLine( "Error: {0}", e.Message );
      }
      Console.WriteLine();
    }

    private static void ExEventDelegate()
    {
      Console.WriteLine( "===EXAMPLE EVENT DELEGATE===" );
      EventClass eventClass = new EventClass() { ClassName = "Beer Party" };

      eventClass.SpacePressed += Subscriber1;
      eventClass.SpacePressed += Subscriber2;
      eventClass.TestEventHandler += BoolSubscriber;
      eventClass.KeyboardInputHandler( "New message incoming" );
      Console.WriteLine();
    }

    private static void BoolSubscriber( object source, bool info )
    {
      Console.WriteLine( "Bool subsriber: bool info: {0}", info );
    }

    private static void Subscriber1( object source, CustomEventArgs e )
    {
      EventClass obj = (EventClass) source;
      Console.WriteLine( "First subscriber: event args {0}", e.Title );
    }

    private static void Subscriber2( object s, CustomEventArgs e )
    {
      var eventClass = s as EventClass;
      Console.WriteLine( "Second subscriber: class name {0}", eventClass.ClassName );
    }

    private delegate Task DelegateTask();

    private delegate void DelegateVoid();

    private static readonly HttpClient client = new HttpClient();

    private static async Task ExHttpRequest()
    {
      client.DefaultRequestHeaders.Add( "app-id", "5fe1440be2a1ce246c33047d" );

      var response = await client.GetAsync( "https://dummyapi.io/data/api/user?limit=10" );

      if( response.IsSuccessStatusCode )
      {
        Console.WriteLine( "Success" );
        var jsonString = await response.Content.ReadAsStringAsync();

        QuickType.UserModelWrapper responseObj = JsonConvert.DeserializeObject<QuickType.UserModelWrapper>( jsonString );
        Console.WriteLine( responseObj.Limit );

        foreach( var user in responseObj.Data )
          user.PrintData();

      }
      else
        Console.WriteLine( "Error : {0}", (int) response.StatusCode );
    }

    private static async Task ExAsyncAwaitProgress()
    {
      long jj = 0;
      void progres( long i )
      { if( i != jj ) { Console.WriteLine( i + "%" ); jj = i; } };

      await Foo( progres );
    }

    private static Task Foo( Action<long> progressChanged )
    {
      return Task.Run( () =>
      {
        for( long i = 0; i <= 10000000000; i++ )
        {
          if( i % 100000000 == 0 )
            progressChanged( i / 100000000 );
        }
      } );
    }

    private static async Task<string> ExAsyncAwait()
    {
      //Task<int> task = Task.Run( async () => { await Task.Delay( 2000 ); Console.WriteLine( "Async done!!" ); return 1; } );
      Task<string> msg = GetMessageAsync( "Test get msg" );

      Task<A> testTask = Task.Run( async () => { await Task.Delay( 3000 ); return new A(); } );

      Console.WriteLine( "Main flow!" );

      var timeOutFunc = Task.Run( async () => await Task.Delay( 5000 ) );

      if( await Task.WhenAny( msg, testTask, timeOutFunc ) != timeOutFunc )
      {
        string msgRes = await msg;

        Console.WriteLine( "GetMessage done" );
        Console.WriteLine( msgRes );

        A a = await testTask;
        a.Test();

        return await msg;
      }
      else
      {
        Console.WriteLine( "Time out" );
        return null;
      }

      //Set timeout
      //if( !task.Wait( TimeSpan.FromMilliseconds( 1000 ) ) )
      //  Console.WriteLine( "Time out" );



    }

    private static async Task<string> GetMessageAsync( string msg )
    {
      await Task.Delay( 2000 );


      return msg;
    }

    private static void ExThreading()
    {
      Thread thread = new Thread( WriteY );
      thread.Start();
      //WriteY();

      for( int i = 0; i < 1e6; i++ )
      {
        Console.Write( "O" );
      }

      thread.Join();
    }

    private static void WriteY()
    {
      for( int i = 0; i < 1e6; i++ )
      {
        Console.Write( "X" );
      }
    }

    private static void ExInterfaceAbstract()
    {
      var impClass = new ImplementationAbstract
      {
        Counter = 200
      };
      impClass.ShowFoo();
    }

    static private void ExVirtOverHide()
    {
      var a = new A();
      var b = new B();
      var c = new C();

      //Overriding and Hiding have same result
      a.Test();
      b.Test();
      c.Test();

      Console.WriteLine();
      //Overriding and Hiding have different result
      a = c as A;
      b = c as B; // Hiding means that subclass component is hidden, base class implemented instead
      a.Test();
      b.Test();   // Hiding : B::Test()     Overriding: C::Test()
      c.Test();   //          C::Test()                 C::Test()
    }

    static private void ExInheritanceCasting()
    {
      // Upcast
      var newChildClass = new ChildClass( 20, "tony" );
      GrandChildClass newGrandChildClass = new GrandChildClass( 52, "Lala", "Berkeley" );
      var newParentClass = newChildClass as ParentClass;
      newParentClass.DisplayInParent();

      ChildClass newChildClass2 = newParentClass as ChildClass;
      newChildClass2.DisplayInChild();

      newGrandChildClass.DisplayInGrandChild();
      newGrandChildClass.DisplayInChild();
      newGrandChildClass.DisplayInParent();

      // Downcast
      var newParentClass2 = new ParentClass( "papa" );
      ChildClass newChild = newParentClass2 as ChildClass;
      //newChild.DisplayInParent(); // Failed due to not parent obj
      //newChild.DisplayInChild(); // Failed due to data lost
    }

    private static void ExLib() => Mathe.Multiply();

    private static void ExLib( string msg ) => Console.WriteLine( msg );

    private static void ExLoop()
    {
      foreach( int val in new int[] { 1, 2, 3, 4 } )
      { Console.WriteLine( val ); }

      int varGoto = 0;
      labelGoto:
      Console.WriteLine( varGoto++ );
      if( varGoto < 6 )
        goto labelGoto;

      varGoto = 0;
      for(; ; )
        if( varGoto < 3 )
          Console.WriteLine( ++varGoto );
        else
          break;
    }

    static private void ExProperties()
    {
      var newCar = new Car<int>( 52 );
      Console.WriteLine( newCar.HiddenProp );
      newCar.HiddenProp = 42;
      Console.WriteLine( newCar.HiddenProp );
    }

    static private void PrintArguments( int x, int y = 100, params int[] args )
    {
      Console.WriteLine( $"{x} and {y}" );
      foreach( var data in args )
        Console.WriteLine( data );
    }

    static private void ExPassByRefNoKeyword( dynamic refin )
    {
      refin.Name = "Herry";
    }

    static private void ExPassByref( ref int refin )
    {
      refin = 42;
    }

    static private void ExArray()
    {
      int sizeOfArr = Convert.ToInt32( Console.ReadLine() );
      int[] intArr = new int[ sizeOfArr ];

      for( int i = 0; i < intArr.Length; i++ )
      {
        intArr[ 0 ] = 1;
        intArr[ 1 ] = 24;
        intArr[ 2 ] = 56;
      }
      foreach( var data in intArr )
        Console.WriteLine( data );

      List<int> intList = new List<int>();
      intList.InsertRange( 0, intArr );

      foreach( var intData in intList )
        Console.WriteLine( intData );
    }

    static private void ExString()
    {
      int age = 45;
      Console.WriteLine( $"Interpolated {age}" );
      Console.WriteLine( "String Format {0}", age );
    }

    static private void NullableExample()
    {
      int? nullInt = null;
      //int nullInt2 = null; Illegal
      Console.WriteLine( nullInt?.ToString() ?? "data is null" );
      Car<int> a = new Car<int>( 7 ), b = a;
      Console.WriteLine( a.Number );
      Console.WriteLine( b.Number );
      b = null;
      Console.WriteLine( b?.Number.ToString() ?? "car is null" );
    }

    static private void ExDoubleParsing()
    {
      string[] values = { "1,643.57", "$1,643.57", "-1.643e6",
                          "-168934617882109132", "123AE6",
                          null, string.Empty, "ABCDEF" };

      NumberFormatInfo provider = new NumberFormatInfo
      {
        NumberDecimalSeparator = ".",
        NumberGroupSeparator = ","
      };

      foreach( var value in values )
      {
        if( double.TryParse( value, NumberStyles.Any, provider, out double number ) )
          Console.WriteLine( "'{0}' --> {1}", value, number );
        else
          Console.WriteLine( "Unable to parse '{0}' --> {1}", value, number );
      }
    }

    private string GetName( string name )
    {
      return Number( "4125" ).ToString();
      //return string.Format("My name is {0}, with number {1}", name == null ? "default" : name, number());
    }

    private int Number()
    {
      return 5;
    }

    private int Number( string strConverted )
    {
      return Convert.ToInt32( strConverted );
    }

    static private void ExCopyByValue()
    {
      int a = 7, b = a;
      Console.WriteLine( "a {0}", a );
      Console.WriteLine( "b {0}", b );
      a = 9;
      Console.WriteLine( "a {0}", a );
      Console.WriteLine( "b {0}", b );
    }

    static private void ExCopyByReference()
    {
      Car<int> a = new Car<int>( 12 ), b = a;
      Console.WriteLine( "a {0}", a.Number );
      Console.WriteLine( "b {0}", b.Number );
      a.Number = 9;
      Console.WriteLine( "a {0}", a.Number );
      Console.WriteLine( "b {0}", b.Number );
    }
  }

  class Car<T>
  {
    private int hiddenProp = 12;

    public int HiddenProp { set => hiddenProp = 15; get => hiddenProp; }
    public int Number { set; get; }

    public Car( int number ) => Number = number;
    public Car( int number, int hidden ) : this( number ) => HiddenProp = hidden;
  }

  partial class PartialClass
  {
    private void CallingClass() => Console.WriteLine( "Calling partial class" );
  }
}