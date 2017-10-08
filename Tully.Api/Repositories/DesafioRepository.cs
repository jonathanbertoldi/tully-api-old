using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;

namespace Tully.Api.Repositories
{
  public class DesafioRepository : IDesafioRepository
  {
    private TullyContext _context;

    public DesafioRepository(TullyContext context)
    {
      _context = context;
    }

    public async Task<Desafio> GetDesafio(int desafioId) =>
      await _context
        .Desafios
        .FirstOrDefaultAsync(a => a.Id == desafioId);

    public async Task<IEnumerable<Desafio>> GetDesafios() =>
      await _context
        .Desafios
        .Where(d => d.RemovidoEm == null)
        .ToListAsync();
  }
}
