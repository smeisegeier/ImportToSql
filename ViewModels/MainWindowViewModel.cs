using Rki.ImportToSql.Helper;
using Rki.ImportToSql.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Rki.ImportToSql.Models.Domain;
using System.IO;


namespace Rki.ImportToSql.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static NLog.Logger _logger= NLog.LogManager.GetCurrentClassLogger();

        public ICommand CommandOpenDoc { get; set; }

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
                OnPropertyChanged(nameof(DropDownIsEnabled));
                OnPropertyChanged(nameof(UploadIsEnabled));
            }
        }
        private bool _toggleIsEnabled = false;

        /// <summary>
        /// FontAwsome spinner.
        /// TODO spinner animation
        /// </summary>
        public bool IsUploading
        {
            get => _isUploading;
            set
            {
                _isUploading = value;
                OnPropertyChanged(nameof(IsUploadingVisibility));
            }
        }
        private bool _isUploading = false;

        public Visibility IsUploadingVisibility => IsUploading? Visibility.Visible : Visibility.Hidden;

        /* Messages area */
        public ObservableCollection<ListBoxItem> ListBoxItems { get; private set; } = new();


        /* Exit area*/
        public ICommand ExitCommand { get; private set; }

        public MainWindowViewModel()
        {
            setupCommands();

            addListBoxItem("App started", Globals.COLOR_SUCCESS, "<System>");
        }

        private void setupCommands()
        {
            ExitCommand = new RelayCommand<object>(o =>
            {
                App.Current.Shutdown();
            });


            UploadCommand = new RelayCommand<object>(
                 o => onUpload()
                );
            DropCommand = new RelayCommand<DragEventArgs>(grid_Drop);

            CheckedCommand = new RelayCommand<RoutedEventArgs>(o =>
            {
                ToggleIsEnabled = true;
                // also remove item from dropdown
                SelectedDropDownItem = null;
            });

            UncheckedCommand = new RelayCommand<RoutedEventArgs>(o => { ToggleIsEnabled = false; });
            CommandOpenDoc = new RelayCommand<object>(o =>{StaticHelper.OpenUrl(Globals.URL_USERDOC);});
        }

        public string VersionInfo => "Network: " + Globals.ApplicationNetworkMode.ToString();


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


        /// <summary>
        /// You cannot use generic / dynamic lists here, finally in the repo methods the type must be known at compiletime.
        /// </summary>
        private void onUpload()
        {
            /* get class type from selection */
            FileSchema selectedFileSchema = FileSchema.GetFileSchemaByDropDownItem(SelectedDropDownItem);
            /* fetch header, then class */
            var csvHeader = StaticHelper.GetHeaderFromCsv(DropFilePathFull);

            #region Imira
            // manual || (auto detected + no schema selected)
            if (selectedFileSchema?.TypeDomainSchema == typeof(Imira)
                || (StaticHelper.GetClassProperties<Imira>().HasSameElements(csvHeader)
                    && selectedFileSchema?.TypeDomainSchema is null)
                )
            {
                // user => ok?
                if (StaticHelper.GetClassFromCsv<Imira, ImiraMap>(DropFilePathFull, out List<Imira> listImira))
                    processUpload(listImira, FileSchema.GetFileSchemaByDomainType(typeof(Imira)));
                return;
            }
            #endregion


            #region Schema01
            // manual || (auto detected + no schema selected)
            if (selectedFileSchema?.TypeDomainSchema == typeof(Test1)
                || (StaticHelper.GetClassProperties<Test1>().HasSameElements(csvHeader)
                    && selectedFileSchema?.TypeDomainSchema is null)
                )
            {
                // user => ok?
                if (StaticHelper.GetClassFromCsv<Test1, Test1Map>(DropFilePathFull, out List<Test1> list1))
                    processUpload(list1, FileSchema.GetFileSchemaByDomainType(typeof(Test1)));
                return;
            }
            #endregion

            #region Schema02
            // manual || (auto detected + no schema selected)
            if (selectedFileSchema?.TypeDomainSchema == typeof(Test2)
                || (StaticHelper.GetClassProperties<Test2>().HasSameElements(csvHeader) 
                    && selectedFileSchema?.TypeDomainSchema is null)
                )
            {
                // user => ok?
                if (StaticHelper.GetClassFromCsv<Test2, Test2Map>(DropFilePathFull, out List<Test2> list2))
                    processUpload(list2, FileSchema.GetFileSchemaByDomainType(typeof(Test2)));
                return;
            }
            #endregion

            #region COALA_Prozessdaten
            // manual || (auto detected + no schema selected)
            if (selectedFileSchema?.TypeDomainSchema == typeof(GsProzessdaten)
                || (StaticHelper.GetClassProperties<GsProzessdaten>().HasSameElements(csvHeader)
                    && selectedFileSchema?.TypeDomainSchema is null)
                )
            {
                // user => ok?
                if (StaticHelper.GetClassFromCsv<GsProzessdaten, GsProzessdatenMap>(DropFilePathFull, out List<GsProzessdaten> list2))
                {
                    processUpload(list2, FileSchema.GetFileSchemaByDomainType(typeof(GsProzessdaten)));
                }
                return;
            }
            #endregion


            StaticHelper.MyMessageBoxNotification("Unknown Type or structure violation.", MessageBoxImage.Error);
        }

        private void processUpload<T>(IList<T> list, FileSchema fileSchema) where T : BaseModel
        {
            // if ListType is known but fileSchema null, then the schema is not included in current network
            if (fileSchema is null)
            {
                StaticHelper.MyMessageBoxNotification("Target is not in current network", MessageBoxImage.Warning);
                return;
            }

            // no entries?
            if (!list.Any())
                return;

            var repo = fileSchema.Repository;
            #region TargetNotExists
            //// target nonexists, and not inmemory?
            //if (fileSchema.ApplicationNetworkMode != ApplicationNetworkModeType.INMEMORY
            //    && !DextersLabor.EfCoreHelper.CheckIfTableExists(repo, repo.TargetSchemaName, repo.TargetTableName))
            //{
            //    if (StaticHelper.MyMessageBoxNotificationYesNo(string.Format("Target {0} does not exist. Create?", repo.TargetPathInfo)))
            //        return;
            //}
            #endregion

            /* prepare vars */
            string messageHeader = string.Format("File is of type: {0}\nItems found in target: {1}\nTargetPath: {2}\n\n",
                typeof(T).Name,
                repo.ItemsGetCount<T>(),
                repo.TargetPathInfo
                );

            // duplicates?
            if (repo.ItemsExist(list))
            {
                addListBoxItem("Duplicate items", Globals.COLOR_DANGER);

                if (StaticHelper.MyMessageBoxNotificationYesNo(messageHeader + "Duplicates! Overwrite Target?"))
                {
                    // logging <- message <- action
                    _logger.Info(addListBoxItem(string.Format(
                        "{0} items deleted",repo.ItemDeleteDuplicates(list)),
                        Globals.COLOR_CHANGE)
                        );
                }
                else
                {
                    // quit on duplicates
                    return;
                }
            }
            else
            {
                // cancel?
                if (!StaticHelper.MyMessageBoxNotificationYesNo(messageHeader + "--> Import these?"))
                {
                    return;
                }
            }


            /* Add List to repo */
            int count = repo.ItemAddList(list);

            /* documentation */
            if (count == 0)
                addListBoxItem("No items were added", Globals.COLOR_DANGER);
            else
            {
                string text = string.Format("+{0} items of <{1}> to {2}", count, typeof(T).Name, repo.TargetPathInfo);
                _logger.Info(addListBoxItem(text, Globals.COLOR_CHANGE));
            }
        }


        /// <summary>
        /// Adds item to ListBox in message area
        /// </summary>
        /// <param name="text">message text</param>
        /// <param name="foreground">predefined colors</param>
        /// <param name="fileName">short filename</param>
        /// <returns>text to piped to logger</returns>
        private string addListBoxItem(string text, Brush foreground = null, string fileName = null)
        {
            fileName = fileName ?? Path.GetFileName(DropFilePathFull);
            ListBoxItems.Add(new ListBoxItem(text, foreground ?? Brushes.Black, fileName));
            return string.Format("{0} -> {1}", fileName, text);
        }
    }
}
