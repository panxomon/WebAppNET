using System;

namespace Web.Base.Extensores
{
    public static class ConvetirDateTime
    {
        public static double ToUnixTimeStamp(this DateTime dateTime)
        {
            var span = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();

            return span.TotalSeconds;
        }
    }
}