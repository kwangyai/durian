using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib.Logger
{
    public interface ILogWriter
    {
        void LogDebug(String message);
        void LogDebug(String message, Exception exc);
        void LogInfo(String message);
        void LogInfo(String message, Exception exc);
        void LogError(String message);
        void LogError(String message, Exception exc);

    }
}
