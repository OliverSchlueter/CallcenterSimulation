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
            this.SuspendLayout();
            // 
            // lbl_channel
            // 
            this.lbl_channel.AutoSize = true;
            this.lbl_channel.Location = new System.Drawing.Point(10, 27);
            this.lbl_channel.Name = "lbl_channel";
            this.lbl_channel.Size = new System.Drawing.Size(86, 18);
            this.lbl_channel.TabIndex = 0;
            this.lbl_channel.Text = "Channel: N/A";
            // 
            // btn_hangUp
            // 
            this.btn_hangUp.Location = new System.Drawing.Point(101, 106);
            this.btn_hangUp.Name = "btn_hangUp";
            this.btn_hangUp.Size = new System.Drawing.Size(156, 34);
            this.btn_hangUp.TabIndex = 1;
            this.btn_hangUp.Text = "Hang Up";
            this.btn_hangUp.UseVisualStyleBackColor = true;
            this.btn_hangUp.Click += new System.EventHandler(this.btn_hangUp_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(10, 45);
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
            // CallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 177);
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

        private System.Windows.Forms.Label lbl_partner;

        private System.Windows.Forms.Label lbl_status;

        private System.Windows.Forms.Button btn_hangUp;

        private System.Windows.Forms.Label lbl_channel;

        #endregion
    }
}