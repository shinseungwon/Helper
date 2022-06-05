using System;
using System.IO;
using System.Net;

namespace Helper
{
    /// <summary>
    /// Class helps connect ftp server and upload and download files
    /// </summary>
    public sealed class FileHelper : Helper
    {
        public string rootpath;
        public string userid;
        public string password;

        /// <summary>
        /// Constructor ( IV = Initial Vector that user should define for encrypting )
        /// </summary>
        /// <param name="rootpath">Ftp path</param>
        /// <param name="userid">Ftp authenication id</param>
        /// <param name="password">Ftp authenication password</param>
        public FileHelper(string rootpath, string userid, string password)
        {
            this.rootpath = rootpath;
            this.userid = userid;
            this.password = password;
            //this.iv = Encoding.Default.GetBytes(iv);
        }

        /// <summary>
        /// Upload to ftp
        /// </summary>
        /// <param name="localPath">full path</param>
        /// <param name="ftpPath">ftp path</param>
        public void Upload(string localPath, string ftpPath)
        {
            try
            {
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(rootpath + "/" + ftpPath);
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(userid, password);
                req.KeepAlive = false;
                req.UseBinary = true;

                byte[] filebytes = File.ReadAllBytes(localPath);
                using Stream stream = req.GetRequestStream();
                stream.Write(filebytes, 0, filebytes.Length);
            }
            catch (Exception e)
            {
                WriteLog("Ftp Upload Error - Path : " + rootpath + "/" + ftpPath + " /ErrorMsg : " + e.ToString());
            }
            finally
            {
                WriteLog("Ftp Upload - Path : " + rootpath + "/" + ftpPath);
            }
        }

        /// <summary>
        /// Download to ftp
        /// </summary>
        /// <param name="localPath">full path</param>
        /// <param name="ftpPath">full path</param>
        public void Download(string localPath, string ftpPath)
        {
            try
            {
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(rootpath + "/" + ftpPath);
                req.Method = WebRequestMethods.Ftp.DownloadFile;
                req.Credentials = new NetworkCredential(userid, password);
                req.KeepAlive = false;
                req.UseBinary = true;

                using FtpWebResponse resp = (FtpWebResponse)req.GetResponse();
                using Stream stream = resp.GetResponseStream();
                using MemoryStream ms = new MemoryStream();

                byte[] buf;
                int count = 0;

                do
                {
                    buf = new byte[1024];
                    count = stream.Read(buf, 0, 1024);
                    ms.Write(buf, 0, count);
                } while (stream.CanRead && count > 0);

                byte[] data = ms.ToArray();

                File.WriteAllBytes(localPath, data);
            }
            catch (Exception e)
            {
                WriteLog("Ftp Download Error - Path : " + rootpath + "/" + ftpPath + " /ErrorMsg : " + e.ToString());                
            }
            finally
            {
                WriteLog("Ftp Download - Path : " + rootpath + "/" + ftpPath);                
            }
        }
    }
}
