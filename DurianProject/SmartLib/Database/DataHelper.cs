using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using SmartLib.Logger;

namespace SmartLib.Database
{
    public class DataTableReader : DataTable 
    {

    }

    public delegate void DataHelperErrorHandler(string Func, string Query, Exception Ex);

    public class DataHelper
    {
        private AutoTransaction TransMode = AutoTransaction.Auto;
        private TransactionScope Trans = null ;

        public event DataHelperErrorHandler Error;
        public TimeSpan TransactionTimeout = new TimeSpan(0, 10, 0);

        ILogWriter log = LogWriterFactory.Create( LoggerName.ApplicationLog, typeof(DataHelper));


        public bool Transaction_Begin()
        {
            try
            {
                TransMode = AutoTransaction.Manual;
                TransactionOptions tsOpt = new TransactionOptions();
                tsOpt.Timeout = TransactionTimeout;
                //tsOpt.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
               

                if (Trans == null)
                    Trans = new TransactionScope(TransactionScopeOption.RequiresNew, tsOpt);

                log.LogDebug("Transaction_Begin: IsolationLevel=" + tsOpt.IsolationLevel.ToString());
                
                return true;
            }
            catch (Exception Ex)
            {
                if (Error != null)
                    Error("Transaction_Begin", "", Ex);
            }
            return false;
        }

        public bool Transaction_End(bool Success)
        {
            if (Trans != null)
            {
                if (Success)
                {
                    Trans.Complete();
                }               
                Trans.Dispose();
                Trans = null;
                TransMode = AutoTransaction.Auto;
                return true;
            }
            return false;
        }


        // Select and Result as a Single Value
        public object SelectValue(string SqlQuery, object Def)
        {            
            try
            {                
                QueryCommand SqlCmd = new QueryCommand();
                SqlCmd.CommandText = SqlQuery;
                SqlCmd.CommandType = CommandType.Text;
                SqlCmd.Prepare();

                object Res = DataService.ExecuteScalar(SqlCmd);
                if (Res == DBNull.Value)
                    return null;

                return Res;
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error("SelectValue", SqlQuery, ex);
            }
            return Def;
        }


        // Select and Result as a DataTable Type
        public DataTable SelectTable(string SqlQuery)
        {
            return SelectTable(SqlQuery, "");
        }

        public DataTable SelectTable(string SqlQuery, string TbName, params object[] Inps)
        {

            try
            {
                QueryCommand SqlCmd = new QueryCommand();
                SqlCmd.CommandText = SqlQuery;
                SqlCmd.CommandType = CommandType.Text;

                if (Inps.Length > 0)
                {
                    int Index = 0;
                    while (Index <= Inps.Length)
                    {
                        SqlCmd.SetParam(Inps[Index].ToString(), Inps[Index + 1]);
                        Index += 2;
                    }
                    
                }
                return DataService.ExecuteQuery(SqlCmd);                    
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error("SelectTable", SqlQuery, ex);
            }
            return null;
        }

        // Select and Result as a DataRow Type
        public DataRow SelectTableRow(string SqlQuery)
        {
            return SelectTableRow(SqlQuery, 0);
        }

        public DataRow SelectTableRow(string SqlQuery, int Row)
        {
            DataTable ResTable = SelectTable(SqlQuery, null);
            if (((ResTable != null)))
            {
                if (((ResTable.Rows.Count > 0) & (Row < ResTable.Rows.Count)))
                {
                    return ResTable.Rows[Row];
                }
            }
            return null;
        }

        public int ExecCommand(string SqlQuery)
        {
            try
            {
                QueryCommand SqlCmd = new QueryCommand();
                SqlCmd.CommandText = SqlQuery;
                SqlCmd.CommandType = CommandType.Text;

                return DataService.ExecuteNonQuery(SqlCmd, TransMode);
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error("Delete_From", SqlQuery, ex);
            }
            return 0;
        }


        public int Increase_Value(string TableName, string FieldName, int StartValue, int IncreaseValue, int MaxValue, bool ReturnAfter)
        {            
            object Current = SelectValue(string.Format("Select {1} From {0}", TableName, FieldName), StartValue);
            int Val = StartValue;
            int Res = Val;

            if (Current != DBNull.Value)
            {
                Val = (int)Current;
                Res = Val;
                Val += IncreaseValue;
            }

            if (ReturnAfter)
                Res = Val;            

            Update_Objects(TableName, FieldName, Val) ;

            return Res;
        }


        public int ExecProcedure(string ProcName, params object[] Inps)
        {
            try
            {
               System.Data.IDbCommand SqlCmd = Db.CreateCommand();
                SqlCmd.CommandText = ProcName;
                SqlCmd.CommandType = CommandType.StoredProcedure;

                int Index = 0;
                while (Index + 3 <= Inps.Length)
                {
                    string Name = Inps[Index].ToString();
                    object Val = Inps[Index + 1];
                    bool OpType = (bool)Inps[Index + 2];

                    System.Data.IDbDataParameter SqlParam = SqlCmd.CreateParameter();

                    SqlParam.ParameterName = Name;
                    SqlParam.Value = Val;
                    SqlParam.Direction = OpType ? ParameterDirection.Output : ParameterDirection.Input;

                    SqlCmd.Parameters.Add(SqlParam);

                    Index += 3;

                }


                int Res = SqlCmd.ExecuteNonQuery();

                int ParamIndex = 0;
                Index = 0;
                while (Index + 3 <= Inps.Length)
                {
                    System.Data.IDbDataParameter SqlParam = (System.Data.IDbDataParameter)SqlCmd.Parameters[ParamIndex];

                    if (SqlParam.Direction == ParameterDirection.Output)
                        Inps[Index + 2] = SqlParam.Value;

                    Index += 3;
                    ParamIndex++;
                }

                return Res;

            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error("ExecProcedure", ProcName, ex);
            }

            return -1;
        }

        public int Insert_Objects(string TableName, params object[] Inps)
        {
            try
            {
                SqlEasy SqlCmd = new SqlEasy();

                SqlCmd.NewInsertStatement(TableName);

                int Index = 0;
                while (Index + 2 <= Inps.Length)
                {                    
                    string Name = Inps[Index].ToString();
                    object Val = Inps[Index + 1];
                    SqlCmd.SetColumnValue(Name, Val);
                    
                    Index += 2;
                }

                int[] Res = SqlCmd.ExecuteNonQuery(TransMode);
                return Res[0];

            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error("Insert_Objects", TableName, ex);
            }
            return -1;
        }

        public int Insert_Values(string TableName, params string[] Inps)
        {
            return Insert_Objects(TableName, Inps);
        }


        public int Update_Objects(string TableName, params object[] Inps)
        {
            try
            {
                SqlEasy SqlCmd = new SqlEasy();

                SqlCmd.NewUpdateStatement(TableName);

                int Len = Inps.Length ;
                int Index = 0;
                while (Index + 2 <= Len)
                {
                    string Name = Inps[Index].ToString();
                    object Val = Inps[Index + 1];
                    SqlCmd.SetColumnValue(Name, Val);

                    Index += 2;
                }

                if (Index < Len )
                    SqlCmd.Where(Inps[Index].ToString());


                int[] Res = SqlCmd.ExecuteNonQuery(TransMode);
                return Res[0];

            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error("Update_Objects", TableName, ex);
            }
            return -1;
        }

        public int Update_Values(string TableName, params string[] Inps)
        {
            return Update_Objects(TableName, Inps);
        }


        public int Delete_From(string TableName, string Where)
        {
            string SqlQuery = "";
            try
            {
                SqlQuery = string.Format("Delete From {0} Where {1} ", TableName, Where);
                return ExecCommand(SqlQuery);
            }
            catch (Exception ex)
            {
                if (Error != null)
                    Error("Delete_From", SqlQuery, ex);
            }
            return 0;
        }


        public bool ClearTable(string TableName)
        {
            return TrancateTable(TableName);
        }

        public bool TrancateTable(string TableName)
        {
            int Res = ExecCommand("Truncate Table " + TableName);
            return (Res > 0) | (Res == -1);
        }




        public object GetDateTimeNowToSqlStr()
        {
            return GetDateTimeToSqlStr(DateTime.Now);
        }

        public string GetDateTimeToSqlStr(DateTime Inp, bool Standard)
        {
            if (Standard)
                return Inp.ToString("s");
            return string.Format("{2:0000}-{1:00}-{0:00} {3:00}:{4:00}:{5:00}", Inp.Day, Inp.Month, Inp.Year, Inp.Hour, Inp.Minute, Inp.Second);
        }

        public string GetDateTimeToSqlStr(DateTime Inp)
        {
            return GetDateTimeToSqlStr(Inp, true);
        }

        public string[] GetPairedObjToSqlStr(object[] Inps)
        {
            int MaxArgs = Inps.Length - 1;
            string[] Res = new string[MaxArgs + 1];
            object InpVal;
            string ResVal;

            for (int Index = 0; Index <= MaxArgs; Index++)
            {

                InpVal = Inps[Index];
                ResVal = "";
                if (((Index % 2) == 0))
                {
                    ResVal = InpVal.ToString();
                }
                else
                {
                    if ((Inps[Index] == null))
                    {
                        ResVal = "null";
                    }
                    else
                    {
                        if ((InpVal is string))
                        {
                            ResVal = "'" + (string)InpVal + "'";
                        }
                        else if ((InpVal is char))
                        {
                            ResVal = "'" + (string)InpVal + "'";
                        }
                        else if ((InpVal is DateTime))
                        {
                            if ((InpVal == null))
                            {
                                ResVal = "null";
                            }
                            else
                            {
                                ResVal = "'" + GetDateTimeToSqlStr((DateTime)InpVal) + "'";
                            }
                        }

                        else
                        {
                            ResVal = InpVal.ToString();
                        }
                    }

                }
                Res[Index] = ResVal;
            }
            return Res;
        }



    }
}
