// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /codepage:1251 /o prg_01.cs
using System;
// ���������� ������������ ���

namespace prg_01
{
    class Program
    {
        static void Main(string[] args)
        {
            // ������������ ����-�����
            //
            Console.WriteLine("���� ���?");
            string name = Console.ReadLine();
            Console.WriteLine(
                "������������ ��� {0}", name);
            // ����������� �� ������ � �����
            Console.WriteLine("������� ����� �����:");
            string s = Console.ReadLine();
            int i = Convert.ToInt32(s);

	         // ����������� �� ������ � ��������������
            Console.WriteLine(
                 "������� �������������� �����:");
            s = Console.ReadLine();
            double d = Convert.ToDouble(s);
            Console.WriteLine("�� �����: " + i + "; " + d );

            Console.WriteLine(
                    "������� ��� ���������� ���������:");
            // ������ �����������
            name = "";
            // ������ ��������� ������, 
            // ��������� �� ����� ����� � �� ����� ������
            while ((i = Console.Read())!= -1 && i != '\n')
            {
                // ���������� ������ ������� �������
                if (i != '\r')
                {
                    // ����������� ������� � ������
                    name += (char) i; // ���������� ����
                }
            }
	         // ��������� ��� ����� �� ������ ������
            if (name.Length > 0)
            {
                s = Environment.GetEnvironmentVariable(name);
                // ��������� ��� ���������� ������
                if (s != null)
                {
                    // ������ � �����������
                    Console.WriteLine(
                             "���������� {0}={1}", name, s);
                }
            }
        }
    }
}

