using System.ComponentModel.DataAnnotations;
using Tully.Api.DataAnnotations;

namespace Tully.Api.ViewModels.AvaliacaoViewModels
{
  public class AvaliacaoPostViewModel
  {
    [Avaliacao]
    public string Tipo { get; set; }
    [Required]
    public int? FotoId { get; set; }
    [Required]
    public int? UsuarioId { get; set; }
  }
}
