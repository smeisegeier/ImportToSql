using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Helper
{
    class DataTableHelper
    {
        private DataTable dataTable;

        

        public DataTableHelper(DataTable dataTable)
        {
            this.dataTable = dataTable;
        }

    }

    class Coulmn
    {
        private String header;
        private ContentType type;
        private List<ColumnContent> columnContents;

        public Coulmn(DataColumn dataColumn)
        {
            if (dataColumn.Table.Rows.Count > 1)
            {
                header = dataColumn.ColumnName;
                type = ContentType.STRING;

                for(int i = 1; i < dataColumn.Table.Rows.Count; i++)
                {
                    columnContents.Add(new ColumnContent(dataColumn.Table.Rows[i].ToString(), ContentType.STRING));
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


        public ColumnContent(String content, ContentType type)
        {
            this.content = content;
            this.type = type;
        }

        public bool tryParseIntoContentType(ContentType type)
        {
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

            return true;
        }
    }

    enum ContentType {INT, STRING, DATETIME}


}
