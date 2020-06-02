using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MVC_HTML_map
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        Controller controller;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            controller = new Controller(pictureBox1, textBox1);
            controller.model.rectsToDraw = new List<Rectangle>();
            controller.model.add = false;
            controller.model.firstTime = true;

            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = "D:\\220__2\\Games\\!всякая фигня 2";
            openFileDialog.Filter = "(*.jpg)|*.jpg|(*.png)|*.png|(*.jpeg)|*.jpeg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
        }

        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            controller.PicBoxMouseUp(sender, e);
        }

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            controller.PicBoxMouseDown(sender, e);
        }

        private void Selection_Paint(object sender, PaintEventArgs e)
        {
            controller.SelectionPaint(sender, e);
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            controller.PicBoxMouseMove(sender, e);
        }

        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            controller.PicBoxPaint(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void Button1_Click(object sender, EventArgs e)
        {
            controller.view.ShowOpenfileDialog(openFileDialog);
            controller.model.ImgPath = openFileDialog.FileName;
            controller.view.pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            controller.CreateLinkRect();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            controller.SaveMap();
        }

        public class Controller
        {
            public View view;
            public Model model;

            public Controller(PictureBox pictureBox, TextBox textBox)
            {
                view = new View(pictureBox, textBox);
                model = new Model();
            }
            public Rectangle GetSelRectangle(Point location)
            {
                int deltaX = location.X - model.orig.X, deltaY = location.Y - model.orig.Y;
                Size s = new Size(Math.Abs(deltaX), Math.Abs(deltaY));
                Rectangle rect = new Rectangle();
                if (deltaX >= 0 & deltaY >= 0)
                    rect = new Rectangle(model.orig, s);
                if (deltaX < 0 & deltaY > 0)
                    rect = new Rectangle(location.X, model.orig.Y, s.Width, s.Height);
                if (deltaX < 0 & deltaY < 0)
                    rect = new Rectangle(location, s);
                if (deltaX > 0 & deltaY < 0)
                    rect = new Rectangle(model.orig.X, location.Y, s.Width, s.Height);
                return rect;
            }

            public void PicBoxPaint(object sender, PaintEventArgs e)
            {
                if (model.add)
                {
                    model.rectsToDraw.Add(model.probablyTheOne);
                    model.add = false;
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Black, model.selRect);
                }
                if (model.rectsToDraw != null)
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        for (int i = 0; i < model.rectsToDraw.Count; ++i)
                        {
                            e.Graphics.DrawRectangle(pen, model.rectsToDraw[i]);
                        }
                    }
                }
            }
            public void PicBoxMouseUp(object sender, MouseEventArgs e)
            {
                view.pictureBox1.Paint -= SelectionPaint;
                view.pictureBox1.Paint += PicBoxPaint;
                model.probablyTheOne = model.selRect;
                view.pictureBox1.Invalidate();
            }
            public void PicBoxMouseDown(object sender, MouseEventArgs e)
            {
                view.pictureBox1.Paint -= PicBoxPaint;
                view.pictureBox1.Paint += SelectionPaint;
                model.orig = e.Location;
            }
            public void PicBoxMouseMove(object sender, MouseEventArgs e)
            {
                model.selRect = GetSelRectangle(e.Location);
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    (sender as PictureBox).Refresh();
            }
            public void SelectionPaint(object sender, PaintEventArgs e)
            {
                e.Graphics.DrawRectangle(model.pen, model.selRect);
            }
            public void CreateLinkRect()
            {
                for (int i = 0; i < model.rectsToDraw.Count; ++i)
                {
                    if (model.probablyTheOne.IntersectsWith(model.rectsToDraw[i]))
                    {
                        view.ShowMessage("Error", "Your rectangle intersects with others!\nChose another");
                        return;
                    }
                }
                model.writer = new StreamWriter(File.Open("HTMLMAP.html", FileMode.Append));
                int imgHeight = Image.FromFile(model.ImgPath).Height,
                    imgWidth = Image.FromFile(model.ImgPath).Width;
                if (model.firstTime)
                {
                    string[] massStr1 = { "<!DOCTYPE HTML PUBLIC>\n<html>\n<head>",
                "<meta http-equiv = \"Content-Type\" content = \"text/html; charset = utf - 8\">",
                " <title> Карта - изображение </title></head><body>",
                "<p><img src = \"" + model.ImgPath + "\" width = \""+ imgWidth +"\" height = \""+ imgHeight +"\" usemap = \"#map\" alt = \"Навигация\" ></p>",
                "<p><map name = \"map\">"
                };
                    for (int i = 0; i < massStr1.Length; ++i)
                        model.writer.Write(massStr1[i]);
                    model.firstTime = false;
                }
                string[] massStr = { "<area shape = \"rect\" alt = \"Закладка 2\" coords = \"" +
                    model.probablyTheOne.X * imgWidth / view.pictureBox1.Width + ","
                    + model.probablyTheOne.Y * imgHeight / view.pictureBox1.Height + ","
                    + (model.probablyTheOne.X + model.probablyTheOne.Width) * imgWidth / view.pictureBox1.Width + ","
                    + (model.probablyTheOne.Y + model.probablyTheOne.Height) * imgHeight / view.pictureBox1.Height + ","
                    + "\" href = \""
                    + view.textBox1.Text + "\">" };
                for (int i = 0; i < massStr.Length; ++i)
                    model.writer.Write(massStr[i]);
                model.writer.Close();
                model.add = true;
                view.pictureBox1.Invalidate();
            }
            public void SaveMap()
            {
                model.writer = new StreamWriter(File.Open("HTMLMAP.html", FileMode.Append));
                model.writer.Write("</map></p></body></html>");
                model.writer.Close();
                view.ShowMessage("Message", "HTML map successfully created!");
            }
        }

        public class View
        {
            public PictureBox pictureBox1 { get; set; }
            public TextBox textBox1 { get; set; }
            public object sender { get; set; }
            public PaintEventArgs e { get; set; }
            public View(PictureBox picture, TextBox text)
            {
                pictureBox1 = picture;
                textBox1 = text;
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
            public Model()
            {
                ImgPath = "";
                pen = new Pen(Brushes.Blue, 0.8f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            }
            public string ImgPath { get; set; }
            public Rectangle selRect { get; set; }
            public Rectangle probablyTheOne { get; set; }
            public Point orig { get; set; }
            public List<Rectangle> rectsToDraw { get; set; }
            public bool add { get; set; }
            public bool firstTime { get; set; }
            public Pen pen { get; set; }
            public StreamWriter writer { get; set; }
        }

    }
}