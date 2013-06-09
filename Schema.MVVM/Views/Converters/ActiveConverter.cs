using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Schema.MVVM.Views.Converters
{
   public class ActiveConverter : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
       {
           var state = (bool)value;
           return state ? new SolidColorBrush(Colors.WhiteSmoke) : new SolidColorBrush(Colors.SkyBlue);
       }

       public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
       {
           throw new NotImplementedException();
       }
    }
}
