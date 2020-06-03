using System;
using Web.Base.Aspectos;
using Web.Base.Cqrs.Command;
using Web.Contexto;
using Web1.Mapper.Map;
using Web1.Models.Actividades;

namespace Web1.Workflows.Actividades
{
    public class CrearActividad : ICommandHandler<NuevaActividad>, IDisposable
    {
        private WebContext _contexto;

        public CrearActividad(WebContext contexto)
        {
            _contexto = contexto;
        }

        [Validar]
        public void Execute(NuevaActividad command)
        {
            var resultado = command.ToEntity();

            _contexto.Actividades.Add(resultado);

            Guardar();

            command.Id = resultado.ActividadId;
        }
       
        public void Guardar()
        {
            _contexto.SaveChanges();
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_contexto != null)
                {
                    _contexto.Dispose();
                    _contexto = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
