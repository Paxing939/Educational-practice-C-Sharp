// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o geometry.cs
using System;

namespace geometry
{
    public class Point : Object
    {   // Свойство X:
        public double X 
        {
            get { return _x * Scale + Shift; }
            set { _x = value; }
        }
        // Поле связанное со свойством X
        private double _x;

        // Метод: В начало координат
        public void SetToOrigin() 
        {
            X = 0.0;
        }
        // Конструктор по умолчанию:
        public Point() : this(0.0) // вызов другого конструктора
        { 
        }
        // Конструктор копирования:
        public Point( Point p) 
        {
            X = p.X;
        }
        // Ещё один конструктор:
        public Point(double v)
        {
            X = v;
        }
        // Статические методы: операции 
        public static Point Add(Point p, Point p1)
        {   // p + p1
            return new Point(p.X + p1.X);
        }
        public static Point Sub(Point p, Point p1)
        {   // p - p1
            return new Point(p.X - p1.X);
        }
        public static Point Mul(Point p, Point p1)
        {   // p * p1
            return new Point(p.X * p1.X);
        }
        public static Point Div(Point p, Point p1)
        {   // p / p1: обходим ситуацию деления на ноль
            return new Point( p1.X == 0.0 ? 1.0 : p.X / p1.X );
        }
        public static double Distance(Point p, Point p1)
        {   // расстояние между точками
            return Math.Abs(p1.X - p.X);
        }
        // перегрузка операторов: 
        public static Point operator +(Point p, Point p1)
        {
            return Add(p, p1);
        }
        public static double operator ^(Point p, Point p1)
        {
            return Distance(p, p1);
        }
        public static Point operator -(Point p, Point p1)
        {
            return Sub(p, p1);
        }
        public static Point operator *(Point p, Point p1)
        {
            return Mul(p, p1);
        }
        public static Point operator /(Point p, Point p1)
        {
            return Div(p, p1);
        }
        // Статические свойства:
        // автоматически реализуемые
        public static double Shift { get; private set; }
        public static double Scale { get; private set; }
		 // Статический конструктор:
        static Point()
        {
            Shift = 0.0;
            Scale = 1.0;
        }
        public static void ShiftScale(double Shift,
                                      double Scale)
        {   // инициализация статических свойств: Scale > 0
            Point.Shift = Shift;
            Point.Scale = Scale == 0 ? 1.0 : Scale;
        }
        //--- Демонстрируются два способа перегрузки 
        //    метода базового класса
        // 1) override означает подмену ссылки на метод 
        //    в таблице виртуальных методов
        public override string ToString()
        {   // Преобразование объекта к строке
            return X.ToString();
        }
        // 2) new означает скрытие метода базового класса 
        //    без изменений в таблице виртуальных методов
        public new bool Equals(Object obj)
        {   // Сравнение объектов на равенство
            Point p = obj as Point; 
                // попытка преобразовать obj к Point
                // вернётся null если obj не потомок Point
            if (p == null) 
                return base.Equals(p); 
                // обращение к методу базового класса
            else
                return p.X == X;
        }
    }

    public class Program
    {
        public static int Main(string[] args)
        {
            Point p = new Point(10.0);
            Point _p = new Point { X = 10.0 };
            Point p1 = new Point(1.0);
            Point p2 = p * p1;

            Console.WriteLine("p == {0}", p);
            Console.WriteLine("_p == {0}", _p);
            Console.WriteLine("p1 == {0}", p1);
            Console.WriteLine("p2 == {0}", p2);

            Point.ShiftScale(5, 7);
            Console.WriteLine(
                "After coordinate conversion: " +
                "Shift = {0}, Scale = {1}",
                Point.Shift, Point.Scale);

             Console.WriteLine("p = {0}", p);
             Console.WriteLine("_p = {0}", _p);
             Console.WriteLine("p1 = {0}", p1);
             Console.WriteLine("p2 = {0}", p2);
             Console.WriteLine("p * p1 = {0}", p * p1);
             return 0;
        }
    }
}
