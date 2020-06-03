using System;
using System.Linq;
using PostSharp.Aspects;
using Web.Base.Validador;

namespace Web.Base.Aspectos
{
    [Serializable]
    public class Validar : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            var instancia = (from p in args.Instance.GetType().GetProperties() select args.Instance.GetType().GetProperty(p.Name).GetValue(args.Instance, null)).ToList();

            if (!instancia.Any()) return;

            var command = args.Arguments.OfType<object>().FirstOrDefault(); 

            var objeto = (ValidarModel)instancia.First();

            objeto.Validar(command);

            if (objeto.EsValido()) return;

            var resultado = string.Join("" + Environment.NewLine, objeto.ListarErrores());

            throw new Exception(resultado);
        }
    }
}