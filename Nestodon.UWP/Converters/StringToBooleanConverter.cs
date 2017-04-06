using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Nestodon.UWP.Converters
{
    public class StringToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!string.IsNullOrEmpty(value?.ToString()))
            {
                return value.ToString() != "0";
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? "1" : "0";
        }
    }
}
