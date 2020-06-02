// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o FCopyb_example.cs
using System;
using System.IO;

class FCopyb_example 
{
    static int Main(string[] args)
    {
        // Проверяем, что два аргумента
        if (args.Length != 2) 
        {
            // Выводим сообщение
            Console.Error.WriteLine( "Ошибка: " + 
                "Неверное количество аргументов");
            return 1; // ошибка
        }      
        try 
        {
            // открываем входной файл
            // для последовательного чтения

            using (FileStream fsrc = new FileStream( args[0], 
                    FileMode.Open, FileAccess.Read ))

            // открываем выходной файл 

            using (FileStream ftarg = new FileStream( args[1], 
                    FileMode.Create, FileAccess.Write )) 
            {
                byte[] Buf = new byte[1024];
                int nread; 
                while(( nread = fsrc.Read( Buf, 0, 1024 )) > 0) 
                {
                    ftarg.Write(Buf, 0, nread);
                }
                return 0; 
            }
        }
        catch (Exception e) 
        { 
            // исключение при копировании
            Console.WriteLine( "Ошибка копирования файла "+
                "{0} в {1}: {2}", args[0], args[1],
                e.Message);
        }
        return 1; //завершение с ошибкой
    }
}
