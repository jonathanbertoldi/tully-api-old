using Tully.Api.DataAnnotations;

namespace Tully.Api.ViewModels.AvaliacaoViewModels
{
  public class AvaliacaoPostViewModel
  {
    [Avaliacao]
    public string Tipo { get; set; }
  }
}
