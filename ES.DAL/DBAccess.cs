using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.DAL
{
    internal class DBAccess
    {
        public DBAccess(CurrentUserModel currentUser)
        {
            SqlConConnectionString = currentUser.ConnectionString;
        }

        private string SqlConConnectionString { get; set; }

        public DataTable ExecReader(SqlCommand sqlcomm)
        {
            SqlConnection m_SqlCon = new SqlConnection();
            m_SqlCon.ConnectionString = SqlConConnectionString;

            sqlcomm.Connection = m_SqlCon;

            DataTable dt = new DataTable();
            m_SqlCon.Open();

            dt.Load(sqlcomm.ExecuteReader());

            m_SqlCon.Close();

            return dt;
        }

        public object ExecScalar(SqlCommand sqlcomm)
        {
            SqlConnection m_SqlCon = new SqlConnection();
            m_SqlCon.ConnectionString = SqlConConnectionString;

            sqlcomm.Connection = m_SqlCon;

            m_SqlCon.Open();

            object o = sqlcomm.ExecuteScalar();

            m_SqlCon.Close();

            return o;
        }

        public void ExecNonQuery(SqlCommand sqlcomm)
        {
            SqlConnection m_SqlCon = new SqlConnection();
            m_SqlCon.ConnectionString = SqlConConnectionString;

            sqlcomm.Connection = m_SqlCon;

            m_SqlCon.Open();

            sqlcomm.ExecuteNonQuery();

            m_SqlCon.Close();
        }
    }
}