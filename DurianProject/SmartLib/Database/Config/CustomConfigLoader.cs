using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using SmartLib.Logger;

namespace SmartLib.Database.Config
{
	public abstract class CustomConfigLoader
	{
		private Dictionary<string, DbConfigInfo> _dbConfigs;
		private Dictionary<String, Type> _dbConnClass;
        private ILogWriter _log = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(CustomConfigLoader));

		public String DefaultConnectionName {
			get{ return DbConfigManager.DefaultConfigurationName; }
			set{ DbConfigManager.DefaultConfigurationName = value; }
		}
		
		public bool LogConnection {
			get{ return DbConfigManager.LogConnection; }
			set{ DbConfigManager.LogConnection = value; }
		}
		
		public bool LogQuery {
			get{ return DbConfigManager.LogQuery; }
			set{ DbConfigManager.LogQuery = value; }
		}
		
		public bool LogError {
			get{ return DbConfigManager.LogError; }
			set{ DbConfigManager.LogError = value; }
		}
		
		public CustomConfigLoader( ) {
			this._dbConfigs = new Dictionary<string,DbConfigInfo>();
			this._dbConnClass = new Dictionary<string,Type>();
		}
		
		public Dictionary<String, DbConfigInfo> GetConfig() {
			return this._dbConfigs;
		}
	
		public void AddConfig( String configName, String connectionString, Type connectionClassType ) {

            if (!(String.IsNullOrEmpty(configName) || String.IsNullOrEmpty(connectionString) || connectionClassType == null))
            {
                if (!this._dbConfigs.ContainsKey(configName))
                {
                    DbConfigInfo cfg = new DbConfigInfo();
                    cfg.ConnectionString = connectionString;
                    cfg.ConfigurationName = configName;

                    cfg.ConnectionClass = connectionClassType.ToString();
                    cfg.AssemblyName = connectionClassType.AssemblyQualifiedName;

                    this._dbConfigs.Add(configName, cfg);

                    this._log.LogInfo("AddConfig, ConfigName: " + configName+ ", ConnectionString: " + connectionString +", Connection Class Type: " + connectionClassType.ToString());
                }
            }
            else
            {
                this._log.LogError("CANNOT ADD CONFIG WITH PARAMETER NULL/EMPTY  VALUE.");
                throw new Exception("CANNOT ADD CONFIG WITH PARAMETER NULL/EMPTY VALUE.");
            }
		}


		public void RemoveConfig(String configName)
		{
			if (this._dbConfigs.ContainsKey(configName))
			{
				DbConfigInfo cfg = this._dbConfigs[configName];
				this._dbConfigs.Remove(configName);
				cfg = null;
			}
		}
		
		
		
		public virtual void Load( ) {
            _log.LogInfo("LOAD CUSTOM CONFIG LOADER.");
		}
		
	}
}
