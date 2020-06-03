
using System.Threading.Tasks;
using System.Web.Http;
using Web.App.Areas.Identity.Models;
using Web.App.Areas.Identity.Permisos;
using Web.Base.Identity.Core;
using Web.Base.Identity.Permisos;

namespace Web.App.Areas.Identity.Controllers
{
    [RecursosRoles(Recursos.Login)]
    public class LoginController : ApiController
    {
        private readonly OmsUserManager _userManager;

        [ClaimsAction(ClaimsActions.Acceder)]
        public LoginController(OmsUserManager userManager)
        {
            _userManager = userManager;
        }

        // POST: api/Login
        [ClaimsAction(ClaimsActions.Crear)]
        public async Task<IHttpActionResult> Post([FromBody]LoginViewModel model)
        {
            var contadorAction = this.ActionContext.ActionArguments.Count;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    //await _userManager.SignInAsync(AuthenticationManager, user, model.RememberMe);
                    //return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return Ok(model);
        }
    }
}
