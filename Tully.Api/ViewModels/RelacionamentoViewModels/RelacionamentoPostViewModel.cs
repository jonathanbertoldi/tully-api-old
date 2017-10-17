using System.ComponentModel.DataAnnotations;

namespace Tully.Api.ViewModels.RelacionamentoViewModels
{
  public class RelacionamentoPostViewModel
  {
    [Required]
    public int UsuarioId { get; set; }
    [Required]
    public int SeguidoId { get; set; }
  }
}
