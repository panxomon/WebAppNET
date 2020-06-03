using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Web.Base.Identity.Core
{ 
    public class OmsUserManager : UserManager<Usuario, long>   
    {
        public ApiUserContext Dbcontext { get; }

        public OmsUserManager(ApiUserContext dbContext) : base(new UsuarioStore(dbContext))
        {
            if (Dbcontext == null)
            {
                Dbcontext = dbContext;
            }

            UserValidator = new UserValidator<Usuario, long>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            //UserTokenProvider = new EmailTokenProvider<ApplicationUser>();
            ClaimsIdentityFactory = new ClaimsFactory();

            // enable lockout on users
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(1);
            MaxFailedAccessAttemptsBeforeLockout = 6;
            UserLockoutEnabledByDefault = true;
        }
        public async Task<IdentityResult> ChangePasswordAsync(Usuario usuario, string contrasena)
        {
            var userPasswordStore = this.Store as IUserPasswordStore<Usuario, long>;
            var store = userPasswordStore;
            if (store == null)
            {
                var errors = new[]
                {
                    "Current UserStore dasent implementemos IUserPasswordStore"
                };

                return await Task.FromResult(new IdentityResult(errors));
            }

            var newPasswordHash = PasswordHasher.HashPassword(contrasena);

            await store.SetPasswordHashAsync(usuario, newPasswordHash);
            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task SignInAsync(IAuthenticationManager authenticationManager, Usuario usuario, bool isPersistent)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalBearer);

            var identity = await this.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ExternalBearer);

            identity.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent
            };

            authenticationManager.SignIn(authenticationProperties, identity);
        }
    }
}