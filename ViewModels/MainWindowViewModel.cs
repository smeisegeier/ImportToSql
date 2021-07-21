﻿using ChoETL;
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
using System.Windows.Input;

namespace Rki.ImportToSql.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly MainWindow _mainWindow;

        public ICommand ExitCommand { get; private set; }
        public ICommand UploadCommand { get; private set; }

        /// <summary>
        /// As long as the RelayCommand is not fully implemented, the window object must be passed
        /// </summary>
        /// <param name="mainWindow">parent window</param>
        public MainWindowViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            ExitCommand = new RelayCommand(o =>
            {
                App.Current.Shutdown();
            });

            UploadCommand = new RelayCommand(
                o => onUpload(csvToJsonFromFullPath(_mainWindow.DropFilePathFull)),
                o => (_mainWindow.DropFilePathFull != null)
            );
        }

        public MainWindowViewModel() { }


        private void onUpload(string json)
        {
            var schema = Test.Schema;
            var schemaList = TestList.Schema;

            var errors = Test.Schema.Validate(json);
            var errors2 = Base.Schema.Validate(json);

            // -> true
            if (json.TryParseJson(out List<Test> result))
            {
                // Do something with result…
            }

            // -> false
            if (json.TryParseJson(out List<Base> resulty))
            {
                // Do something with result…
            }

            List<Test> list = JsonConvert.DeserializeObject<List<Test>>(json);
            List<Base> xd = JsonConvert.DeserializeObject<List<Base>>(json);

            StaticHelper.MyMessageBoxNotificationInfo(string.Join("|", list.Select(x => x.ToString())));

        }

        // https://stackoverflow.com/questions/29337930/can-i-determine-whether-the-string-can-deserialize-by-newtonsoft
        private static bool tryParseJson(string json, out JObject jObject)
        {
            try
            {
                jObject = JObject.Parse(json);
                return true;
            }
            catch
            {
                jObject = null;
                return false;
            }
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