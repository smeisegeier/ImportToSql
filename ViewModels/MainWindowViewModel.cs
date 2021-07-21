using ChoETL;
using Rki.ImportToSql.Helper;
using Rki.ImportToSql.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rki.ImportToSql.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand ExitCommand { get; private set; }
        public ICommand GotoUploadCommand { get; private set; }

        public ICommand UploadCommand { get; private set; }

        // CRIT window is messed up
        public MainWindowViewModel()
        {
            ExitCommand = new RelayCommand(o =>
            {
                App.Current.Shutdown();
            });

            GotoUploadCommand = new RelayCommand(o =>
            {
                UploadWindow uploadWindow = new UploadWindow();
                uploadWindow.DataContext = new UploadWindowViewModel();
                uploadWindow.Show();
                
            });

            UploadCommand = new RelayCommand(o => CsvToJson());
        }

        // https://stackoverflow.com/questions/10824165/converting-a-csv-file-to-json-using-c-sharp
        // https://www.codeproject.com/Articles/1145337/Cinchoo-ETL-CSV-Reader
        public void CsvToJson()
        {
            string csv = @"Id, Name, City
                1, Tom, NY
                2, Mark, NJ
                3, Lou, FL
                4, Smith, PA
                5, Raj, DC
                ";

            StringBuilder sb = new StringBuilder();
            using (var p = ChoCSVReader.LoadText(csv).WithFirstLineHeader())
            {
                using (var w = new ChoJSONWriter(sb))
                    w.Write(p);
            }
            StaticHelper.MyMessageBoxNotificationInfo(sb.ToString());
        }
    }
}
