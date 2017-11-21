using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Update
{
       
    public class FtpClient
    {

        #region Event
        public delegate void Delegate_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e);
        public delegate void Delegate_DownloadDataCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
        public delegate void Delegate_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e);
        public delegate void Delegate_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e);
        
        public event Delegate_DownloadProgressChanged DownloadProgressChanged;
        public event Delegate_DownloadDataCompleted DownloadDataCompleted;
        public event Delegate_UploadProgressChanged UploadProgressChanged;
        public event Delegate_UploadFileCompleted UploadFileCompleted;
        #endregion  Event

        private string usr = String.Empty;
        private string password = String.Empty;
        private Uri uri;
        public long totaldownloadsize = 0;
        public long totaluploadsize = 0;

        FtpWebRequest Request = null;
        FtpWebResponse Response = null;

        private string _ErrorMsg;
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set { _ErrorMsg = value; }  
        }

        public FtpClient(string url, string user, string passwd)
        {
            uri = new Uri(url);
            this.usr = user;
            this.password = passwd;
        }

        private FtpWebResponse Open(Uri uri,string ftpMethod)
        {
            try
             {
                Request = (FtpWebRequest) WebRequest.Create(uri);
                Request.UseBinary = true;
                Request.Method = ftpMethod;
                Request.Credentials = new NetworkCredential(this.usr, this.password);
                
                return (FtpWebResponse) Request.GetResponse();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
                throw ex;
            }
        }

        private byte[] DownloadFile(string remoteFile)
        {
            long filesize=0;
            try
            {
                Response = Open(new Uri(this.uri.ToString() + remoteFile), WebRequestMethods.Ftp.GetFileSize);
                filesize=Response.ContentLength;
                if (filesize > 0)
                {
                    MemoryStream mem = new MemoryStream((int)filesize);
                    Response = Open(new Uri(this.uri.ToString() + remoteFile), WebRequestMethods.Ftp.DownloadFile);
                    using (Stream reader = Response.GetResponseStream())
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while (true)
                        {
                            bytesRead = reader.Read(buffer, 0, buffer.Length);
                            totaldownloadsize += bytesRead;
                            if (bytesRead == 0)
                                break;
                            mem.Write(buffer, 0, bytesRead);
                        }

                    }
                    if (mem.Length > 0)
                        return mem.ToArray();
                    else return null;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                ErrorMsg = ex.ToString();
                throw ex;
            }
        }

        public bool DownloadFile(string remoteFileName,string LocalPath,string localname) //Default set  Local file name same to remote file name,and overwrite it
        {
            byte[] filedata = null;
            try
            {
                if (!IsValidFileChars(remoteFileName) ||!IsValidFileChars(localname))
                {
                    throw new Exception("Invalide file name");
                }
                if (!Directory.Exists(LocalPath))
                {
                    throw new Exception("Invalide Local Path");
                }
                string LocalFullPath = Path.Combine(LocalPath, System.IO.Path.GetFileName(localname));

                filedata = DownloadFile(remoteFileName);
                if (filedata != null)
                {
                    using (FileStream stream = new FileStream(LocalFullPath, FileMode.Create))  //if local file is exsiting ,overwrite it
                    {
                        stream.Write(filedata, 0, filedata.Length);
                        stream.Flush();
                        stream.Close();
                        return true;
                    }
                }
                else return false;
            }catch(Exception ex)
            {
                ErrorMsg = ex.ToString();
                throw ex;
            }
        }
               
        public void DownloadFileAsync(string remoteFileName, string LocalPath)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName))
                {
                    throw new Exception("Invalide remote file name");
                }
                if (File.Exists(LocalPath))
                {
                    throw new Exception("Invalide Local Path！");
                }
                string LocalFullPath = Path.Combine(LocalPath, System.IO.Path.GetFileName(remoteFileName));

                MyWebClient client = new MyWebClient();
                client.Proxy = null;
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.Credentials = new NetworkCredential(this.usr, this.password);

                client.DownloadFileAsync(new Uri(this.uri.ToString() + remoteFileName), LocalFullPath);                              
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
                throw ex;
            }
        }

        public void UploadFileAsync(string LocalFullPath, string RemoteFileName)
        {
            try
            {
                if (!IsValidFileChars(RemoteFileName) || !IsValidFileChars(Path.GetFileName(LocalFullPath)))
                {
                    throw new Exception("Invalide file name!");
                }
                //if (FileExist(RemoteFileName))
                //{
                //    throw new Exception("FTP服务上面已经存在同名文件！");
                //}
                if (File.Exists(LocalFullPath))
                {
                    MyWebClient client = new MyWebClient();

                    client.UploadProgressChanged += new UploadProgressChangedEventHandler(client_UploadProgressChanged);
                    client.UploadFileCompleted += new UploadFileCompletedEventHandler(client_UploadFileCompleted);
                    client.Credentials = new NetworkCredential(this.usr, this.password);
                  
                    client.UploadFileAsync(new Uri(this.uri.ToString() + RemoteFileName), LocalFullPath);
                }
                else
                {
                    throw new Exception("No such local file");
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
                throw ex;
            }
        }

        private bool IsValidFileChars(string FileName)
        {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();
            char[] NameChar = FileName.ToCharArray();
            foreach (char C in NameChar)
            {
                if (Array.BinarySearch(invalidFileChars, C) >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (DownloadDataCompleted != null)
            {
                DownloadDataCompleted(sender, e);
            }
        }
      
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
            {
                DownloadProgressChanged(sender, e);
            }
        }

        void client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(sender, e);
            }
        }

        void client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            //if (_isDeleteTempFile)
            //{
            //    if (File.Exists(_UploadTempFile))
            //    {
            //        File.SetAttributes(_UploadTempFile, FileAttributes.Normal);
            //        File.Delete(_UploadTempFile);
            //    }
            //    _isDeleteTempFile = false;
            //}
            if (UploadFileCompleted != null)
            {
                UploadFileCompleted(sender, e);
            }
        }

        #region override 
        internal class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                FtpWebRequest req = (FtpWebRequest)base.GetWebRequest(address);
                req.UsePassive = false;
                return req;
            }
        }
        #endregion


    }
}
