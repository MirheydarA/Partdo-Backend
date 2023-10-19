using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Utilities
{
    public static class EnumHelper
    {
        public static string GetEnumDisplayName(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            var output = attributes.Length > 0 && !string.IsNullOrEmpty(attributes[0].Name) ? attributes[0].Name : value.ToString();
            return output;
        }
    }
}
