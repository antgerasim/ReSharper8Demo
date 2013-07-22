using System;

namespace CSharp.InlineParameter
{
  public class Employee
  {
    public void DoSomething(string format)
    {
      ((Action) (() => Console.WriteLine(format)))();
    } 

    public void Work(string work)
    {
      DoSomething(work);
    }
  }
}