using Tully.Api.DataAnnotations;

namespace Tully.Api.ViewModels.AvaliacaoViewModels
{
  public class AvaliacaoPatchViewModel
  {
    [Avaliacao]
    public string Tipo { get; set; }
  }
}
