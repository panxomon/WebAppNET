using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Web.Contexto.Entidades.Actividades;

namespace Web.Contexto.Mapping.Actividades
{
    public class RequisitoMap : EntityTypeConfiguration<Requisito>
    {
        public RequisitoMap()
        {
            HasKey(x => x.RequisitoId);

            Property(x => x.RequisitoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Nombre);
            Property(x => x.Objetivo);
            Property(x => x.Adjunto);
            Property(x => x.Tolerancia);
            Property(x => x.TipoRequisito);
        }
    }
}
