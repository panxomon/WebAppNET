using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using PostSharp.Aspects;

namespace Web.Base.Aspectos
{
    [Serializable]
    public class UsuarioAspect : MethodInterceptionAspect
    {
        public string Usuario { get; set; }

        public override void OnInvoke(MethodInterceptionArgs args) 
        {
            var _configuracion = new Configuracion(); 

            var usuario = _configuracion.ObtenerUsuario((ApiController)args.Instance);

            var algo = Arguments.Create<string>(usuario); 

            args.ReturnValue = algo;

            for (int i = 0; i < args.Arguments.Count  ; i++)
            {
                args.Arguments[i] = usuario;
            }

            base.OnInvoke(args);

        }

        private readonly LogAuditor _aspect1 = new LogAuditor();
        private readonly LogExcepcion _aspect2 = new LogExcepcion();


        public IEnumerable<AspectInstance> ProvideAspects(object targetElement)
        {
            MemberInfo nfo = (MemberInfo)targetElement;

            yield return new AspectInstance(targetElement, _aspect1);

            if (nfo.ReflectedType.IsPublic && !nfo.Name.Equals(".ctor"))
            {
                yield return new AspectInstance(targetElement, _aspect2);
            }
        }

    }
}
