using System.ComponentModel.DataAnnotations;

namespace Tully.Api.ViewModels.UsuarioViewModels
{
  public class UsernameViewModel
  {
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
  }
}
