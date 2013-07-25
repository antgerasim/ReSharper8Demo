using System;

namespace CSharp.InlineParameter
{
  public class Employee
  {
    public void DoSomething(Action action)
    {
        action();
    } 

    public void Work(string work)
    {
      DoSomething(()=> Console.WriteLine(work));
    }
  }
}