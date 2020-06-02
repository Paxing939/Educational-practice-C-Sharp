// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o Contacts.cs
using System;
using System.Collections;

namespace Contacts
{
    class Contact : 
        IComparable, IComparer, IDisposable
    {
        public static string[] Names = {
          "*Contact", "*Mobile", "Work", "Home", 
          "E-mail", "WWW-page", "Address"
        };
        private static int _sortBy = 0;
        public static int SortBy {
            get { return _sortBy; }
            set {
                if (value >= Names.Length || 
                    value < 0) {
                    throw new 
                        IndexOutOfRangeException();
                }
                _sortBy = value;
            }
        }
        public virtual bool ValidName(string str) 
            { return true; }
        public virtual bool ValidPhone(string str) 
            { return true; }
        public virtual bool ValidAddress(string str)
            { return true; }
        public virtual bool ValidEMail(string str)
            { return true; }
        public virtual bool ValidWWWPage(string str)
            { return true; }

        private string[] Areas = null;
        public int Length 
            { get { return Areas.Length; }}
        public string this[int idx] 
        {   get {
                if (idx >= Length || idx < 0) {
                    throw new 
                        IndexOutOfRangeException();
                }
                return Areas[idx];
            }
            set {
                if (idx >= Length || idx < 0) {
                    throw new 
                        IndexOutOfRangeException();
                }
                if((idx == 0 && 
                     ValidName(value)== false)||
                   (idx > 0 && idx < 4 &&
                     ValidPhone(value)== false)||
                   (idx == 4 && 
                     ValidEMail(value)== false)||
                   (idx == 5 && 
                     ValidWWWPage(value)== false)){
                    throw new ArgumentException();
                }
                Areas[idx] = value;
            }
        }
        //IDisposable
        public void Dispose()
        {
            if (Areas != null)
            {
                for (int i = 0; i < Length; i++)
                {
                    Areas[i] = null;
                }
                Areas = null;
            }
            GC.SuppressFinalize(this);
        }
        //IComparable
        public int CompareTo(Object y)
        {
            Contact cy = (Contact)y;
            return this.Areas[Contact.SortBy]. 
                   CompareTo(
                       cy.Areas[Contact.SortBy]);
        }
        //IComparer
        public int Compare(Object x, Object y)
        {
            return ((Contact)x).CompareTo(y);
        }
        public override string ToString()
        {
            if (Areas == null)
            {
                return "||||||";
            }
            string res = Areas[0];
            for (int i = 1; i < Areas.Length; i++)
            {
                res += "|" + Areas[i];
            }
            return res;
        }

        // constructors:
        private Contact() { }
        private void Setup(string[] args)
        {
            if (args.Length < 2 || 
                args.Length > Names.Length){
                throw new ArgumentException();
            }
            Areas = new string[Names.Length];
            int i = 0;
            for (; i < args.Length; i++) {
                this[i] = args[i];
            }

            while (i < Names.Length)
            {
                Areas[i++] = "";
            }
        }
        public Contact(string str)
        {
            string[] args = str.Split( 
                /*new char[] {*/ '|' /*}*/ );
            Setup(args);
        }
        public Contact(params string[] args)
        {
            Setup(args);
        }
       //format strings for area printout
        public static string Fmt(int i)
        {
            return (i == 4 || i == 5) 
                ? "{0,-17}" : "{0,-9}";
        }
    }

    class Program
    {
        static void SortAndPrint(ArrayList pl)
        // simple printout
        {

            Console.WriteLine(
                "----- Sorted by: {0}",
                Contact.Names[Contact.SortBy]);
            pl.Sort((Contact)pl[0]);
            foreach (Contact c in pl) {
                for (int i = 0; i < c.Length; i++) {
                    string str = c[i];
                    if (str.Length > 0) {
                        Console.Write("{0}: {1} ",
                            Contact.Names[i], str);
                    }
                }
                Console.WriteLine();
            }
        }
        static void SortAndPrint(
            ArrayList pl, IComparer pc)
        {
            Console.WriteLine(
                "----- Sorted by: {0}", 
                 Contact.Names[Contact.SortBy]);
            pl.Sort(pc);
            int n = 0;
            foreach (string str in Contact.Names) {
                Console.Write(
                   Contact.Fmt(n++), str);
            }
            Console.WriteLine();
            foreach (Contact c in pl) {
                for (int i = 0; i < c.Length; i++)
                {
                    string str = c[i];
                    Console.Write(
                      Contact.Fmt(i),(str.Length > 0 
                         ? str : " "));
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {

            using (Contact pc = new Contact(
"Joahn|1234567|9876543||joahn@gmail.com||")
            )
            {
                ArrayList pl = new ArrayList(3);
                pl.Add(pc);
                pl.Add(new Contact(
"Ann|2345678|8765432||nann@gmail.com||"));
                pl.Add(new Contact( 
"Mary","3456789","7654321","","mary@gmail.com"));

                Contact.SortBy = 0;
                SortAndPrint(pl, pc);
                Contact.SortBy = 1;
                SortAndPrint(pl, pc);
                Contact.SortBy = 2;
                SortAndPrint(pl, pc);
                Contact.SortBy = 4;
                SortAndPrint(pl, pc);
            }
        }
    }
}
