using System;
using System.Windows.Forms;

namespace CustomerClient.forms
{
    public partial class MainForm : Form
    {
        private static MainForm _instance = new MainForm();
        public static MainForm Instance  => _instance;
        public MainForm()
        {
            _instance = this;
            InitializeComponent();
        }

        private void btn_call_Click(object sender, EventArgs e)
        {
            CustomerClientApp.Instance.CallHandler.Call(cb_channel.Text);
        }

        private void cb_channel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}