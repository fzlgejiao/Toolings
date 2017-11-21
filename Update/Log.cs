using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Update
{
    public class Log
    {
        private static object lockObject = new object();

        public static void LogString(string message, object sender)
        {
            lock (lockObject)
            {
                using (var fileStream = new FileStream("log.txt", FileMode.Append, FileAccess.Write))
                {
                    using (var sw = new StreamWriter(fileStream))
                    {
                        sw.WriteLine(string.Format("[{0}]-[{1}]-[{2}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message, sender.ToString()));
                    }
                }
            }
        }
    }
}
