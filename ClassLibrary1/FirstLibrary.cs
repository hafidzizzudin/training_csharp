using System;

namespace FirstLibrary.Testing
{
  public static class Mathe
  {
    static public void Multiply()
    {
      Console.WriteLine( "Run multiply changed" );
      //throw new Exception("Test custom exception");
    }
  }

  public class ParentClass
  {
    public string Name { set; get; }

    public ParentClass() => Name = "";
    public ParentClass( string name ) => Name = name;

    public void CheckWhichChild()
    {
      if( !( this is SecondChildClass ) )
        Console.WriteLine( $"I am other child, which is {GetType().Name}" );
      else
        Console.WriteLine( $"I am {nameof( SecondChildClass )}" );
    }

    public void DisplayInParent()
    {
      Console.WriteLine( $"Parent name: {Name}" );
    }
  }

  public class SecondChildClass : ParentClass
  {

  }

  public class ChildClass : ParentClass
  {
    public int Age { set; get; }

    public ChildClass() => Age = 0;

    public ChildClass( int age, string name ) : base( name ) { Age = age; }

    public void DisplayInChild()
    {
      Console.WriteLine( $"Parent name: {Name}, age: {Age}" );
    }
  }

  public class GrandChildClass : ChildClass
  {
    public string School { set; get; }

    public GrandChildClass()
    {
      School = "";
      Age = 0;
      Name = "";
    }

    public GrandChildClass( int age, string name, string school ) : base( age, name ) { School = school; }

    public void DisplayInGrandChild() => Console.WriteLine( $"Parent name: {Name}, age: {Age}, school: {School}" );
  }

  //=================================================================================================================
  //OVERRIDING and HIDING
  //=================================================================================================================
  //Normal Inhertance
  //public class A
  //{
  //    public void Test() => Console.WriteLine("A::Test()");
  //}

  //public class B : A
  //{
  //}

  //public class C : B
  //{
  //}

  //Hiding With new Keyword
  //public class A
  //{
  //  public void Test() => Console.WriteLine( "A::Test()" );
  //}

  //public class B : A
  //{
  //  public new void Test() => Console.WriteLine( "B::Test()" );
  //}

  //public class C : B
  //{
  //  public new void Test() => Console.WriteLine( "C::Test()" );
  //}

  //Overriding With virtual And override Keyword
  public class A
  {
    public virtual void Test() => Console.WriteLine( "A::Test()" );
  }

  public class B : A
  {
    public override void Test() => Console.WriteLine( "B::Test()" );
  }

  public class C : B
  {
    public override void Test() => Console.WriteLine( "C::Test()" );
  }

  //sealed keyword, prevent overriding a derived class
  //public class A
  //{
  //  public virtual void Test() => Console.WriteLine( "A::Test()" );
  //}

  //public class B : A
  //{
  //  public sealed override void Test() => Console.WriteLine( "B::Test()" );
  //}

  //public class C : B
  //{
  //  public new void Test() => Console.WriteLine( "C::Test()" );
  //}

  //interface and Abstract Class
  interface IInterface
  {
    void ShowFoo();
  }

  public abstract class AbstractClass
  {
    public abstract int Counter { get; set; }
  }

  public class ImplementationAbstract : AbstractClass, IInterface
  {
    public override int Counter { get; set; }

    public void ShowFoo()
    {
      Console.WriteLine( $"Implementation from interface, Counter {Counter}" );
    }
  }
}

