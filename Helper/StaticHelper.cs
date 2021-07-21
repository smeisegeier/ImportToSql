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
    }
}
