using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Controls.ListViewExtended
{
    /// <summary>
    /// Cet attribut permet d'indiquer quelles propriétés d'une classe (POCO par exemple) pourront être utilisée par la recherche de <see cref="ListViewExtended"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ListViewExtendedSearchAttribute : Attribute
    {
        /// <summary>
        /// Permet de retrouver la propriété indiquant si l'objet peut servir dans la recherche
        /// </summary>
        /// <returns></returns>
        public static List<string> GetPropertyInfosForSearch(object element)
        {
            IEnumerable<PropertyInfo> typeInfoProperties = element.GetType().GetProperties();
            var properties = from prop in typeInfoProperties
                             let attrs = prop.GetCustomAttributes(false)
                             where attrs.Any(obj => obj.GetType() == typeof(ListViewExtendedSearchAttribute))
                             select prop;
            var propertyInfos = properties as PropertyInfo[] ?? properties.ToArray();
            return propertyInfos.Any() ? propertyInfos.Select(p => p.Name).ToList() : null;
        }
    }
}
