// ���������� � MSVC 2010x86 :
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
                // ������ ���� ���� � ������
                Console.Write( str );
                // ������� �� �������
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

