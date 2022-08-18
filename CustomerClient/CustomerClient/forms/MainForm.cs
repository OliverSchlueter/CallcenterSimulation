using System;
using System.Windows.Forms;

namespace CustomerClient.forms
{
    public partial class MainForm : Form
    {
        private static MainForm _instance = new MainForm();
        public static MainForm Instance { get => _instance; }
        public MainForm()
        {
            _instance = this;
            InitializeComponent();
        }
    }
}