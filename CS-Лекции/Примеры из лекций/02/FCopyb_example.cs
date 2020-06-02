// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o FCopyb_example.cs
using System;
using System.IO;

class FCopyb_example 
{
    static int Main(string[] args)
    {
        // ���������, ��� ��� ���������
        if (args.Length != 2) 
        {
            // ������� ���������
            Console.Error.WriteLine( "������: " + 
                "�������� ���������� ����������");
            return 1; // ������
        }      
        try 
        {
            // ��������� ������� ����
            // ��� ����������������� ������

            using (FileStream fsrc = new FileStream( args[0], 
                    FileMode.Open, FileAccess.Read ))

            // ��������� �������� ���� 

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
            // ���������� ��� �����������
            Console.WriteLine( "������ ����������� ����� "+
                "{0} � {1}: {2}", args[0], args[1],
                e.Message);
        }
        return 1; //���������� � �������
    }
}
