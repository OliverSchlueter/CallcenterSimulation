using System.ComponentModel;

namespace CustomerClient.forms
{
    partial class CallForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_channel = new System.Windows.Forms.Label();
            this.btn_hangUp = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.lbl_partner = new System.Windows.Forms.Label();
            this.rtb_chatLog = new System.Windows.Forms.RichTextBox();
            this.tb_chat = new System.Windows.Forms.TextBox();
            this.btn_chatSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_channel
            // 
            this.lbl_channel.AutoSize = true;
            this.lbl_channel.Location = new System.Drawing.Point(12, 42);
            this.lbl_channel.Name = "lbl_channel";
            this.lbl_channel.Size = new System.Drawing.Size(86, 18);
            this.lbl_channel.TabIndex = 0;
            this.lbl_channel.Text = "Channel: N/A";
            // 
            // btn_hangUp
            // 
            this.btn_hangUp.Location = new System.Drawing.Point(12, 316);
            this.btn_hangUp.Name = "btn_hangUp";
            this.btn_hangUp.Size = new System.Drawing.Size(231, 57);
            this.btn_hangUp.TabIndex = 1;
            this.btn_hangUp.Text = "Hang Up";
            this.btn_hangUp.UseVisualStyleBackColor = true;
            this.btn_hangUp.Click += new System.EventHandler(this.btn_hangUp_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(12, 81);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(85, 18);
            this.lbl_status.TabIndex = 2;
            this.lbl_status.Text = "Status: None";
            // 
            // lbl_partner
            // 
            this.lbl_partner.AutoSize = true;
            this.lbl_partner.Location = new System.Drawing.Point(11, 9);
            this.lbl_partner.Name = "lbl_partner";
            this.lbl_partner.Size = new System.Drawing.Size(82, 18);
            this.lbl_partner.TabIndex = 3;
            this.lbl_partner.Text = "Partner: N/A";
            // 
            // rtb_chatLog
            // 
            this.rtb_chatLog.BackColor = System.Drawing.Color.White;
            this.rtb_chatLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_chatLog.Location = new System.Drawing.Point(381, 6);
            this.rtb_chatLog.Name = "rtb_chatLog";
            this.rtb_chatLog.ReadOnly = true;
            this.rtb_chatLog.Size = new System.Drawing.Size(291, 333);
            this.rtb_chatLog.TabIndex = 4;
            this.rtb_chatLog.Text = "";
            // 
            // tb_chat
            // 
            this.tb_chat.Location = new System.Drawing.Point(381, 348);
            this.tb_chat.Name = "tb_chat";
            this.tb_chat.Size = new System.Drawing.Size(224, 25);
            this.tb_chat.TabIndex = 5;
            // 
            // btn_chatSend
            // 
            this.btn_chatSend.Location = new System.Drawing.Point(611, 345);
            this.btn_chatSend.Name = "btn_chatSend";
            this.btn_chatSend.Size = new System.Drawing.Size(61, 30);
            this.btn_chatSend.TabIndex = 6;
            this.btn_chatSend.Text = "Send";
            this.btn_chatSend.UseVisualStyleBackColor = true;
            this.btn_chatSend.Click += new System.EventHandler(this.btn_chatSend_Click);
            // 
            // CallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 387);
            this.Controls.Add(this.btn_chatSend);
            this.Controls.Add(this.tb_chat);
            this.Controls.Add(this.rtb_chatLog);
            this.Controls.Add(this.lbl_partner);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.btn_hangUp);
            this.Controls.Add(this.lbl_channel);
            this.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "CallForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CallForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public System.Windows.Forms.RichTextBox rtb_chatLog;
        private System.Windows.Forms.TextBox tb_chat;
        private System.Windows.Forms.Button btn_chatSend;

        private System.Windows.Forms.Label lbl_partner;

        private System.Windows.Forms.Label lbl_status;

        private System.Windows.Forms.Button btn_hangUp;

        private System.Windows.Forms.Label lbl_channel;

        #endregion
    }
}