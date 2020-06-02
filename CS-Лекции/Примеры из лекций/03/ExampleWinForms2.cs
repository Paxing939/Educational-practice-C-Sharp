// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o ExampleWinForms2.cs

/* Example application Windows Forms #2
 * Demonstrates: Menu, StatusLine, Client area, 
 *               Resize event, Mouse events
 * Written by Sergey Gutnikov
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
namespace ExampleWinForms2 {
    static class Program  {
        [STAThread] //<-Required for WinForms
        static void Main() {
            Application.EnableVisualStyles();

            Application.Run(
                new AppWindow(
                    "Hello world!", 300, 300));
        }
    }
    class AppWindow : Form
    {   // event handler for FormClosing
        private void AppWindow_FormClosing(
            Object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show( 
                "Are you sure to exit?",
                         Text, // message tittle
                    MessageBoxButtons.YesNo, 
                    // message buttons
                    MessageBoxIcon.Question)	
                    // message icon
                 != DialogResult.Yes)		
                    // result
            {
                e.Cancel = true;
            }
        }

        // event handler for Resize:

        private void AppWindow_Resize(
            object sender, System.EventArgs e)
        {
            ArrangeClientArea();
        }

        // constructor:
        public AppWindow(string title, 
            int width, int height )
        {
            // setup event handler:
            FormClosing += AppWindow_FormClosing;
            Resize += AppWindow_Resize;
            // set application title, width, height:

            Text = title;
            if (width > 0) Width = width; 
            if (height > 0) Height = height;
            // Center window:
            CenterToScreen();
            // Add controls
            BuildControls();
        }
        private void BuildControls()
        {
            // Add tooltip
            BuildToolTip();
            // Add menu
            BuildMenu();
            // Add StatusLine
            BuildStatusLine();
            // Add button
            BuildButton();
            // Position client area items:
            ArrangeClientArea();
            // Setup minimum of size changing:
            MinimumSize = AppMinSize( 
                NcSize.Width = 
                    (Width - ClientSize.Width + 1),
                NcSize.Height = 
                    (Height - ClientSize.Height + 1));
        }
        private Size NcSize = new Size();

        private Rectangle GetClientRectangle()
        {
            return new Rectangle(
                NcSize.Width / 2,
                mnuMain.Bounds.Bottom,
                Width - NcSize.Width,
                statusStrip.Bounds.Top -
                   mnuMain.Bounds.Bottom );
        }
        private Size AppMinSize(
            int ncWidth,  // non client width
            int ncHeight) // height
        {
  //- a button in client area with size (100,50):
            ncWidth += btnClickMe.Width;
            ncHeight += btnClickMe.Height;
            // plus menu and status height:
            ncHeight += mnuMain.Height +
                statusStrip.Height;
            return new Size(ncWidth, ncHeight);
        }

        private void ArrangeClientArea()
        {
            // center button to client area:
            if (btnClickMe != null)
            {
                btnClickMe.SetBounds(
                   (ClientSize.Width- 
                       btnClickMe.Width)/2 + 1,
                   (ClientSize.Height-
                       btnClickMe.Height)/2 + 1,
                   btnClickMe.Width, 
                   btnClickMe.Height );
            }
        }
        // 1) declare GUI-control variables:
        //members for status line:
        private StatusStrip statusStrip;
        private 
        ToolStripStatusLabel toolStripStatusLabel;

        private void BuildStatusLine()
        {   // 2) set-up GUI-controls:
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new
                ToolStripStatusLabel();
            // freeze layout:
            statusStrip.SuspendLayout();
            SuspendLayout();

            statusStrip.Items.AddRange(
                new ToolStripItem[] {
                    toolStripStatusLabel });
            statusStrip.Location = 
                new Point(0, 248);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(292, 30);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip";

            toolStripStatusLabel.BorderSides = 0; 
            toolStripStatusLabel.BorderStyle =
                Border3DStyle.Flat;
            toolStripStatusLabel.IsLink = false;
            toolStripStatusLabel.Name =
                "toolStripStatusLabel";
            toolStripStatusLabel.Size = 
                new Size(246, 28);

            toolStripStatusLabel.Spring = true; 
                // fill space
            toolStripStatusLabel.Text = "";
            toolStripStatusLabel.Alignment =
                   ToolStripItemAlignment.Left;
            toolStripStatusLabel.TextAlign =
                ContentAlignment.TopLeft;

            // 3) Insert GUI-control to 
            //       Form's ControlCollection
            Controls.Add(statusStrip);
            // defrost layout:
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();

            ResumeLayout(false);
            PerformLayout();
        }
        // 1) declare GUI-control variables:
        //members for simple menu:
        private MenuStrip mnuMain =
            new MenuStrip(); // all menu-system
        private ToolStripMenuItem mnuFile = new 
            ToolStripMenuItem(); // pop-down menu
        private ToolStripMenuItem mnuFileChFolder =
             new ToolStripMenuItem(); 
             // pop-down menu item
        private ToolStripMenuItem mnuFileOpenFile =
             new ToolStripMenuItem(); 
             // pop-down menu item

        private ToolStripMenuItem 
            mnuFileSaveAsFile =
             new ToolStripMenuItem(); 
             // pop-down menu item
        private ToolStripMenuItem mnuFileExit =
             new ToolStripMenuItem(); 
             // pop-down menu item
        private ToolStripMenuItem mnuHelp =
             new ToolStripMenuItem(); 
             // pop-down menu
        private ToolStripMenuItem mnuHelpAbout =
             new ToolStripMenuItem(); 
             // pop-down menu item

        private void BuildMenu()
        {
            // 2) set-up GUI-controls:
            //insert pop-down menu to main menu
            mnuFile.Text = "&File"; 
                //set text with Alt-command: Alt-F
            mnuMain.Items.Add(mnuFile);
            mnuHelp.Text = "&Help"; 
                //set text with Alt-command: Alt-H
            mnuMain.Items.Add(mnuHelp);
            //insert Choose Folder command to 
            //   pop-down menu
            mnuFileChFolder.Text = 
                "&Choose Folder..."; 
                //set text with command: C
            mnuFile.DropDownItems.Add(
                mnuFileChFolder);

            // event for command
            //     (System.EventHandler)
            mnuFileChFolder.Click += 
                (o, s) => OnFileChooseFolder();
            mnuFileChFolder.MouseEnter += 
                (o, s) => 
                  toolStripStatusLabel.Text =
                     "Choose an existing folder";
            mnuFileChFolder.MouseLeave += 
                (o, s) => 
                  toolStripStatusLabel.Text = "";
            //insert Open command to pop-down menu
            mnuFileOpenFile.Text = "&Open"; 
                //set text with command: O
            mnuFile.DropDownItems.Add(
                mnuFileOpenFile);
            // set-up event handler for Open command
            mnuFileOpenFile.Click += 
                (o, s) => OnFileOpen();
            mnuFileOpenFile.MouseEnter += (o, s) => 
                  toolStripStatusLabel.Text =
                     "Open an existing file";
            mnuFileOpenFile.MouseLeave +=
                (o, s) => 
                  toolStripStatusLabel.Text = "";
            //insert SaveAs command to pop-down menu
            mnuFileSaveAsFile.Text = "&Save As"; 
                //set text with command: S
            mnuFile.DropDownItems.Add(
                mnuFileSaveAsFile);
            // event handler for command
            mnuFileSaveAsFile.Click +=
                (o, s) => OnFileSaveAs();
            mnuFileSaveAsFile.MouseEnter += (o,s) => 
                   toolStripStatusLabel.Text =
                     "Save a file with a new name";
            mnuFileSaveAsFile.MouseLeave +=
                 (o, s) => 
                   toolStripStatusLabel.Text = "";
            //insert Separator to pop-down menu
            mnuFile.DropDownItems.Add(
                new ToolStripSeparator());
            //insert Exit command to pop-down menu
            mnuFileExit.Text = "E&xit";
                //set text with command: x
            mnuFile.DropDownItems.Add( mnuFileExit);
            // handler for command
            mnuFileExit.Click += 
                (o, s) => Application.Exit();
            mnuFileExit.MouseEnter += (o, s) => 
                  toolStripStatusLabel.Text =
                     "Exit application...";
            mnuFileExit.MouseLeave += (o, s) => 
                  toolStripStatusLabel.Text = "";
            //insert About command to pop-down menu
            mnuHelpAbout.Text = "&About..."; 
                //set text with command: A
            mnuHelp.DropDownItems.Add(
                mnuHelpAbout);
            // set-up event handler
            mnuHelpAbout.Click += (o, s) =>
                OnHelpAbout();
            mnuHelpAbout.MouseEnter += (o, s) =>
                  toolStripStatusLabel.Text =
                     "Show information about " +
                     " the programm...";
            mnuHelpAbout.MouseLeave += (o, s) => 
                  toolStripStatusLabel.Text = "";
            // add menu to form:
            Controls.Add(mnuMain);
            MainMenuStrip = mnuMain;   
                // special form member to seting-up
        }
        // 1) declare GUI-control variables:
        private Button btnClickMe = new Button();
        private void BuildButton()
        {
            // 2) set-up GUI-controls:
            btnClickMe.Text = "Click Me..."; 
                //set text
            btnClickMe.SetBounds(50, 50, 100, 50);
                //set bounds (x,y,width,height)
            btnClickMe.Click += (o, s) => {
                MessageBox.Show("You've done that!",
                                btnClickMe.Text);
            };
            btnClickMe.MouseEnter += (o, s) => 
                  toolStripStatusLabel.Text =
                    "Click button to action...";
            btnClickMe.MouseLeave += (o, s) => 
                  toolStripStatusLabel.Text = "";
            // Set up the ToolTip text
            toolTip.SetToolTip( 
                btnClickMe as Control, "Click");
            // 3) Insert to ControlCollection
            Controls.Add(btnClickMe);
        }
        ToolTip toolTip = new ToolTip();

        private void BuildToolTip()
        {
            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be 
            //   displayed whether or not 
            //   the form is active.
            toolTip.ShowAlways = true;
        }

        private void OnFileOpen()
        {
            using (OpenFileDialog openFileDialog =
                       new OpenFileDialog())
            {
                openFileDialog.InitialDirectory =
                    Environment.GetFolderPath(
                       Environment.SpecialFolder.
                           Personal); 
                    // My Documents folder.
                openFileDialog.Filter =
   "Database (*.mdb) |*.mdb|All files (*.*) |*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.DefaultExt = "mdb";
                openFileDialog.FileName = "*.mdb";

                openFileDialog.RestoreDirectory =
                    true;
                openFileDialog.Multiselect = 
                    false; 
                    // if true - use FileNames[]
                    //    to get selected
                if (openFileDialog.ShowDialog()==
                        DialogResult.OK)
                {
                    MessageBox.Show( 
                      "File selected: " +
                        openFileDialog.FileName);
                }
            }
        }
        private void OnFileSaveAs()
        {
            using (SaveFileDialog saveFileDialog=
                       new SaveFileDialog())
            {   
                saveFileDialog.InitialDirectory =
                    Environment.GetFolderPath(
                      Environment.SpecialFolder.
                        Personal); 
                        // My Documents folder.
                saveFileDialog.Filter =
"Database (*.mdb) |*.mdb|All files (*.*) |*.*";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.DefaultExt = "mdb";
                saveFileDialog.FileName = "*.mdb";

                saveFileDialog.RestoreDirectory =
                    true;
                if (saveFileDialog.ShowDialog()==
                        DialogResult.OK )
                {
                    MessageBox.Show(
                        "File selected: " +
                        saveFileDialog.FileName);
                }
            }
        }

        private void OnFileChooseFolder()
        {            
	    using (FolderBrowserDialog
                   folderBrowserDialog =
                       new FolderBrowserDialog())
            {
                // Set the help text
                folderBrowserDialog.Description =
      "Select the directory that you want to use";
                // Don't allow to create new files
                folderBrowserDialog.
                    ShowNewFolderButton = false;
                // My Documents folder:
                folderBrowserDialog.SelectedPath = 
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.Personal );
                folderBrowserDialog.RootFolder =			        
                        Environment.SpecialFolder.MyComputer;
                // Show the FolderBrowserDialog.
                if (folderBrowserDialog.
                        ShowDialog(this) ==
                            DialogResult.OK)
                {//SelectedPath=="" for MyComputer
                    MessageBox.Show(
                        "Folder choosen: " +
                         folderBrowserDialog.
                         SelectedPath);
                }
       }}

      private void OnHelpAbout()
      {
            MessageBox.Show(
                "MinForms #2 sample application\n"+
                "Written by Sergey Gutnikov", 	
                "About...",             
                // message tittle
                MessageBoxButtons.OK, 	
                // message buttons
                MessageBoxIcon.Information);
                // message icon
      }
   }
}
