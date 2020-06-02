// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o ExampleDelegate.cs
using System;
using System.IO;

namespace ExampleDelegate
{
        // Простой делегат:
    public delegate int IntOp(int x, int y);

        // Обобщённый делегат: 
        //    (сравните с C++ template)
    public delegate T TOp<T>(T x, T y);

        // простые арифметические операции:
    public class Simple
    {
        // масштаб:
        public int X = 1;
        public int Y = 1;

 	    // конструктор
        public Simple(int Ax, int Ay) 
        { 
            X = Ax; Y = Ay; 
        }

            // сумма с масштабированием 
        public int Add(int x, int y) 
        { 
            return (X * x + Y * y); 
        }

            // статические умножение и деление
        public static int Mul(int x, int y)
        {
            return (x * y); 
        }
        public static int Div(int x, int y)
        {
            return (x / y); 
        }
    }

    class Program
    {   
        static void Main(string[] args)
        {
            Console.WriteLine( "Delegate example:\n");

                // объявляем переменные-делегаты 
 		// создаём и инициализируем объекты
            Simple ps = new Simple(2, 2);
            IntOp a = new IntOp(ps.Add);

 		// анонимный метод
            IntOp s = delegate( int ax, int ay )
            {
                return (ax - ay);
            };
            // ещё один с лямбда выражением:
            IntOp s1 = (int ax, int ay) =>
            {
                return (ax - ay);
            };
            // ещё короче:
            IntOp s2 = (int ax, int ay) => (ax - ay);          

            TOp<int> m = Simple.Mul;
            TOp<int> d = new 
                TOp<int>(Simple.Div);
            int x = 100, y = 50;

            // вызовы через переменные-делегаты:
            Console.WriteLine( "a({0}, {1}) = {2}",
                x, y, a(x, y));
            Console.WriteLine("s({0}, {1}) = {2}",
                x, y, s(x, y));
            Console.WriteLine("s1({0}, {1}) = {2}",
                x, y, s1(x, y));
            Console.WriteLine("s2({0}, {1}) = {2}",
                x, y, s2(x, y));
            Console.WriteLine("m({0}, {1}) = {2}",
                x, y, m(x, y));
            Console.WriteLine("d({0}, {1}) = {2}",
                x, y, d(x, y));
        }
    }
}
