using System;

namespace SomeRandomAssembly
{
    public class NewR8Analysis
    {
        private readonly int field;

        public NewR8Analysis()
        {
            field = 3;
            Console.WriteLine(field);
        }
        public void ArrayInits()
        {
            int[] c1 = new int[] { 0, 2, 3 };
        }


}