using System;

namespace Tully.Api.Models
{
  public class Relacionamento
  {
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int SeguidoId { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? RemovidoEm { get; set; }

    public virtual Usuario Usuario { get; set; }
    public virtual Usuario Seguido { get; set; }
  }
}