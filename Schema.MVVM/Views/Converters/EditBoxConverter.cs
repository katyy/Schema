using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Schema.MVVM.Views.Converters
{
    public class EditBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = bool.Parse(parameter as string);
            var val = (bool)value;

            return val == param ?
               Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
