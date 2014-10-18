using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

namespace DurianWeb.DataAccess
{
    public enum SqlHelperType
    {
        Insert = 1,
        InsertIdentity = 2,
        Update = 3,
        Delete = 4,
        DeleteFlag = 5
    }


    public class SqlHelper
    {

        private string _tableName;
        private string _where;
        private Dictionary<string, object> _column;
        private Dictionary<string, object> _whereParam;
        private SqlHelperType _statementType;
        private SqlConnection _con;

        private SqlTransaction _tran;
        

        public SqlHelper()
        {
            this._tableName = string.Empty;
            this._column = new Dictionary<string, object>();
            this._whereParam = new Dictionary<string, object>();
        }
        public SqlHelper(SqlConnection con, SqlTransaction tran)
        {
            this._tableName = string.Empty;
            this._column = new Dictionary<string, object>();
            this._whereParam = new Dictionary<string, object>();
            this._con = con;
            this._tran = tran;
        }

        public void NewInsertStatement(string tableName)
        {
            this._tableName = tableName;
            this._statementType = SqlHelperType.Insert;
        }

        public void NewInsertIdentityStatement(string tableName)
        {
            this._tableName = tableName;
            this._statementType = SqlHelperType.InsertIdentity;
        }

        public void NewUpdateStatement(string tableName)
        {
            this._tableName = tableName;
            this._statementType = SqlHelperType.Update;
        }

        public void NewDeleteStatement(string tableName)
        {
            this._tableName = tableName;
            this._statementType = SqlHelperType.Delete;
        }

        public void Where(string @where)
        {
            this._where = @where;
        }

        public void WhereParam(string ColumnName, object ColumnValue)
        {
            try
            {
                _whereParam.Add(ColumnName, ColumnValue);

            }
            catch (Exception ex)
            {
            }
        }

        public void SetColumnValue(string ColumnName, object ColumnValue)
        {
            try
            {
                _column.Add(ColumnName, ColumnValue);

            }
            catch (Exception ex)
            {
            }
        }

        public object Execute()
        {
            switch (this._statementType)
            {
                case SqlHelperType.Insert:
                    InsertExecute();
                    break;
                case SqlHelperType.InsertIdentity:
                    return InsertIdentity();
                case SqlHelperType.Update:
                    UpdateExecute();
                    break;
                case SqlHelperType.Delete:
                    DeleteExecute();
                    break;
            }

            return null;
        }


        private void InsertExecute()
        {
            SqlParameterCollection opc = new SqlCommand().Parameters;
            bool isFirst = true;
            string SqlVal = string.Empty;
            SqlParameter op = null;
            string sql = "INSERT INTO " + this._tableName + " (";
            foreach (object item_loopVariable in this._column)
            {
                item = item_loopVariable;
                if (isFirst)
                {
                    sql += item.Key.ToString();
                    isFirst = false;
                }
                else
                {
                    sql += "," + item.Key.ToString();
                }

                op = new SqlParameter();
                op.ParameterName = "@" + item.Key.ToString();
                op.Value = item.Value;
                opc.Add(op);

                if (SqlVal == string.Empty)
                {
                    SqlVal += "@" + item.Key.ToString();
                    isFirst = false;
                }
                else
                {
                    SqlVal += ",@" + item.Key.ToString();
                }

            }

            sql += ") VALUES (";
            sql += SqlVal;
            sql += ")";


            SqlUtility.SqlExecute(sql, opc);
        }

        private string InsertIdentity()
        {

            SqlParameterCollection opc = new SqlCommand().Parameters;
            bool isFirst = true;
            SqlParameter op = null;
            string SqlVal = string.Empty;
            string sql = "INSERT INTO " + this._tableName + " (";
            foreach (object item_loopVariable in this._column)
            {
                item = item_loopVariable;
                if (isFirst)
                {
                    sql += item.Key.ToString();
                    isFirst = false;
                }
                else
                {
                    sql += "," + item.Key.ToString();
                }


                op = new SqlParameter();
                op.ParameterName = "@" + item.Key.ToString();
                //op.SqlDbType = SqlUtility.GetSqlType(Me._tableName, item.Key.ToString())
                op.Value = item.Value;
                opc.Add(op);
                if (SqlVal == string.Empty)
                {
                    SqlVal += "@" + item.Key.ToString();
                }
                else
                {
                    SqlVal += ",@" + item.Key.ToString();
                }
            }

            sql += ") VALUES (";
            sql += SqlVal;
            sql += ") SELECT CAST(scope_identity() AS int)";

            if ((_con == null))
            {
                return SqlUtility.SqlExecuteScalar(sql, opc);
            }
            else
            {
                //Dim dt As New DataTable
                //dt = SqlUtility.SqlToTable(sql, opc, _tran, _con)
                //Return dt.Rows(0)(0)
                return SqlUtility.SqlExecuteScalar(sql, opc, _con, _tran);
            }

        }

        private void UpdateExecute()
        {
            SqlParameterCollection opc = new SqlCommand().Parameters;
            SqlParameter op = null;
            bool isFirst = true;

            string sql = "UPDATE " + this._tableName + " SET ";
            foreach (object item_loopVariable in this._column)
            {
                item = item_loopVariable;
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql += ",";
                }

                sql += item.Key.ToString() + " = @" + item.Key.ToString();
                op = new SqlParameter();
                op.ParameterName = "@" + item.Key.ToString();
                op.Value = item.Value;
                opc.Add(op);
            }

            if (!string.IsNullOrEmpty(this._where))
            {
                sql += " WHERE " + this._where;
            }

            foreach (object item_loopVariable in this._whereParam)
            {
                item = item_loopVariable;
                op = new SqlParameter();
                op.ParameterName = "@" + item.Key.ToString();
                op.Value = item.Value;
                opc.Add(op);
            }

            if ((_con == null))
            {
                SqlUtility.SqlExecute(sql, opc);
            }
            else
            {
                SqlUtility.SqlExecute(sql, opc, _tran, _con);
            }

        }

        private void DeleteExecute()
        {
            SqlParameterCollection opc = new SqlCommand().Parameters;
            SqlParameter op = null;
            bool isFirst = true;

            string sql = "DELETE FROM " + this._tableName;

            if (!string.IsNullOrEmpty(this._where))
            {
                sql += " WHERE " + this._where;
            }

            foreach (object item_loopVariable in this._whereParam)
            {
                item = item_loopVariable;
                op = new SqlParameter();
                op.ParameterName = "@" + item.Key.ToString();
                op.Value = item.Value;
                opc.Add(op);
            }

            SqlUtility.SqlExecute(sql, opc);
        }

    }
}
