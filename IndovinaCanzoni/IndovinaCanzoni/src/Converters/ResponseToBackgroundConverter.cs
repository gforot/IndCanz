using System.Windows.Data;
using System.Windows.Media;
using IndovinaCanzoni.Model;

namespace IndovinaCanzoni.Converters
{
    public class ResponseToBackgroundConverter : IValueConverter
    {
        private static Color _wrongResponseColor = Colors.Red;
        private static Color _rightResponseColor = Colors.Green;
        private static Color _undefinedResponseColor = Colors.Cyan;

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ResponseState)
            {
                switch ((ResponseState)value)
                {
                    case ResponseState.Correct:
                        return new SolidColorBrush(_rightResponseColor);
                    case ResponseState.Wrong:
                        return new SolidColorBrush(_wrongResponseColor);
                }
            }
            return new SolidColorBrush(_undefinedResponseColor);
   
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
