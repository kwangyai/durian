using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Config;

namespace SmartLib.Logger
{
    public class LogWriter : ILogWriter
    {
        ILog _log = null;
        private Type _classType;
        private static String _configStr;
        private static DateTime _floderDate;

        static LogWriter()
        {

            String exeDir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            String cfgFileName = "SmartLib.Logger.log4net-app.config"; 
            String dbFile = String.Format("{0}\\SYSTEM-LOG\\{1}\\log.mdb", exeDir, DateTime.Now.ToString("dd-MM-yyyy"));

            if (System.Web.HttpContext.Current != null)
                cfgFileName = "SmartLib.Logger.log4net-web.config";
            
            _floderDate = DateTime.Now.Date;
		
            //GET Embedded Resource
            Stream inFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(cfgFileName);

            TextReader rd = new StreamReader(inFile);
            String cfgStr = rd.ReadToEnd();
            _configStr = cfgStr.Replace("@DIR", exeDir);
	    
            cfgStr = cfgStr.Replace("@DIR", exeDir);
            cfgStr = cfgStr.Replace("@DATE", _floderDate.ToString("dd-MM-yyyy"));

            byte[] dataBytes = Encoding.ASCII.GetBytes(cfgStr);
            Stream stData = new MemoryStream(dataBytes);
            
            if (stData != null) {
                log4net.Config.XmlConfigurator.Configure(stData);
                stData.Close();
            }
            
        }

        private void ReloadConfig()
        {
            if (DateTime.Now.Date != _floderDate.Date)
            {
                _floderDate = DateTime.Now.Date;
                String cfgStr = _configStr;
                cfgStr = cfgStr.Replace("@DATE", _floderDate.ToString("dd-MM-yyyy"));

                byte[] dataBytes = Encoding.ASCII.GetBytes(cfgStr);
                Stream stData = new MemoryStream(dataBytes);

                if (stData != null)
                {
                    log4net.Config.XmlConfigurator.Configure(stData);
                    stData.Close();
                }
            }
        }

        public LogWriter(LoggerName name, Type classType)
        {
            this.ReloadConfig();
            this._log = LogManager.GetLogger(name.ToString());
            this._classType = classType;
        }

        public LogWriter(String name, Type classType)
        {
            this.ReloadConfig();
            this._log = LogManager.GetLogger(name);
            this._classType = classType;
        }

        public void LogDebug(String message)
        {
            this._log.Debug("[" + this._classType.ToString() + "] " + message);

        }


        public void LogDebug(String message, Exception exc)
        {
            this._log.Debug("[" + this._classType.ToString() + "] " + message, exc);
        }


        public void LogInfo(String message)
        {
            this._log.Info("[" + this._classType.ToString() + "] " + message);
        }


        public void LogInfo(String message, Exception exc)
        {
            this._log.Info("[" + this._classType.ToString() + "] " + message, exc);
        }

        public void LogError(String message)
        {
            this._log.Error("[" + this._classType.ToString() + "] " + message.ToString());
        }

        public void LogError(String message, Exception exc)
        {
            this._log.Error("[" + this._classType.ToString() + "] " + message, exc);
        }
    }
}
