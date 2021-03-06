using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Base.Identity.Core
{
    public class Usuario : IdentityUser<long, UsuarioLogin, UsuarioRole, UsuarioClaim>
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(OmsUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}