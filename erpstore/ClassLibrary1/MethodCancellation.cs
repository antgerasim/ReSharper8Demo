using System;
using System.Threading;

namespace SomeRandomAssembly
{
    public static class Async
    {
        public static void F() { }
        public static void F(CancellationToken token) { }

        public static void G<T>(int a, T t) { }
        public static void G<T>(int a, T t, CancellationToken token) { }

        internal static void G<T1, T2>(T1 a, Func<T2> t) { }
        internal static void G<T1, T2>(T1 a, Func<T2> t, CancellationToken token) { }

        public static void U() { }
        internal static void U(CancellationToken token) { }

        public static void U2(string a) { }
        public static void U2(int a, CancellationToken token) { }
    }

    public class AB
    {
        private static readonly CancellationToken staticToken;

        public void M1()
        {
            Async.F();

            {
                CancellationToken a;
                Async.F();
            }

            Async.F();
        }

        public void M2(CancellationToken a)
        {
            Async.F();
            Async.U();
            Async.U2("Aaa");
        }

        public void M3()
        {
            var source = new CancellationTokenSource();
            Async.G(1, "aaa");

            Async.U();
        }

        public void M4(CancellationToken a, CancellationToken b)
        {
            Async.G(1, () => "aaa");
        }

        public void M5()
        {
            System.Action<CancellationToken> f = t =>
            {
                Async.G(1, () => "aaa");
            };
        }
    }

    public class BC
    {
        private CancellationToken myInstanceToken;

        public void M1()
        {
            Async.F();
        }
    }
}