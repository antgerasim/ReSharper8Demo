namespace CSharp.PullParameter
{
  abstract class Parent
  {
    protected int parentFoo;
    protected abstract int Solve(int a, int b, int c, int d);
  }

  class Child : Parent 
  {
    private int childBar;

    protected override int Solve(int a, int b, int c, int d)
    {
      return a + b - c;
    }

    public void Foo()
    {
      Solve(1, 2, 3, parentFoo);
    }
  }
}