using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.ViewModels.RelacionamentoViewModels
{
  public class RelacionamentoViewModel
  {
    public int Id { get; set; }
    public UsuarioViewModel Usuario { get; set; }
    public UsuarioViewModel Seguido { get; set; }
  }
}
