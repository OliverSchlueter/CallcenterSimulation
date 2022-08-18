using System;
using System.Windows.Forms;
using CustomerClient.forms;

namespace CustomerClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            new CustomerClientApp();

            Application.EnableVisualStyles();
            Application.Run(MainForm.Instance);
        }
    }
}