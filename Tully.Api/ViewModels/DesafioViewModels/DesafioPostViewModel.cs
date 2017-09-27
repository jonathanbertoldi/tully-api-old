using System.ComponentModel.DataAnnotations;

namespace Tully.Api.ViewModels.DesafioViewModels
{
    public class DesafioPostViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Pais { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Foto { get; set; }
    }
}
