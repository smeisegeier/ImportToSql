using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Rki.ImportToSql.ViewModels
{
    public class ListBoxItem
    {
        public string Text { get; set; }
        public Brush Foreground { get; set; }

        public ListBoxItem(string text, Brush foreground)
        {
            Text = text;
            Foreground = foreground;
        }
    }
}
