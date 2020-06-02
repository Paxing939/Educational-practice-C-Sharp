// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o FCopy_example.cs
using System;
using System.IO;

class FCopy_example
{   
    static int Main(string[] args)
    {   
        // Проверяем что два аргумента
        if (args.Length != 2) 
        {
            // Выводим сообщение
            Console.Error.WriteLine( "Ошибка: " + 
                "Неверное количество аргументов");
            return 1; // Выходим по ошибке
        }
        try 
        {
           // открываем входной файл для
           // последовательного чтения
           using (FileStream fsrc = new FileStream( args[0], 
                      FileMode.Open, FileAccess.Read ))
           // открываем выходной файл, 
           // если существует - перезаписать
           using (FileStream ftarg = new FileStream( args[1],
                      FileMode.Create, FileAccess.Write )) 
           {
               int b; // читаем до конца файла
               while ((b = fsrc.ReadByte())!= -1 )
               {
                   ftarg.WriteByte( (byte) b ); 
                   // выводим байт в выходной файл
               }
               return 0; //нет ошибок                
           }
        } 
        catch (Exception e) 
        { 
            // исключения при копировании файла
            Console.WriteLine( "Ошибка копирования " +
                "файла {0} в {1}: {2}", args[0], args[1],
                e.Message);
        }
        return 1; //код завершения по ошибке
    }
}
