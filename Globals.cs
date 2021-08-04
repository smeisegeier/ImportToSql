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
        public static readonly string URL_USERDOC = "https://www.rki.de";

        public static readonly ApplicationNetworkModeType ApplicationNetworkMode = ApplicationNetworkModeType._ALL;
    }

    [Flags]
    public enum ApplicationNetworkModeType
    {
        VLAN = 1,
        LAN = 2,
        LOCAL = 4,
        _ALL = 7,
        _RKI = 3
    }

    // TODO sql logging
    // TODO multistage messages (import -> write)
    // TODO azure doc
    // TODO [InterfaceDb].[COALA].[GS_Prozessdaten]
}
