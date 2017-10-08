using System.ComponentModel.DataAnnotations;

namespace Tully.Api.ViewModels.FotoViewModels
{
  public class FotoPostViewModel
  {
    [Required]
    public int? UsuarioId { get; set; }
    [Required]
    public int? DesafioId { get; set; }
    [Required]
    public string FotoUrl { get; set; }
  }
}
