namespace CSharp.CodeCompletion
{
  public class Person
  {
    public int id;
    public string Name { get; set; }
    public int Age { get; set; }

    public virtual string GetGreeting()
    {
      return string.Format("Hello, I am {0} and I'm {1} years old.", Name, Age);
    }
  }


  class Manager
  {
    
  }
}