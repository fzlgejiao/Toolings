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
using System.IO;

using System.Net;
using System.Diagnostics;

namespace Update
{
    public partial class Form1 : RadForm
    {
        List<string> updatefilelist;
        List<string> remotefilelist;
        FtpClient m_FtpClient = null;
        const string ftpuser = "utcnadmin";
        const string ftppasswd = "Uster1111";
        int LocalVerion = 0;
        string Appname = string.Empty;
        MyXML myXml = new MyXML(Application.StartupPath + @"/AutoUpdate.xml");
        string remoteDir = string.Empty;
        int downloadnumber = 0;

        public Form1()
        {
            InitializeComponent();
            string url = myXml.GetXMLNode("URLAddres");
            LocalVerion = Int16.Parse(myXml.GetXMLNode("UpdateInfo", "Version").Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[3]);
            Appname = myXml.GetXMLNode("RestartApp", "AppName");
            remoteDir = Properties.Settings.Default.RmoteDir;

            if (url != null)
            {
                m_FtpClient = new FtpClient(url, ftpuser, ftppasswd);
                m_FtpClient.DownloadProgressChanged += new FtpClient.Delegate_DownloadProgressChanged(ClientDownLoadProgressChanged);
                m_FtpClient.DownloadDataCompleted += new FtpClient.Delegate_DownloadDataCompleted(ClientDownLoadCompleted);

                m_FtpClient.UploadProgressChanged += new FtpClient.Delegate_UploadProgressChanged(ClientUploadProgressChanged);
                m_FtpClient.UploadFileCompleted += new FtpClient.Delegate_UploadFileCompleted(ClientUploadFileCompleted);
            }
            try
            {
                if (m_FtpClient.DownloadFile(@"/RingRailPCB/AutoUpdate.xml", Application.StartupPath, "XmlTemp"))
                {
                    MyXML myXmlTemp = new MyXML(Application.StartupPath + @"/XmlTemp");
                    int remoteversion = Int16.Parse(myXmlTemp.GetXMLNode("UpdateInfo", "Version").Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[3]);
                    if (int.Parse(Properties.Settings.Default.IsAutoUpdate) == 1)
                    {
                        if (remoteversion > LocalVerion)
                        {
                            this.RadupdateLable.Text = "发现新版本，请按<Update>更新";
                        }
                        else if (remoteversion == LocalVerion)
                        {
                            ReStartApp();
                        }
                    }
                }
                else
                {
                    this.RadupdateLable.Text = "Error to Get Remote Updater File!";
                    this.radButton_update.Enabled = false;
                }
            }catch
            {
                RadMessageBox.Show("Ftp site disconnected!");
                this.RadupdateLable.Text = "Ftp site disconnected!";
                this.radButton_update.Enabled = false;
                this.radButton_upload.Enabled = false;
            }
        }

        public void ReStartApp()
        {
            if(Appname!=String.Empty)
            {
                Process app = new Process();
                app.StartInfo.FileName = Path.Combine(Application.StartupPath,Appname);
                app.Start();

                System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process process in processList)
                {
                    if (process.ProcessName == System.IO.Path.GetFileNameWithoutExtension(Appname))
                    {
                        System.Environment.Exit(0); 
                    }
                }
            }
        }

        public void ClientDownLoadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.radProgressBar.Value1 = e.ProgressPercentage;
        }

        public void ClientDownLoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {   
            try
            {
                this.radProgressBar.Text = e.Error.Message;
                this.radProgressBar.Text = "Download Error!";
            }
            catch
            {
                this.radProgressBar.Text = "Download Complete!";
                downloadnumber++;
                if (downloadnumber == remotefilelist.Count)
                {
                    downloadnumber = 0;
                    //copy the xml file
                    System.IO.File.Copy(Application.StartupPath + @"/XmlTemp", Application.StartupPath + @"/AutoUpdate.xml", true);
                    ReStartApp();
                }
            }
        }

        public void ClientUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            this.radProgressBar.Value1 = e.ProgressPercentage;
        }

        public void ClientUploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            try
            {
                this.RadupdateLable.Text = e.Error.Message;
                this.radProgressBar.Text = "Upload Error!";
            }
            catch
            {
                this.radProgressBar.Text = "Upload Complete!";               
            }
        }

        private void radButton_upload_Click(object sender, EventArgs e)
        {
            if (PasswordInput.ShowForm() == System.Windows.Forms.DialogResult.OK)
            {
                using (OpenFileDialog OpenDialog = new OpenFileDialog())
                {
                    OpenDialog.Multiselect = true;
                    if(OpenDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                    {
                        //upload the choosen files and the XML to FTP Site
                        updatefilelist = new List<string>(OpenDialog.FileNames);
                        updatefilelist.Add(Application.StartupPath + @"\AutoUpdate.xml");
                        myXml.UpdateXMLfileList(updatefilelist);
                        myXml.UpdateXMLVersionNum(LocalVerion+1);
                        myXml.SetXMLUpdateTime();
                        UploadToFTPSite(updatefilelist);                        
                    }
                }
            }
        }

        private void UploadToFTPSite(List<string> filelist)
        {
            if (filelist != null)
            {
                this.radProgressBar.Text = "";
                this.radProgressBar.Value1 = 0;
                for(int i=0;i<filelist.Count;i++)
                {
                    if (m_FtpClient != null)
                    {
                        this.RadupdateLable.Text = String.Format("Uploading File[{0}/{1}]: " + System.IO.Path.GetFileName(filelist[i]), i + 1, filelist.Count);
                        m_FtpClient.UploadFileAsync(filelist[i], @"/RingRailPCB/" + System.IO.Path.GetFileName(filelist[i]));
                    }
                }
            }
        }

        private void radButton_update_Click(object sender, EventArgs e)
        {
            if (m_FtpClient != null)
            {
                //bool isdownloadok = m_FtpClient.DownloadFile(@"/RingRailPCB/Ring_ULPT_tester.exe", Application.StartupPath + @"\Ftpdownload\");
                this.radProgressBar.Text = "";
                this.radProgressBar.Value1 = 0;

                remotefilelist = new List<string>();
                remotefilelist = myXml.GetXMLfileList();
                for (int i = 0; i < remotefilelist.Count; i++)
                {
                    this.RadupdateLable.Text = String.Format("Downloading RemoteFiles[{0}/{1}]: " + System.IO.Path.GetFileName(remotefilelist[i]), i + 1, remotefilelist.Count);
                    m_FtpClient.DownloadFileAsync(@"/RingRailPCB/" + remotefilelist[i], Application.StartupPath);
                }
            }
        }
    }
}
