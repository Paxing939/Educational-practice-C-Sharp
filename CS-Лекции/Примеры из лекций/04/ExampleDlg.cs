// Sample from MSDN:
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ExampleDlg
{

// Application object:
class Program
{
    static void Main()
    {
        DialogResult res = CreateExamDlg();
        MessageBox.Show( "Dialog returns " + res.ToString(), "Example Dialog Box" );
    }

    static public DialogResult CreateExamDlg()
    {
        // Create a new instance of the form.
        Form formDlg = new Form();
        // Create two buttons to use as the accept and cancel buttons.
        Button btnOK = new Button ();
        Button btnCancel = new Button ();
        Button btnAbort = new Button ();
        Button btnRetry = new Button ();
        Button btnIgnore = new Button ();
        Button btnYes = new Button ();
        Button btnNo = new Button ();

        // Set the text of btnOK to "OK".
        btnOK.Text = "OK";
        // Set the position of the button on the form.
        btnOK.Location = new Point (10, 10);
        btnOK.DialogResult = DialogResult.OK;
        // Set the text of btnCancel to "Cancel".
        btnCancel.Text = "Cancel";
        // Set the position of the button based on the location of btnOK.
        btnCancel.Location
            = new Point (btnOK.Left, btnOK.Height + btnOK.Top + 10);
        btnCancel.DialogResult = DialogResult.Cancel;

        // Set the text of btnAbort to "Abort".
        btnAbort.Text = "Abort";
        // Set the position of the button based on the location of btnOK.
        btnAbort.Location
            = new Point (btnCancel.Left, btnCancel.Height + btnCancel.Top + 10);
        btnAbort.DialogResult = DialogResult.Abort;

        // Set the text of btnRetry to "Retry".
        btnRetry.Text = "Retry";
        // Set the position of the button based on the location of btnOK.
        btnRetry.Location
            = new Point (btnAbort.Left, btnAbort.Height + btnAbort.Top + 10);
        btnRetry.DialogResult = DialogResult.Retry;

        // Set the text of btnIgnore to "Ignore".
        btnIgnore.Text = "Ignore";
        // Set the position of the button based on the location of btnOK.
        btnIgnore.Location
            = new Point (btnRetry.Left, btnRetry.Height + btnRetry.Top + 10);
        btnIgnore.DialogResult = DialogResult.Ignore;

        // Set the text of btnYes to "Yes".
        btnYes.Text = "Yes";
        // Set the position of the button based on the location of btnOK.
        btnYes.Location
            = new Point (btnIgnore.Left, btnIgnore.Height + btnIgnore.Top + 10);
        btnYes.DialogResult = DialogResult.Yes;

        // Set the text of btnNo to "No".
        btnNo.Text = "No";
        // Set the position of the button based on the location of btnOK.
        btnNo.Location
            = new Point (btnYes.Left, btnYes.Height + btnYes.Top + 10);
        btnNo.DialogResult = DialogResult.No;

        // Set the caption bar text of the form.
        formDlg.Text = "Example Dialog Box";
        // Display a help button on the form.
        formDlg.HelpButton = true;

        // Define the border style of the form to a dialog box.
        formDlg.FormBorderStyle = FormBorderStyle.FixedDialog;
        // Set the MaximizeBox to false to remove the maximize box.
        formDlg.MaximizeBox = false;
        // Set the MinimizeBox to false to remove the minimize box.
        formDlg.MinimizeBox = false;
        // Set the accept button of the form to btnOK.
        formDlg.AcceptButton = btnOK;
        // Set the cancel button of the form to btnCancel.
        formDlg.CancelButton = btnCancel;
        // Set the start position of the form to the center of the screen.
        formDlg.StartPosition = FormStartPosition.CenterScreen;

        // Add btnOK to the form.
        formDlg.Controls.Add(btnOK);
        // Add btnCancel to the form.
        formDlg.Controls.Add(btnCancel);
        formDlg.Controls.Add(btnAbort);
        formDlg.Controls.Add(btnRetry);
        formDlg.Controls.Add(btnIgnore);
        formDlg.Controls.Add(btnYes);
        formDlg.Controls.Add(btnNo);

        // Display the form as a modal dialog box.
        return formDlg.ShowDialog();
    }
}


}
