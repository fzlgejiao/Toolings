namespace Update
{
    partial class PasswordInput
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
            this.radTextBox_passwd = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radButton_ok = new Telerik.WinControls.UI.RadButton();
            this.radButton_cancel = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox_passwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton_ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton_cancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radTextBox_passwd
            // 
            this.radTextBox_passwd.Location = new System.Drawing.Point(74, 62);
            this.radTextBox_passwd.Name = "radTextBox_passwd";
            this.radTextBox_passwd.PasswordChar = '*';
            this.radTextBox_passwd.Size = new System.Drawing.Size(257, 20);
            this.radTextBox_passwd.TabIndex = 0;
            this.radTextBox_passwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.radTextBox_passwd_KeyPress);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(74, 24);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(117, 22);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "请输入密码：";
            // 
            // radButton_ok
            // 
            this.radButton_ok.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton_ok.Location = new System.Drawing.Point(74, 105);
            this.radButton_ok.Name = "radButton_ok";
            this.radButton_ok.Size = new System.Drawing.Size(110, 24);
            this.radButton_ok.TabIndex = 2;
            this.radButton_ok.Text = "OK";
            this.radButton_ok.Click += new System.EventHandler(this.radButton_ok_Click);
            // 
            // radButton_cancel
            // 
            this.radButton_cancel.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton_cancel.Location = new System.Drawing.Point(221, 105);
            this.radButton_cancel.Name = "radButton_cancel";
            this.radButton_cancel.Size = new System.Drawing.Size(110, 24);
            this.radButton_cancel.TabIndex = 3;
            this.radButton_cancel.Text = "Cancel";
            this.radButton_cancel.Click += new System.EventHandler(this.radButton_cancel_Click);
            // 
            // PasswordInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 168);
            this.Controls.Add(this.radButton_cancel);
            this.Controls.Add(this.radButton_ok);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radTextBox_passwd);
            this.Name = "PasswordInput";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "PasswordInput";
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox_passwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton_ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton_cancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox radTextBox_passwd;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton radButton_ok;
        private Telerik.WinControls.UI.RadButton radButton_cancel;
    }
}