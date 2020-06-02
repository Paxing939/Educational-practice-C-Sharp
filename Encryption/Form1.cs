using System;
using System.Windows.Forms;
using System.IO;

namespace Encryption
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        bool writeKey = true;
        public Form1()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            InitializeComponent();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
            string filePathOrig = openFileDialog.FileName;
            int indFName = filePathOrig.LastIndexOf('\\');
            string fileName = filePathOrig.Substring(indFName + 1, filePathOrig.Length - 1 - indFName);

            StandMessageBox("Message", "Chose new folder");

            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            string filePathNew = fbd.SelectedPath;
            if (filePathNew == filePathOrig.Substring(0, filePathOrig.Length - fileName.Length - 1))
            {
                StandMessageBox("Message", "You have chosen similar folders!");
                return;
            }

            Random rnd = new Random();
            byte[] encData = new byte[512], key = new byte[128];
            rnd.NextBytes(key);

            var reader = new BinaryReader(File.Open(filePathOrig, FileMode.Open));
            if (writeKey)
            {
                File.WriteAllBytes(filePathNew + fileName + ".key", key);
            }
            var writer = new BinaryWriter(File.Open(filePathNew + fileName, FileMode.Append));
            int n = 0;
            while (reader.PeekChar() > -1)
            {
                n = reader.Read(encData, 0, 512);
                for (int i = 0, j = 0; i < n; ++i, ++j)
                {
                    encData[i] ^= key[j];
                    if(j == 127)
                    {
                        j = 0;
                    }
                }
                writer.Write(encData, 0, n);
            }
            writer.Close();
            reader.Close();
            StandMessageBox("Message", "File successfully encrypted!");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
            string filePathOrig = openFileDialog.FileName;
            int indFName = filePathOrig.LastIndexOf('\\');
            string fileName = filePathOrig.Substring(indFName + 1, filePathOrig.Length - 1 - indFName);

            StandMessageBox("Message", "Chose file with key");

            if (openFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
            string keyPathOrig = openFileDialog.FileName;

            StandMessageBox("Message", "Chose new folder");

            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            string filePathNew = fbd.SelectedPath;
            string tmp = filePathOrig.Substring(0, filePathOrig.Length - fileName.Length - 1);
            if (filePathNew == filePathOrig.Substring(0, filePathOrig.Length - fileName.Length - 1))
            {
                StandMessageBox("Message", "You have chosen similar folders!");
                return;
            }

            byte[] decipData = new byte[512], key = new byte[128];

            if (writeKey)
            {
                key = File.ReadAllBytes(keyPathOrig);
            }

            var reader = new BinaryReader(File.Open(filePathOrig, FileMode.Open));
            var writer = new BinaryWriter(File.Open(filePathNew + '\\' + fileName, FileMode.Append));
            int n = 0;
            while (reader.PeekChar() > -1)
            {
                n = reader.Read(decipData, 0, 512);
                for (int i = 0, j = 0; i < n; ++i, ++j)
                {
                    decipData[i] ^= key[j];
                    if (j == 127)
                    {
                        j = 0;
                    }
                }
                writer.Write(decipData, 0, n);
            }
            writer.Close();
            reader.Close();
            StandMessageBox("Message", "File successfully deciphered!");
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            writeKey = true;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            writeKey = false;
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
