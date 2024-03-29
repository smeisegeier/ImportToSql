﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Rki.ImportToSql.ViewModels
{
    public class ListBoxItem
    {
        private DateTime _timeStamp = DateTime.Now;
        public string TimeStamp => new TimeSpan(_timeStamp.Hour, _timeStamp.Minute, _timeStamp.Second).ToString();

        public string Text { get; set; }
        public string FileName { get; set; }
        public Brush Foreground { get; set; }

        public ListBoxItem(string text, Brush foreground, string fileName)
        {
            Text = text;
            FileName = fileName;
            Foreground = foreground;
        }
    }
}
