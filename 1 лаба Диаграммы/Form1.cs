using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _1_лаба_Диаграммы
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        public Form1()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();

            InitializeComponent();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            colorDialog.FullOpen = true;
            colorDialog.Color = this.BackColor;
        }
        private void Form1_Load(object sender, EventArgs e) { }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = new Color();
            string[] strSectors = new string[10];
            string strQuestion;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
            string filename = openFileDialog.FileName;
            StreamReader istr = new StreamReader(filename);
            strQuestion = istr.ReadLine();
            this.label1.Font = new Font("Georgia", 20);
            this.label1.Text = strQuestion;
            this.chart1.Series["Cars"].Font = new Font("Georgia", 11);
            for (int i =0; !istr.EndOfStream; ++i)
            {
                strSectors[i] = istr.ReadLine();
                string[] words = strSectors[i].Split(new char[] { ' ' });
                this.chart1.Series["Cars"].Points.AddXY(words[0], int.Parse(words[1]));
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.Cancel) { return; }
            this.chart1.Series["Cars"].LabelForeColor = colorDialog.Color;
            this.label1.ForeColor = colorDialog.Color;
        }

        private void fontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.Cancel) { return; }
            this.chart1.Series["Cars"].Font = fontDialog.Font;
            this.label1.Font = fontDialog.Font;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutDialog = new AboutBox1();
            aboutDialog.ShowDialog();
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }
    }
}