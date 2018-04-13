using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    public partial class DbObject
    {

        #region "Private static Property"
        //private static readonly string dataProvider = ConfigurationManager.AppSettings.Get("DataProvider");
        //private static readonly DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

        //private static readonly string ConnectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
        //private static readonly string ConfigConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        #endregion

        #region "Member variable"
        private SqlConnection m_Connection;
        private string m_ConnectionString;
        #endregion

        #region "Public Property"
        public SqlConnection Connection
        {
            get
            {
                if (m_Connection == null)
                    m_Connection = new SqlConnection(m_ConnectionString);
                return m_Connection;
            }
            set
            {
                m_Connection = value;
            }
        }
        //public string ConnectionString
        //{
        //    get
        //    {
        //        return m_ConnectionString;
        //    }
        //    set
        //    {
        //        m_ConnectionString = value;
        //    }
        //}
        #endregion

        #region "Constructor"
        /// <summary>
        /// su dung default connectstring
        /// </summary>
        public DbObject()
        {

        }
        public DbObject(string newConnectionString)
        {
            //this.ConnectionString = newConnectionString;
            this.Connection = new SqlConnection(newConnectionString);
        }
        /// <summary>
        /// su dung default connectstring
        /// </summary>
        /// <returns></returns>
        public static DbObject CreateInstant()
        {
            return new DbObject();
        }
        public static DbObject CreateInstant(string connectionStrings)
        {
            return new DbObject(connectionStrings);
        }
        #endregion

        ~DbObject()
        {
            if (this.Connection.State != ConnectionState.Closed)
            {
                // this.Connection.Close();
            }

            //this.ConnectionString = null;
            this.Connection = null;
        }

        #region "Private Method"
        private SqlCommand BuildIntCommand(string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = this.BuildQueryCommand(storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        private SqlCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, this.Connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }
        #endregion

        #region "Public Method - RunProdure"
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            this.Connection.Open();
            SqlCommand command = this.BuildQueryCommand(storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            return command.ExecuteReader();
        }

        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            //int num = 0;
            //try
            //{
            this.Connection.Open();
            SqlCommand command = this.BuildIntCommand(storedProcName, parameters);
            //command.CommandTimeout = 500;
            rowsAffected = command.ExecuteNonQuery();
            int num = (int)command.Parameters["ReturnValue"].Value;
            this.Connection.Close();
            //}
            //catch
            //{
            //    rowsAffected = 0;
            //    this.Connection.Close();
            //}
            return num;
        }

        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out string output)
        {
            this.Connection.Open();
            SqlCommand command = this.BuildIntCommand(storedProcName, parameters);
            command.ExecuteNonQuery();
            output = parameters[parameters.Length - 1].Value.ToString();
            int num = (int)command.Parameters["ReturnValue"].Value;
            this.Connection.Close();
            return num;
        }

        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            DataSet dataSet = new DataSet();
            try
            {
                this.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = this.BuildQueryCommand(storedProcName, parameters);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet, tableName);
                this.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Connection.Close();
            }
            return dataSet;
        }

        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, out string output)
        {
            DataSet dataSet = new DataSet();
            try
            {
                this.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = this.BuildQueryCommand(storedProcName, parameters);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet, tableName);
                output = "";
                foreach (IDataParameter para in parameters)
                {
                    if (para.Direction == ParameterDirection.InputOutput)
                    {
                        output = para.Value.ToString();
                        break;
                    }
                }
                this.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Connection.Close();
            }
            return dataSet;
        }

        public void RunProcedure(string storedProcName, IDataParameter[] parameters, DataSet dataSet, string tableName)
        {
            //try
            //{
            this.Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = this.BuildIntCommand(storedProcName, parameters);
            adapter.Fill(dataSet, tableName);
            this.Connection.Close();
            //}
            //catch
            //{
            //    this.Connection.Close();
            //}
            this.Connection.Close();
        }

        public DataSet RunQuery(string Query, string tableName)
        {
            DataSet dataSet = new DataSet();
            //try
            //{
            this.Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(Query, this.Connection);
            command.CommandType = CommandType.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, tableName);
            this.Connection.Close();
            //}
            //catch
            //{
            //    this.Connection.Close();
            //}
            return dataSet;
        }
        #endregion

        #region "Public Method - RunProdure - No care connection"
        /// <summary>
        /// Run store - Open/Close connection manual
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SqlDataReader RunProcedureCon(string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = this.BuildQueryCommand(storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            return command.ExecuteReader();
        }

        /// <summary>
        /// Run store - Open/Close connection manual
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="rowsAffected"></param>
        /// <returns></returns>
        public int RunProcedureCon(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            SqlCommand command = this.BuildIntCommand(storedProcName, parameters);
            rowsAffected = command.ExecuteNonQuery();
            int num = (int)command.Parameters["ReturnValue"].Value;
            return num;
        }

        /// <summary>
        /// Run store - Open/Close connection manual
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet RunProcedureCon(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = this.BuildQueryCommand(storedProcName, parameters);
            adapter.Fill(dataSet, tableName);
            return dataSet;
        }

        /// <summary>
        /// Run store - Open/Close connection manual
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        public void RunProcedureCon(string storedProcName, IDataParameter[] parameters, DataSet dataSet, string tableName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = this.BuildIntCommand(storedProcName, parameters);
            adapter.Fill(dataSet, tableName);
        }

        public DataSet RunQuery(string Query, string tableName, bool isCloseConnection)
        {
            if (isCloseConnection)
                return RunQuery(Query, tableName);
            else
            {
                DataSet dataSet = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(Query, this.Connection);
                command.CommandType = CommandType.Text;
                adapter.SelectCommand = command;
                adapter.Fill(dataSet, tableName);
                return dataSet;
            }
        }
        #endregion
    }
}
