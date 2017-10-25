using System;
using System.Collections.Generic;

namespace Tully.Api.Models
{
  public class Foto
  {
    public int Id { get; set; }
    public string FotoUrl { get; set; }
    public int UsuarioId { get; set; }
    public int DesafioId { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }
    public DateTime? RemovidoEm { get; set; }

    public virtual Usuario Usuario { get; set; }
    public virtual Desafio Desafio { get; set; }
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; }
  }
}
