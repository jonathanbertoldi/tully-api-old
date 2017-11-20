using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;

namespace Tully.Api.Repositories
{
  public class AvaliacaoRepository : IAvaliacaoRepository
  {
    private TullyContext _context;

    public AvaliacaoRepository(TullyContext context)
    {
      _context = context;
    }

    public async Task<Avaliacao> GetAvaliacao(int avaliacaoId) =>
      await _context.Avaliacoes
        .Include(a => a.Foto)
        .Include(a => a.Usuario)
        .FirstOrDefaultAsync(a => a.Id == avaliacaoId);

    public async Task<IEnumerable<Avaliacao>> GetFotoAvaliacoes(int fotoId) =>
      await _context.Avaliacoes
        .Include(a => a.Foto)
        .Include(a => a.Usuario)
        .Where(a => a.FotoId == fotoId)
        .ToListAsync();
  }
}
