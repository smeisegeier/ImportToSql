using System;
using System.Collections.Generic;
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

        public string DropFilePathFull { get; set; } = @"C:\Users\MeisegeierS\Desktop\lol.csv";

        public MainWindow()
        {
            InitializeComponent();
        }

        // HACK this should be in ViewModel
        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string item in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (item.EndsWith(".csv") || item.EndsWith(".txt"))
                        tbCsvPath.Text = DropFilePathFull = item;
                }
            }
        }
    }



}
