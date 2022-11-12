using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rki.ImportToSql.Helper
{
    // TODO -> package?

    public static class StaticHelper
    {
        public static bool MyMessageBoxNotificationYesNo(string _message)
        {
            MessageBoxResult _res = MessageBox.Show(_message, "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            return (_res == MessageBoxResult.Yes);
        }
        public static void MyMessageBoxNotification(string _message, MessageBoxImage messageBoxImage) =>
            MessageBox.Show(_message, "Message", MessageBoxButton.OK, messageBoxImage, MessageBoxResult.OK);

        public static void MyMessageBoxNotificationInfo(string _message) => MyMessageBoxNotification(_message, MessageBoxImage.Information);

        /// <summary>
        /// Opens the given URL in standard browser
        /// </summary>
        /// <param name="url">url to be opened</param>
        public static void OpenUrl(string url)
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        /// <summary>
        /// Reads csv from given path and maps into class.
        /// </summary>
        /// <remarks>
        /// dependencies: nuget CsvHelper
        /// columns are CASE SENSITIVE!
        /// </remarks>
        /// <typeparam name="T">Type of target class, must have parameterless ctor</typeparam>
        /// <typeparam name="TMap">Mapping convention, must be drived from ClassMap</typeparam>
        /// <param name="csvFullPath">Full path to csv file</param>
        /// <param name="records">List of given type</param>
        /// <returns></returns>
        public static bool GetClassFromCsv<T, TMap>(string csvFullPath, out List<T> records)
            where TMap : ClassMap
            where T : new()
        {
            using (var reader = new StreamReader(csvFullPath))
            using (var csv = new CsvReader(reader, csvConfigurationStandard))
            {
                try
                {
                    csv.Context.RegisterClassMap<TMap>();
                    records = csv.GetRecords<T>().ToList();
                }
                catch (Exception ex)
                {
                    if (ex is FieldValidationException)
                    {
                        StaticHelper.MyMessageBoxNotification(ex.Message+"\nField: "+(ex as FieldValidationException).Field, MessageBoxImage.Error);
                    }
                    else
                    {
                        StaticHelper.MyMessageBoxNotification(ex.Message, MessageBoxImage.Error);
                    }
                    // (SQL) TODO logging here
                    records = null; // must be set in using statement
                    return false;
                }
                return true;
            }
        }

        public static List<string> GetHeaderFromCsv(string csvFullPath)
        {
            using (var reader = new StreamReader(csvFullPath))
            using (var csv = new CsvReader(reader, csvConfigurationStandard))
            {
                csv.Read();
                csv.ReadHeader();
                return csv.HeaderRecord.ToList();
            }
        }

        private static CsvConfiguration csvConfigurationStandard => new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8};


        public static List<string> GetClassProperties<T>() => GetClassProperties(typeof(T));


        /// <summary>
        /// Get list of class properties
        /// </summary>
        /// <param name="type">Type of class</param>
        /// <param name="includeDerived">true? include derived properties</param>
        /// <param name="includeOverridden">true? include overridden properties</param>
        /// <returns></returns>
        public static List<string> GetClassProperties(Type type, bool includeDerived = false, bool includeOverridden = false)
        {
            List<string> result = new();
            PropertyInfo[] properties = includeDerived? 
                type.GetProperties() : 
                type.GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);
            properties?.ToList().ForEach(property => 
            {
                var methodInfo = property.GetGetMethod(false);
                // includeOverridden true => methodInfo can be different from base class
                if (methodInfo == methodInfo.GetBaseDefinition() || includeOverridden)
                    result.Add(property.Name); 
            });
            return result;
        }
    }
}
