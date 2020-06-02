// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o ExamGraph.cs

using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ExamGraph
{
    static class Program
    {   // Application object:
        [STAThread] //<-Required for WinForms
        static void Main(string[] args)
        {
            string fileimg = args.Length < 1 ?
                strDefImage : args[0];
            Image img = null;
            do try
            {
                img = Image.FromFile(fileimg, true);
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(
                    "Error: file not found " + e.Message,
                    strAppTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                fileimg = ChooseFile(
                    "Select image to show", 
                    "Jpeg images (*.jpg)|*.jpg", "jpg", 
                    "*.jpg", strDefImage );
            }
            while ( img == null );
            Application.EnableVisualStyles();
            Application.Run(new AppWindow(
                strAppTitle, img));
        } // Select file by std-dialog: 
        static string ChooseFile( 
            string title, string filter, 
            string defExt, string fname, 
            string defFile )
        {
            using (OpenFileDialog openFileDialog =
                        new OpenFileDialog())
            {
                openFileDialog.Title = title;
                openFileDialog.InitialDirectory = ".";
                openFileDialog.Filter = filter;
                openFileDialog.FilterIndex = 0;
                openFileDialog.DefaultExt = defExt;
                openFileDialog.FileName = fname;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() ==
                        DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return defFile;
        }
        static readonly string strAppTitle =
            "Graphics sample #3";
        static readonly string strDefImage =
            "imagex.jpg";
    }

    // class of application Window:
    class AppWindow : Form
    {
        Size NcSize;
        ImageWindow ImgWnd;

        public AppWindow(
            string title, Image Aimg)
        {
            // set application title, 
            //   image, width, height:
            Text = title;
            // set required client area and size
            NcSize = new Size(
                Width - ClientSize.Width,
                Height - ClientSize.Height);
            MaximumSize = new Size(
                NcSize.Width + Aimg.Width,
                NcSize.Height + Aimg.Height);
            ClientSize = new Size(640, 480);
            // ImgWnd
            AutoScroll = true;
            ImgWnd = new ImageWindow(Aimg);
            ImgWnd.SetBounds(0, 0,
                Aimg.Width, Aimg.Height);
            Controls.Add(ImgWnd);
            // Center window:
            CenterToScreen();
        }
    }

    class ImageWindow : Control
    {
        Image Img;

        public ImageWindow(Image Aimg)
        {
            // set application title, 
            //   image, width, height:
            Img = Aimg;
            // set required client area and size
            ClientSize = new Size(
                Aimg.Width, Aimg.Height);
            MinimumSize = new Size(
                Aimg.Width, Aimg.Height);
            MaximumSize = MinimumSize;
            // setup event handler:
            Paint += ImageWindow_Paint;
        }
        private void ImageWindow_Paint(
            object sender, PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            Draw(g);
        }

        private void Draw(Graphics g)
        {
            g.DrawImage(Img, 0, 0,
                Img.Width, Img.Height);
        }
    }
}

