using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tully.Api.Models;

namespace Tully.Api.Data.Mappings
{
  public static class RelacionamentoMapping
  {
    public static void Map(ModelBuilder builder)
    {
      builder.Entity<Relacionamento>(b =>
      {
        b.HasKey(a => a.Id);

        b.HasOne(r => r.Usuario)
          .WithMany(u => u.Seguindo)
          .HasForeignKey(r => r.UsuarioId)
          .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(r => r.Seguido)
          .WithMany(u => u.Seguidores)
          .HasForeignKey(r => r.SeguidoId)
          .OnDelete(DeleteBehavior.Restrict);

        b.Property(r => r.CriadoEm)
          .IsRequired()
          .ForSqlServerHasDefaultValueSql("GETDATE()");
      });
    }
  }
}