using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.ViewModels.NotificacaoViewModels
{
  public class NotificacaoViewModel
  {
    public int Id { get; set; }
    public string Mensagem { get; set; }
    public bool Visto { get; set; }

    public UsuarioViewModel Usuario { get; set; }
  }
}
