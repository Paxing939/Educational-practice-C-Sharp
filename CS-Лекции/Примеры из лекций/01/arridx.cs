// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o arridx.cs
using System;
using System.IO;
using System.Text;

namespace arridx
{
    public abstract class CArrayExtra //: IArrayExtra
    {
        public int Length { get; protected set; }
            // Размер массива
        public abstract int this[int idx] { get; set; }
            // Индексатор
        public abstract bool IndexInRange(int idx);
            // метод проверки индекса
    }
    public class ArrayXtra : CArrayExtra
    {
        private int _min;
        private int _max;
        private int[] arr;

        public ArrayXtra(int nMin, int nMax)
        {
            if (nMax <= nMin)
            {
                throw new ArgumentException();
            }
            _min = nMin;
            _max = nMax;
            Length = nMax - nMin + 1;
            arr = new int[Length];
        }

        // abstract -> override
        public override bool IndexInRange(int idx)
        {
            return (idx >= _min && idx <= _max);
        }

        public override int this[int idx]
        {
            get
            {
                if (IndexInRange(idx) == false)
                {   // Возбуждаем исключение
                    throw new IndexOutOfRangeException();
                }
                return arr[idx - _min];
            }
            set
            {
                if (IndexInRange(idx) == false)
                {   // Возбуждаем исключение
                    throw new IndexOutOfRangeException();
                }
                arr[idx - _min] = value;
            }
        }
    }
    public interface IArrayExtra
    // Интерфейс аналог CArrayExtra 
    {
        int Length { get; }
        bool IndexInRange(int idx);
        int this[int idx] { get; set; }
    }

    public class ArrayExtra : IArrayExtra
    {
        private int _min;
        private int _max;
        private int[] arr;

        public int Length { get; private set; }

        public ArrayExtra(int nMin, int nMax)
        {
            if (nMax <= nMin)
            {
                throw new ArgumentException();
            }
            _min = nMin;
            _max = nMax;
            Length = nMax - nMin + 1;
            arr = new int[Length];
        }
        public bool IndexInRange(int idx)
        {
            return (idx >= _min && idx <= _max);
        }
        public int this[int idx]
        {
            get
            {
                if (IndexInRange(idx) == false)
                {
                    throw new IndexOutOfRangeException();
                }
                return arr[idx - _min];
            }
            set
            {
                if (IndexInRange(idx) == false)
                {
                    throw new IndexOutOfRangeException();
                }
                arr[idx - _min] = value;
            }
        }
    }
    public class AExtra : ArrayXtra, IArrayExtra
    {
        public AExtra(int nMin, int nMax)
            : base(nMin, nMax) { } 
    }
    class Program
    {
        public static void TestIndex(
                IArrayExtra pa, int nMin, int nMax)
        {
            int i;
            // test set
            for (i = nMin; i <= nMax; i++) {
                pa[i] = i;
            }
            // test get
            for (i = nMin; i <= nMax; i++)
            {
                Console.Write(" {0}", pa[i]);
            }
            Console.WriteLine();
        }

        public static void FindBounds(
            IArrayExtra pa, out int nMin,
                            out int nMax )
        // on start - nMin == any correct index in array
        {
            if (pa.Length <= 0)
            {
                throw new ArgumentException();
            }
            int n = Int32.MaxValue;
            int d = pa.Length - 2;
            while (pa.IndexInRange(n) == false)
            {
                n -= d;
            }
            nMin = n;
            try
            {
                while (true)
                {
                    n = pa[nMin - 1];
                    nMin--;
                }
            }
            catch (Exception e)
            {
                if ((e is IndexOutOfRangeException) == false)
                // ( e as IndexOutOfRangeException )== null
                {
                    throw e;
                }
            }
            nMax = nMin + pa.Length - 1;
        }

        public static int Main(string[] args)
        {
            ArrayExtra a = null;

            try {
                a = new ArrayExtra(-3, 3);
                TestIndex(a, -3, 3);
                int i, j;

                FindBounds(a, out i, out j);
                Console.WriteLine(
                   "Array bounds:[{0}, {1}]",
                   i, j);

                // exception:
                a = new ArrayExtra(3, -3);
            }

            catch (Exception e) {
                Console.Error.WriteLine(
                    "Runtime error:\n{0}",
                    e.ToString());
            }

            return 0;
        }
    }
}

