using System;

namespace CSharp.CodeCompletion
{
  public class ToString
  {
    string Foo()
    {
      var dt = DateTime.Now;
      return dt.ToString("");
    }

    string Bar()
    {
      return Guid.NewGuid().ToString("");
    }

    string Baz()
    {
      var ts = new TimeSpan();
      decimal d = 0;
      return string.Format("{0:c}{1:P}", ts, d);
    }
  }
}