using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Modules
{
    public class WebModules
    {
        private string _ProcName = "";
        private string ProcName { set { _ProcName = value; } }
        private string ConString()
        {
            string ConStr = ConfigurationManager.ConnectionStrings["MyWeb"].ConnectionString;
            return ConStr;
        }
        [NonAction]
        public string getVal(string ProcName, string[] Para,String[] ParaVal)
        {
            string val = "";
            using (DataTable dt=new DataTable())
            {
                FillTable(ProcName, Para, ParaVal, dt);
                if (dt.Rows.Count > 0)
                {
                    val = dt.Rows[0][0].ToString();
                }
            }
            return val;
        }
        public DataTable FillTable(string ProcName, string[] Para, String[] ParaVal,DataTable dt)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConString());
            builder.AsynchronousProcessing = true;
            using (SqlConnection con=new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = con.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                this.ProcName = ProcName;
                setParameter(command);
                cmdHelper(command, Para, ParaVal);
                if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken) con.Open();
                using(SqlDataAdapter da=new SqlDataAdapter(command))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public DataTable FillTable(string Sql, DataTable dt)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConString());
            builder.AsynchronousProcessing = true;
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Sql, con))
                {
                    if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken) con.Open();
                    command.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public DataSet FillDataSet(string ProcName, string[] Para, String[] ParaVal, DataSet ds)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConString());
            builder.AsynchronousProcessing = true;
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = con.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                this.ProcName = ProcName;
                setParameter(command);
                cmdHelper(command, Para, ParaVal);
                if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken) con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.Fill(ds);
                }
            }
            return ds;
        }
        public DataSet FillDataSet(string Sql,DataSet ds)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConString());
            builder.AsynchronousProcessing = true;
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand command=new SqlCommand(Sql, con))
                {
                    if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken) con.Open();
                    command.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

        public void ExecQuery(string ProcName, string[] Para, String[] ParaVal)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConString());
            builder.AsynchronousProcessing = true;
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = con.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                this.ProcName = ProcName;
                setParameter(command);
                cmdHelper(command, Para, ParaVal);
                if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken) con.Open();
                IAsyncResult result = command.BeginExecuteNonQuery();
                command.EndExecuteNonQuery(result);
            }
        }
        private void setParameter(SqlCommand _Command)
        {
            _Command.CommandText = _ProcName;
            _Command.Parameters.Clear();
        }
        public void cmdHelper(SqlCommand cmd, string[] Para, String[] ParaVal)
        {
            cmd.Parameters.Clear();
            if (!DBNull.Equals(Para, null))
            {
                for (int i = 0; i < Para.Length; i++)
                {
                    cmd.Parameters.AddWithValue(Para[i].ToString(), ParaVal[i].ToString());
                }
            }
        }
    }
}