using System.Threading.Tasks;

namespace Tully.Api.Repositories.Contracts
{
  public interface IRepository
  {
    Task Add<T>(T entity) where T : class;
    Task<bool> SaveAllAsync();
  }
}
