using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Base.Identity.Core
{
    public class UsuarioRole : IdentityUserRole<long>
    {
        public long UsuarioRoleId { get; set; }
    }
}