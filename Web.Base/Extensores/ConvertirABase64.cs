using System;
using System.Net;

namespace Web.Base
{
    public static class ConvertirABase64
    {
        public static string ConvertirUrlImagenABase64(this string urlImagen)
        {
            var uri = new Uri(urlImagen);

            using (var client = new WebClient())
            {
                var imageBytes = client.DownloadData(uri);
                var base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
       
    }
}
