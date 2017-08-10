using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tully.Api.Models;

namespace Tully.Api.Data
{
    public class TullyContext : IdentityDbContext<Usuario, Perfil, int>
    {
        public TullyContext(DbContextOptions<TullyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(b => b.ToTable("Usuario"));
            builder.Entity<Perfil>(b => b.ToTable("Perfil"));
        }
    }
}
