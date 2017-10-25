using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tully.Api.Models;

namespace Tully.Api.Data.Mappings
{
  public static class AvaliacaoMapping
  {
    public static void Map(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Avaliacao>(b =>
      {
        b.ToTable("Avaliacao");

        b.HasKey(a => a.Id);

        b.Property(a => a.Tipo)
          .IsRequired()
          .HasMaxLength(8);

        b.Property(a => a.CriadoEm)
          .IsRequired()
          .ForSqlServerHasDefaultValueSql("GETDATE()");

        b.Property(a => a.UsuarioId)
          .IsRequired();
        b.HasOne(a => a.Usuario)
          .WithMany(a => a.Avaliacoes)
          .HasForeignKey(a => a.UsuarioId)
          .OnDelete(DeleteBehavior.Restrict);

        b.Property(a => a.FotoId)
          .IsRequired();
        b.HasOne(a => a.Foto)
          .WithMany(c => c.Avaliacoes)
          .HasForeignKey(a => a.FotoId);
      });
    }
  }
}
