using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace IndovinaCanzoni.Converters
{
    public class ResultToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? responseResult = (bool?)value;
            if (responseResult.HasValue)
            {
                return responseResult.Value ? new BitmapImage(new Uri("/images/check.png", UriKind.Relative)) :
                    new BitmapImage(new Uri("/images/delete.png", UriKind.Relative));
            }

            return new BitmapImage(new Uri("/images/questionmark.png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
