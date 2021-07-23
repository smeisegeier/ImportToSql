using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Rki.ImportToSql.Helper
{
    class DataTableHelper
    {
        private DataTable dataTable;

        private List<Column> columns = new List<Column>();
        

        public DataTableHelper(DataTable dataTable)
        {
            this.dataTable = dataTable;

            for(int i = 0; i < dataTable.Columns.Count; i++)
            {
                columns.Add(new Column(this.dataTable.Columns[i]));
            }
        }

        public Column getColumnByName(String name)
        {
            foreach(Column item in columns)
            {
                if (item.getColumnHeader() == name) return item;
            }

            return null;

        }


        public bool createSqlTable()
        {

            return false;
        }


        public static string getDataTypeByContentType(ContentType type)
        {
            switch (type)
            {
                case ContentType.STRING:
                    return "nvarchar(max)";
                case ContentType.INT:
                    return "int";
                case ContentType.DATETIME:
                    return "datetime2(0)";
                default:
                    throw new Exception(type.ToString() + " wurde nicht behandelt") ;
            }
        }

    }

    class Column
    {
        private String header;
        private ContentType type;
        private List<ColumnContent> columnContents = new List<ColumnContent>();

        public Column(DataColumn dataColumn, ContentType type = ContentType.STRING)
        {
            if (dataColumn.Table.Rows.Count > 1)
            {
                header = dataColumn.ColumnName;
                this.type = type;

                for(int i = 1; i < dataColumn.Table.Rows.Count; i++)
                {
                    //columnContents.Add(new ColumnContent(dataColumn.Table.Rows[i].ToString(), this.type));
                    columnContents.Add(new ColumnContent(dataColumn.Table.Rows[i][dataColumn.ColumnName].ToString(), this.type));

                }

            }
        }

        public bool parseIntoContentType(ContentType type)
        {
            foreach(ColumnContent item in columnContents)
            {
                if (item.tryParseIntoContentType(type) == false) return false;
            }
            this.type = type;

            return true;
        }


        public String getColumnHeader()
        {
            return header;
        }

    }

    class ColumnContent
    {
        private ContentType type;
        private String content;

        public ColumnContent(String content)
        {
            this.content = content;
            this.type = ContentType.STRING;
        }

        public ColumnContent(String content, ContentType type) : this(content)
        {
            if (!tryParseIntoContentType(type)) throw new Exception(content + " konnte nicht in zum Contenttyp " + type.ToString() + " umgewandelt werden!");
        }

        public bool tryParseIntoContentType(ContentType type)
        {
            if (this.type == type) return true;

            switch (type)
            {
                case ContentType.STRING:
                    type = ContentType.STRING;
                    break;
                case ContentType.INT:

                    try
                    {
                        int.Parse(content);
                        type = ContentType.INT;
                    } catch { return false; };
                    break;

                case ContentType.DATETIME:
                    try
                    {
                        DateTime.Parse(content);
                        type = ContentType.DATETIME;
                    }
                    catch { return false; };
                    break;
                default:
                    return false;
            }

            this.type = type;

            return true;
        }
    }

    enum ContentType {INT, STRING, DATETIME}


}
