using System.Collections.Generic;
using System.Linq;

namespace Web.Base.Validador
{
    public abstract class ValidarModel
    {
        private IList<Regla> _listaReglas = new List<Regla>();

        public abstract void Validar<Tentity>(Tentity entidad) where Tentity : class;

        public IList<string> Reglas
        {
            get { return ListarErrores(); }
        }

        public void AgregarRegla(Regla regla)
        {
            _listaReglas.Add(regla);
        }

        public bool EsValido()
        {
            return Reglas.Count <= 0;
        }

        public IList<string> ListarErrores() 
        {
            return  _listaReglas.Select(x => x.Propiedad + ": " +  x.Descripcion).ToList();
        }
    }
}
