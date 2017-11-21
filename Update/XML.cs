using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Update
{
    class MyXML
    {

        private string xmlfullname = string.Empty;

        public MyXML(string xmlname)
        {
            xmlfullname = xmlname;
        }

        public void SetXMLUpdateTime()
        {
            UpdateXMLNode("UpdateInfo", "UpdateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public void UpdateXMLVersionNum(int version)
        {
            string newVersion = "1.0.0." + version.ToString();
            UpdateXMLNode("UpdateInfo", "Version", newVersion);
        }

        public void UpdateXMLfileList(List<string> filelist)
        {
            if (filelist != null)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlfullname);

                XmlNode fileListroot = xml.DocumentElement.SelectSingleNode("UpdateFileList");
                fileListroot.RemoveAll();
                foreach (var filename in filelist)
                {
                    var UpdateElement = xml.CreateElement("UpdateFile");
                    UpdateElement.SetAttribute("FileName", System.IO.Path.GetFileName(filename));
                    fileListroot.AppendChild(UpdateElement);
                }
                xml.Save(xmlfullname);
            }
        }

        public List<string> GetXMLfileList()
        {
            List<string> filelist = new List<string>();

            XmlDocument xml = new XmlDocument();
            xml.Load(xmlfullname);

            XmlNode fileListroot = xml.DocumentElement.SelectSingleNode("/AutoUpdate/UpdateFileList");
            foreach (var xe in fileListroot.ChildNodes)
            {
                var xmle = (XmlElement)xe;
                filelist.Add(xmle.GetAttribute("FileName"));
            }
            xml.Save(xmlfullname);
            return filelist;
        }

        public void UpdateXMLNode(string Attribute, string nodename, string value)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlfullname);
            try
            {
                XmlElement xe = (XmlElement)xml.DocumentElement.SelectSingleNode(String.Format("/AutoUpdate/{0}/{1}", Attribute, nodename));
                if (xe.Name == nodename)
                {
                    xe.InnerText = value;
                }
                xml.Save(xmlfullname);
            }
            catch (Exception ex)
            {
                Log.LogString(ex.ToString(), this);
            }
        }

        public string GetXMLNode(string nodename)
        {
            string result = "";
            if (!File.Exists(xmlfullname))
                return result;

            XmlDocument xml = new XmlDocument();
            xml.Load(xmlfullname);
            try
            {
                XmlElement xe = (XmlElement)xml.DocumentElement.SelectSingleNode("/AutoUpdate/" + nodename);

                if (xe.Name == nodename)
                {
                    result = xe.InnerText;
                }

                xml.Save(xmlfullname);
            }
            catch (Exception ex)
            {
                Log.LogString(ex.ToString(), this);
            }

            return result;
        }

        public string GetXMLNode(string attr, string nodename)
        {
            string result = "";
            if (!File.Exists(xmlfullname))
                return result;

            XmlDocument xml = new XmlDocument();
            xml.Load(xmlfullname);
            try
            {
                XmlElement xe = (XmlElement)xml.DocumentElement.SelectSingleNode(String.Format("/AutoUpdate/{0}/{1}", attr, nodename));
                if (xe.Name == nodename)
                {
                    result = xe.InnerText;
                }
                xml.Save(xmlfullname);
            }
            catch (Exception ex)
            {
                Log.LogString(ex.ToString(), this);
            }
            return result;
        }
    }


    
}
