// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o fnd.cs
using System;

class fnd {
    static int Main(string[] args)
    {
        // ������������� �������� ��.������� � UTF-8
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // ��������� ��� ���� ��������
        if (args.Length != 1) {
            // ������� ���������
            Console.Error.WriteLine( "������: " +
                "�������� ���������� ���������� ���������");
            return 1; 
            // �������, ���������� ��� ����������: 
            // 1 �������� ������
        }

        string s; // ������ ������ � ���������� s
        // ��������� ����������� �� ������� ������
        while ((s = Console.In.ReadLine())!= null )
        {
            if (s.IndexOf( args[0] ) >= 0 )
                    // ����� ��������� args[0]
                Console.Out.WriteLine(s);  
                // ������� s ���� �����
        }
        return 0;
        // �������, ���������� ��� ����������: 
        // 0 ���������� ���������� ������
    }
}


