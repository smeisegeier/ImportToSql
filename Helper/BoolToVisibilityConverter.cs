using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Rki.ImportToSql.Helper
{
    public class BoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			switch (value)
			{
				case true:
					return Visibility.Hidden;
				case false:
					return Visibility.Visible;
				default:
					return Visibility.Hidden;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is Visibility)
			{
				if ((Visibility)value == Visibility.Visible)
					return false;
				else
					return true;
			}
			return null;
		}
	}
}
