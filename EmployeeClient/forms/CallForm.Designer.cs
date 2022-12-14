using System.ComponentModel;

namespace EmployeeClient.forms
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
            this.lbl_customer = new System.Windows.Forms.Label();
            this.rtb_chatLog = new System.Windows.Forms.RichTextBox();
            this.btn_chatSend = new System.Windows.Forms.Button();
            this.tb_chat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_channel
            // 
            this.lbl_channel.AutoSize = true;
            this.lbl_channel.Location = new System.Drawing.Point(12, 49);
            this.lbl_channel.Name = "lbl_channel";
            this.lbl_channel.Size = new System.Drawing.Size(86, 18);
            this.lbl_channel.TabIndex = 0;
            this.lbl_channel.Text = "Channel: N/A";
            // 
            // btn_hangUp
            // 
            this.btn_hangUp.Location = new System.Drawing.Point(12, 300);
            this.btn_hangUp.Name = "btn_hangUp";
            this.btn_hangUp.Size = new System.Drawing.Size(196, 75);
            this.btn_hangUp.TabIndex = 1;
            this.btn_hangUp.Text = "Hang Up";
            this.btn_hangUp.UseVisualStyleBackColor = true;
            this.btn_hangUp.Click += new System.EventHandler(this.btn_hangUp_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(13, 86);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(85, 18);
            this.lbl_status.TabIndex = 2;
            this.lbl_status.Text = "Status: None";
            // 
            // lbl_customer
            // 
            this.lbl_customer.AutoSize = true;
            this.lbl_customer.Location = new System.Drawing.Point(12, 9);
            this.lbl_customer.Name = "lbl_customer";
            this.lbl_customer.Size = new System.Drawing.Size(73, 18);
            this.lbl_customer.TabIndex = 3;
            this.lbl_customer.Text = "Customer: ";
            // 
            // rtb_chatLog
            // 
            this.rtb_chatLog.BackColor = System.Drawing.Color.White;
            this.rtb_chatLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_chatLog.Location = new System.Drawing.Point(350, 6);
            this.rtb_chatLog.Name = "rtb_chatLog";
            this.rtb_chatLog.ReadOnly = true;
            this.rtb_chatLog.Size = new System.Drawing.Size(337, 307);
            this.rtb_chatLog.TabIndex = 4;
            this.rtb_chatLog.Text = "";
            // 
            // btn_chatSend
            // 
            this.btn_chatSend.Location = new System.Drawing.Point(620, 319);
            this.btn_chatSend.Name = "btn_chatSend";
            this.btn_chatSend.Size = new System.Drawing.Size(67, 34);
            this.btn_chatSend.TabIndex = 5;
            this.btn_chatSend.Text = "Send";
            this.btn_chatSend.UseVisualStyleBackColor = true;
            this.btn_chatSend.Click += new System.EventHandler(this.btn_chatSend_Click);
            // 
            // tb_chat
            // 
            this.tb_chat.Location = new System.Drawing.Point(350, 325);
            this.tb_chat.Name = "tb_chat";
            this.tb_chat.Size = new System.Drawing.Size(264, 25);
            this.tb_chat.TabIndex = 6;
            // 
            // CallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(699, 387);
            this.Controls.Add(this.tb_chat);
            this.Controls.Add(this.btn_chatSend);
            this.Controls.Add(this.rtb_chatLog);
            this.Controls.Add(this.lbl_customer);
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

        public System.Windows.Forms.Button btn_chatSend;
        public System.Windows.Forms.TextBox tb_chat;

        public System.Windows.Forms.RichTextBox rtb_chatLog;

        public System.Windows.Forms.Label lbl_customer;

        public System.Windows.Forms.Label lbl_status;

        private System.Windows.Forms.Button btn_hangUp;

        public System.Windows.Forms.Label lbl_channel;

        #endregion
    }
}