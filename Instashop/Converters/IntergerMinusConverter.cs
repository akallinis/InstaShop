using System.Globalization;
using System.Windows.Data;

namespace Instashop.Converters;

public class IntegerMinusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double width && double.TryParse(parameter.ToString(), out double subtractValue))
        {
            return width - subtractValue;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double width && double.TryParse(parameter.ToString(), out double addValue))
        {
            return width + addValue;
        }
        return value;
    }
}
