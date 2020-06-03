using System;

namespace Web.Base.Identity.Permisos
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public abstract class RecursoAttribute : Attribute
    {
        public Enum ListaRecursos { get; set; }

        public RecursoAttribute(Enum recurso)   
        {
            this.ListaRecursos = recurso;
        }
    }
}
