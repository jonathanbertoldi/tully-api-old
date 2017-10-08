using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Models;
using Tully.Api.ViewModels.DesafioViewModels;

namespace Tully.Api.Repositories.Contracts
{
  public interface IDesafioRepository
  {
    Task<Desafio> GetDesafio(int desafioId);
    Task<IEnumerable<Desafio>> GetDesafios();
  }
}
