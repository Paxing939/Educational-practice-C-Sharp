// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o ExamGraph2.cs

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExamGraph2
{
    static class Program
    {
        private const int GraphX = 400;
        private const int GraphY = 400;
        [STAThread] //<-Required for WinForms
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.Run(
                   new AppWindow(strAppTitle,
                       GraphX, GraphY));
            }
            catch (Exception e)
            {
                MessageBox.Show(
                     "Error: " + e.Message,
                     strAppTitle,
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                );
            }
        }

        static readonly string strAppTitle =
           "Graphics sample #4";
    }
    // class of application Window:
    class AppWindow : Form
    {
        Size NcSize;
        Random Rand;
        Bitmap Bmp;
        Graphics Buffer;

        public AppWindow(string title,
                         int width, int height)
        {
            Rand = new Random();
            // Buffer:
            Bmp = new Bitmap(width, height);
            Buffer = Graphics.FromImage(Bmp);
            Buffer.FillRectangle(
                new SolidBrush(Color.Black),
                0, 0, Bmp.Width, Bmp.Height);
            // set application title:
            Text = title;
            // set required client area and size
            NcSize = new Size(
                Width - ClientSize.Width,
                Height - ClientSize.Height);
            ClientSize = new Size(width, height);
            MinimumSize = new Size(
                NcSize.Width + width,
                NcSize.Height + height);
            MaximumSize = MinimumSize;
            BackColor = Color.Black;
            InitDraw();
            // setup event handler:
            Paint += AppWindow_Paint;
            Click += AppWindow_Click;
            // Center window:
            CenterToScreen();
        }

        private void AppWindow_Paint(
            object sender, PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            Draw(g);
        }
        private void AppWindow_Click(
            object sender, System.EventArgs e)
        {
            DrawRandomLine(Buffer, true);
        }
        private void Draw(Graphics g)
        {
            Buffer.Flush();
            g.DrawImage(Bmp, 0, 0,
                Bmp.Width, Bmp.Height);

        }
        private void InitDraw()
        {
            for (int i = 0; i < 10; i++)
            {
                DrawRandomLine(Buffer, false);
            }
        }
        private void DrawRandomLine(
            Graphics g, bool bInvalidate)
        {
            int x = Rand.Next(0, ClientSize.Width);
            int y = Rand.Next(0, ClientSize.Height);
            int x1 = Rand.Next(0, ClientSize.Width);

            int y1 =
                Rand.Next(0, ClientSize.Height);

            using (Pen p = new Pen(
                       Color.FromArgb(
                           Rand.Next(0, 0xFF),
                           Rand.Next(0, 0xFF),
                           Rand.Next(0, 0xFF))))
            {
                g.DrawLine(p, x, y, x1, y1);
            }
            if (bInvalidate)
            {
                int n;
                if (x > x1)
                {
                    n = x; x = x1; x1 = n;
                }
                if (y > y1)
                {
                    n = y; y = y1; y1 = n;
                }
                Invalidate(new Rectangle(
                    x, y, x1 - x, y1 - y));
            }
        }
    }
}
