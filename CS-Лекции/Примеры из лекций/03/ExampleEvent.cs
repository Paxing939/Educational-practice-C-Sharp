// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o ExampleEvent.cs
using System;
using System.IO;

namespace ExampleEvent
{
        // ������� ��� �������:
    public delegate void TEventFirst( string msg );
    public delegate bool TEventSecond( string msg, int value);
        // ���������� ������ ���� ����������

        // ������� ��������
    public class Sender
    {
            // �������� ������� ��� 
            // ����������� �����������:
        public event TEventFirst evFst;
        public event TEventSecond evSec;

        // � ���� ������ ��������� �������:
        public void DoCalls( string msg, int value )
        {
            Console.WriteLine( "*** Send events for ({0},{1}):", 
                               msg, value );
            if ( evFst != null ) 
                // ��������� ���� �� ����������:
            {
                evFst(msg);
            }
            if (evSec != null) 
                // ��������� ���� �� ����������:
            {
                if ( evSec(msg, value) == false )
                {
                    Console.WriteLine( 
                        "*** Event ({0},{1}) was not processed!",
                        msg, value);
                }
            }
        }    
    }
    
    // ��������� 1:
    public class ReceiverOne
    {
            // ������ �����������:
        public void OnTEventFirst( string msg )
        {
            Console.Write( "[1]: got event: ({0})\n", msg );
        }
        public bool OnTEventSecond( string msg, int value )
        {
            Console.Write( "[1]: got event: ({0},{1})\n", 
                           msg, value);
            return (value < 0) ? false : true;
        }
    } 

    // ��������� 2:
    public class ReceiverTwo
    {
            // ������ �����������:
        public void OnTEventFirst( string msg) 
        { 
            Console.Write( "[2]: got event: ({0})\n", msg );
        }
        public bool OnTEventSecond( string msg, int value )
        {
            Console.Write( "[2]: got event: ({0},{1})\n", 
                           msg, value);
            return (value == 0) ? false : true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int an1_called = 0,
                an2_called = 0;

            Console.WriteLine( "Event example:\n");
 
                // ���������� ����������, �������� ��������:
            Sender snd = new Sender();

            ReceiverOne rec1 = new ReceiverOne();
            ReceiverTwo rec2 = new ReceiverTwo();
 
                // ����������� ������������ �������:
            snd.evFst += rec1.OnTEventFirst;
            snd.evFst += rec2.OnTEventFirst;

                // ����������� ������������ �������:
            snd.evSec += rec1.OnTEventSecond;
            snd.evSec += rec2.OnTEventSecond;
        
                // ���������� �������:
            snd.DoCalls("event #100", 100);
            snd.DoCalls("event #0", 0);
            snd.DoCalls("event #-100", -100);

                // ������� ��������:
            snd.evFst -= rec1.OnTEventFirst;
            snd.evFst -= rec2.OnTEventFirst;

                // ���������� �������:
            snd.DoCalls("event #99", 99);

                // ����������� ������������ �������:
            snd.evFst += delegate(string msg)
                {
                    Console.Write( "[0]: got event: ({0})\n", msg);
                    // ��������� ���������� 
                    // �������� ������ ��������:
                    an1_called++;
                };
           snd.evSec += delegate(string msg, int value)
                {
                    Console.Write( "[0]: got event: ({0},{1})\n", 
                                   msg, value);
                    // ��������� ���������� 
                    // �������� ������ ��������:
                    an2_called++;
                    return true;
                };
           snd.evSec += ((msg, value) => (value != -1));

               // ���������� ��� �������
           snd.DoCalls("event #33", 33);
           snd.DoCalls("event #-1", -1);
               // ������������� ��������� ����������:
           Console.WriteLine( "an1_called = {0}, an2_called = {1}",
               an1_called, an2_called);
       }
   }
}

