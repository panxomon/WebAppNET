using System.Collections.Generic;

namespace Web.App.Areas.Identity.Models
{
    public class Rol
    {
        public string Nombre { get; set; }
        public List<Claims> Claims { get; set; }

        public Rol()
        {
            Claims = new List<Claims>();
        }
    }

}