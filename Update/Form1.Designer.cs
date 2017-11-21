namespace Update
{
    partial class Form1
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
            this.radProgressBar = new Telerik.WinControls.UI.RadProgressBar();
            this.radButton_update = new Telerik.WinControls.UI.RadButton();
            this.radButton_upload = new Telerik.WinControls.UI.RadButton();
            this.RadupdateLable = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radProgressBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton_update)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton_upload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadupdateLable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radProgressBar
            // 
            this.radProgressBar.Location = new System.Drawing.Point(86, 75);
            this.radProgressBar.Name = "radProgressBar";
            this.radProgressBar.Size = new System.Drawing.Size(403, 37);
            this.radProgressBar.TabIndex = 0;
            // 
            // radButton_update
            // 
            this.radButton_update.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton_update.Location = new System.Drawing.Point(86, 137);
            this.radButton_update.Name = "radButton_update";
            this.radButton_update.Size = new System.Drawing.Size(117, 38);
            this.radButton_update.TabIndex = 1;
            this.radButton_update.Text = "Update";
            this.radButton_update.Click += new System.EventHandler(this.radButton_update_Click);
            // 
            // radButton_upload
            // 
            this.radButton_upload.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton_upload.Location = new System.Drawing.Point(369, 137);
            this.radButton_upload.Name = "radButton_upload";
            this.radButton_upload.Size = new System.Drawing.Size(120, 38);
            this.radButton_upload.TabIndex = 2;
            this.radButton_upload.Text = "Publish";
            this.radButton_upload.Click += new System.EventHandler(this.radButton_upload_Click);
            // 
            // RadupdateLable
            // 
            this.RadupdateLable.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadupdateLable.Location = new System.Drawing.Point(86, 32);
            this.RadupdateLable.Name = "RadupdateLable";
            this.RadupdateLable.Size = new System.Drawing.Size(2, 2);
            this.RadupdateLable.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 207);
            this.Controls.Add(this.RadupdateLable);
            this.Controls.Add(this.radButton_upload);
            this.Controls.Add(this.radButton_update);
            this.Controls.Add(this.radProgressBar);
            this.Name = "Form1";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Update";
            ((System.ComponentModel.ISupportInitialize)(this.radProgressBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton_update)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton_upload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadupdateLable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadProgressBar radProgressBar;
        private Telerik.WinControls.UI.RadButton radButton_update;
        private Telerik.WinControls.UI.RadButton radButton_upload;
        private Telerik.WinControls.UI.RadLabel RadupdateLable;
    }
}

