using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using Web.Base.Log;
using PostSharp.Aspects;

namespace Web.Base.Aspectos
{
    [Serializable]
    public class LogExcepcion : OnMethodBoundaryAspect
    {
        private Configuracion _configuracion;
        
        private ILog _log;

        public override void OnException(MethodExecutionArgs args)
        {
            var cadenaConexion = "name=Log";
            var contexto = new LogApiContext(cadenaConexion);

            _configuracion = new Configuracion();

            var listaParametros = args.Arguments.OfType<object>();

            var error = EstablecerError(args, listaParametros);

            _log = new LogManager(contexto);
            _log.GuardarException(error);

            if (args.Exception is Exception)
            {
                LanzarHttpException(args.Exception.Message);
            }

            LanzarHttpException(args.Exception.Message);
        }
       
        private Error EstablecerError(MethodExecutionArgs args, IEnumerable<object> listaParametros)
        {
            var usuario = string.Empty;
            _configuracion = new Configuracion();
            
            if (((ApiController)args.Instance).User != null)
            {
                usuario = _configuracion.ObtenerUsuario((ApiController)args.Instance);
            }

            var ip = _configuracion.ObtenerIp((ApiController)args.Instance);

            var error = new Error
            {
                Entidad = _configuracion.ObtenerInstancia(args.Instance.ToString()),
                Metodo = args.Method.Name,
                FechaError = DateTime.Now,
                Usuario = usuario, 
                Parametros = _configuracion.CrearJsonParametros(listaParametros.ToList()),
                MensajeError = args.Exception.Message,
                IpOrigen = ip,
                StackTrace = args.Exception.StackTrace ?? args.Exception.GetType().ToString()
            };

            return error;
        }

        private void LanzarHttpException(string mensajeError)
        {
            var contextResult = new HttpActionContext
            {
                Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent<ErrorModel>(new ErrorModel { Mensaje = mensajeError }, new JsonMediaTypeFormatter(), "application/json")
                }
            };

            throw new HttpResponseException(contextResult.Response);
        }

     
    }
}
