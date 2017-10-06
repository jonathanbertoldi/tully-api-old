using Microsoft.EntityFrameworkCore;
using Tully.Api.Models;

namespace Tully.Api.Data.Mappings
{
  public static class DesafioMapping
  {
    public static void Map(ModelBuilder builder)
    {
      builder.Entity<Desafio>(b =>
      {
        b.ToTable("Desafio");

        b.Property(d => d.Foto)
          .IsRequired()
          .ForSqlServerHasDefaultValueSql("'fotos_desafio/default-place-photo.jpg'");

        b.Property(d => d.CriadoEm)
          .IsRequired()
          .ForSqlServerHasDefaultValueSql("GETDATE()");
      });
    }
  }
}
