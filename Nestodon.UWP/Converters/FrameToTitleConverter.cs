using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Nestodon.UWP.ViewModels.Base;

namespace Nestodon.UWP.Converters
{
    public class FrameToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var page = value as Page;
            if (page != null)
            {
                var content = page;
                var dataContext = content?.DataContext;
                var context = dataContext as ViewModelModule;
                if (context != null)
                {
                    return context.TitleContent;
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
