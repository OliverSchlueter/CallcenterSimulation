using System;
using System.Windows.Forms;
using EmployeeClient.calls;

namespace EmployeeClient.forms
{
    public partial class CallForm : Form
    {
        private Call _call;
        public CallForm(Call call)
        {
            InitializeComponent();

            _call = call;
            _call.CallForm = this;
            lbl_channel.Text = $"Channel: {_call.Channel}";
            lbl_status.Text = $"Status: {call.CallStatus.ToString().ToLower()}";
        }

        private void CallForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_call.CallStatus == CallStatus.InCall)
                _call.HangUp();
        }

        private void btn_hangUp_Click(object sender, EventArgs e)
        {
            // closing will cause to run CallForm_FormClosing
            Close();
        }
    }
}