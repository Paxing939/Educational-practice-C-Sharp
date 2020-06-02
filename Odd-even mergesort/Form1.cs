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

namespace Odd_even_mergesort
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        char[] mass = new char[256];
        List<string> fileText;
        string filePath;
        public Form1()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();

            InitializeComponent();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
            filePath = openFileDialog.FileName;
            string[] fileText = File.ReadAllLines(filePath);
            if (fileText.Length == 0)
            {
                StandMessageBox("Error", "Chosen file is empty!");
                return;
            }
            OddEvenMergeSorter s = new OddEvenMergeSorter();
            s.sort(fileText);
            File.WriteAllLines(filePath, fileText);
            StandMessageBox("Message", "Text in the file has been successfully sorted!");
        }
        
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
        public class OddEvenMergeSorter //implements Sorter
        {
            private string[] a;

            public void sort(string[] a)
            {
                this.a = a;
                oddEvenMergeSort(0, a.Length - 1);
            }

            /* sorts a piece of length n of the array
               starting at position lo
             */
            private void oddEvenMergeSort(int lo, int n)
            {
                if (n > 1)
                {
                    int m = n / 2;
                    oddEvenMergeSort(lo, m);
                    oddEvenMergeSort(lo + m, m);
                    oddEvenMerge(lo, n, 1);
                }
            }

            /* lo is the starting position and
              n is the length of the piece to be merged,
              r is the distance of the elements to be compared
            */
            private void oddEvenMerge(int lo, int n, int r)
            {
                int m = r * 2;
                if (m < n)
                {
                    oddEvenMerge(lo, n, m);      // even subsequence
                    oddEvenMerge(lo + r, n, m);    // odd subsequence
                    for (int i = lo + r; i + r < lo + n; i += m)
                        compare(i, i + r);
                }
                else
                    compare(lo, lo + r);
            }      

            private void compare(int i, int j)
            {
                    
                if (FuncCompare(a[i], a[j]))
                    exchange(i, j);
            }

            private void exchange(int i, int j)
            {
                string t = a[i];
                a[i] = a[j];
                a[j] = t;
            }
            public bool FuncCompare(string left, string right)
            {
                if (left[0] >= right[0])
                {
                    return true;
                }
                else
                {
                    if (left[0] == right[0])
                    {
                        if (left[1] >= right[1])
                            return true;
                        else
                        {
                            if (left[1] == right[1])
                            {
                                if (left[2] >= right[2])
                                    return true;
                                else
                                    return false;
                            }
                            else return false;
                        }
                    }
                    else return false;
                }
            }
        }    // end class OddEvenMergeSorter
    }
}
