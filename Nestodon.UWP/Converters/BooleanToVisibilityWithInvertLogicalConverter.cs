using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Nestodon.UWP.Converters
{
    /// <summary>
    /// Returns Visibility.Visible if input boolean value is set to true, Visibility.Collapsed if false.
    /// </summary>
    /// <param name="parameter">Used to swap the logic (Visibility.Visible if false / Visibility.Collapsed if true).</param>
    public class BooleanToVisibilityWithInvertLogicalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var invert = false;
                if (parameter != null && parameter is bool)
                {
                    invert = (bool)parameter;
                }

                return (bool)value
                           ? (!invert ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed)
                           : (!invert ? Windows.UI.Xaml.Visibility.Collapsed : Windows.UI.Xaml.Visibility.Visible);
            }
            return Windows.UI.Xaml.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
