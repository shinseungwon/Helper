using System;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Helper
{
    /// <summary>
    /// Extra tool functions
    /// </summary>
    public static class Tools
    {        
        public static class GridHelper
        {
            /// <summary>
            /// Datatable to html table
            /// </summary>
            /// <param name="datatable">Datatable</param>
            /// <param name="style">Table style class name</param>
            /// <param name="header">Draw header to column name</param>
            /// <returns>Html string</returns>
            public static string GetHtml(DataTable datatable, string style, bool header = true)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table class='" + style + "'>");

                if (header)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn col in datatable.Columns)
                    {
                        sb.Append("<th>" + col.ColumnName + "</th>");
                    }
                    sb.Append("</tr>");
                }

                foreach (DataRow row in datatable.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn col in datatable.Columns)
                    {
                        sb.Append("<td>" + row[col] + "</td>");
                    }
                    sb.Append("</tr>");
                }

                sb.Append("</table>");
                return sb.ToString();
            }

            public static string GetXml(DataTable dt)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<table>");

                foreach (DataRow row in dt.Rows)
                {
                    sb.Append("<rows>");
                    foreach (DataColumn col in dt.Columns)
                    {
                        sb.Append("<" + col + ">" + row[col] + "</" + col + ">");
                    }
                    sb.Append("</rows>");
                }

                sb.Append("</table>");
                return sb.ToString();
            }

            public static string GetJson(DataTable dt)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{ \"table\" : [");

                foreach (DataRow r in dt.Rows)
                {
                    sb.Append("{");
                    foreach (DataColumn c in dt.Columns)
                    {
                        sb.Append("\"" + c.ColumnName + "\" : " + "\"" + r[c] + "\",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]}");
                return sb.ToString();
            }

        }

        public static class EncryptHelper
        {
            public static string Decrypt(string key, string data)
            {
                return Convert.ToBase64String(Decrypt(
                    Encoding.ASCII.GetBytes(data)
                    , Encoding.ASCII.GetBytes(key)));
            }

            public static string Encrypt(string key, string data)
            {
                return Convert.ToBase64String(Encrypt(
                    Encoding.ASCII.GetBytes(data)
                    , Encoding.ASCII.GetBytes(key)));
            }

            //Decrypt byte[]
            public static byte[] Decrypt(byte[] data, byte[] key)
            {
                MemoryStream ms = new MemoryStream();
                Rijndael alg = Rijndael.Create();
                alg.Key = key;
                alg.IV = key;

                using (CryptoStream cs = new CryptoStream
                    (ms, alg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                byte[] decryptedData = ms.ToArray();
                return decryptedData;
            }

            //Encrypt byte[]
            public static byte[] Encrypt(byte[] data, byte[] key)
            {
                MemoryStream ms = new MemoryStream();
                Rijndael alg = Rijndael.Create();
                alg.Key = key;
                alg.IV = key;

                using (CryptoStream cs = new CryptoStream
                    (ms, alg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                byte[] encryptedData = ms.ToArray();
                return encryptedData;
            }

            //Get encrypt key
            public static byte[] GetKey()
            {
                Random r = new Random();
                byte[] array = new byte[16];
                r.NextBytes(array);
                return array;
            }

            //TextFile to 2D Matrix
            private static string[][] MyTrimming(string str)
            {
                string[] lines = str.Replace("\r", "").Split('\n');
                List<string> lineList = new List<string>();
                foreach (string s in lines)
                {
                    if (!lineList.Contains(s))
                    {
                        lineList.Add(s);
                    }
                }

                List<string[]> tmp = new List<string[]>();
                foreach (string s in lineList)
                {
                    if (s.Length > 0)
                    {
                        string[] items = s.Split(' ');
                        List<string> stacks = new List<string>();
                        foreach (string ss in items)
                        {
                            if (ss.Length > 0)
                            {
                                stacks.Add(MyTrim(ss));
                            }
                        }
                        tmp.Add(stacks.ToArray());
                    }
                }

                return tmp.ToArray();
            }

            //Remove space in both sides, also remove \r \n \t
            private static string MyTrim(string s)
            {
                return s.Trim()
                    .Replace("\r", "")
                    .Replace("\n", "")
                    .Replace("\t", "");
            }
        }
    }
}
