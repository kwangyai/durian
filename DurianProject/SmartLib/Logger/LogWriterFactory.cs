using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace SmartLib.Logger
{
    public class LogWriterFactory
    {
        public static ILogWriter Create( LoggerName logName, Type classType )
        {

		ILogWriter log = new LogWriter(logName, classType);

		return log;
        }


	    internal static ILogWriter Create(String logName, Type classType)
	    {

		    ILogWriter log = new LogWriter(logName, classType);

		    return log;
	    }
    }
}
