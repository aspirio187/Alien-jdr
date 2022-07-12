using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Alien.UI.Helpers
{
    public class Base64ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url = value as string;
            if (url is null) return false;

            BitmapImage bitmapImage = new();
            try
            {
                bitmapImage.BeginInit();
                bitmapImage.UriSource = url.Equals("url") || string.IsNullOrEmpty(url) ? new Uri("https://i.postimg.cc/xjGdXzC7/VIDE.png") : new Uri(url);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
