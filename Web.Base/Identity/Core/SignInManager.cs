using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Web.Base.Identity.Core
{
    public class SignInManager : SignInManager<Usuario, long>
    {
        public SignInManager(OmsUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
    }
}