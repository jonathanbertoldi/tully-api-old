using System.Threading.Tasks;
using Tully.Api.Models;

namespace Tully.Api.Repositories.Contracts
{
  public interface IUsuarioRepository
  {
    Task<Usuario> GetAdministrador(int id);
    Task<Usuario> GetUsuario(int id);
  }
}
