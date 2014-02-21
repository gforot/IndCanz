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
            bool titleAnswerDone = false;
            ResponseBase response = null;
            if (parameter is bool)
            {
                titleAnswerDone = (bool)parameter;
            }
            else
            {
                return new SolidColorBrush(Colors.Purple);
            }
            if (value is ResponseBase)
            {
                response = value as ResponseBase;
            }
            else
            {
                return new SolidColorBrush(Colors.Magenta);
            }

            if (!titleAnswerDone)
            {
                return new SolidColorBrush(_undefinedResponseColor);
            }

            if (response.IsCorrect)
            {
                return new SolidColorBrush(_rightResponseColor);
            }
            else if (response.IsSelected)
            {
                return new SolidColorBrush(_wrongResponseColor);
            }
            else
            {
                return new SolidColorBrush(_undefinedResponseColor);
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
