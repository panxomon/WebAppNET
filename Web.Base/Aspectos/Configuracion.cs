using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Newtonsoft.Json;

namespace Web.Base.Aspectos
{
    [Serializable]
    public class Configuracion
    {
        public string ObtenerInstancia(string instancia)
        {
            return instancia.Split('.').Last();
        }

        public string ObtenerUsuario(ApiController controller)
        {
            ClaimsPrincipal claim = null;  

            if ((controller).User == null)
            {
                
            }
            else
            {
                claim = (controller).User as ClaimsPrincipal;    
            }

            var firstOrDefault = claim.Claims.FirstOrDefault(x => x.Type.ToLower() == "mailcliente");
            var usuario = firstOrDefault != null ? firstOrDefault.Value : claim.Identity.Name;

            return usuario;
        }

        public string ObtenerIp(ApiController controller)
        {
            ClaimsPrincipal claim = null;

            if ((controller).User == null)
            {

            }
            else
            {
                claim = (controller).User as ClaimsPrincipal;
            }

            var ipRemota = claim.Claims.FirstOrDefault(x => x.Type.ToLower() == "remoteip");

            var ipLocal = claim.Claims.FirstOrDefault(x => x.Type.ToLower() == "localip");


            var usuario = ipRemota != null ? ipRemota.Value : claim.Identity.Name;

            return "IP Remota: " +  usuario + " ip local: " + ipLocal;
        }

        public string CrearJsonParametros(IList<object> lista) 
        {
            var json = JsonConvert.SerializeObject(lista, (Formatting) System.Xml.Formatting.Indented);
             
            return json;
        }
    }
}
