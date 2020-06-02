// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o ReadToEnd_example.cs
using System;
using System.IO;

class ReadToEnd_example
{
    public static void Main()
    {
        string filename = "file.txt";
        try 
        {
            using (StreamReader sr = new StreamReader( filename,
                       System.Text.Encoding.GetEncoding(1251) )
                  )
            {
                string str = sr.ReadToEnd();
                // читаем весь файл в строку
                Console.Write( str );
                // выводим на консоль
            }
        }
        catch (Exception e) // исключение при чтении
        {
	      Console.WriteLine( 
  	          "Ошибка при чтении файла {0}: {1}",
                    filename, e.Message);

        }
    }
}

