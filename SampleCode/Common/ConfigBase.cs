using System;
using System.Collections.Generic;

namespace SampleCode
{
    public class ConfigBase<T> where T : class
    {
        public T Config { get; protected set; }

        public virtual void Initialize(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                Config = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(fileName));
            }
            else
            {
                throw new Exception($"ConfigBase() file not found, {fileName}");
            }
        }
    }

    public class ServerInfo
    {
        public int Index { get; set; }
        public Address Host { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
    }

    public class Address
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string URL { get; set; }
    }

}
