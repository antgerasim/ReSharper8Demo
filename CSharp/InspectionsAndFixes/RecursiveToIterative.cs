namespace CSharp.InspectionsAndFixes
{
  public class RecursiveToIterative
  {
    int Sum(int n)
    {
      if (n == 0) return 0;
      else return n + Sum(n - 1);
    }
  }
}