using System.ComponentModel.DataAnnotations;

namespace Tully.Api.ViewModels.UsuarioViewModels
{
    public class UsuarioUpdateViewModel
    {
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FotoPerfil { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}
