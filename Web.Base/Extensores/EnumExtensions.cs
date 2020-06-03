using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Base.Extensores
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            if (value == null)
            {
                return string.Empty; 
            }

            var field = value.GetType().GetField(value.ToString());

            if (field == null)
            {
                return string.Empty;
            }

     
            var attribs = field.GetCustomAttributes(typeof(DisplayAttribute), true);
            if (attribs.Length > 0)
            {
                return ((DisplayAttribute)attribs[0]).GetName();
            }
     
            attribs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);

            if (attribs.Length > 0)
            {
                return ((DescriptionAttribute)attribs[0]).Description;
            }

     
            return value.ToString().ToSeparatedWords();
        }
    }
}