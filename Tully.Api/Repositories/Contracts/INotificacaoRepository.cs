using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Models;

namespace Tully.Api.Repositories.Contracts
{
  public interface INotificacaoRepository
  {
    Task<Notificacao> GetNotificacao(int notificacaoId);
    Task<IEnumerable<Notificacao>> GetUsuarioNotificacoes(int usuarioId);
  }
}
