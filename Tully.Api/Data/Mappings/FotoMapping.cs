using Microsoft.EntityFrameworkCore;
using Tully.Api.Models;

namespace Tully.Api.Data.Mappings
{
  public static class FotoMapping
  {
    public static void Map(ModelBuilder builder)
    {
      builder.Entity<Foto>(b =>
      {
        b.ToTable("Foto");

        b.Property(a => a.FotoUrl)
          .IsRequired();

        b.Property(a => a.CriadoEm)
          .IsRequired()
          .ForSqlServerHasDefaultValueSql("GETDATE()");

        b.Property(a => a.UsuarioId)
          .IsRequired();
        b.HasOne(a => a.Usuario)
          .WithMany(a => a.Fotos)
          .HasForeignKey(a => a.UsuarioId);

        b.Property(a => a.DesafioId)
          .IsRequired();
        b.HasOne(a => a.Desafio)
          .WithMany(a => a.Fotos)
          .HasForeignKey(a => a.DesafioId);
      });
    }
  }
}
