using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rki.ImportToSql.Helper
{
    public static class StaticHelper
    {
        // TODO -> package?
        public static bool MyMessageBoxNotificationYesNo(string _message)
        {
            MessageBoxResult _res = MessageBox.Show(_message, "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            return (_res == MessageBoxResult.Yes);
        }
        public static void MyMessageBoxNotification(string _message, MessageBoxImage messageBoxImage) => 
            MessageBox.Show(_message, "Message", MessageBoxButton.OK, messageBoxImage, MessageBoxResult.OK);

        public static void MyMessageBoxNotificationInfo(string _message) => MyMessageBoxNotification(_message, MessageBoxImage.Information);

        // TODO enable!
        public static void OpenUrl(string url)
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

    }
}
