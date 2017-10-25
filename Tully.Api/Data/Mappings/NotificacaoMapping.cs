using Microsoft.EntityFrameworkCore;
using Tully.Api.Models;

namespace Tully.Api.Data.Mappings
{
  public static class NotificacaoMapping
  {
    public static void Map(ModelBuilder builder) 
    {
      builder.Entity<Notificacao>(b =>
      {
        b.ToTable("Notificacao");

        b.HasKey(n => n.Id);

        b.Property(n => n.Mensagem)
          .IsRequired()
          .HasMaxLength(255);

        b.Property(n => n.Visto)
          .IsRequired();

        b.Property(n => n.UsuarioId)
          .IsRequired();
        b.HasOne(n => n.Usuario)
          .WithMany(u => u.Notificacoes)
          .HasForeignKey(n => n.UsuarioId);
      });
    }
  }
}