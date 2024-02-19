using System.Globalization;
using System.Windows.Automation;
using System.Windows.Data;

namespace Instashop.Converters;

public class IntegerMinusConverter : IValueConverter
{
    private const int AMOUNT = 40;
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return width - AMOUNT;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return width + AMOUNT;
        }
        return value;
    }
}
