using ChoETL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
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
            bool match;
            IList<string> messages;
            JArray jlist = JArray.Parse(json);
            match = jlist.IsValid(Schema03Anmeldungen.Schema, out messages);


            if (json.ToJsonTryParse(out List<Test1> list1))
            {
                processUpload(list1, Test1.Repo);
                return;
            }

            if (json.ToJsonTryParse(out List<Test2> list2))
            {
                processUpload(list2, Test2.Repo);
                return;
            }

            if (json.ToJsonTryParse(out List<Schema03Anmeldungen> list3))
            {
                processUpload(list3, Schema03Anmeldungen.Repo);
                return;
            }

            StaticHelper.MyMessageBoxNotificationInfo("Unknown Type or structure violation.");

        }

        private void processUpload<T>(List<T> list, BaseRepo repo) where T : BaseModel
        {
            // no entries?
            if (!list.Any())
                return;

            /* Feedback to user */
            if (repo.ItemsExist(list))
            {
                StaticHelper.MyMessageBoxNotificationInfo("Duplicate!");
                //addListBoxItem("Duplicate", Globals.COLOR_DANGER);
                return;
            }

            if (!StaticHelper.MyMessageBoxNotificationYesNo(string.Format("File is of type: {0}\nItems found in target: {1}\nTargetPath: {2}\n\nImport these? ",
                typeof(T).Name,
                repo.ItemsGetCount<T>(),
                repo.TargetPathInfo
                )))
            {
                return;
            }


            /* Add List to repo */
            int count = repo.ItemAddList(list);
            if (count == 0)
                addListBoxItem("No items were added", Globals.COLOR_DANGER);
            else
                addListBoxItem(string.Format("+{0} items of <{1}> to {2}", count, typeof(T).Name, repo.TargetPathInfo),
                    Globals.COLOR_CHANGE);


            //StaticHelper.MyMessageBoxNotificationInfo(BaseModel.PrintList(repo.ItemsGetAll<T>()));
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
