using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MVC_Split_image
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        public Form1()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            InitializeComponent();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller(pictureBox1);

            controller.view.ShowOpenfileDialog(openFileDialog);
            controller.FillRowsCols(textBox1, textBox2);
            controller.model.ImgPath = openFileDialog.FileName;
            controller.view.ShowPicture(Image.FromFile(controller.model.ImgPath));
            int indOfFName = controller.SplitImage();
            controller.view.ShowMessage("Message", "Images were put in the folder\n"
                    + controller.model.ImgPath.Substring(0, indOfFName + 1) + "Icons");
        }

        public class Controller
        {
            public View view;
            public Model model;

            public Controller(PictureBox pictureBox)
            {
                view = new View(pictureBox);
                model = new Model();
            }

            public void FillRowsCols(TextBox textBox1, TextBox textBox2)
            {
                model.Rows = int.Parse(textBox2.Text.ToString());
                model.Cols = int.Parse(textBox1.Text.ToString());
            }

            public int SplitImage()
            {
                int indOfFName = model.ImgPath.LastIndexOf('\\');

                Directory.CreateDirectory(model.ImgPath.Substring(0, indOfFName + 1) + "Icons");
                string filename;
                using (var src = Bitmap.FromFile(model.ImgPath) as Bitmap)
                {
                    using (var dst = new Bitmap(src.Width / model.Cols, src.Height / model.Rows, src.PixelFormat))
                    using (var gfx = Graphics.FromImage(dst))
                        for (int y = 0; y < model.Rows; y++)
                        {
                            for (int x = 0; x < model.Cols; x++)
                            {
                                var rec = new Rectangle(x * dst.Width, y * dst.Height, dst.Width, dst.Height);
                                gfx.DrawImage(src, 0, 0, rec, GraphicsUnit.Pixel);
                                filename = string.Format(model.ImgPath.Substring(0, indOfFName + 1) + @"Icons\{0:000}x{1:000}.jpg", y + 1, x + 1);
                                dst.Save(filename);
                            }
                        }
                }
                return indOfFName;
            }
        }

        public class View
        {
            private PictureBox pictureBox1 {get; set;}
            public View(PictureBox picture)
            {
                pictureBox1 = picture;
            }
            public void ShowPicture(Image image)
            {
                pictureBox1.Image = image;
            }

            public void ShowOpenfileDialog(OpenFileDialog openFileDialog)
            {
                if (openFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
            }
            public void ShowMessage(string title, string text)
            {
                MessageBox.Show(text, title,
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public class Model
        {
            private string mImgPath;
            private int mRows, mCols;
            public Model()
            {
                mImgPath = "";
                mRows = mCols = 0;
            }
            public string ImgPath { get; set; }
            public int Rows { get; set; }
            public int Cols { get; set; }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
