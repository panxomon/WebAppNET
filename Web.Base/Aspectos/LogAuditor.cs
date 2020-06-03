using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using PostSharp.Aspects;
using Web.Base.Log;

namespace Web.Base.Aspectos 
{
    [Serializable]
    public class LogAuditor : OnMethodBoundaryAspect
    {
        private Configuracion _configuracion;
        private ILog _log;

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var cadenaConexion = "name=Log";
            var contexto = new LogApiContext(cadenaConexion);

            var listaParametros = args.Arguments.OfType<object>();

            var auditor = EstablecerAuditor(args, listaParametros);

            _log = new LogManager(contexto);
            _log.GuardarLog(auditor);
        } 

        private string DireccionIp()
        {
            var listaIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            if ((listaIP == null || listaIP.Length == 0))
            {
                return listaIP.FirstOrDefault().MapToIPv4().ToString();
            }

            var direccionIp = listaIP[1].MapToIPv4().ToString();

            return direccionIp;
        }

        private Auditor EstablecerAuditor(MethodExecutionArgs args, IEnumerable<object> listaParametros)
        {
            _configuracion = new Configuracion();

            var direccionIp = DireccionIp();

            var usuario = _configuracion.ObtenerUsuario((ApiController)args.Instance);

            var auditor = new Auditor
            {
                Entidad = _configuracion.ObtenerInstancia(args.Instance.ToString()),
                Accion = args.Method.Name,
                FechaRegistro = DateTime.Now,
                ModificadoPor = usuario,
                Parametros = _configuracion.CrearJsonParametros(listaParametros.Select(x => x).ToList()),
                Nombre = direccionIp
            };

            return auditor;
        }
    }
}
