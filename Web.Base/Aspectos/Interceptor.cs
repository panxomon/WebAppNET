using System;
using System.Web.Http;
using PostSharp.Aspects;

namespace Web.Base.Aspectos
{
    [Serializable]
    public class Interceptor : LocationInterceptionAspect
    {
        public override void OnGetValue(LocationInterceptionArgs args)
        {
            var configuracion = new Configuracion();

            var usuario = configuracion.ObtenerUsuario((ApiController)args.Instance);

            args.Value = usuario;

            base.OnSetValue(args);
        }
    }
}
