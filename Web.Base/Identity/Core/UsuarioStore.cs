using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Base.Identity.Core
{
    public class UsuarioStore : UserStore<Usuario, Roles, long, UsuarioLogin, UsuarioRole, UsuarioClaim>
    {
        public UsuarioStore(ApiUserContext context) : base(context)
        {
        }
    }
}