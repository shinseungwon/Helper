using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Helper
{
    /// <summary>
    /// Logger class
    /// </summary>
    public sealed class LogHelper
    {
        private readonly string ProgramName;
        private readonly string FullPath;
        private readonly string Ip;

        /// <summary>
        /// Constructor
        /// </summary>
        public LogHelper(string name = null)
        {
            ProgramName = AppDomain.CurrentDomain.FriendlyName;

            if (name != null)
            {
                FullPath = Directory.GetCurrentDirectory() + @"\" + name;
            }
            else
            {
                FullPath = Directory.GetCurrentDirectory() + @"\" + DateTime.Now.ToString("yyyyMMdd");
            }

            if (!Directory.Exists(FullPath))
            {
                Directory.CreateDirectory(FullPath);
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    Ip = host.AddressList[i].ToString();
                }
            }
        }

        /// <summary>
        /// Write log text file ( each month each file )
        /// </summary>
        /// <param name="str">Log string</param>
        public void WriteText(string str)
        {
            FileStream fs;

            if (!File.Exists(FullPath + ".txt"))
            {
                fs = File.Create(FullPath + ".txt");
            }
            else
            {
                fs = new FileStream(FullPath + ".txt", FileMode.Append);
            }

            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + "[" + ProgramName + "]" + "[" + Ip + "]" + str);
            }
        }

        /// <summary>
        /// Write log to file ( txt, csv )
        /// </summary>
        /// <param name="str">Log file string</param>
        /// <param name="title">File name</param>
        /// <returns>0:wrote successfully, 1:file already exist</returns>
        public int WriteFile(string str, string title = ".txt")
        {
            string FullPathWithTitle = FullPath + @"\"
                + DateTime.Now.ToString("yyyyMMddHHmmss") + title;

            if (File.Exists(FullPathWithTitle))
            {
                return 1;
            }
            else
            {
                using (FileStream fs = File.Create(FullPathWithTitle))
                {
                    fs.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                }
                return 0;
            }
        }
    }
}
