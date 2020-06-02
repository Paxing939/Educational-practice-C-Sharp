// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /codepage:1251 /o prg_01.cs
using System;
// Подключили пространство имён

namespace prg_01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Элементарный ввод-вывод
            //
            Console.WriteLine("Ваше имя?");
            string name = Console.ReadLine();
            Console.WriteLine(
                "Приветствуем Вас {0}", name);
            // Конвертация из строки в целые
            Console.WriteLine("Введите целое число:");
            string s = Console.ReadLine();
            int i = Convert.ToInt32(s);

	         // Конвертация из строки в действительные
            Console.WriteLine(
                 "Введите действительное число:");
            s = Console.ReadLine();
            double d = Convert.ToDouble(s);
            Console.WriteLine("Вы ввели: " + i + "; " + d );

            Console.WriteLine(
                    "Введите имя переменной окружения:");
            // Читаем посимвольно
            name = "";
            // читаем очередной символ, 
            // проверяем на конец файла и на конец строки
            while ((i = Console.Read())!= -1 && i != '\n')
            {
                // пропускаем символ возврат каретки
                if (i != '\r')
                {
                    // накапливаем символы в строке
                    name += (char) i; // приведение типа
                }
            }
	         // проверяем что ввели не пустую строку
            if (name.Length > 0)
            {
                s = Environment.GetEnvironmentVariable(name);
                // Проверяем что переменная найдна
                if (s != null)
                {
                    // Печать с параметрами
                    Console.WriteLine(
                             "Переменная {0}={1}", name, s);
                }
            }
        }
    }
}

