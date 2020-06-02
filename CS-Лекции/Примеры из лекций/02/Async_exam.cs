// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o Async_exam.cs
using System;
using System.IO;
using System.Threading;

public class Async_exam 
{
    const int BufSize = 1024; 
        // ������ ������
    static byte[] byteData = new byte[BufSize]; // �����
    static FileStream fs; 
    static IAsyncResult aResult; 
        // ��������� ����������� ��������
    public static void Main(string[] args)
    {  
        try 
        {
            // ��������� ���� ��� ������������ ������
            fs = new FileStream( "example.txt",
                         FileMode.Open, FileAccess.Read, 
                         FileShare.Read, BufSize, true);

            // ������ ������������ ������(��.����)
            AsyncRead();

            // ... ����� ����� ��������� �-���� ��������
            // ������������ � ����������� �������

            // ���������������� ���������� ��������� 
            // ���� �� ����������� ����������� ������
            while ( !aResult.IsCompleted ) 
            {
                Thread.Sleep(100);
            } 
            fs.Close(); // ��������� ����

            // ��������� ��� ��.������
            fs = new FileStream( "write.txt",
                         FileMode.Create, FileAccess.Write, 
                         FileShare.None, BufSize, true );
            // ������ ����������� ������(��.����)
            AsyncWrite();

            // ... ����� ����� ��������� �-���� �������� 
            // ������������ � ����������� �������

            // ���������������� ���������� ��������� 
            // ���� �� ����������� ����������� ������
            while ( !aResult.IsCompleted )
            {
                Thread.Sleep(100);
            }
            fs.Close(); // ��������� ����
        }
        catch (IOException err)
        // ������ ��� ������/������
        {
            Console.WriteLine(err.Message);
        }
    }
       // ���������� ���������� ������������ ������
    public static void HandleRead( IAsyncResult ar ) 
    {
        int nbytes = fs.EndRead(ar); 
            // ������� ���� ���������?
        Console.WriteLine( "{0} bytes read", nbytes);
    }

        // ������ �������� ������������ ������
    public static void AsyncRead() 
    {
        // ������ ������ �� ����������
        AsyncCallback cb = new AsyncCallback(HandleRead); 
        aResult = fs.BeginRead( byteData, 
            0, byteData.Length, cb, null);
    }

        // ���������� ���������� ��.������
    public static void HandleWrite( IAsyncResult ar ) 
    {
        fs.EndWrite(ar);
        Console.WriteLine("{0} bytes write", byteData.Length );
    }

        // ������ �������� ����������� ������
    public static void AsyncWrite() 
    {
        // ������ ������ �� ���������� ����������
        AsyncCallback cb = new AsyncCallback( HandleWrite );
        aResult = fs.BeginWrite(byteData, 
            0, byteData.Length, cb, null);
    }
}
