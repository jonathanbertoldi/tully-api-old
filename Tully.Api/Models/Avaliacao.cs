using System;

namespace Tully.Api.Models
{
  public class Avaliacao
  {
    public int Id { get; set; }
    public string Tipo { get; set; }
    public int UsuarioId { get; set; }
    public int FotoId { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? RemovidoEm { get; set; }

    public virtual Usuario Usuario { get; set; }
    public virtual Foto Foto { get; set; }
  }
}
