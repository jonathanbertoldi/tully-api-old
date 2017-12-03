using System;
using Tully.Api.ViewModels.AvaliacaoViewModels;
using Tully.Api.ViewModels.DesafioViewModels;
using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.ViewModels.FotoViewModels
{
  public class FotoViewModel
  {
    public int Id { get; set; }
    public string FotoUrl { get; set; }
    public int Curtidas { get; set; }
    public int Descurtidas { get; set; }
    public DateTime CriadoEm { get; set; }
    public UsuarioViewModel Usuario { get; set; }
    public DesafioViewModel Desafio { get; set; }
    public AvaliacaoSimplesViewModel Avaliacao { get; set; }
  }
}
