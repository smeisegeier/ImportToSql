using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;
using Rki.ImportToSql.ViewModels;
using Rki.ImportToSql.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Rki.ImportToSql
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void ApplicationStartup(object sender, StartupEventArgs e)
        {
            _logger.Info("App started");

            /* setup repo (dont do)*/
            //ensureAllRepositories();

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = new MainWindowViewModel();
            mainWindow.Show();
        }

        private static void ensureAllRepositories()
        {
            FileSchema.ListOfAvailableFileSchemas.ToList().ForEach(x =>
            {
                try
                {
                    // correct approach: always try to ensure at startup. Connectivity should be checked later again for button - it could be on/off
                    x.Repository.Database.EnsureCreated();
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    var msg = string.Format("Server not present.{0}{1}", Environment.NewLine, ex.Message);
                    Helper.StaticHelper.MyMessageBoxNotificationInfo(msg);
                    _logger.Error(msg);
                    return;
                }
            });
        }
    }
}
