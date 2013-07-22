using System;

namespace CSharp.InlineParameter
{
  public class InlineParameter
  {
    public Tuple<double, double> Solve(double a, double b)
    {
      double q = -0.5 * (b + Math.Sign(b) * Math.Sqrt(b * b - 4 * a * 16));
      return Tuple.Create(q / a, 16 / q);
    } 

    public Tuple<double, double> SolveForA(int a)
    {
      return Solve(a, 10);
    }

    public Tuple<double, double> SolveForB(int b)
    {
      return Solve(1, b);
    }
  }
}
