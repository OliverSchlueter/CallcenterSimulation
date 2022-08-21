using System;
using System.Windows.Forms;
using EmployeeClient.forms;

namespace EmployeeClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new EmployeeClientApp();
            
            Application.EnableVisualStyles();
            Application.Run(MainForm.Instance);
        }
    }
}