// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o Serialize_example.cs
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

class Serialize_example
{
    static string filename = "files.dat";

    // ��� ������ ��� �������� ������ 
    [Serializable]
    class ProductData
    {
        public const int MaxName = 256; 
 	  // ����� ������������ ������
        private string Name;  
          // ������������ ������
        private long Code;  
          // ��� ������
        private double Price; 
          // ���� �� �������

 	  // ������������� �������
        public ProductData(string AName, long ACode, double APrice)
        {
            if (AName.Length > ProductData.MaxName)
            {
                throw new ArgumentException();
            }
            Name = AName;
            Code = ACode;
            Price = APrice;
        }

        public ProductData() 
        {
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
            using (FileStream file = new FileStream( 
                       filename, FileMode.Create )) 
            {
                BinaryFormatter bf = new BinaryFormatter();
bf.Serialize(file, (Int32) 3 );
                // ������ ��������� ��������
                // ������� � ��������� � ����
                ProductData p= new ProductData(
                    "��� ������ DILMAH 50 ��.", 
                    9312631122268, 9370);
                bf.Serialize(file, p);
                p = new ProductData(
                    "������� ������ 20 ��.",
                    4810410024338, 1700);
                bf.Serialize(file, p);
                p = new ProductData(
                    "����� ��������� 250 ��.",
                    4810411002236, 5890);
                bf.Serialize(file, p);
            }
        }
        catch (Exception e) 
        {
            // ���������� �� ����� ������ � ����
            Console.WriteLine( "������ ������ {0}: {1}", 
                filename, e.Message);
        }
    }
    // ������ �������� ������� �� ����� 
    // � �������� �� �������
    static void ReadData()
    { 
        try 
        {
            using (FileStream file = new FileStream(filename, 
                       FileMode.Open )) 
            {
                BinaryFormatter bf = new BinaryFormatter();
Int32 num = (Int32) bf.Deserialize(file);
Console.WriteLine(num);
                ProductData p = (ProductData) bf.Deserialize(file); 
                Console.WriteLine(p);
                p = (ProductData) bf.Deserialize(file); 
                Console.WriteLine(p);
                p = (ProductData) bf.Deserialize(file); 
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
