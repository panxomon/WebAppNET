using Web.Base.Cqrs.Query;

namespace Web1.Models.Actividades
{
    public class ActividadResult : IQueryResult
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
}
