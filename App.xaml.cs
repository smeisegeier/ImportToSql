using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;
using Rki.ImportToSql.ViewModels;
using Rki.ImportToSql.Views;
using System;
using System.Windows;

namespace Rki.ImportToSql
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ApplicationStartup(object sender, StartupEventArgs e)
        {
            /* setup repo*/
            //Globals.RepoTest1 = new ();
            //Globals.RepoTest2 = new ();
            try
            {
                // correct approach: always try to ensure at startup. Connectivity should be checked later again for button - it could be on/off
                Test1.Repo.Database.EnsureCreated();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo(string.Format("Server not present.{0}{1}", Environment.NewLine, ex.Message));
            }

            //Test1.Repo.ItemAddList(Test1.GetDefaultValues());

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = new MainWindowViewModel();
            mainWindow.Show();
        }
    }
}
