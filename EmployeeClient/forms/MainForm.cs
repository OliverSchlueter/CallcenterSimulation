using System;
using System.Windows.Forms;

namespace EmployeeClient.forms
{
    public partial class MainForm : Form
    {
        private static MainForm _instance = new MainForm();
        public static MainForm Instance => _instance;
        
        private MainForm()
        {
            InitializeComponent();

            cb_channel.Items.Add("Support");
            cb_channel.Items.Add("Apply");
            
            cb_channel.Text = cb_channel.Items[0].ToString();
        }

        private void cb_channel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_call_Click(object sender, EventArgs e)
        {
            EmployeeClientApp.Instance.CallHandler.Call(cb_channel.Text);
        }
    }
}