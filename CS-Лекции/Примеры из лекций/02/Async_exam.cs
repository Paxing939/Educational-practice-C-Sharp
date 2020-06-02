// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o Async_exam.cs
using System;
using System.IO;
using System.Threading;

public class Async_exam 
{
    const int BufSize = 1024; 
        // размер буфера
    static byte[] byteData = new byte[BufSize]; // буфер
    static FileStream fs; 
    static IAsyncResult aResult; 
        // результат асинхронной операции
    public static void Main(string[] args)
    {  
        try 
        {
            // Открываем файл для асинхронного чтения
            fs = new FileStream( "example.txt",
                         FileMode.Open, FileAccess.Read, 
                         FileShare.Read, BufSize, true);

            // Запуск асинхронного чтения(см.ниже)
            AsyncRead();

            // ... Здесь можно выполнять к-либо действия
            // одновременно с асинхронным чтением

            // Приостанавливаем выполнение программы 
            // пока не завершилось асинхронное чтение
            while ( !aResult.IsCompleted ) 
            {
                Thread.Sleep(100);
            } 
            fs.Close(); // Закрываем файл

            // Открываем для ас.записи
            fs = new FileStream( "write.txt",
                         FileMode.Create, FileAccess.Write, 
                         FileShare.None, BufSize, true );
            // Запуск асинхронной записи(см.ниже)
            AsyncWrite();

            // ... Здесь можно выполнять к-либо действия 
            // одновременно с асинхронной записью

            // Приостанавливаем выполнение программы 
            // пока не завершилась асинхронная запись
            while ( !aResult.IsCompleted )
            {
                Thread.Sleep(100);
            }
            fs.Close(); // Закрываем файл
        }
        catch (IOException err)
        // ошибки при чтении/записи
        {
            Console.WriteLine(err.Message);
        }
    }
       // Обработчик завершения асинхронного чтения
    public static void HandleRead( IAsyncResult ar ) 
    {
        int nbytes = fs.EndRead(ar); 
            // Сколько байт прочитано?
        Console.WriteLine( "{0} bytes read", nbytes);
    }

        // Запуск операции асинхронного чтения
    public static void AsyncRead() 
    {
        // Создаём ссылку на обработчик
        AsyncCallback cb = new AsyncCallback(HandleRead); 
        aResult = fs.BeginRead( byteData, 
            0, byteData.Length, cb, null);
    }

        // Обработчик завершения ас.записи
    public static void HandleWrite( IAsyncResult ar ) 
    {
        fs.EndWrite(ar);
        Console.WriteLine("{0} bytes write", byteData.Length );
    }

        // Запуск операции асинхронной записи
    public static void AsyncWrite() 
    {
        // Создаём ссылку на обработчик завершения
        AsyncCallback cb = new AsyncCallback( HandleWrite );
        aResult = fs.BeginWrite(byteData, 
            0, byteData.Length, cb, null);
    }
}
