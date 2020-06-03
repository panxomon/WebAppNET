using System.Linq;
using System.Web.Http;
using Web.Base.Cqrs.Command;
using Web.Base.Cqrs.Query;
using Web.Base.Identity.Permisos;
using Web.Contexto;
using Web1.Mapper.Map;
using Web1.Models.Actividades;
using Web1.Permisos;

namespace Web1.Controllers
{
    [GrupoProyecto(RecursosProyecto.Actividades)]
    public class ActividadesController : ApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private WebContext _contexto;

        [ClaimsAction(ClaimsActions.Acceder)]
        public ActividadesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, WebContext contexto)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _contexto = contexto;
        }

        // GET api/<controller>
        [ClaimsAction(ClaimsActions.Listar)]
        public IHttpActionResult Get()
        {
            var resultado = _contexto.Actividades.AsNoTracking().ToList();

            var retorno = resultado.Select(ActividadProfileMap.ToResult);

            return Ok(retorno);
        }

        // GET api/<controller>/5
        [ClaimsAction(ClaimsActions.Obtener)]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var resultado = _contexto.Actividades.FirstOrDefault(x => x.ActividadId == id);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado.ToResult());
        }

        // POST api/<controller>
        [ClaimsAction(ClaimsActions.Crear)]
        public IHttpActionResult Post([FromBody]NuevaActividad model)
        {
            _commandDispatcher.Dispatch(model);

            return Created(Request.RequestUri + "/" + model.Id.ToString(), model);
        }

        // PUT api/<controller>/5
        [ClaimsAction(ClaimsActions.Actualizar)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [ClaimsAction(ClaimsActions.Eliminar)]
        public IHttpActionResult Delete(int id)
        {
            var model = new ActividadPorId {Id = id};

            _commandDispatcher.Dispatch(model);

            return Ok();
        }
    }
}
