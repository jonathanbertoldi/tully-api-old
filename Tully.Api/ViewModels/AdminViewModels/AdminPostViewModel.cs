using System.ComponentModel.DataAnnotations;

namespace Tully.Api.ViewModels.AdminViewModels
{
    public class AdminPostViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
