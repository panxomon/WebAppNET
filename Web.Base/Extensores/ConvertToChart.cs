using System.Linq;

namespace Web.Base.Extensores
{
    public static class ConvertToChart
    {
        public static char ToChart(this string param)
        {
            return param.ToCharArray(0, 1).FirstOrDefault();
        }
    }
}
