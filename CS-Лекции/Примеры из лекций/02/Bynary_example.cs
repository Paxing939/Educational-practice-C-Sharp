// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o Bynary_example.cs
using System;
using System.IO;

class Bynary_example 
{
    static string filename = "file.dat";
    
    // ��� ������ ��� �������� 
    // ������ � ��������
    class ProductData 
    {
        public const int MaxName = 256; 
        // ����. ����� ������������ ������
        private string Name;  
        // ������������ ������
        private long Code;    // ��� ������
        private double Price; 
        // ���� �� �������

        // ������������� �������:
        public ProductData(string AName, long ACode, double APrice)
        {
            if ( AName.Length > ProductData.MaxName )
            {
                throw new ArgumentException();
            }
            Name = AName;
            Code = ACode;
            Price = APrice;
        }

        // ������������� ������� �� �����
        public ProductData( BinaryReader file ) 
        {
            Read(file); 
        }   

        // ������ �� �����
        public void Read( BinaryReader file )
        {
            Name = file.ReadString();
            Code = file.ReadInt64();
            Price = file.ReadDouble();
        }

        // ���������� � ����
        public void Write( BinaryWriter file )
        {
            file.Write(Name);
            file.Write(Code);
            file.Write(Price);
        }

        // ���������� � ������ ������
        public override string ToString()
        {
            return Name + " " + 
                Code.ToString() + " " + 
                Price.ToString();
        }
    }
    // ���������� �������� ������� � ����
    static void WriteData() 
    {
        try 
        {
            using (BinaryWriter file = new BinaryWriter( new 
                      FileStream( filename, FileMode.Create )))
            {
                // ������ ��������� ��������
                // ������� � ��������� � ����
                ProductData p = new ProductData(
                    "��� ������ DILMAH 50 ��.", 
                    9312631122268, 9370);
                p.Write(file);
                p = new ProductData(
                    "������� ������ 20 ��.", 
                    4810410024338, 1700);
                p.Write(file);
                p = new ProductData(
                    "����� ��������� 250 ��.", 
                    4810411002236, 5890);
                p.Write(file);
            }
        }
        catch (Exception e) 
        // ���������� �� ����� ������ � ����
        {
            Console.WriteLine( "������ ��� ������ {0}: {1}",
                filename, e.Message);
        }
    }
    // ������ �������� ������� �� �����
    // � �������� �� �������
    static void ReadData() 
    {
        try 
        {
            using (BinaryReader file = new BinaryReader(new 
                       FileStream( filename, FileMode.Open )))
            {
                ProductData p = new ProductData(file);
                Console.WriteLine(p);
                p = new ProductData(file);
                Console.WriteLine(p);
                p = new ProductData(file);
                Console.WriteLine(p);
            }
        }
        catch (Exception e)
        //���������� ��� ������ �� �����
        {
            Console.WriteLine( "������ ������ {0}: {1}", 
                filename, e.Message);
        }
    }
    static void Main(string[] args)
    {
        WriteData();
        ReadData();
    }
}
