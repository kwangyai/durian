using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib.Util
{
    public class TimeStamp : System.IDisposable
    {

        #region Constructor & Destructor & Dispose Memory Release

        public TimeStamp()
        {
        }

        ~TimeStamp()
        {
            this.DisposeMemory();
        }

        private void DisposeMemory()
        {
            System.GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            this.DisposeMemory();
        }

        #endregion


        public static DateTime ToDateTime(double p_timeSpan)
        {
            DateTime t = Convert.ToDateTime("1/1/1970 7:00:00 AM");

            return t.AddSeconds(p_timeSpan);
        }

        public static double ToUnixTimeStamp()
        {
            DateTime dtCurTime = DateTime.Now;

            TimeSpan ts = dtCurTime.Subtract(Convert.ToDateTime("1/1/1970 7:00:00 AM"));

            return Math.Ceiling(ts.TotalSeconds);
        }

        public static double ToUnixTimeStamp(DateTime dt)
        {
           
            TimeSpan ts = dt.Subtract(Convert.ToDateTime("1/1/1970 7:00:00 AM"));

            return Math.Ceiling(ts.TotalSeconds);
        }

    }
}
