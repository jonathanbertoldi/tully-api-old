using System;

namespace Tully.Api.Models
{
    public class Desafio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Descrição { get; set; }
        public string Endereço { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? RemovidoEm { get; set; }
    }
}
