using System.Linq;
using System.Web.Http;
using Web.Contexto;

namespace Web1.Controllers
{
    public class ItemsController : ApiController
    {
        private WebContext _contexto;

        public ItemsController(WebContext contexto)
        {
            _contexto = contexto;
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
