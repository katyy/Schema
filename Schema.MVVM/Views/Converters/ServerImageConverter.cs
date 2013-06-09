using System;
using System.Globalization;
using System.Windows.Data;
using Shema.Server.Models;

namespace Schema.MVVM.Views.Converters
{
    public class ServerImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ServerModel)
            {
                return "/Images/server_database.png";
            }

            return "/Images/database.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
