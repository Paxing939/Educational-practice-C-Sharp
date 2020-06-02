// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o Serialize_example.cs
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

class Serialize_example
{
    static string filename = "files.dat";

    // Тип данных для описания товара 
    [Serializable]
    class ProductData
    {
        public const int MaxName = 256; 
 	  // Длина наименования товара
        private string Name;  
          // наименование товара
        private long Code;  
          // код товара
        private double Price; 
          // цена за единицу

 	  // инициализация объекта
        public ProductData(string AName, long ACode, double APrice)
        {
            if (AName.Length > ProductData.MaxName)
            {
                throw new ArgumentException();
            }
            Name = AName;
            Code = ACode;
            Price = APrice;
        }

        public ProductData() 
        {
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
            using (FileStream file = new FileStream( 
                       filename, FileMode.Create )) 
            {
                BinaryFormatter bf = new BinaryFormatter();
bf.Serialize(file, (Int32) 3 );
                // Создаём несколько описаний
                // товаров и сохраняем в файл
                ProductData p= new ProductData(
                    "Чай чёрный DILMAH 50 гр.", 
                    9312631122268, 9370);
                bf.Serialize(file, p);
                p = new ProductData(
                    "Шоколад чёрный 20 гр.",
                    4810410024338, 1700);
                bf.Serialize(file, p);
                p = new ProductData(
                    "Зефир ванильный 250 гр.",
                    4810411002236, 5890);
                bf.Serialize(file, p);
            }
        }
        catch (Exception e) 
        {
            // исключение во время записи в файл
            Console.WriteLine( "Ошибка записи {0}: {1}", 
                filename, e.Message);
        }
    }
    // Читаем описания товаров из файла 
    // и печатаем на консоль
    static void ReadData()
    { 
        try 
        {
            using (FileStream file = new FileStream(filename, 
                       FileMode.Open )) 
            {
                BinaryFormatter bf = new BinaryFormatter();
Int32 num = (Int32) bf.Deserialize(file);
Console.WriteLine(num);
                ProductData p = (ProductData) bf.Deserialize(file); 
                Console.WriteLine(p);
                p = (ProductData) bf.Deserialize(file); 
                Console.WriteLine(p);
                p = (ProductData) bf.Deserialize(file); 
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
