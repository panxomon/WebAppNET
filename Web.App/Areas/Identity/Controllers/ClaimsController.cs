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
    [RecursosRoles(Recursos.Claims)]
    public class ClaimsController : ApiController
    {
        private readonly RoleManager _roleManager;
        private readonly ClaimedActionsProvider _claimedActionsProvider;

        [ClaimsAction(ClaimsActions.Acceder)]
        public ClaimsController(RoleManager roleManager, ClaimedActionsProvider claimedActionsProvider)
        {
            _roleManager = roleManager;
            _claimedActionsProvider = claimedActionsProvider;
        }

        [LogExcepcion]
        [ClaimsAction(ClaimsActions.Obtener)]
        public async Task<IHttpActionResult> Get()
        {
            var claimPrincipal = ((ApiController)ActionContext.ControllerContext.Controller).User as ClaimsPrincipal;
            var rol = claimPrincipal.Claims.FirstOrDefault(x => x.Type.ToLower() == "rol");

            if (rol == null)
            {
                throw new Exception("No existe");
            }


            var role = await _roleManager.FindByNameAsync(rol.Value);

            var claimGroups = _claimedActionsProvider.GetClaimGroups();

            var assignedClaims = await _roleManager.GetClaimsAsync(role.Name);

            var viewModel = new RoleClaimsViewModel()
            {
                RoleId = role.Id,
                RoleName = role.Name,
            };

            foreach (var claimGroup in claimGroups)
            {
                var claimGroupModel = new RoleClaimsViewModel.ClaimGroup()
                {
                    GroupId = claimGroup.GroupId,
                    GroupName = claimGroup.GroupName,
                    GroupClaimsCheckboxes = claimGroup.Claims
                        .Select(c => new SelectListItem()
                        {
                            Value = String.Format("{0}#{1}", claimGroup.GroupId, c),
                            Text = c,
                            Selected = assignedClaims.Any(ac => ac.Type == claimGroup.GroupId.ToString() && ac.Value == c)
                        }).ToList()
                };
                viewModel.ClaimGroups.Add(claimGroupModel);
            }

            return Ok(viewModel);
        }


        //// GET: api/Claims
        [LogAuditor, LogExcepcion]
        [ClaimsAction(ClaimsActions.Listar)]
        public async Task<IHttpActionResult> Get(long id)
        {

            var role = await _roleManager.FindByIdAsync(id);

            var claimGroups = _claimedActionsProvider.GetClaimGroups();

            var assignedClaims = await _roleManager.GetClaimsAsync(role.Name);

            var roles = new List<Rol>();

            foreach (var claimGroup in claimGroups)
            {
                var rolCreado = new Rol
                {
                    Nombre = claimGroup.GroupName,
                    Claims =
                    claimGroup.Claims
                        .Select(c => new Claims
                        {
                            Valor = string.Format("{0}#{1}", claimGroup.GroupId, c),
                            Text = c,
                            Selected = assignedClaims.Any(ac => ac.Type == claimGroup.GroupId.ToString() && ac.Value == c)
                        }).ToList()
                };

                roles.Add(rolCreado);

            }

            return Ok(roles);

        }

        //GET: api/Claims/5 id de ROL


        // PUT: api/Claims/5 
        [LogAuditor, LogExcepcion]
        [ClaimsAction(ClaimsActions.Actualizar)]
        public async Task<IHttpActionResult> Put([FromBody]RoleClaimsViewModel viewModel)
        {
            var role = await _roleManager.FindByIdAsync(viewModel.RoleId);
            var roleClaims = await _roleManager.GetClaimsAsync(role.Name);

            //TODO: Metodo feo, buscar la forma eficiente de comparar listas

            foreach (var removedClaim in roleClaims)
            {
                await _roleManager.RemoveClaimAsync(role.Id, removedClaim);
            }

            var submittedClaims = viewModel
                .SelectedClaims
                .Select(s =>
                {
                    var tokens = s.Split('#');
                    if (tokens.Count() != 2)
                    {
                        throw new Exception(String.Format("Claim {0} se encuentra en diferente formato", s));
                    }
                    return new Claim(tokens[0], tokens[1]);
                }).ToList();


            roleClaims = await _roleManager.GetClaimsAsync(role.Name);

            foreach (var submittedClaim in submittedClaims)
            {
                var hasClaim = roleClaims.Any(c => c.Value == submittedClaim.Value && c.Type == submittedClaim.Type);
                if (!hasClaim)
                {
                    await _roleManager.AddClaimAsync(role.Id, submittedClaim);
                }
            }

            roleClaims = await _roleManager.GetClaimsAsync(role.Name);

            //TODO: LOGOUT  al usuario cuando se cambien los permisos.

            return Created(Request.RequestUri + "/" + role.Id, viewModel);

            //return await
            //    Task.FromResult(
            //        Created(
            //                Request, 
            //                Url.Link("getClaims", new{controller = "Claims",id = role.Id})
            //                )
            //                );
        }

        public bool Options()
        {
            return true;
        }
    }
}
