using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Tully.Api.Models
{
    public class Usuario : IdentityUser<int>
    {
        public string Nome { get; set; }
        public string FotoPerfil { get; set; }
        public string FotoCapa { get; set; }
        public string Experiencia { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string País { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? RemovidoEm { get; set; }
    }
}
