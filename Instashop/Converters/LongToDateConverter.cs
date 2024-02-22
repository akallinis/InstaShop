
using System.Globalization;
using System.Windows.Data;

namespace Instashop.Converters;

public class LongToDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        if (value is long)
        {
            return epoch.AddMilliseconds((long)value);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();//Not needed
    }
}
