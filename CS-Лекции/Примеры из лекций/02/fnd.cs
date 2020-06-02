// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o fnd.cs
using System;

class fnd {
    static int Main(string[] args)
    {
        // Устанавливаем кодироку ст.потоков в UTF-8
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Проверяем что один аргумент
        if (args.Length != 1) {
            // Выводим сообщение
            Console.Error.WriteLine( "Ошибка: " +
                "Неверное количество аргументов программы");
            return 1; 
            // выходим, возвращаем код завершения: 
            // 1 означает ошибка
        }

        string s; // Читаем строку в переменную s
        // Проверяем закончились ли входные данные
        while ((s = Console.In.ReadLine())!= null )
        {
            if (s.IndexOf( args[0] ) >= 0 )
                    // Поиск подстроки args[0]
                Console.Out.WriteLine(s);  
                // Выводим s если нашли
        }
        return 0;
        // выходим, возвращаем код завершения: 
        // 0 показывает отсутствие ошибок
    }
}


