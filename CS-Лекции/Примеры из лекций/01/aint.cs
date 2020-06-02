// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o aint.cs
using System;
// Подключили пространство имён

namespace aint
{
    class Program
    {
        // Обобщённый метод - возвращает одномерный
	// массив типа T
        static T[] GetArray<T>(int cx)
        {
            return new T[cx];
        }

	// Обобщённый метод - возвращает двумерный 
	// массив типа T
        static T[][] GetArray<T>(int cy, int cx)
        {
            T[][] r = new T[cy][];

            for (int i = 0; i < cy; i++)
            {
                r[i] = new T[cx];
            }

            return r;

        }

	// Обобщённый метод - возвращает трёхмерный 
	// массив типа T
        static T[][][] GetArray<T>(int cz, int cy, int cx)
        {
            T[][][] r = new T[cz][][];
            for (int i = 0; i < cz; i++)
            {
                r[i] = new T[cy][];
                for (int j = 0; j < cy; j++)
                {
                    r[i][j] = new T[cx];
                }
            }
            return r;
        }

	// Обобщённый метод - возвращает двумерный 
 	// прямоугольный массив типа T
        static void GetArray<T>(
                       out T[,] r, int cy, int cx)
        {
            r = new T[cy, cx];
        }

	// Обобщённый метод - возвращает трёхмерный 
 	// прямоугольный массив типа T
        static void GetArray<T>(
              out T[, ,] r, int cz, int cy, int cx)
        {
            r = new T[cz, cy, cx];
        }

        static void Main()
        {
            int[] pix = GetArray<int>(3);
            Console.WriteLine("pix.Length={0}",
                               pix.Length);

            int[][] piyx = GetArray<int>(5, 3);
            Console.WriteLine(
              "piyx.Length={0}, piyx[0].Length={1}",
               piyx.Length, piyx[0].Length );

            int[][][] pizyx = GetArray<int>(7, 5, 3);
     
            Console.WriteLine(
               "pizyx.Length={0}, pizyx[0].Length={1}, "+
               "pizyx[0][0].Length={2}",
                pizyx.Length, pizyx[0].Length,
                pizyx[0][0].Length );
            int[,] aiyx = null;
            GetArray<int>(out aiyx, 5, 3);
            Console.WriteLine("aiyx.Length={0}",
                               aiyx.Length);
            int[, ,] aizyx = null;
            GetArray<int>(out aizyx, 7, 5, 3);
            Console.WriteLine("aizyx.Length={0}",
                               aizyx.Length);
        }
    }
}
