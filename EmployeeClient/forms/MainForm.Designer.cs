namespace EmployeeClient.forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.lbl_serverStatus = new System.Windows.Forms.Label();
            this.lbl_Id = new System.Windows.Forms.Label();
            this.cb_channel = new System.Windows.Forms.ComboBox();
            this.btn_call = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_serverStatus
            // 
            this.lbl_serverStatus.AutoSize = true;
            this.lbl_serverStatus.ForeColor = System.Drawing.Color.Red;
            this.lbl_serverStatus.Location = new System.Drawing.Point(12, 235);
            this.lbl_serverStatus.Name = "lbl_serverStatus";
            this.lbl_serverStatus.Size = new System.Drawing.Size(115, 18);
            this.lbl_serverStatus.TabIndex = 0;
            this.lbl_serverStatus.Text = "Server status: N/A";
            // 
            // lbl_Id
            // 
            this.lbl_Id.AutoSize = true;
            this.lbl_Id.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lbl_Id.Location = new System.Drawing.Point(12, 253);
            this.lbl_Id.Name = "lbl_Id";
            this.lbl_Id.Size = new System.Drawing.Size(55, 18);
            this.lbl_Id.TabIndex = 1;
            this.lbl_Id.Text = "Your ID:";
            this.lbl_Id.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_channel
            // 
            this.cb_channel.FormattingEnabled = true;
            this.cb_channel.Location = new System.Drawing.Point(152, 85);
            this.cb_channel.Name = "cb_channel";
            this.cb_channel.Size = new System.Drawing.Size(121, 26);
            this.cb_channel.TabIndex = 2;
            this.cb_channel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cb_channel_KeyPress);
            // 
            // btn_call
            // 
            this.btn_call.Location = new System.Drawing.Point(152, 128);
            this.btn_call.Name = "btn_call";
            this.btn_call.Size = new System.Drawing.Size(121, 25);
            this.btn_call.TabIndex = 3;
            this.btn_call.Text = "Next Call";
            this.btn_call.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 280);
            this.Controls.Add(this.btn_call);
            this.Controls.Add(this.cb_channel);
            this.Controls.Add(this.lbl_Id);
            this.Controls.Add(this.lbl_serverStatus);
            this.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Callcenter | Employee";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox cb_channel;
        public System.Windows.Forms.Button btn_call;

        public System.Windows.Forms.Label lbl_serverStatus;
        public System.Windows.Forms.Label lbl_Id;

        #endregion
    }
}