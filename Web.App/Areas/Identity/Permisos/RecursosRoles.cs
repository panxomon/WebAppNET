using Web.Base.Identity.Permisos;

namespace Web.App.Areas.Identity.Permisos
{
    public class RecursosRoles : ClaimsGroupAttribute
    {
        public Recursos Resources { get; private set; }
         
        public RecursosRoles(Recursos recursos) : base(recursos)
        {
            Resources = recursos;
        }
    }
}