using Tully.Api.ViewModels.DesafioViewModels;
using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.ViewModels.FotoViewModels
{
  public class FotoViewModel
  {
    public int Id { get; set; }
    public string FotoUrl { get; set; }
    public UsuarioViewModel Usuario { get; set; }
    public DesafioViewModel Desafio { get; set; }
  }
}
