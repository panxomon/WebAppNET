using Web.Contexto.Enums.Actividad;

namespace Web.Contexto.Entidades.Actividades
{
    public class Requisito
    {
        public long RequisitoId { get; set; }

        public string Nombre { get; set; }
        public string Objetivo { get; set; }
        public string Tolerancia { get; set; }
        public string Adjunto { get; set; }
        public TipoRequisito TipoRequisito { get; set; }
    }
}
