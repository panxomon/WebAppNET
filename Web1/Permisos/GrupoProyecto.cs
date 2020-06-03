using Web.Base.Identity.Permisos;

namespace Web1.Permisos
{
    public class GrupoProyecto  : ClaimsGroupAttribute
    {
        public RecursosProyecto Resources { get; private set; }

        public GrupoProyecto(RecursosProyecto resource) : base(resource)
        {
            Resources = resource;
        }
    }
}
