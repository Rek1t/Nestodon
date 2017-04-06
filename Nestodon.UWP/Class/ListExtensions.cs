using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestodon.UWP.Class
{
    public static class ListExtensions
    {
        public static List<object> ToListObject<T>(this List<T> list)
        {
            return list.Select(o => (object) o).ToList();
        }
    }
}
