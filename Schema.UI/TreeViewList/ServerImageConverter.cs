namespace Schema.UI.TreeViewList
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    using Shema.Server.Models;

    public class ServerImageConverter : IValueConverter
    {
        public object Convert(object o, Type type, object parameter, CultureInfo culture)
        {
            if (o is ServerModel)
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
