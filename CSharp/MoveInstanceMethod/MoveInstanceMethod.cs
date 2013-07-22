using System.Collections.Generic;

namespace CSharp.MoveInstanceMethod
{
  internal class Person
  {
    private readonly Operation operation = new Operation();

    public Operation Operation
    {
      get { return operation; }
    }

    public int GetResult()
    {
      return operation.GetResult();
    }
  }

  internal class Factory
  {
    private readonly List<Person> workers = new List<Person>();

    private void OneDay()
    {
      var result = 0;
      foreach (var worker in workers)
      {
        worker.Operation.DoWork();
        result += worker.GetResult();
      }
    }
  }

  class Operation
  {
    public void Start()
    {
    }

    public void Do()
    {
    }

    public void Verify()
    {
    }

    public void End()
    {
    }

    public int GetResult()
    {
      return 0;
    }


    public void DoWork()
    {
      Start();
      Do();
      Verify();
      End();
    }
  }
}