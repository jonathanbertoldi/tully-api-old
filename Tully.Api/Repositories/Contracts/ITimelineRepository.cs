using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Models;

namespace Tully.Api.Repositories.Contracts
{
  public interface ITimelineRepository
  {
    Task<IEnumerable<Foto>> GetUsuarioTimeline(int usuarioId);
  }
}
