using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Microsoft.VisualBasic;

namespace Nestodon.UWP.Class
{
    public class ResultSearch
    {
        public string ModuleName { get; set; }
        public List<object> Results { get; set; }
        public DataTemplate DataTemplateForItems { get; set; }
        public Action<object> OpenAction { get; set; } 
    }
}
