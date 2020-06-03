using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Web.App.Areas.Identity.Models;
using Web.App.Areas.Identity.Permisos;
using Web.Base.Aspectos;
using Web.Base.Identity.Core;
using Web.Base.Identity.Permisos;

namespace Web.App.Areas.Identity.Controllers
{
    [RecursosRoles(Recursos.Account)]
    public class AccountController : ApiController
    {
        private readonly RoleManager _roleManager;
        private readonly ClaimedActionsProvider _claimedActionsProvider;
        private readonly OmsUserManager _userManager;

        [ClaimsAction(ClaimsActions.Acceder)]
        public AccountController(RoleManager roleManager, ClaimedActionsProvider claimedActionsProvider, OmsUserManager userManager)
        {
            _roleManager = roleManager;
            _claimedActionsProvider = claimedActionsProvider;
            _userManager = userManager;
        }

        // GET api/account
        [LogExcepcion]
        [ClaimsAction(ClaimsActions.Listar)]
        public IHttpActionResult Get()
        {
            var viewModel = new UsersIndexViewIndex()
            {
                Users = _userManager.Users.ToList()
            };

            return Ok(viewModel);
        }

        // GET api/account
        [LogExcepcion]
        [ClaimsAction(ClaimsActions.Obtener)]
        public IHttpActionResult Get([FromUri]string mail)
        {
            //var viewModel = new ApplicationUser();

            var resultado = _userManager.Users.FirstOrDefault(x => x.Email == mail);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        [LogExcepcion]
        [ClaimsAction(ClaimsActions.Obtener)]
        public async Task<IHttpActionResult> Get(long id, string tip)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userRoles = await _userManager.GetRolesAsync(id);

            var userclaims = new List<Claim>();

            foreach (var role in userRoles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                userclaims.AddRange(roleClaims);
            }

            var claimGroups = _claimedActionsProvider.GetClaimGroups();

            var viewModel = new UserClaimsViewModel()

            {
                UserName = user.UserName,
            };

            foreach (var claimGroup in claimGroups)
            {
                var claimGroupModel = new UserClaimsViewModel.ClaimGroup
                {
                    GroupId = claimGroup.GroupId,
                    GroupName = claimGroup.GroupName,
                    GroupClaimsCheckboxes = claimGroup.Claims
                        .Select(c => new SelectListItem()
                        {
                            Value = String.Format("{0}#{1}", claimGroup.GroupId, c),
                            Text = c,
                            Selected = userclaims.Any(ac => ac.Type == claimGroup.GroupId.ToString() && ac.Value == c)
                        }).ToList()
                };
                viewModel.ClaimGroups.Add(claimGroupModel);
            }

            return Ok(viewModel);
        }



        //[System.Web.Http.HttpPost]
        //public IHttpActionResult LogOff()
        //{
        //    InvalidateUserSession();

        //    //OAuthManager.SignOut(DefaultAuthenticationTypes.ExternalBearer); 
        //    return Ok(new { message = "Logout successful." });
        //}

        //private void InvalidateUserSession()
        //{
        //    var context = Request.GetOwinContext();

        //    context.Authentication.SignOut(DefaultAuthenticationTypes.ExternalBearer);

        //    string authToken = GetCurrentBearerAuthrorizationToken();

        //    var asd = context.Request.User.Identity;

        //    var auth = asd.GetUserName();
        //}

        //private string GetCurrentBearerAuthrorizationToken()
        //{
        //    var current = Request.GetOwinContext().Authentication;

        //    var message = HttpContext.Current.Request.Headers["Authorization"];

        //    string authToken = null;
        //    if (message != null) 
        //    {
        //        if (current.User.Identity.AuthenticationType == "Bearer")
        //        {
        //            authToken = HttpContext.Current.Request.Headers.Get("Authorization");
        //        }
        //    }
        //    return authToken;
        //}

        //private IAuthenticationManager OAuthManager 
        //{
        //    get
        //    {
        //        return Request.GetOwinContext().Authentication;
        //        return HttpContext.Current.GetOwinContext().Authentication;
        //    }
        //}

        public class UsersIndexViewIndex
        {
            public List<Usuario> Users { get; set; }
        }

    }
}
