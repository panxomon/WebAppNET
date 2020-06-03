using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Web.Base.Identity.Core;

namespace Web.App.Providers.OAuthPrivider
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        private OmsUserManager _userManager;
        private RoleManager _roleManager;
        private bool _esError;

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            _esError = false;
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var esCliente = context.Request.ReadFormAsync().Result.Select(x => x.Key.ToLower() == "rut").FirstOrDefault(x => x.Equals(true));

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            _userManager = new OmsUserManager(new ApiUserContext());
            _roleManager = new RoleManager(_userManager.Dbcontext);

            await ValidarUsuario(context, identity);

            _userManager.Dispose();

            if (_esError)
            {
                return;
            }

            var ticket = new AuthenticationTicket(identity, null);

            context.Validated(ticket);
        }

        private async Task ValidarUsuario(OAuthGrantResourceOwnerCredentialsContext context, ClaimsIdentity identity)
        {
            var usuario = await _userManager.FindAsync(context.UserName, context.Password);

            if (usuario == null)
            {
                context.SetError("no_permiso", "El Email o password son incorrectos.");
                _esError = true;
                return;
            }

            var authManager = context.OwinContext.Authentication;

            var user = await _userManager.FindAsync(usuario.Email, context.Password);
            await _userManager.SignInAsync(authManager, user, isPersistent: false);

            var userRoles = context.OwinContext.Authentication.AuthenticationResponseGrant.Identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                                        .Select(c => c.Value)
                                        .ToList();

            await EstablecerRoles(identity, userRoles);

            identity.AddClaim(new Claim("UsuarioId", usuario.Id.ToString()));

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName.ToLower()));
        }

        private async Task EstablecerRoles(ClaimsIdentity identity, IEnumerable<string> userRoles)
        {
            foreach (var roleName in userRoles)
            {
                var cachedClaims = await _roleManager.GetClaimsAsync(roleName);

                identity.AddClaim(new Claim("rol", roleName));
                identity.AddClaims(cachedClaims);
            }
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }
    }
}