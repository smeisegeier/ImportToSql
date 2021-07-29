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
using System.IO;

namespace Rki.ImportToSql.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        /* DropZone area*/
        public RelayCommand<DragEventArgs> DropCommand { get; private set; }


        /* DropDown area*/
        public IEnumerable<DropDownItem> DropDownItems => FileSchema.ListOfAllFileSchemas.Select(x => x.DropDownItem) ?? new List<DropDownItem>();
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

        public bool FilePathIsValid => File.Exists(DropFilePathFull)
            && (DropFilePathFull.EndsWith(".csv") || DropFilePathFull.EndsWith(".txt"));
        //{
        //    get
        //    {
        //        Regex pattern = new Regex(@"[a-zA-Z]:[\\\/](?:[a-zA-Z0-9_-]+[\\\/])*([a-zA-Z0-9_-]+\.)(csv|txt)");
        //        return pattern.IsMatch(DropFilePathFull);
        //    }
        //}
        public Brush FilePathColor => FilePathIsValid ? Globals.COLOR_SUCCESS : Globals.COLOR_DANGER;


        public bool ToggleIsEnabled
        {
            get => _toggleIsEnabled;
            private set
            {
                _toggleIsEnabled = value;
                //OnPropertyChanged();
                OnPropertyChanged(nameof(DropDownIsEnabled));
                OnPropertyChanged(nameof(UploadIsEnabled));
            }
        }
        private bool _toggleIsEnabled = false;

        public bool IsUploading
        {
            get => _isUploading;
            set
            {
                _isUploading = value;
                OnPropertyChanged();
            }
        }
        // HACK still buggy
        private bool _isUploading = true;


        //public Visibility IsUploading2
        //{
        //    get => _isUploading2;
        //    set
        //    {
        //        _isUploading2 = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private Visibility _isUploading2 = Visibility.Visible;

        //public Visibility IsUploadingVisibility => IsUploading? Visibility.Visible : Visibility.Hidden;

        /* Messages area */
        public ObservableCollection<ListBoxItem> ListBoxItems { get; private set; } = new();


        /* Exit area*/
        public ICommand ExitCommand { get; private set; }

        // TODO FontAwesome spinner 
        // https://stackoverflow.com/questions/6359848/wpf-loading-spinner

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

            addListBoxItem("App Started", Globals.COLOR_SUCCESS, "<System>");
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

        private bool checkJsonObjects(JSchema jsonSchema, JArray jsonArray)
        {
            if (jsonSchema == null)
            {
                StaticHelper.MyMessageBoxNotification("Schema Error", MessageBoxImage.Error);
                return false;
            }
            // then we have a json array
            if (!jsonArray.IsValid(jsonSchema, out IList<string> messages))
            {
                string text = string.Join(Environment.NewLine, messages);
                StaticHelper.MyMessageBoxNotification(text, MessageBoxImage.Error);
                addListBoxItem(text, Globals.COLOR_DANGER);
                return false;
            }
            return true;
        }

        /// <summary>
        /// You cannot use generic / dynamic lists here, finally in the repo methods the type must be known at compiletime.
        /// </summary>
        private void onUpload(string json)
        {
            // set spinner
            IsUploading = true;
            //IsUploading2 = Visibility.Visible;

            FileSchema selectedFileSchema = FileSchema.GetFileSchemaByDropDownItem(SelectedDropDownItem);
            JSchema jsonSchema = selectedFileSchema?.JsonSchema;
            // parse json -> array
            JArray jsonArray = JArray.Parse(json);

            #region Schema03
            // manual?
            if (selectedFileSchema?.TypeDomainSchema == typeof(Schema03Anmeldungen))
            {
                // then schema must be present
                if (!checkJsonObjects(jsonSchema, jsonArray))
                    return;
            }
            // auto
            if (json.ToJsonTryParse(out IList<Schema03Anmeldungen> list03))
            {
                processUpload(list03, FileSchema.GetFileSchemaByDomainType(typeof(Schema03Anmeldungen)));
                return;
            }
            #endregion

            #region Schema01
            // manual?
            if (selectedFileSchema?.TypeDomainSchema == typeof(Test1))
            {
                // then schema must be present
                if (!checkJsonObjects(jsonSchema, jsonArray))
                    return;
            }
            // auto
            if (json.ToJsonTryParse(out IList<Test1> list01))
            {
                processUpload(list01, FileSchema.GetFileSchemaByDomainType(typeof(Test1)));
                return;
            }
            #endregion

            #region Schema02
            // manual?
            if (selectedFileSchema?.TypeDomainSchema == typeof(Test2))
            {
                // then schema must be present
                if (!checkJsonObjects(jsonSchema, jsonArray))
                    return;
            }
            // auto
            if (json.ToJsonTryParse(out IList<Test2> list02))
            {
                processUpload(list02, FileSchema.GetFileSchemaByDomainType(typeof(Test2)));
                return;
            }
            #endregion

            StaticHelper.MyMessageBoxNotification("Unknown Type or structure violation.", MessageBoxImage.Error);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="fileSchema"></param>
        private void processUpload<T>(IList<T> list, FileSchema fileSchema) where T : BaseModel
        {
            IsUploading = false;
            //IsUploading2 = Visibility.Hidden;

            // no entries?
            if (!list.Any())
                return;

            var repo = fileSchema.Repository;
            string messageHeader = string.Format("File is of type: {0}\nItems found in target: {1}\nTargetPath: {2}\n\n",
                typeof(T).Name,
                repo.ItemsGetCount<T>(),
                repo.TargetPathInfo
                );

            /* Duplicates */
            if (repo.ItemsExist(list))
            {
                StaticHelper.MyMessageBoxNotification(messageHeader + "--> Duplicate!", MessageBoxImage.Error);
                addListBoxItem("Duplicate items", Globals.COLOR_DANGER);
                return;
            }

            if (!StaticHelper.MyMessageBoxNotificationYesNo(messageHeader + "--> Import these?"))
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

        private void addListBoxItem(string text, Brush foreground = null, string fileName = null)
        {
            ListBoxItems.Add(new ListBoxItem(text, foreground ?? Brushes.Black, fileName ?? Path.GetFileName(DropFilePathFull)));
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
    }
}
