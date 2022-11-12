using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Rki.ImportToSql.Helper
{
	/// <summary>
	/// Converter is not used here, instead there are 2 coupled properties in the vm (bool -> Visibility)
	/// </summary>
    public class BoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			switch (value)
			{
				case true:
					return Visibility.Visible;
				case false:
					return Visibility.Hidden;
				default:
					return Visibility.Hidden;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is Visibility)
			{
				if ((Visibility)value == Visibility.Visible)
					return true;
				else
					return false;
			}
			return null;
		}
	}
}
