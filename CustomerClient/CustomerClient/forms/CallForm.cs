using System;
using System.Windows.Forms;
using CustomerClient.Calls;

namespace CustomerClient.forms
{
    public partial class CallForm : Form
    {
        private Call _call;
        public CallForm(Call call)
        {
            InitializeComponent();
            _call = call;
            lbl_channel.Text = $"Channel: {_call.Channel}";
            lbl_status.Text = $"Status: {call.CallStatus.ToString().ToLower()}";

            _call.CallAcceptedEvent += (sender, args) =>
                lbl_status.Text = $"Status: {args.Status.ToString().ToLower()}";
        }

        private void CallForm_FormClosing(object sender, FormClosingEventArgs e)
        {
         _call.HangUp();   
        }

        private void btn_hangUp_Click(object sender, EventArgs e)
        {
            // closing will cause to run CallForm_FormClosing
            Close();
        }
    }
}