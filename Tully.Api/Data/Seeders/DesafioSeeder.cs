using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Models;

namespace Tully.Api.Data.Seeders
{
    public static class DesafioSeeder
    {
        public static async Task SeedDesafios(TullyContext context)
        {
            if (!context.Desafios.Any())
            {
                var desafios = new List<Desafio>
                {
                    new Desafio()
                    {
                        Nome = "Praça da Sé",
                        Endereco = "Praça da Sé, 92",
                        Descricao = "A Praça da Sé é um espaço público localizado no bairro da Sé, " +
                        "no distrito homônimo, no Centro do município de São Paulo, no Brasil. É " +
                        "considerado o centro geográfico da cidade. Nela, localiza-se o monumento " +
                        "marco zero do município.A partir dele, contam - se as distâncias de todas " +
                        "as rodovias que partem de São Paulo, bem como a numeração das vias " +
                        "públicas da cidade.",
                        Cidade = "São Paulo",
                        Latitude = "-23.549776",
                        Longitude = "-46.633991"
                    },
                    new Desafio()
                    {
                        Nome = "Avenida Paulista",
                        Endereco = "Av. Paulista, 92",
                        Descricao = "A Avenida Paulista é um dos logradouros mais importantes do município " +
                        "de São Paulo, a capital do estado homônimo. Está localizada no limite entre as " +
                        "zonas Centro-Sul, Central e Oeste; e em uma das regiões mais elevadas da cidade, " +
                        "chamada de Espigão da Paulista.",
                        Cidade = "São Paulo",
                        Latitude = "-23.563263",
                        Longitude = "-46.654243"
                    },
                    new Desafio()
                    {
                        Nome = "Parque Ibirapuera",
                        Endereco = "Parque Ibirapuera",
                        Descricao = "O Parque Ibirapuera é o mais importante parque urbano da cidade de São Paulo, " +
                        "no Brasil. Foi inaugurado em 21 de agosto de 1954 para a comemoração do quarto centenário " +
                        "da cidade. No município, é superado em tamanho apenas pelo Parque do Carmo e pelo Parque " +
                        "Anhanguera. Foi eleito, em 2015, um dos melhores parques do planeta pelo renomado jornal " +
                        "britânico The Guardian, ficando ao lado de parques famosos como o Buttes-Chaumont de Paris" +
                        ", o Boboli de Florença, a High Line de Nova Iorque, o Hampstead Heath de Londres e o Parque " +
                        "Güell de Barcelona.",
                        Cidade = "São Paulo",
                        Latitude = "-23.584631",
                        Longitude = "-46.656050"
                    },
                    new Desafio()
                    {
                        Nome = "Pavilhão do Anhembi",
                        Endereco = "Av. Olavo Fontoura, 1200",
                        Descricao = "O Anhembi Parque (antiga Arena Skol Anhembi) é um complexo cultural-comercial " +
                        "localizado na Avenida Olavo Fontoura, em Santana, na Zona Norte da cidade de São Paulo, " +
                        "no Brasil. Com localização privilegiada, possuindo estação de metrô próxima e estradas de " +
                        "fácil acesso.",
                        Cidade = "São Paulo",
                        Latitude = "-23.515221",
                        Longitude = "-46.642282"
                    },
                    new Desafio()
                    {
                        Nome = "Praça Comandante Linneu Gomes",
                        Endereco = "Praça Comandante Linneu Gomes",
                        Descricao = "O Aeroporto de São Paulo/Congonhas - Deputado Freitas Nobre, ou simplesmente " +
                        "Aeroporto de Congonhas, (IATA: CGH, ICAO: SBSP) é um aeroporto doméstico no município de " +
                        "São Paulo, o segundo mais movimentado do Brasil.",
                        Cidade = "São Paulo",
                        Latitude = "-23.626159",
                        Longitude = "-46.661299"
                    }
                };

                await context.AddRangeAsync(desafios);
                await context.SaveChangesAsync();
            }
        }
    }
}
