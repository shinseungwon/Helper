using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Helper
{
    public class ConfigHelper : Helper
    {
        private readonly Dictionary<string, string> Configs;

        public ConfigHelper(string filePath)
        {
            Configs = new Dictionary<string, string>();
            string configText = File.ReadAllText(filePath);
            string[] configLines = configText.Replace("\r\n", "\n").Split('\n');
            string[] keyAndValue;
            string key, value;
            int i;

            foreach (string s in configLines)
            {
                if (s.Length > 1)
                {
                    if (!s.StartsWith('`'))
                    {
                        keyAndValue = s.Split(':');
                        key = keyAndValue[0];
                        value = "";
                        for (i = 1; i < keyAndValue.Length; i++)
                        {
                            value += keyAndValue[i];
                            if (i != keyAndValue.Length - 1)
                            {
                                value += ":";
                            }
                        }
                        Configs.Add(key, value);
                    }
                }
            }
        }

        public string GetConfig(string key)
        {
            string res = "";
            try
            {
                res = Configs[key];
            }
            catch
            {

            }
            return res;
        }
    }
}
