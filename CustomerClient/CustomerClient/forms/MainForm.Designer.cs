using System.ComponentModel;

namespace CustomerClient.forms
{
    partial class MainForm
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
            this.lbl_connectionStatus = new System.Windows.Forms.Label();
            this.lbl_clientId = new System.Windows.Forms.Label();
            this.btn_call = new System.Windows.Forms.Button();
            this.cb_channel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_connectionStatus
            // 
            this.lbl_connectionStatus.AutoSize = true;
            this.lbl_connectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lbl_connectionStatus.Location = new System.Drawing.Point(12, 182);
            this.lbl_connectionStatus.Name = "lbl_connectionStatus";
            this.lbl_connectionStatus.Size = new System.Drawing.Size(115, 18);
            this.lbl_connectionStatus.TabIndex = 0;
            this.lbl_connectionStatus.Text = "Server status: N/A";
            // 
            // lbl_clientId
            // 
            this.lbl_clientId.AutoSize = true;
            this.lbl_clientId.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lbl_clientId.Location = new System.Drawing.Point(12, 200);
            this.lbl_clientId.Name = "lbl_clientId";
            this.lbl_clientId.Size = new System.Drawing.Size(55, 18);
            this.lbl_clientId.TabIndex = 1;
            this.lbl_clientId.Text = "Your ID:";
            // 
            // btn_call
            // 
            this.btn_call.Location = new System.Drawing.Point(152, 99);
            this.btn_call.Name = "btn_call";
            this.btn_call.Size = new System.Drawing.Size(115, 26);
            this.btn_call.TabIndex = 2;
            this.btn_call.Text = "Call";
            this.btn_call.UseVisualStyleBackColor = true;
            this.btn_call.Click += new System.EventHandler(this.btn_call_Click);
            // 
            // cb_channel
            // 
            this.cb_channel.FormattingEnabled = true;
            this.cb_channel.Location = new System.Drawing.Point(152, 67);
            this.cb_channel.Name = "cb_channel";
            this.cb_channel.Size = new System.Drawing.Size(115, 26);
            this.cb_channel.TabIndex = 3;
            this.cb_channel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cb_channel_KeyPress);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 227);
            this.Controls.Add(this.cb_channel);
            this.Controls.Add(this.btn_call);
            this.Controls.Add(this.lbl_clientId);
            this.Controls.Add(this.lbl_connectionStatus);
            this.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Callcenter | Customer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox cb_channel;

        public System.Windows.Forms.Button btn_call;

        public System.Windows.Forms.Label lbl_clientId;

        public System.Windows.Forms.Label lbl_connectionStatus;

        #endregion
    }
}