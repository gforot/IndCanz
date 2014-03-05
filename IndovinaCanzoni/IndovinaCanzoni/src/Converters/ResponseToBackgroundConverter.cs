using System.Windows.Data;
using System.Windows.Media;
using IndovinaCanzoni.Model;

namespace IndovinaCanzoni.Converters
{
    public class ResponseToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ResponseState)
            {
                switch ((ResponseState)value)
                {
                    case ResponseState.Correct:
                        return App.Current.Resources["okResponseBackground"] as Brush;
                    case ResponseState.Wrong:
                        return App.Current.Resources["koResponseBackground"] as Brush;
                }
            }
            return App.Current.Resources["undefinedResponseBackground"] as Brush;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
