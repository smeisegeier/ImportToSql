using ChoETL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rki.ImportToSql.Helper;
using Rki.ImportToSql.Models;
using Rki.ImportToSql.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Rki.ImportToSql.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        public ICommand ExitCommand { get; private set; }
        public ICommand UploadCommand { get; private set; }
        public RelayCommand<DragEventArgs> DropCommand { get; private set; }

        private string _dropFilePathFull = "xde";
        public string DropFilePathFull
        {
            get => _dropFilePathFull;
            set
            {
                _dropFilePathFull = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// As long as the RelayCommand is not fully implemented, the window object must be passed
        /// </summary>
        /// <param name="mainWindow">parent window</param>
        public MainWindowViewModel()
        {
            ExitCommand = new RelayCommand<object>(o =>
            {
                App.Current.Shutdown();
            });


            UploadCommand = new RelayCommand<object>(
                 o => onUpload(csvToJsonFromFullPath(DropFilePathFull)),
                 o => (DropFilePathFull?.Length > 5)
                );
            DropCommand = new RelayCommand<DragEventArgs>(grid_Drop);
        }

        // https://stackoverflow.com/questions/6205472/mvvm-passing-eventargs-as-command-parameter
        private void grid_Drop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (path.EndsWith(".csv") || path.EndsWith(".txt"))
                        DropFilePathFull = path;
                }
            }
        }


        private void onUpload(string json)
        {
            // TODO Schema Validation
            /*             
            var schema = Test.Schema;
            var schemaList = TestList.Schema;
            var errors = Test.Schema.Validate(json);
            var errors2 = Base.Schema.Validate(json);
            */

            if (json.ToJsonTryParse(out List<Test1> list1))
            {
                //processUpload(list1);
                Globals.RepoTest1.Tests1AddItem(list1);
                StaticHelper.MyMessageBoxNotificationInfo(Base.PrintList(Globals.RepoTest1.Tests1GetItems()));
            }


            if (json.ToJsonTryParse(out List<Test2> list2))
            {
                processUpload(list2);
            }
        }

        private void processUpload<T>(List<T> list) where T : Base
        {
            // no entries?
            if (!list.Any())
                return;

            /* Fetch repo */

            /* Add List to repo */

            /* Feedback to user */
            StaticHelper.MyMessageBoxNotificationInfo(list[0].Message);
            //StaticHelper.MyMessageBoxNotificationInfo());
        }

        // https://stackoverflow.com/questions/10824165/converting-a-csv-file-to-json-using-c-sharp
        // https://www.codeproject.com/Articles/1145337/Cinchoo-ETL-CSV-Reader
        private string csvToJsonFromContent(string content)
        {

            StringBuilder sb = new StringBuilder();
            using (var p = ChoCSVReader.LoadText(content).WithFirstLineHeader())
            {
                using (var w = new ChoJSONWriter(sb))
                    w.Write(p);
            }
            return sb.ToString();
        }

        private string csvToJsonFromFullPath(string fullPath)
        {
            StringBuilder sb = new StringBuilder();
            using (var p = new ChoCSVReader(fullPath).WithFirstLineHeader())
            {
                using (var w = new ChoJSONWriter(sb))
                    w.Write(p);
            }
            return sb.ToString();
        }

        private string mock() =>
            @"Id, Name, City
                1, Tom, NY
                2, Mark, NJ
                3, Lou, FL
                4, Smith, PA
                5, Raj, DC
                ";

    }
}
