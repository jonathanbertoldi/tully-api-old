using System.Reflection;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Repositories.Contracts;

namespace Tully.Api.Repositories
{
  public class Repository : IRepository
  {
    private TullyContext _context;

    public Repository(TullyContext context)
    {
      _context = context;
    }

    public async Task Add<T>(T entity) where T : class
    {
      await _context.AddAsync(entity);
    }

    public async Task<bool> SaveAllAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }
  }
}
