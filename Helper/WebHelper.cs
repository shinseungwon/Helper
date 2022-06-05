using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Helper
{
    public sealed class WebHelper : Helper
    {
        private readonly List<string> keys = new List<string>();
        private readonly List<string> values = new List<string>();

        /// <summary>
        /// web request
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="method">post or get</param>
        /// <returns></returns>
        public string Request(string url, string method = "POST", string content = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.UserAgent = AppDomain.CurrentDomain.FriendlyName;

            if (method == "POST")
            {
                for (int i = 0; i < keys.Count; i++)
                {
                    request.Headers[keys[i]] = values[i];
                }
                keys.Clear();
                values.Clear();
            }

            if (content != null)
            {
                using (Stream stream = request.GetRequestStream())
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.Write(content);
                }
                request.ContentLength = content.Length;
            }

            string res = "";

            try
            {
                HttpWebResponse wresp = (HttpWebResponse)request.GetResponse();
                Stream wstream = wresp.GetResponseStream();
                byte[] buffer = new byte[1024];

                while (wstream.Read(buffer, 0, 1024) > 0)
                {
                    res += Encoding.Default.GetString(buffer);
                }
            }
            catch (Exception e)
            {
                WriteLog("request type : " + method + "/url : " + url + "/error : " + e.ToString());
            }

            return res;
        }
    }
}
