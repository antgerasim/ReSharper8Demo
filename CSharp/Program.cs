using CSharp.InlineParameter;
using CSharp.PullParameter;

namespace CSharp
{
  class Program
  {
    static void Main(string[] args)
    {
      var ip = new InlineParameter.InlineParameter();
      var foo = ip.SolveForA(1);
      var bar = ip.SolveForB(2);

      var emp = new Employee();
      emp.Work("Foo");

      Child c = new Child();
      c.Foo();
    }
  }
}
