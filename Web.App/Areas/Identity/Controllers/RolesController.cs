using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Web.App.Areas.Identity.Models;
using Web.App.Areas.Identity.Permisos;
using Web.Base.Aspectos;
using Web.Base.Identity.Core;
using Web.Base.Identity.Permisos;

//using System.Web.Mvc;

namespace Web.App.Areas.Identity.Controllers
{
    [RecursosRoles(Recursos.Roles)]
    public class RolesController : ApiController
    {
        private readonly OmsUserManager _userManager;
        private readonly RoleManager _roleManager;

        [ClaimsAction(ClaimsActions.Acceder)]
        public RolesController(OmsUserManager userManager, RoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/Roles
        [LogExcepcion]
        [ClaimsAction(ClaimsActions.Listar)]
        public IHttpActionResult Get()
        {
            var allRoles = _roleManager.Roles.ToList();

            return Ok(allRoles);
        }

        //GET: api/Roles/5
        [LogExcepcion]
        [ClaimsAction(ClaimsActions.Obtener)]
        public async Task<IHttpActionResult> Get(int userId)
        {
            var resultadoUsuario = string.Empty;//usuario.ObtenerPorId(userId);


            var usuarioApp = _userManager.Users.FirstOrDefault(x => x.Email == resultadoUsuario);

            var assignedRoles = await _userManager.GetRolesAsync(usuarioApp.Id);

            var allRoles = _roleManager.Roles.ToList();

            var userRoles = allRoles.Select(r => new SelectListItem()
            {
                Value = r.Name,
                Text = r.Name,
                Selected = assignedRoles.Contains(r.Name),
            }).ToList();

            var viewModel = new UsuarioRolesViewModel()
            {
                Username = usuarioApp.UserName,
                UserId = (int) usuarioApp.Id, 
                UserRoles = userRoles,
            };
            return Ok(viewModel);
        }

        // POST: api/Roles
        [LogAuditor, LogExcepcion]
        [ClaimsAction(ClaimsActions.Crear)]
        public async Task<IHttpActionResult> Post([FromBody]RoleViewModel viewModel)
        {
            var nuevoRol = new Roles()
            {
                Name = viewModel.Nombre
            };

            await _roleManager.CreateAsync(nuevoRol);

            return Ok(nuevoRol);

        }

        // PUT: api/Roles/5
        //[LogAuditor, LogExcepcion]
        [ClaimsAction(ClaimsActions.Actualizar)]
        public async Task<IHttpActionResult> Put([FromBody]UsuarioRolesViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.UserId);

            var possibleRoles = await _roleManager.Roles.ToListAsync();

            var userRoles = await _userManager.GetRolesAsync(user.Id);

            var submittedRoles = viewModel.SelectedRoles;

            var shouldUpdateSecurityStamp = false;

            foreach (var submittedRole in submittedRoles)
            {
                var hasRole = await _userManager.IsInRoleAsync(user.Id, submittedRole);
                if (!hasRole)
                {
                    shouldUpdateSecurityStamp = true;
                    await _userManager.AddToRoleAsync(user.Id, submittedRole);
                }
            }

            foreach (var removedRole in possibleRoles.Select(r => r.Name).Except(submittedRoles))
            {
                shouldUpdateSecurityStamp = true;
                await _userManager.RemoveFromRoleAsync(user.Id, removedRole);
            }

            if (shouldUpdateSecurityStamp)
            {
                await _userManager.UpdateSecurityStampAsync(user.Id);
            }

            return Ok(true);
        }

        // DELETE: api/Roles/5
        [LogAuditor, LogExcepcion]
        [ClaimsAction(ClaimsActions.Eliminar)]
        public async Task<IHttpActionResult> Delete(long userId)
        {

            var rolIdentity = await _roleManager.FindByIdAsync(userId);

            if (rolIdentity.Users.Count() != 0)
                throw new Exception("No se puede eliminar el rol, porque tiene usuarios asignados");

            var assignedClaims = await _roleManager.GetClaimsAsync(rolIdentity.Name);

            if (assignedClaims.Count() != 0)
                throw new Exception("No se puede eliminar el rol " + rolIdentity.Name + ", debe quitar los claims asignados");

            await _roleManager.DeleteAsync(rolIdentity);

            return Ok(true);
        }

        public IHttpActionResult Options()
        {
            return Ok();
        }
    }
}
