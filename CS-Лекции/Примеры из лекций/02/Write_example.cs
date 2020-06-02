// ���������� � MSVC 2010x86 :
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
                // ��������� ������� � ����� UNIX:
                sw.NewLine = "\n";
                // ����� ������
                sw.WriteLine("������ ������");
                // ����� ������ ����� � �����������������
                int intg = 1234;
                sw.WriteLine( "0x{0:X}", intg );
                // ����� ��������������� �����
                double flt = 1234.098765;
                sw.WriteLine( flt ); // ������ �� ���������
                sw.WriteLine( "{0:f3}", flt ); 
                   // ������ � ����� ������� ����� �������
            }
        }
        catch (Exception e)
        {
	    Console.WriteLine(
                "������ ��� ������ � ���� {0}: {1}", 
                    filename, e.Message);
        }
    }
}

