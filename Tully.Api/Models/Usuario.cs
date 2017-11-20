using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Tully.Api.Models
{
  public class Usuario : IdentityUser<int>
  {
    public string Nome { get; set; }
    public string FotoPerfil { get; set; }
    public int Experiencia { get; set; }
    public string Cidade { get; set; }
    public string Pais { get; set; }
    public string DeviceId { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? RemovidoEm { get; set; }

    public virtual ICollection<Relacionamento> Seguindo { get; set; }
    public virtual ICollection<Relacionamento> Seguidores { get; set; }
    public virtual ICollection<Foto> Fotos { get; set; }
    public virtual ICollection<Notificacao> Notificacoes { get; set; }
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; }
  }
}
