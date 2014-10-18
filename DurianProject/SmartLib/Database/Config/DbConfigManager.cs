using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using SmartLib.Logger;

namespace SmartLib.Database.Config
{
    public class DbConfigManager
    {
        private static CustomConfigLoader _customCfgLoader;
        private static Dictionary<String, DbConfigInfo> _configInfos;
        private static DbConfigInfo _defaultConfigInfo;
        private static bool _isMultipleDB;
        private static ILogWriter _log = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(DbConfigManager));

        internal static String DefaultConfigurationName = String.Empty;
        internal static String CustomConfigLoader = String.Empty;
        internal static bool LogConnection;
        internal static bool LogQuery;
        internal static bool LogError;

        private static void init()
        {
            if (_customCfgLoader == null)
            {
                _configInfos = ConfigurationManager.GetSection("dbEasy") as Dictionary<string, DbConfigInfo>;

                if (!String.IsNullOrEmpty(CustomConfigLoader))
                {
                    _configInfos = null;
                    String[] asmInfo = CustomConfigLoader.Split(',');

                    if (asmInfo.Length < 1)
                    {
                        _log.LogError("INVALID CONFIGURATION in dbEasy, CustomConfigLoader");
                        throw new Exception("INVALID CONFIGURATION in dbEasy, CustomConfigLoader");
                    }
                    else
                    {
                        try
                        {
                            String className = asmInfo[0];
                            String asmName = CustomConfigLoader.Substring(asmInfo[0].Length + 1, CustomConfigLoader.Length - asmInfo[0].Length - 1).Trim();
                            _customCfgLoader = (CustomConfigLoader)Activator.CreateInstance(asmName, className).Unwrap();
                            _customCfgLoader.Load();
                            _configInfos = _customCfgLoader.GetConfig();

                            _log.LogInfo("LOAD DB ASSEMBLY FINISHED.");

                        }
                        catch (Exception exc)
                        {
                            _log.LogError("CAN NOT LOAD DB ASSEMBLY.", exc);
                            throw new Exception("CAN NOT LOAD DB ASSEMBLY.", exc);
                        }

                    }
                }


                if (_configInfos.Count > 1)
                    _isMultipleDB = true;

                foreach (DbConfigInfo cfg in _configInfos.Values)
                {
                    if (_configInfos.Count > 1)
                    {
                        if (cfg != null && DefaultConfigurationName == cfg.ConfigurationName)
                        {
                            _defaultConfigInfo = cfg;
                            break;
                        }
                    }
                    else
                    {
                        _defaultConfigInfo = cfg;
                        DefaultConfigurationName = cfg.ConfigurationName;
                    }
                }


            }
        }

        public static Dictionary<String, DbConfigInfo> GetAllConfigInfos()
        {
            init();
            return _configInfos;
        }

        public static DbConfigInfo GetDefaultConfigInfo()
        {
            init();
            return _defaultConfigInfo;
        }

        public static bool IsMultipleDB
        {
            get { return _isMultipleDB; }
        }
    }
}
