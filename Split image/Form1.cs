using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Split_image
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
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
            string picPath = openFileDialog.FileName;
            pictureBox1.Image = Image.FromFile(picPath);
            int indOfFName = picPath.LastIndexOf('\\');

            Directory.CreateDirectory(picPath.Substring(0, indOfFName + 1) + "Icons");
            int rows = int.Parse(textBox2.Text.ToString()), cols = int.Parse(textBox1.Text.ToString());
            string filename;
            using (var src = Bitmap.FromFile(picPath) as Bitmap)
            {
                using (var dst = new Bitmap(src.Width / cols, src.Height / rows, src.PixelFormat))
                using (var gfx = Graphics.FromImage(dst))
                    for (int y = 0; y < rows; y++)
                    {
                        for (int x = 0; x < cols; x++)
                        {
                            var rec = new Rectangle(x * dst.Width, y * dst.Height, dst.Width, dst.Height);
                            gfx.DrawImage(src, 0, 0, rec, GraphicsUnit.Pixel);
                            filename = string.Format(picPath.Substring(0, indOfFName + 1) + @"Icons\{0:000}x{1:000}.jpg", y + 1, x + 1);
                            dst.Save(filename);
                        }
                    }
            }
            MessageBox.Show("Images were put in the folder\n" 
                    + picPath.Substring(0, indOfFName + 1) + "Icons", "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
