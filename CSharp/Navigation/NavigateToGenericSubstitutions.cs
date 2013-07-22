using System;
using CSharp.Annotations;

namespace CSharp.Navigation
{
  [UsedImplicitly]
  public class NavigateToGenericSubstitutions 
  {
    void Print<T>(T t, string u)
    {
      Console.WriteLine("{0} and {1}", t, u);
    }

    void Foo()
    {
      int n = 0;
      string s = "hello";
      Print(n,s);
    }

    private void Bar()
    {
      float f = 42;
      string s = "meaning of life";
      Print(f,s);

      AddType("Foo", "Bar");
    }

    private void AddType(string foo, string bar)
    {
      throw new NotImplementedException();
    }
  }
}