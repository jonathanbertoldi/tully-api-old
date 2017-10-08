using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Models;

namespace Tully.Api.Repositories.Contracts
{
  public interface IFotoRepository
  {
    Task<Foto> GetFoto(int fotoId);
    Task<IEnumerable<Foto>> GetUsuarioFotos(int usuarioId);
  }
}
