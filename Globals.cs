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

        // DOC placing static Repos into Classes
        //       public static RepoTest1 RepoTest1 { get; set; } = new();
        //public static RepoTest2 RepoTest2 { get; set; } = new();
    }
}
