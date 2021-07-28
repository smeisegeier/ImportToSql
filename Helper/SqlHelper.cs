using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Helper
{
    static class SqlHelper
    {
        private static Regex reg_columnName = new Regex("[^a-zA-Z0-9_]+$");


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


        public static bool createTable(String _server, String _database, String[] _columns, SqlDataTypes[] _datatypes, String _table, String _schema = "dbo")
        {
            //exists table already?
            if (existsTable(_server, _database, _table, _schema)) return false;

            //same number of columns and datatypes?
            if (_columns.Length != _datatypes.Length) return false;


            //check names of columns
            if (!checkColumNames(_columns)) return false;

            //create table
            string conStr = GetConnectionStringSQL(_server, _database, 5);
            using (SqlConnection SqlConnection = new SqlConnection(conStr))
            {
                SqlConnection.Open();
                using (SqlCommand SqlCommand = new SqlCommand(sql_CreateTable(_server, _database, _columns, _datatypes, _table, _schema) , SqlConnection))
                {
                    try
                    {
                        SqlCommand.ExecuteNonQuery();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }


            return existsTable(_server, _database, _table, _schema);
        }

        private static bool existsTable(String _server, String _database, String _table, String _schema)
        {

            foreach (String item in GetTablesList(_server, _database, _schema))
            {
                if (item.ToString() == _table) return true;
            }

            return false;
        }


        private static String sql_CreateTable(String _server, String _database, String[] _columns, SqlDataTypes[] _sqlDataTypes, String _table, String _schema = "dbo")
        {

            String sql ="CREATE TABLE " + _schema + "." + _table + "(";

            for(int i = 0; i < _columns.Length; i++)
            {
                if (i > 0) sql += ",";
                sql += " " + _columns[i] + " " + getSqlDataTypeAsString(_sqlDataTypes[i]);
            }


            sql += ")";

            return sql;
        }




        private static bool checkColumNames(String[] _columns)
        {
            List<String> columnNames = new List<string>();

            foreach(String item in _columns)
            {
                //check used letters
                //if(!reg_columnName.IsMatch(item)) return false;
               
                //exists more than 1 columnheader with same name?
                if (columnNames.Contains(item))
                {
                    return false;
                } else
                {
                    columnNames.Add(item);
                }
            }

            return true;
        }


        public enum SqlDataTypes { NVARCHAR_MAX, INT, DATETIME}

        private static String getSqlDataTypeAsString(SqlDataTypes _datatypes)
        {
            switch (_datatypes)
            {
                case SqlDataTypes.NVARCHAR_MAX:
                    return "nvarchar(max)";
                case SqlDataTypes.INT:
                    return "int";
                case SqlDataTypes.DATETIME:
                    return "datetime2(0)";
                default:
                    throw new Exception("SqlDataType is not handled yet!");
            }
        }
    }
}
