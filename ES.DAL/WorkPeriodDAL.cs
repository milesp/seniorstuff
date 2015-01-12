using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.DAL
{
    public class WorkPeriodDAL
    {
        // This function returns the primary key of the inserted record.
        public int Insert()
        {
            string sql = "";

            sql += " INSERT INTO WorkPeriod ";
            sql += " ( [COLUMN NAMES] ) ";
            sql += " VALUES ";
            sql += " ( [PARAMETER NAMES] ) ";

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = sql;

            // Execute the SQL query

            return -1;
        }
    }
}
