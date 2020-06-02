// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o geometry.cs
using System;

namespace geometry
{
    public class Point : Object
    {   // �������� X:
        public double X 
        {
            get { return _x * Scale + Shift; }
            set { _x = value; }
        }
        // ���� ��������� �� ��������� X
        private double _x;

        // �����: � ������ ���������
        public void SetToOrigin() 
        {
            X = 0.0;
        }
        // ����������� �� ���������:
        public Point() : this(0.0) // ����� ������� ������������
        { 
        }
        // ����������� �����������:
        public Point( Point p) 
        {
            X = p.X;
        }
        // ��� ���� �����������:
        public Point(double v)
        {
            X = v;
        }
        // ����������� ������: �������� 
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
        {   // p / p1: ������� �������� ������� �� ����
            return new Point( p1.X == 0.0 ? 1.0 : p.X / p1.X );
        }
        public static double Distance(Point p, Point p1)
        {   // ���������� ����� �������
            return Math.Abs(p1.X - p.X);
        }
        // ���������� ����������: 
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
        // ����������� ��������:
        // ������������� �����������
        public static double Shift { get; private set; }
        public static double Scale { get; private set; }
		 // ����������� �����������:
        static Point()
        {
            Shift = 0.0;
            Scale = 1.0;
        }
        public static void ShiftScale(double Shift,
                                      double Scale)
        {   // ������������� ����������� �������: Scale > 0
            Point.Shift = Shift;
            Point.Scale = Scale == 0 ? 1.0 : Scale;
        }
        //--- ��������������� ��� ������� ���������� 
        //    ������ �������� ������
        // 1) override �������� ������� ������ �� ����� 
        //    � ������� ����������� �������
        public override string ToString()
        {   // �������������� ������� � ������
            return X.ToString();
        }
        // 2) new �������� ������� ������ �������� ������ 
        //    ��� ��������� � ������� ����������� �������
        public new bool Equals(Object obj)
        {   // ��������� �������� �� ���������
            Point p = obj as Point; 
                // ������� ������������� obj � Point
                // ������� null ���� obj �� ������� Point
            if (p == null) 
                return base.Equals(p); 
                // ��������� � ������ �������� ������
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
