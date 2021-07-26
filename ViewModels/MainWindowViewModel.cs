using ChoETL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rki.ImportToSql.Helper;
using Rki.ImportToSql.Models;
using Rki.ImportToSql.Services;
using Rki.ImportToSql.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Rki.ImportToSql.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand ExitCommand { get; private set; }
        public ICommand UploadCommand { get; private set; }
        public RelayCommand<DragEventArgs> DropCommand { get; private set; }

        public bool UploadEnabled => DropFilePathFull?.Length > 5;
        public ObservableCollection<ListBoxItem> ListBoxItems { get; private set; } = new();



        public string DropFilePathFull
        {
            get => _dropFilePathFull;
            set
            {
                _dropFilePathFull = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UploadEnabled));
            }
        }
        private string _dropFilePathFull = "xde";

        public MainWindowViewModel()
        {
            ExitCommand = new RelayCommand<object>(o =>
            {
                App.Current.Shutdown();
            });


            UploadCommand = new RelayCommand<object>(
                 o => onUpload(csvToJsonFromFullPath(DropFilePathFull))
                );

            DropCommand = new RelayCommand<DragEventArgs>(grid_Drop);

            addListBoxItem("App Started", Globals.COLOR_SUCCESS);
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
                processUpload(list1, Test1.Repo);

            if (json.ToJsonTryParse(out List<Test2> list2))
                processUpload(list2, Test2.Repo);

        }

        private void processUpload<T>(List<T> list, BaseDbContext repo) where T : BaseModel
        {
            // no entries?
            if (!list.Any())
                return;

            /* Feedback to user */
            if (repo.ItemsExist(list))
            {
                addListBoxItem("Duplicate", Globals.COLOR_DANGER);
                return;
            }

            if (!StaticHelper.MyMessageBoxNotificationYesNo(string.Format("Type found: {0} \nItems found: {1} ",
                typeof(T).Name,
                repo.ItemsGetCount<T>())))
                    return;

            /* Add List to repo */
            int count = repo.ItemAddList(list);
            if (count == 0)
                addListBoxItem("No items were added", Globals.COLOR_DANGER);
            else
                addListBoxItem(string.Format("{0} items added to <{1}>", count, typeof(T).Name), Globals.COLOR_CHANGE);


            StaticHelper.MyMessageBoxNotificationInfo(BaseModel.PrintList(repo.ItemsGetAll<T>()));
        }

        private void addListBoxItem(string text, Brush foreground = null)
        {
            ListBoxItems.Add(new ListBoxItem(text, foreground ?? Brushes.Black));
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
