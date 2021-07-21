using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Helper
{
    static class CsvHelper
    {

        public static DataTable readCsv(string path, char seperator)
        {
            DataTable _table = new DataTable();
            String[] csv = File.ReadAllLines(@path);
            string[] zeile = new string[csv.Length];

            int anzahlColumns = 0;

            for (int i = 0; i < csv.Length; i++)
            {
                zeile = csv[i].Split(seperator);

                if (i == 0)
                {
                    foreach (string item in zeile)
                    {
                        if (_table.Columns[item] == null)
                        {
                            _table.Columns.Add(item);
                        }
                        else
                        {
                            bool newNameFound = false;
                            int j = 1;
                            while (!newNameFound)
                            {
                                if (_table.Columns[item + '_' + j] == null)
                                {
                                    _table.Columns.Add(item + "_" + j);
                                    newNameFound = true;
                                }
                                j++;
                            }
                        }
                        anzahlColumns++;
                    }
                }

                //Fehler wird nur geworfen, wenn es mehr Inhalte als es Spalten gibt
                if (zeile.Length > anzahlColumns) throw new Exception("Anzahl der Spalten ist nicht gleichbleibend!");
                _table.Rows.Add(zeile);
            }

            return _table;
        }

    }
}
