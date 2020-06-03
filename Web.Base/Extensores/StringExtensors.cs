namespace Web.Base.Extensores
{
    public static class StringExtensors
    {

        //public static string GenerateSlug(this string phrase)
        //{
        //    var str = phrase.RemoveAccent().ToLower();
        //    // invalid chars           
        //    str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        //    // convert multiple spaces into one space   
        //    str = Regex.Replace(str, @"\s+", " ").Trim();
        //    // cut and trim 
        //    str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
        //    str = Regex.Replace(str, @"\s", "-"); // hyphens   
        //    return str;
        //}

        //public static string RemoveAccent(this string txt)
        //{
        //    var bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
        //    return System.Text.Encoding.ASCII.GetString(bytes);
        //}

        public static decimal CastearDecimal(this string dato)
        {

            decimal entero;
            var enteroValido = decimal.TryParse(dato, out entero);

            return enteroValido ? entero : -1;

        }

        public static int CastearEntero(this string dato)
        {

            int entero;
            var enteroValido = int.TryParse(dato, out entero);

            return enteroValido ? entero : -1;        
        }

        public static string[] SplitCuentaCorreo(this string cuentaCorreo)
        {

            var indexArroba = cuentaCorreo.IndexOf('@');
            var cuenta = cuentaCorreo.Substring(0, indexArroba);
            var dominio = cuentaCorreo.Substring(indexArroba + 1);

            return new []{cuenta, dominio};

        }

    }
}
