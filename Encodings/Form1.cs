using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encodings
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        string filePath;
        List<string> allLinesText;
        public Form1()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();

            InitializeComponent();

            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 1;
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string strInEncod = this.comboBox1.Text;
            if(strInEncod == "ibm-866") { strInEncod = "IBM866"; }
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
            filePath = openFileDialog.FileName;

            Encoding encoding;
            using (var reader = new System.IO.StreamReader(filePath, true))
            {
                encoding = reader.CurrentEncoding;
            }
            if (encoding != Encoding.GetEncoding(strInEncod))
            {
                StandMessageBox("Error", "Encoding you have chosen and encoding in the file are not the same!");
            }
            allLinesText = File.ReadAllLines(filePath, Encoding.GetEncoding(strInEncod)).ToList();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string strInEncod = this.comboBox2.Text;
            if (strInEncod == "ibm-866") { strInEncod = "IBM866"; }
            if(strInEncod == this.comboBox1.Text)
            {
                StandMessageBox("Error", "Change encoding of output file!");
                return;
            }
            if (filePath != "")
            {
                File.WriteAllLines(filePath, allLinesText, Encoding.GetEncoding(strInEncod));
                StandMessageBox("Error", "Encoding has been changed successfully!");
                filePath = "";
            }
            else
            {
                StandMessageBox("Error", "Chose file!");
            }
        }

        public void StandMessageBox(string title, string strText)
        {
            MessageBox.Show(strText, title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
