using System;
using System.Collections.Generic;

namespace Tully.Api.Models
{
  public class Desafio
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Pais { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Descricao { get; set; }
    public string Url { get; set; }
    public string Telefone { get; set; }
    public string Foto { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? RemovidoEm { get; set; }

    public virtual ICollection<Foto> Fotos { get; set; }
  }
}
