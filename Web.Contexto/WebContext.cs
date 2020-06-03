
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Web.Contexto.Entidades.Actividades;
using Web.Contexto.Mapping.Actividades;

namespace Web.Contexto
{
    public class WebContext : DbContext
    {
        private const string _conexion = "Name=conexion";

        public DbSet<Actividad> Actividades { get; set; }       
        public DbSet<Requisito> Requisitos { get; set; }

        public WebContext() : base(_conexion)
        {

        }

        public WebContext(string cadenaDeConexion = null) : base(cadenaDeConexion ?? (_conexion))
        {
            Database.SetInitializer<WebContext>(new CreateDatabaseIfNotExists<WebContext>());
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ActividadMap());           
            modelBuilder.Configurations.Add(new RequisitoMap());
        }
    }
}
