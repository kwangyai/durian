using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using SmartLib.Logger;

// 
//<configSections>
//<section name="dbEasy" type="SmartLib.Database.Config.DbConfigurationSectionHandler, SmartLib"/>
//</configSections>


//<connectionStrings>
//		<add name="onePos_Con" connectionString="Persist Security Info=False;User ID=oneuser;Password=16108;Initial Catalog=OnePos;Data Source=(local)"/>
//		<add name="enbCon_Con" connectionString="Persist Security Info=False;User ID=oneuser;Password=16108;Initial Catalog=EnablerDb;Data Source=(local)"/>
//</connectionStrings>


//<dbEasy default="DB1">
//	<add name="DB1" connectionString="onePos_Con" parameterPrefix="@" connectionClass="System.Data.SqlClient.SqlConnection, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
//	<add name="DB2" connectionString="enbCon_Con" parameterPrefix="@" connectionClass="System.Data.SqlClient.SqlConnection, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
//</dbEasy>

//<httpModules>
//<add type="SmartLib.Database.Web.OpenConnectionInView, SmartLib" name="OpenConnectionInView"/>
//</httpModules>


namespace SmartLib.Database.Config
{

    public class DbConfigurationSectionHandler : IConfigurationSectionHandler
    {

        private const String SELECTED_NAME = "default";
        private const String LOG_CONNECTION = "logConnection";
        private const String LOG_QUERY = "logQuery";
        private const String LOG_ERROR = "logError";
        private const String CONFIGURRATION_MGR_CLASS_NAME = "customConfigLoader";
        private const String ELEMENT = "add";
        private const String ATTRIBUTE_NAME = "name";
        private const String ATTRIBUTE_PARAM_PREFIX = "parameterPrefix";
        private const String ATTRIBUTE_CONN_CLASS = "connectionClass";
        private const String ATTRIBUTE_CONN_STR = "connectionString";
        private ILogWriter _log = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(DbConfigurationSectionHandler));


        public object Create(object parent, object configContext, XmlNode section)
        {
            Dictionary<string, DbConfigInfo> configs = new Dictionary<string, DbConfigInfo>();

            foreach (XmlAttribute attrib in section.Attributes)
            {
                if (attrib.NodeType == XmlNodeType.Attribute)
                {
                    if (attrib.Name == SELECTED_NAME)
                        DbConfigManager.DefaultConfigurationName = attrib.Value;
                    else if (attrib.Name == CONFIGURRATION_MGR_CLASS_NAME)
                        DbConfigManager.CustomConfigLoader = attrib.Value;
                    else if (attrib.Name == LOG_CONNECTION && attrib.Value.ToLower() == "true")
                        DbConfigManager.LogConnection = true;
                    else if (attrib.Name == LOG_QUERY && attrib.Value.ToLower() == "true")
                        DbConfigManager.LogQuery = true;
                    else if (attrib.Name == LOG_ERROR && attrib.Value.ToLower() == "true")
                        DbConfigManager.LogError = true;
                }

            }

            if (!String.IsNullOrEmpty(DbConfigManager.CustomConfigLoader) && DbConfigManager.CustomConfigLoader.IndexOf("AppDbConfigManager") == -1)
            {
                return configs;
            }

            foreach (XmlNode child in section.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element)
                {
                    DbConfigInfo dbCfg = new DbConfigInfo();

                    foreach (XmlAttribute childAttrib in child.Attributes)
                    {
                        if (childAttrib.NodeType == XmlNodeType.Attribute)
                        {
                            if (childAttrib.Name == ATTRIBUTE_NAME)
                            {
                                dbCfg.ConfigurationName = childAttrib.Value;

                                if (childAttrib.Value == DbConfigManager.DefaultConfigurationName)
                                {
                                    //dbCfg.IsDefaultConfig = true;
                                }
                            }

                            else if (childAttrib.Name == ATTRIBUTE_PARAM_PREFIX)
                            {
                                dbCfg.DbParamPrefix = childAttrib.Value;
                            }
                            else if (childAttrib.Name == ATTRIBUTE_CONN_CLASS)
                            {
                                dbCfg.ConnectionClass = childAttrib.Value;

                                string[] sp = childAttrib.Value.Split(',');

                                if (sp.Length > 0)
                                {
                                    dbCfg.AssemblyName = childAttrib.Value.Substring(sp[0].Length + 1, childAttrib.Value.Length - sp[0].Length - 1).Trim();
                                    dbCfg.ConnectionClass = sp[0];
                                }
                                else
                                {
                                    this._log.LogError("Require Assembly Configuration.");
                                    throw new Exception("Require Assembly Configuration");
                                }
                            }

                            else if (childAttrib.Name == ATTRIBUTE_CONN_STR)
                            {
                                dbCfg.ConnectionClass = childAttrib.Value.Trim();
                                dbCfg.ConnectionString = dbCfg.ConnectionClass.Trim();


                                if ((ConfigurationManager.ConnectionStrings[dbCfg.ConnectionString] != null))
                                {
                                    dbCfg.ConnectionString = ConfigurationManager.ConnectionStrings[dbCfg.ConnectionString].ConnectionString;
                                }

                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(dbCfg.ConfigurationName))
                    {
                        configs.Add(dbCfg.ConfigurationName, dbCfg);
                    }
                }
            }

            this._log.LogInfo("dbEasy Configuration, ( app.config for WinApp, web.config for web App), Loaded.");
            return configs;
        }

    }

    //END CLASS DbEasyConfigSectionHandler 



}
