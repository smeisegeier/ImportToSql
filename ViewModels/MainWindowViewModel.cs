using ChoETL;
using Newtonsoft.Json.Schema;
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
using System.Text.RegularExpressions;
using Rki.ImportToSql.Models.Domain;
using System.Collections;

namespace Rki.ImportToSql.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        /* DropZone area*/
        public RelayCommand<DragEventArgs> DropCommand { get; private set; }


        /* DropDown area*/
        public IEnumerable<DropDownItem> DropDownItems => FileSchema.ListOfAllFileSchemas.Select(x => x.DropDownItem)?? new List<DropDownItem>();
        public DropDownItem SelectedDropDownItem
        {
            get => _selectedDropDownItem;
            set
            {
                _selectedDropDownItem = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UploadIsEnabled));
            }
        }
        private DropDownItem _selectedDropDownItem;

        public RelayCommand<RoutedEventArgs> CheckedCommand { get; private set; }
        public RelayCommand<RoutedEventArgs> UncheckedCommand { get; private set; }
        public bool DropDownIsEnabled => !ToggleIsEnabled;

        /* Button area*/
        public ICommand UploadCommand { get; private set; }
        public bool UploadIsEnabled => FilePathIsValid && (ToggleIsEnabled || SelectedDropDownItem != null);
        public string DropFilePathFull
        {
            get => _dropFilePathFull;
            set
            {
                _dropFilePathFull = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UploadIsEnabled));
                OnPropertyChanged(nameof(FilePathIsValid));
                OnPropertyChanged(nameof(FilePathColor));
            }
        }
        private string _dropFilePathFull = "Drop file to get path here";

        public bool FilePathIsValid
        {
            get
            {
                Regex pattern = new Regex(@"[a-zA-Z]:[\\\/](?:[a-zA-Z0-9_-]+[\\\/])*([a-zA-Z0-9_-]+\.)(csv|txt)");
                return pattern.IsMatch(DropFilePathFull);
            }
        }
        //  => (DropFilePathFull.EndsWith(".csv") || DropFilePathFull.EndsWith(".txt"))
        public Brush FilePathColor => FilePathIsValid ? Globals.COLOR_SUCCESS : Globals.COLOR_DANGER;


        public bool ToggleIsEnabled
        {
            get => _toggleIsEnabled;
            private set
            {
                _toggleIsEnabled = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DropDownIsEnabled));
                OnPropertyChanged(nameof(UploadIsEnabled));
            }
        }
        private bool _toggleIsEnabled;


        /* Messages area */
        public ObservableCollection<ListBoxItem> ListBoxItems { get; private set; } = new();


        /* Exit area*/
        public ICommand ExitCommand { get; private set; }



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

            CheckedCommand = new RelayCommand<RoutedEventArgs>(o =>
            {
                ToggleIsEnabled = true;
                // also remove item from dropdown
                SelectedDropDownItem = null;
            });
            UncheckedCommand = new RelayCommand<RoutedEventArgs>(o => { ToggleIsEnabled = false; });

            addListBoxItem("App Started", Globals.COLOR_SUCCESS);
        }

        // https://stackoverflow.com/questions/6205472/mvvm-passing-eventargs-as-command-parameter
        private void grid_Drop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    DropFilePathFull = path;
                }
            }
        }

        private void onUpload(string json)
        {
            if (ToggleIsEnabled)
                onUploadAuto(json);
            else
                onUploadManual(json);
        }

        private void onUploadManual(string json)
        {
            // parse json -> array
            JArray jsonArray = JArray.Parse(json);

            // get the FileSchema item off of the user selected dropdown item
            FileSchema fileSchema = FileSchema.ListOfAllFileSchemas.FirstOrDefault(x => x.DropDownItem == SelectedDropDownItem);
            Type typeDtoSchema = fileSchema.TypeDtoSchema;
            JSchema jsonSchema = fileSchema.JsonSchema;
            BaseRepo repository = fileSchema.Repository;

            // create a generic List where the type is unknown at compile time
            Type typeGenericList = typeof(List<>).MakeGenericType(new[] { typeDtoSchema });
            IList genericList = (IList)Activator.CreateInstance(typeGenericList);

            if (jsonSchema == null)
            {
                StaticHelper.MyMessageBoxNotificationInfo("Schema Error");
                return;
            }

            if (!jsonArray.IsValid(jsonSchema, out IList<string> messages))
            {
                StaticHelper.MyMessageBoxNotificationInfo(string.Join(Environment.NewLine, messages));
            }
            
            if (json.ToJsonTryParse(out genericList))
            {
                // HACK
                //processUpload(genericList, repository);
                return;
            }

        }

        private void onUploadAuto(string json)
        {

            if (json.ToJsonTryParse(out IList<Schema03Anmeldungen> list3))
            {
                processUpload(list3, Schema03Anmeldungen.Repo);
                return;
            }


            if (json.ToJsonTryParse(out IList<Test1> list1))
            {
                processUpload(list1, Test1.Repo);
                return;
            }

            if (json.ToJsonTryParse(out IList<Test2> list2))
            {
                processUpload(list2, Test2.Repo);
                return;
            }

            StaticHelper.MyMessageBoxNotificationInfo("Unknown Type or structure violation.");
        }

        private void processUpload<T>(IList<T> list, BaseRepo repo) where T : BaseModel
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
