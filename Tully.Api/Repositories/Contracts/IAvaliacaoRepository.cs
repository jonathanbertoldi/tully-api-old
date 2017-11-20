using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Models;

namespace Tully.Api.Repositories.Contracts
{
  public interface IAvaliacaoRepository
  {
    Task<Avaliacao> GetAvaliacao(int avaliacaoId);
    Task<IEnumerable<Avaliacao>> GetFotoAvaliacoes(int fotoId);
  }
}
