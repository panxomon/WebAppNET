using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using Web.Base.Identity.Permisos;

namespace Web.Base.Identity.Core
{
    public class ClaimsAuthorisationFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            ////TODO: descomentar return solo para depurar.
            //return;

            var username = ((ApiController)actionContext.ControllerContext.Controller).User as ClaimsPrincipal;

            var tipoControlador = actionContext.ControllerContext.Controller.GetType();

            var controllerGroup = tipoControlador.GetCustomAttribute<ClaimsGroupAttribute>();

            if (controllerGroup == null)
            {
                return;
            }

            var actionClaim = actionContext.ActionDescriptor.GetCustomAttributes<ClaimsActionAttribute>().FirstOrDefault();

            actionClaim = actionClaim ?? new ClaimsActionAttribute(ClaimsActions.Acceder);

            var groupId = controllerGroup.GetGroupId();
            var claimValue = actionClaim.Claim.ToString();

            var hasClaim = username.HasClaim(groupId, claimValue);

            if (!hasClaim)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}