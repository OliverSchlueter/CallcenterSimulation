using System;
using System.Windows.Forms;

namespace CustomerClient.forms
{
    public partial class MainForm : Form
    {
        private static MainForm _instance = new ();
        public static MainForm Instance => _instance;
        private MainForm()
        {
            _instance = this;
            InitializeComponent();

            //TODO: get open channels from server
            cb_channel.Items.Add("Support");
            cb_channel.Items.Add("Apply");
            
            cb_channel.Text = cb_channel.Items[0].ToString();
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