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

            btn_chatSend.Enabled = false;
            tb_chat.Enabled = false;
            
            _call.CallAcceptedEvent += (sender, args) =>
            {
                lbl_status.Text = $"Status: {args.Status.ToString().ToLower()}";
                lbl_partner.Text = $"Partner: {call.PartnerId}";
                btn_chatSend.Enabled = true;
                tb_chat.Enabled = true;
            };
        }

        private void CallForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_call.CallStatus == CallStatus.InCall)
                    _call.HangUp();
        }

        private void btn_hangUp_Click(object sender, EventArgs e)
        {
            // closing will cause to run CallForm_FormClosing
            _call.HangUp();
        }

        private void btn_chatSend_Click(object sender, EventArgs e)
        {
            string text = tb_chat.Text;

            if (text.Length == 0)
                return;

            rtb_chatLog.Text += $"You: {text}\n";
            tb_chat.Text = "";
            
            _call.SendMessage($"chat:{text}");
        }
    }
}