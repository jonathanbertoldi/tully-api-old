using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;

namespace Tully.Api.Repositories
{
  public class FotoRepository : IFotoRepository
  {
    private TullyContext _context;

    public FotoRepository(TullyContext context)
    {
      _context = context;
    }

    public async Task<Foto> GetFoto(int fotoId) =>
      await _context
        .Fotos
        .Include(a => a.Desafio)
        .Include(a => a.Usuario)
        .Include(a => a.Avaliacoes)
        .FirstOrDefaultAsync(a => a.Id == fotoId);

    public async Task<IEnumerable<Foto>> GetUsuarioFotos(int usuarioId) =>
      await _context
        .Fotos
        .Include(a => a.Desafio)
        .Include(a => a.Usuario)
        .Include(a => a.Avaliacoes)
        .Where(a => a.UsuarioId == usuarioId)
        .OrderByDescending(a => a.CriadoEm)
        .ToListAsync();
  }
}
