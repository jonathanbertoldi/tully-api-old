using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Models;

namespace Tully.Api.Repositories.Contracts
{
  public interface IRelacionamentoRepository
  {
    Task<IEnumerable<Usuario>> GetSeguidores(int usuarioId);
    Task<IEnumerable<Usuario>> GetSeguindo(int usuarioId);
    Task<Relacionamento> GetRelacionamento(int relacionamentoId);
    Task<Relacionamento> GetRelacionamentoPorUsuario(int usuarioId, int seguidoId);
  }
}
