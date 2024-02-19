
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Instashop.Converters;

public class UrlToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string url && Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
        {
            try
            {
                using (var client = new WebClient())
                {
                    byte[] data = client.DownloadData(uri);

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = new System.IO.MemoryStream(data);
                    bitmap.EndInit();
                    bitmap.Freeze(); // Freeze the BitmapImage to avoid memory leaks

                    return bitmap;
                }
            }
            catch (Exception)
            {
                // Handle any exceptions (e.g., invalid URL, network error)
                return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/no-image-icon.png"));
            }
        }

        return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/no-image-icon.png"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException(); // ConvertBack is not supported
    }
}
