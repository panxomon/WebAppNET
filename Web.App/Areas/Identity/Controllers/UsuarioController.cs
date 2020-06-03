using System.Linq;
using System.Web.Http;
using Web.App.Areas.Identity.Models;
using Web.App.Areas.Identity.Permisos;
using Web.Base.Aspectos;
using Web.Base.Identity.Core;
using Web.Base.Identity.Permisos;

namespace Web.App.Areas.Identity.Controllers
{
    [RecursosRoles(Recursos.Usuario)]
    public class UsuarioController : ApiController
    {
        private OmsUserManager _userManager;

        [ClaimsAction(ClaimsActions.Acceder)]
        public UsuarioController(OmsUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: api/Usuario
        [LogExcepcion]
        [ClaimsAction(ClaimsActions.Listar)]
        public IHttpActionResult Get()
        {
            var usuarios = _userManager.Users.ToList();

            return Ok(usuarios);
        }


        // GET: api/Usuario/5
        [LogExcepcion]
        [ClaimsAction(ClaimsActions.Obtener)]
        public IHttpActionResult Get(int id)
        {
            var resultado = _userManager.FindByIdAsync(id);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);

        }

        // POST: api/Usuario
        [LogAuditor, LogExcepcion]
        [ClaimsAction(ClaimsActions.Crear)]
        public IHttpActionResult Post([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new Usuario
            {
                UserName = model.Mail,
                Email = model.Mail,
                Rut = model.Rut,
                Nombre = model.Nombre
            };

            _userManager.CreateAsync(user, model.Password);

            model.Id = user.Id;            
            return Created(Request.RequestUri + "/" + user.Id, user);

        }

        //// PUT: api/Usuario/5
        //[LogAuditor, LogExcepcion]
        //[ClaimsAction(ClaimsActions.Actualizar)]
        //public async Task<IHttpActionResult> Put(int id, [FromBody] LoginModel model)
        //{

        //    Mapper.CreateMap<LoginModel, Usuario>();
        //    var userMapper = Mapper.Map<Usuario>(model);
        //    userMapper.UsuarioId = id;

        //    var usuarioSeleccionado = _repositorio.ObtenerPorId(userMapper.UsuarioId);
        //    if (usuarioSeleccionado == null)
        //        throw new Exception("Nombre usuario no existe.");

        //    var passwordAntigua = usuarioSeleccionado.Password;

        //    var usuarioIdentity = _userManager.FindByEmail(usuarioSeleccionado.Mail);
        //    if (usuarioIdentity == null)
        //        throw new Exception("Nombre del usuario no existe.");

        //    if (!_repositorio.Actualizar(userMapper))
        //        return InternalServerError();

        //    usuarioIdentity.Email = userMapper.Mail;
        //    usuarioIdentity.UserName = userMapper.Mail;

        //    await _userManager.ChangePasswordAsync(usuarioIdentity.Id, passwordAntigua, userMapper.Password);

        //    var resultadoIdentity = _userManager.Update(usuarioIdentity);

        //    if (!resultadoIdentity.Succeeded)
        //        return BadRequest();

        //    return Ok(true);


        //}

        //// DELETE: api/Usuario/5 
        //[LogAuditor, LogExcepcion]
        //[ClaimsAction(ClaimsActions.Eliminar)]
        //public async Task<IHttpActionResult> Delete(int id)
        //{

        //    var usuarioSeleccionado = _repositorio.ObtenerPorId(id);

        //    if (usuarioSeleccionado == null)
        //        throw new Exception("Nombre del servidor no existe.");

        //    var usuarioIdentity = _userManager.FindByEmail(usuarioSeleccionado.Mail);
        //    if (usuarioIdentity == null)
        //        throw new Exception("Nombre del servidor no existe.");

        //    _repositorio.Eliminar(usuarioSeleccionado);

        //    var resultadoIdentity = _userManager.Delete(usuarioIdentity);

        //    if (!resultadoIdentity.Succeeded)
        //        return BadRequest();

        //    return Ok(true);

        //}


        //public bool Options()
        //{
        //    return true;
        //}
    }
}
