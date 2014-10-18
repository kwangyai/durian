using System;
using System.Collections.Generic;
using System.Text;

namespace DmShared.Collections
{
    public class DelimitString
    {

        public char Separator = ',';
        public string Value;
        public string[] Values;

        public DelimitString()
        {
        }

        public DelimitString(string Inp)
        {
            Parse(Inp);
        }

        public int Parse()
        {
            Values = Value.Split(Separator);
            return Values.Length;
        }

        public int Parse(string Inp)
        {
            Value = Inp;
            return Parse();
        }

        public int Count
        {
            get
            {
                if ((Value == null))
                {
                    return 0;
                }
                return Values.Length;
            }
        }



        public string GetKey(int Index, string Def)
        {
            string CurVal = GetString(Index, Def);
            int SubIndex = CurVal.IndexOf("=");
            if ((SubIndex > 0))
            {
                return CurVal.Substring(0, SubIndex);
            }
            return Def;
        }


        public string GetString(int Index, string Def)
        {
            if (((Values != null)))
            {
                if ((Index < Values.Length))
                {
                    return Values[Index];
                }
            }
            return Def;
        }


        public string GetString(string Key, string Def)
        {
            if (((Values != null)))
            {
                for (int Cnt = 0; Cnt <= Values.Length - 1; Cnt++)
                {
                    string CurVal = GetString(Cnt, "");
                    if ((CurVal.Length > 0))
                    {
                        int SubIndex = CurVal.IndexOf("=");
                        if ((SubIndex > 0))
                        {
                            string PropName = CurVal.Substring(0, SubIndex);
                            if ((PropName == Key))
                            {
                                return CurVal.Substring(SubIndex + 1);
                            }
                        }
                    }
                }
            }
            return Def;
        }

        public int GetInteger(int Index, int Def)
        {
            try
            {
                string Val = GetString(Index, "0");
                if ((Val.Length > 0))
                {
                    return int.Parse(Val);
                }
            }
            catch
            {
            }
            return Def;
        }

    }
}
