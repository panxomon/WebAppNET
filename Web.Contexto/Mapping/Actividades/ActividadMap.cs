using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Web.Contexto.Entidades.Actividades;

namespace Web.Contexto.Mapping.Actividades
{
    public class ActividadMap : EntityTypeConfiguration<Actividad>
    {
        public ActividadMap()
        {
            HasKey(x => x.ActividadId);

            Property(x => x.ActividadId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Nombre); 
            Property(x => x.Codigo);

            Property(x => x.Descripcion);
        }
    }
}
