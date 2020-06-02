using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace HTML_map
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        List<Rectangle> rectsToDraw = new List<Rectangle>() { };
        Rectangle selRect , probablyTheOne;
        Point orig;
        Pen pen = new Pen(Brushes.Blue, 0.8f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
        bool add = false, firstTime = true;
        string filePath;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.MouseDown += pictureBox1_MouseDown;

            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = "D:\\220__2\\Games\\!всякая фигня 2";
            openFileDialog.Filter = "(*.jpg)|*.jpg|(*.png)|*.png|(*.jpeg)|*.jpeg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
        }

        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Возвращаем основную процедуру рисования
            pictureBox1.Paint -= Selection_Paint;
            pictureBox1.Paint += pictureBox1_Paint;
            probablyTheOne = selRect;
            //draw = false;
            pictureBox1.Invalidate();
        }

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //Назначаем процедуру рисования при выделении
            pictureBox1.Paint -= pictureBox1_Paint;
            pictureBox1.Paint += Selection_Paint;
            orig = e.Location;
        }

        //Рисование мышкой с нажатой кнопкой
        private void Selection_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(pen, selRect);
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //при движении мышкой считаем прямоугольник и обновляем picturebox
            selRect = GetSelRectangle(orig, e.Location);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                (sender as PictureBox).Refresh();
        }

        //основное событие рисования
        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (add)
            {
                rectsToDraw.Add(probablyTheOne);
                add = false;
            }
            else
            {
                e.Graphics.DrawRectangle(Pens.Black, selRect);
            }
            if (rectsToDraw != null)
            using (Pen pen = new Pen(Color.Red, 2))
            {
                for (int i = 0; i < rectsToDraw.Count; ++i)
                {
                    e.Graphics.DrawRectangle(pen, rectsToDraw[i]);
                }
            }
        }

        Rectangle GetSelRectangle(Point orig, Point location)
        {
            int deltaX = location.X - orig.X, deltaY = location.Y - orig.Y;
            Size s = new Size(Math.Abs(deltaX), Math.Abs(deltaY));
            Rectangle rect = new Rectangle();
            if (deltaX >= 0 & deltaY >= 0)
                rect = new Rectangle(orig, s);
            if (deltaX < 0 & deltaY > 0)
                rect = new Rectangle(location.X, orig.Y, s.Width, s.Height);
            if (deltaX < 0 & deltaY < 0)
                rect = new Rectangle(location, s);
            if (deltaX > 0 & deltaY < 0)
                rect = new Rectangle(orig.X, location.Y, s.Width, s.Height);
            return rect;
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
            filePath = openFileDialog.FileName;
            pictureBox1.Image = Image.FromFile(filePath);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rectsToDraw.Count; ++i)
            {
                if (probablyTheOne.IntersectsWith(rectsToDraw[i]))
                {
                    StandMessageBox("Error", "Your rectangle intersects with others!\nChose another");
                    return;
                }
            }
            StreamWriter writer = new StreamWriter(File.Open("HTMLMAP.html", FileMode.Append));
            int imgHeight = Image.FromFile(filePath).Height, imgWidth = Image.FromFile(filePath).Width;
            if (firstTime)
            {
                string[] massStr1 = { "<!DOCTYPE HTML PUBLIC>\n<html>\n<head>",
                "<meta http-equiv = \"Content-Type\" content = \"text/html; charset = utf - 8\">",
                " <title> Карта - изображение </title></head><body>",
                "<p><img src = \"" + filePath + "\" width = \""+ imgWidth +"\" height = \""+ imgHeight +"\" usemap = \"#map\" alt = \"Навигация\" ></p>",
                "<p><map name = \"map\">"
                };
                for (int i = 0; i < massStr1.Length; ++i)
                    writer.Write(massStr1[i]);
                firstTime = false;
            }
            string[] massStr = { "<area shape = \"rect\" alt = \"Закладка 2\" coords = \"" + 
                    probablyTheOne.X * imgWidth / pictureBox1.Width + ","
                    + probablyTheOne.Y * imgHeight / pictureBox1.Height + ","
                    + (probablyTheOne.X + probablyTheOne.Width) * imgWidth / pictureBox1.Width + ","
                    + (probablyTheOne.Y + probablyTheOne.Height) * imgHeight / pictureBox1.Height + ","
                    + "\" href = \"" 
                    + textBox1.Text + "\">" };
            for (int i = 0; i < massStr.Length; ++i)
                writer.Write(massStr[i]);
            writer.Close();
            add = true;
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(File.Open("HTMLMAP.html", FileMode.Append));
            writer.Write("</map></p></body></html>");
            writer.Close();
            StandMessageBox("Message", "HTML map successfully created");
            this.Close();
        }

        public void StandMessageBox(string title, string strText)
        {
            MessageBox.Show(strText, title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
