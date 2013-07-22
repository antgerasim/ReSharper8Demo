using System.Threading;
using System.Threading.Tasks;

namespace CSharp
{
  public static class CancelationOverload
  {
    public async static Task<int> Foo(this int x) { return x;} 
    public async static Task<int> Foo(this int x, CancellationToken t)
    {
      return x;
    }

    async static void Bar(CancellationToken t)
    {
      await 1.Foo(t);
    }
    
  }
}