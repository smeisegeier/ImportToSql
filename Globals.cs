using Rki.ImportToSql.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Rki.ImportToSql
{
    /// <summary>
    /// Application wide constants
    /// </summary>
    public static class Globals
    {
        public static readonly Brush COLOR_SUCCESS = Brushes.DarkGreen;
        public static readonly Brush COLOR_CHANGE = Brushes.Purple;
        public static readonly Brush COLOR_DANGER = Brushes.Red;
        public static readonly string URL_USERDOC =
            "https://dev.azure.com/dexterRki/RKI/_wiki/wikis/RKI.wiki/28/ImportToSql-manual";
        public static readonly string DEFAULT_TABLE_NAME = "ImportFromSql";

        public static readonly ApplicationNetworkModeType ApplicationNetworkMode = ApplicationNetworkModeType._RKISERVERONLY;

        public readonly static string? TEST_CON = System.Configuration.ConfigurationManager.AppSettings.Get("test_con");
    }

    [Flags]
    public enum ApplicationNetworkModeType
    {
        VLAN = 1,
        LAN = 2,
        LOCAL = 4,
        INMEMORY = 8,
        _ALL = 15,
        _RKISERVERONLY = 3,
        _HOMEOFFICE = 12
    }
}
