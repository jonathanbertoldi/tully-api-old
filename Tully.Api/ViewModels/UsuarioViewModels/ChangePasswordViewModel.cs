using System.ComponentModel.DataAnnotations;

namespace Tully.Api.ViewModels.UsuarioViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string SenhaAtual { get; set; }
        [Required]
        public string SenhaNova { get; set; }
    }
}
