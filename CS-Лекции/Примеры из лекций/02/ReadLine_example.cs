// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o ReadLine_example.cs
using System;
using System.IO;

class ReadLine_example
{
    public static void Main()
    {
        string filename = "file.txt";
        try 
        {
            using (StreamReader sr = new StreamReader( filename,
                       System.Text.Encoding.GetEncoding(1251) )
                  )
                for (;;) // ����������� ����
                {
                    string str = sr.ReadLine(); 
                    // ��������� ������
                    if (str == null) break; // ����� �����? 
                        // ���� �� - ����� �� �����
                    Console.WriteLine(str); 
                }
        }
        catch (Exception e) // ���������� ��� ������
        {
	      Console.WriteLine( 
  	          "������ ��� ������ ����� {0}: {1}",
                    filename, e.Message);

        }
    }
}

