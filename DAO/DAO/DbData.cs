using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    public class DbData
    {
        private string m_ConnectionString = "";

        protected string ConnectionString
        {
            get { return m_ConnectionString; }
            set { m_ConnectionString = value; }
        }

        public DbData()
        {
        }

        protected DataTable GetData(string store, SqlParameter[] paras = null)
        {
            DataTable dt = null;
            DbObject db = new DbObject(ConnectionString);
            DataSet ds;
            if (paras == null)
                paras = new SqlParameter[] { };
            ds = db.RunProcedure(store, paras, "t");
            if (ds != null && ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        protected DataSet GetDataSet(string store, SqlParameter[] paras = null)
        {
            DbObject db = new DbObject(ConnectionString);
            DataSet ds;
            if (paras == null)
                paras = new SqlParameter[] { };
            ds = db.RunProcedure(store, paras, "t");
            return ds;
        }

        protected DataTable GetData(string store, SqlParameter[] paras, out string output)
        {
            DataTable dt = null;
            DbObject db = new DbObject(ConnectionString);
            DataSet ds;
            if (paras == null)
                paras = new SqlParameter[] { };
            ds = db.RunProcedure(store, paras, "t", out output);
            if (ds != null && ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        protected DataSet GetDataSet(string store, SqlParameter[] paras, out string output)
        {
            DbObject db = new DbObject(ConnectionString);
            DataSet ds;
            if (paras == null)
                paras = new SqlParameter[] { };
            ds = db.RunProcedure(store, paras, "t", out output);
            return ds;
        }

        protected int ReturnData(string store, SqlParameter[] paras)
        {
            DbObject db = new DbObject(ConnectionString);
            if (paras == null)
                paras = new SqlParameter[] { };
            int rowsAffected;
            int result = db.RunProcedure(store, paras, out rowsAffected);
            return result;
        }

        protected int ReturnData(string store, SqlParameter[] paras, out string output)
        {
            DbObject db = new DbObject(ConnectionString);
            if (paras == null)
                paras = new SqlParameter[] { };
            int result = db.RunProcedure(store, paras, out output);
            return result;
        }

        protected bool UpdateData(string store, SqlParameter[] paras)
        {
            DbObject db = new DbObject(ConnectionString);
            if (paras == null)
                paras = new SqlParameter[] { };
            int rowsAffected = 0;
            int result = db.RunProcedure(store, paras, out rowsAffected);
            if (rowsAffected > 0)
                return true;
            return false;
        }

        protected int UpdateData(string store, SqlParameter[] paras, out string output)
        {
            DbObject db = new DbObject(ConnectionString);
            if (paras == null)
                paras = new SqlParameter[] { };
            int result = db.RunProcedure(store, paras, out output);
            return result;
        }
    }
}
