using System;

namespace SomeRandomAssembly
{
    internal class A
    {
        public void Func1(int x)
        {
            if (x > 0)
            {
                Func1(x - 1);
                return;
            }

            Console.WriteLine(x);
            return;
        }

        public void Func2(ref int x)
        {
            if (x > 0)
            {
                Func2(ref x);
            }
            else if (x < 0)
            {
                return;
            }
            else
            {
                var y = 1;
                Func2(ref y);
            }
        }

        public void Func3(int x, out int y, params int[] xs)
        {
            if (x > 0)
            {
                Func3(x - 1, out y, 1, 2, 3);
            }
            else if (x < 0)
            {
                y = 1;
                return;
            }
            else
            {
                var yy = 1;
                y = 123;
                Func2(ref yy);
            }
        }

        public int Func4(int x, int y = 123)
        {
            if (x > 0)
            {
                Func4(x - 1);
                Func4(x - 2);
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public int Func5(int x, int y = 123)
        {
            if (x > 0)
            {
                Func5(x - 1);
                return Func5(x - 2);
            }
            else
            {
                return 2;
            }
        }
    }
}