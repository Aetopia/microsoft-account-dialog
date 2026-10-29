using System;
using System.Windows.Forms;
using static System.Windows.Forms.UnhandledExceptionMode;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetUnhandledExceptionMode(ThrowException);
        Application.Run(new MainForm());
    }
}