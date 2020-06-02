// ���������� � MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:winexe /platform:x86 /o ExampleWinForms1.cs
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
namespace ExampleWinForms1
{   // Application object:
    class Program
    {	[STAThread] //<-Required for WinForms
        static void Main(string[] args)
        {
            Application.Run(new AppWindow(
				"Hello world!", 300, 200));
        }
    }
    // ������� ���� ����������:
    class AppWindow : Form
    {   // constructor:
        public AppWindow(string title, 
            int width, int height)
        {// ��������� � ������� ����:
            Text = title;
            if (width > 0)
            {
                Width = width;
            }
            if (height > 0)
            {
                Height = height;
            }
            // ��������� ������� ����:
            MinimumSize = new Size(
                Width, Height);
            MaximumSize = new Size(
                Width, Height);
            // ���������� ����:
            CenterToScreen();
}}}

