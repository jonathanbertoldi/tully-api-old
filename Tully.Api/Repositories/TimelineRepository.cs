using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;

namespace Tully.Api.Repositories
{
  public class TimelineRepository : ITimelineRepository
  {
    private TullyContext _context;

    public TimelineRepository(TullyContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Foto>> GetUsuarioTimeline(int usuarioId)
    {
      var fotos = await _context.Users
        .Include(a => a.Seguindo)
        .Include(a => a.Fotos)
        .Where(a => a.Id == usuarioId)
        .SelectMany(a => a.Seguindo)
        .Select(a => a.Seguido)
        .SelectMany(a => a.Fotos)
        .Include(a => a.Usuario)
        .OrderByDescending(a => a.CriadoEm)
        .ToListAsync();

      return await _context.Fotos
        .Include(a => a.Avaliacoes)
        .Include(a => a.Usuario)
        .Include(a => a.Desafio)
        .Where(a => fotos.Any(b => b.Id == a.Id))
        .OrderByDescending(a => a.CriadoEm)
        .ToListAsync();
    }
  }
}
