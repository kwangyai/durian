using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib
{
    public static class General
    {
        public static bool GetBit(long Inp, int Bit)
        {
            return ((Inp >> Bit) & 1) > 0;
        }

        public static long SetBit(long Inp, int Bit, bool En)
        {
            long BitVal = (1 << Bit);
            if ((En))
            {
                return Inp | BitVal;
            }
            return Inp & (~BitVal);

        }

        public static string GetStrBefore(string Ch, string Inp, bool IncludeCh)
        {
            int Pos = Inp.IndexOf(Ch);
            if (Pos >= 0)
            {
                if (IncludeCh)
                {
                    return Inp.Substring(0, Pos + Ch.Length);
                }
                return Inp.Substring(0, Pos);
            }
            return "";
        }

        public static string GetStrAfter(string Ch, string Inp, bool IncludeCh)
        {
            int Pos = Inp.IndexOf(Ch);
            if (Pos >= 0)
            {
                if (IncludeCh == false)
                {
                    Pos += Ch.Length;
                }
                return Inp.Substring(Pos);
            }
            return "";
        }

        public static string GetStrLimit(string Inp, int Max, string Additional)
        {

            if ((Inp.Length > Max))
            {
                return Inp.Substring(0, Max) + Additional;
            }
            return Inp;

        }

        public static string GetStrRemoveBetween(string Inp, string StCh, string EnCh)
        {
            string ResStr = Inp;
            string CurStr = Inp;

            int StPos = 0;
            int EnPos = 0;

            while ((StPos >= 0))
            {
                StPos = ResStr.IndexOf(StCh);
                EnPos = ResStr.IndexOf(EnCh);

                if ((StPos < 0))
                {
                    return ResStr;
                }

                CurStr = ResStr.Substring(0, StPos);
                if ((EnPos >= 0))
                {
                    CurStr = CurStr + ResStr.Substring(EnPos + EnCh.Length);
                }
                ResStr = CurStr;
            }
            return ResStr;
        }






        public static string GetStrBetween(string St, string En, string Inp, bool IncludeCh)
        {
            int StPos = Inp.IndexOf(St);
            if (StPos >= 0)
            {
                int EnPos = Inp.IndexOf(En, StPos);
                if (EnPos < 0)
                {
                    EnPos = Inp.Length;
                }
                else
                {
                    if (IncludeCh == true)
                    {
                        EnPos += En.Length;
                    }
                }

                if (IncludeCh == false)
                {
                    StPos += St.Length;
                }


                return Inp.Substring(StPos, EnPos - StPos);
            }
            return "";
        }







    }
}
