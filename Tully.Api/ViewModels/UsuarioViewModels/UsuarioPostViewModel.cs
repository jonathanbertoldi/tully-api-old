using System.ComponentModel.DataAnnotations;

namespace Tully.Api.ViewModels.UsuarioViewModels
{
    public class UsuarioPostViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Email { get; set; }
        public string FotoPerfil { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}
