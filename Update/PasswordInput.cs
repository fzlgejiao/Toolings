using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Update
{
    public partial class PasswordInput : RadForm
    {
        public DialogResult DialogResult { get; set; }

        public PasswordInput()
        {
            InitializeComponent();
            CenterToScreen();
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public static DialogResult ShowForm()
        {
            var form = new PasswordInput();
            form.ShowDialog();
            return form.DialogResult;
        }

        private string GetSystemPassword()
        {
            var rawdata = Properties.Settings.Default.Password;
            var preArrayStr = string.Empty;
            var result = string.Empty;
            if (!string.IsNullOrEmpty(rawdata))
            {
                if (rawdata.Length % 2 == 0)
                {
                    for (int i = 0; i < rawdata.Length; i++)
                    {
                        preArrayStr += rawdata[i];
                        if (i % 2 == 1)
                        {
                            preArrayStr += " ";
                        }
                    }

                    var array = preArrayStr.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < array.Length; i++)
                    {
                        var value = Convert.ToInt32(array[i], 16);
                        result += (char)value;
                    }

                    return result;
                }
                return "";
            }
            return "";
        }
                
        private void radButton_ok_Click(object sender, EventArgs e)
        {
            var pw = this.radTextBox_passwd.Text.Trim();

            if (pw == GetSystemPassword())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("密码错误!!!");
            }
        }

        private void radButton_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radTextBox_passwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                radButton_ok_Click(this.radButton_ok, EventArgs.Empty);
            }
        }
    }
}
