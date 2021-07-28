using Rki.ImportToSql.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rki.ImportToSql.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String server = "(localdb)\\MSSQLLocalDB";
        String database = "StagingArea";

        private string csvPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string item in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (item.EndsWith(".csv")) csvPath = item;
                    tbCsvPath.Text = csvPath;
                }
            }
        }

        private void Test_DataTableHelper()
        {
            DataTableHelper dataTableHelper = new DataTableHelper(CsvHelper.readCsv(csvPath, ';'));
            if (!dataTableHelper.getColumnByName("Zahl").parseIntoContentType(ContentType.INT)) MessageBox.Show("Inhalt konnte nicht umgewandelt werden!");
            if (!dataTableHelper.getColumnByName("Datum").parseIntoContentType(ContentType.DATETIME)) MessageBox.Show("Datum konnte nicht umgewandelt werden!");

            SqlHelper.SqlDataTypes[] helper = { SqlHelper.SqlDataTypes.NVARCHAR_MAX, SqlHelper.SqlDataTypes.INT, SqlHelper.SqlDataTypes.DATETIME };

            SqlHelper.createTable(server, database, dataTableHelper.getColumnNames(), helper, "Test", "dbo");

            //List<String> tables =  SqlHelper.GetTablesList(server, database);
            //DataTable tab = SqlHelper.getTableInformation(server, database, tables[0].ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Test_DataTableHelper();
        }
    }



}
