using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Instashop.Converters;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            if (value is bool)
            {
                return (bool)value == false ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
        else
        {
            return Visibility.Hidden;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visible)
        {
            if (visible == Visibility.Visible)
            {
                return true;
            }
        }
        return false;
    }
}
