using System;
using System.Windows.Forms;
using CustomerClient.forms;

namespace CustomerClient
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CustomerClientApp app = new CustomerClientApp();

            Application.EnableVisualStyles();
            Application.Run(MainForm.Instance);
        }
    }
}