using System;
using System.Globalization;
using CSharp.InlineParameter;

namespace CSharp.CodeCompletion
{
  public class Person
  {
    public int id;

    public int ID
    {
      get { return id; }
      set { id = value; }
    }

    public Person(int id)
    {
      this.id = id;
    }

    public Person(string name, int age)
    {
      Name = name;
      Age = age;
    }

    public Person(int id, string name, int age)
    {
      this.id = id;
      Name = name;
      Age = age;
    }
    
    public string Name { get; set; }
    public int Age { get; set; }

    public virtual string GetGreeting()
    {
      return string.Format("Hello, I am {0} and I'm {1} years old.", Name, Age);
    }

    public void MyMethod()
    {
      decimal d = 42;
      string.Format("{0:}", d);
    }
  }
  
}