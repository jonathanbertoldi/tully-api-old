using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Tully.Api.Models
{
    public class Usuario : IdentityUser<int>
    {
        public string Nome { get; set; }
        public string FotoPerfil { get; set; }
        public int Experiencia { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? RemovidoEm { get; set; }
    }
}
