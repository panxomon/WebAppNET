using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Base.Identity.Core
{
    public class ApiUserContext : IdentityDbContext<Usuario, Roles, long, UsuarioLogin, UsuarioRole, UsuarioClaim>
    {
        private const string _conexion = "Name=Identity";

        public ApiUserContext(string cadenaDeConexion = null) : base(cadenaDeConexion ?? (_conexion))
        {
        }

        public IDbSet<RoleClaim> RoleClaim { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();           

            modelBuilder.Entity<UsuarioLogin>().Map(c =>
            {
                c.ToTable("UsuarioLogin");
                c.Properties(p => new
                {
                    p.UserId,
                    p.LoginProvider,
                    p.ProviderKey
                });
            }).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });          
        }
    }
}