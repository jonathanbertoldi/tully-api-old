using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tully.Api.Data.Mappings;
using Tully.Api.Models;

namespace Tully.Api.Data
{
    public class TullyContext : IdentityDbContext<Usuario, Perfil, int>
    {
        public DbSet<Desafio> Desafios { get; set; }

        public TullyContext(DbContextOptions<TullyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(b =>
            {
                b.ToTable("Usuario");

                b.Property(u => u.FotoPerfil)
                    .IsRequired()
                    .ForSqlServerHasDefaultValueSql("'fotos_perfil/default-photo.jpg'");

                b.Property(u => u.CriadoEm)
                    .IsRequired()
                    .ForSqlServerHasDefaultValueSql("GETDATE()");
            });
            builder.Entity<Perfil>(b => b.ToTable("Perfil"));

            DesafioMapping.Map(builder);
        }
    }
}
