using System;
using System.Collections.Generic;

namespace SampleCode
{
    public class ConfigServer
    {
        public ServerInfo  ServerInfo { get; set; }
    }

    public class ServerConfig : ConfigBase<ConfigServer>
    {
        public override void Initialize(string fileName)
        {
            base.Initialize(fileName);
        }
    }

    public class ConfigManager
    {
        #region Singleton
        private static ConfigManager _instance = null;

        public static ConfigManager instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new ConfigManager();
                }

                return _instance;
            }
        }
        #endregion

        public ServerConfig ServerConfig { get; private set; }

        public void Initialize(string rootPath, string config_path)
        {
            string configFile = CreateConfigPath(rootPath, config_path);

            ServerConfig = new ServerConfig();

            ServerConfig.Initialize(configFile);
        }

        public string CreateConfigPath(string rootPath, string configPath)
        {
            return $"{rootPath}{System.IO.Path.DirectorySeparatorChar}{configPath}{System.IO.Path.DirectorySeparatorChar}config.json";
        }
    }
}
