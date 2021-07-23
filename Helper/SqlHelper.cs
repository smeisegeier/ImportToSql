using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Helper
{
    static class SqlHelper
    {
        private static string GetConnectionStringSQL(String _server, String _database, int timeout = 30)
        {
            return "Data Source=" + _server + ";Initial Catalog=" + _database + ";Integrated Security=True;Connection Timeout=" + timeout.ToString();
        }


        public static List<string> GetTablesList(String _server, String _database, string schema = "dbo")
        {
            List<string> list = new List<string>();
            string conStr = GetConnectionStringSQL(_server, _database, 5);
            using (SqlConnection SqlConnection = new SqlConnection(conStr))
            {
                SqlConnection.Open();
                using (SqlCommand SqlCommand = new SqlCommand("select TABLE_NAME from information_schema.tables where TABLE_SCHEMA = '" + schema + "'", SqlConnection))
                {
                    using (IDataReader dr = SqlCommand.ExecuteReader())
                    {
                        while (dr.Read())
                            list.Add(dr[0].ToString());
                    }
                }
            }
            return list;
        }

        public static DataTable getTableInformation(String _server, String _database, String _table)
        {
            DataTable _retDatatable = new DataTable();

            string conStr = GetConnectionStringSQL(_server, _database, 5);
            using (SqlConnection SqlConnection = new SqlConnection(conStr))
            {
                SqlConnection.Open();
                using (SqlCommand SqlCommand = new SqlCommand("SELECT TABLE_NAME, COLUMN_NAME, ORDINAL_POSITION, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, DATETIME_PRECISION FROM StagingArea.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '" + _table + "'", SqlConnection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(SqlCommand))
                    {
                        da.Fill(_retDatatable);
                    }
                }
            }

            return _retDatatable;
        }

    }
}
