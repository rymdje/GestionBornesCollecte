using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GestionBornesCollecte.Converters
{
    public class NiveauToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int niveau)
            {
                if (niveau >= 85) return new SolidColorBrush(Colors.Red);
                if (niveau >= 50) return new SolidColorBrush(Colors.Orange);
                return new SolidColorBrush(Colors.Green);
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}