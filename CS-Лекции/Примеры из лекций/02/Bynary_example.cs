// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o Bynary_example.cs
using System;
using System.IO;

class Bynary_example 
{
    static string filename = "file.dat";
    
    // Тип данных для описания 
    // товара в магазине
    class ProductData 
    {
        public const int MaxName = 256; 
        // Макс. длина наименования товара
        private string Name;  
        // наименование товара
        private long Code;    // код товара
        private double Price; 
        // цена за единицу

        // инициализация объекта:
        public ProductData(string AName, long ACode, double APrice)
        {
            if ( AName.Length > ProductData.MaxName )
            {
                throw new ArgumentException();
            }
            Name = AName;
            Code = ACode;
            Price = APrice;
        }

        // инициализация объекта из файла
        public ProductData( BinaryReader file ) 
        {
            Read(file); 
        }   

        // чтение из файла
        public void Read( BinaryReader file )
        {
            Name = file.ReadString();
            Code = file.ReadInt64();
            Price = file.ReadDouble();
        }

        // сохранение в файл
        public void Write( BinaryWriter file )
        {
            file.Write(Name);
            file.Write(Code);
            file.Write(Price);
        }

        // Распечатка в строку текста
        public override string ToString()
        {
            return Name + " " + 
                Code.ToString() + " " + 
                Price.ToString();
        }
    }
    // Записываем описания товаров в файл
    static void WriteData() 
    {
        try 
        {
            using (BinaryWriter file = new BinaryWriter( new 
                      FileStream( filename, FileMode.Create )))
            {
                // Создаём несколько описаний
                // товаров и сохраняем в файл
                ProductData p = new ProductData(
                    "Чай чёрный DILMAH 50 гр.", 
                    9312631122268, 9370);
                p.Write(file);
                p = new ProductData(
                    "Шоколад чёрный 20 гр.", 
                    4810410024338, 1700);
                p.Write(file);
                p = new ProductData(
                    "Зефир ванильный 250 гр.", 
                    4810411002236, 5890);
                p.Write(file);
            }
        }
        catch (Exception e) 
        // исключение во время записи в файл
        {
            Console.WriteLine( "Ошибка при записи {0}: {1}",
                filename, e.Message);
        }
    }
    // Читаем описания товаров из файла
    // и печатаем на консоль
    static void ReadData() 
    {
        try 
        {
            using (BinaryReader file = new BinaryReader(new 
                       FileStream( filename, FileMode.Open )))
            {
                ProductData p = new ProductData(file);
                Console.WriteLine(p);
                p = new ProductData(file);
                Console.WriteLine(p);
                p = new ProductData(file);
                Console.WriteLine(p);
            }
        }
        catch (Exception e)
        //исключение при чтении из файла
        {
            Console.WriteLine( "Ошибка чтения {0}: {1}", 
                filename, e.Message);
        }
    }
    static void Main(string[] args)
    {
        WriteData();
        ReadData();
    }
}
