using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rki.ImportToSql.Helper
{
    public static class StaticHelper
    {
        // TODO source out to package?
        public static void MyMessageBoxNotificationInfo(string _message) => 
            MessageBox.Show(_message, "Info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        public static bool MyMessageBoxNotificationYesNo(string _message)
        {
            MessageBoxResult _res = MessageBox.Show(_message, "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            return (_res == MessageBoxResult.Yes);
        }
    }
}
