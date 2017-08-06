using System.ComponentModel.DataAnnotations;

namespace Tully.Api.ViewModels
{
    public class CredenciaisViewModel
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
