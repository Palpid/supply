namespace HKSupply.Forms.Master
{
    partial class ChangePassword
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtRepeatPassword = new System.Windows.Forms.TextBox();
            this.lblRepeatPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.lblMsgBar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(71, 277);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(157, 42);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRepeatPassword
            // 
            this.txtRepeatPassword.Location = new System.Drawing.Point(70, 224);
            this.txtRepeatPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepeatPassword.Name = "txtRepeatPassword";
            this.txtRepeatPassword.PasswordChar = '*';
            this.txtRepeatPassword.Size = new System.Drawing.Size(272, 22);
            this.txtRepeatPassword.TabIndex = 9;
            // 
            // lblRepeatPassword
            // 
            this.lblRepeatPassword.AutoSize = true;
            this.lblRepeatPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepeatPassword.Location = new System.Drawing.Point(66, 191);
            this.lblRepeatPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRepeatPassword.Name = "lblRepeatPassword";
            this.lblRepeatPassword.Size = new System.Drawing.Size(156, 20);
            this.lblRepeatPassword.TabIndex = 8;
            this.lblRepeatPassword.Text = "Repeat Password";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(70, 146);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(272, 22);
            this.txtNewPassword.TabIndex = 7;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPassword.Location = new System.Drawing.Point(66, 112);
            this.lblNewPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(133, 20);
            this.lblNewPassword.TabIndex = 6;
            this.lblNewPassword.Text = "New Password";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(70, 76);
            this.txtOldPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(272, 22);
            this.txtOldPassword.TabIndex = 12;
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOldPassword.Location = new System.Drawing.Point(66, 42);
            this.lblOldPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(126, 20);
            this.lblOldPassword.TabIndex = 11;
            this.lblOldPassword.Text = "Old Password";
            // 
            // lblMsgBar
            // 
            this.lblMsgBar.Location = new System.Drawing.Point(28, 333);
            this.lblMsgBar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMsgBar.Name = "lblMsgBar";
            this.lblMsgBar.Size = new System.Drawing.Size(376, 26);
            this.lblMsgBar.TabIndex = 13;
            this.lblMsgBar.Text = "Message Bar";
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(457, 378);
            this.Controls.Add(this.lblMsgBar);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.lblOldPassword);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRepeatPassword);
            this.Controls.Add(this.lblRepeatPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePassword";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.ChangePassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRepeatPassword;
        private System.Windows.Forms.Label lblRepeatPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.Label lblMsgBar;
    }
}