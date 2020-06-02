// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o Write_example.cs
using System;
using System.IO;

class Write_example
{
    public static void Main() 
    {
        string filename = "filew.txt";
        try 
        {
            using (StreamWriter sw = new StreamWriter(filename, 
                       false, System.Text.Encoding.Default) )
            { 
                // Назначаем признак в стиле UNIX:
                sw.NewLine = "\n";
                // вывод текста
                sw.WriteLine("Строка текста");
                // вывод целого числа в шестнадцатиричном
                int intg = 1234;
                sw.WriteLine( "0x{0:X}", intg );
                // вывод действительного числа
                double flt = 1234.098765;
                sw.WriteLine( flt ); // формат по умолчанию
                sw.WriteLine( "{0:f3}", flt ); 
                   // формат с тремя цифрами после запятой
            }
        }
        catch (Exception e)
        {
	    Console.WriteLine(
                "Ошибка при записи в файл {0}: {1}", 
                    filename, e.Message);
        }
    }
}

