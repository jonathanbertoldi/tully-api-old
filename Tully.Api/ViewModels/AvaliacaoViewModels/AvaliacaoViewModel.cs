using System;
using Tully.Api.ViewModels.FotoViewModels;
using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.ViewModels.AvaliacaoViewModels
{
  public class AvaliacaoViewModel
  {
    public int Id { get; set; }
    public string Tipo { get; set; }
    public DateTime CriadoEm { get; set; }

    public FotoViewModel Foto { get; set; }
    public UsuarioViewModel Usuario { get; set; }
  }
}
