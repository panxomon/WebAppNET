using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Web.Base.Identity.Core
{
    public class ClaimsFactory : ClaimsIdentityFactory<Usuario, long>
    {
        public override async Task<ClaimsIdentity> CreateAsync(UserManager<Usuario, long> userManager, Usuario user, string authenticationType)
        {
            var claimsIdentity = await base.CreateAsync(userManager, user, authenticationType);

            claimsIdentity.AddClaim(new Claim("Web:App", "11"));

            return claimsIdentity;
        }
    }
}
