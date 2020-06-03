using System.Collections.Generic;

namespace Web.Contexto.Entidades.Actividades
{
    public class Actividad
    {
        public long ActividadId { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; } 
        public string Codigo { get; set; }

        public virtual ICollection<Requisito> Requisitos { get; set; }

        public Actividad()
        {
            Requisitos = new List<Requisito>();
        }
    }
}
