using System;
using System.Linq;
using Web1.Models.Actividades;
using Web.Base.Cqrs.Command;
using Web.Contexto;

namespace Web1.Workflows.Actividades
{
    public class EliminarActividad : ICommandHandler<ActividadPorId>, IDisposable
    {
        private WebContext _contexto;

        public EliminarActividad(WebContext contexto)
        {
            _contexto = contexto;
        }

        public void Execute(ActividadPorId command)
        {
            var actividad = _contexto.Actividades.FirstOrDefault(x => x.ActividadId == command.Id);

            _contexto.Actividades.Remove(actividad);

            Guardar();
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
