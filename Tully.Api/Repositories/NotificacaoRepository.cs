using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;

namespace Tully.Api.Repositories
{
  public class NotificacaoRepository : INotificacaoRepository
  {
    private TullyContext _context;

    public NotificacaoRepository(TullyContext context)
    {
      _context = context;
    }

    public async Task<Notificacao> GetNotificacao(int notificacaoId) =>
      await _context.Notificacoes
        .Include(a => a.Usuario)
        .FirstOrDefaultAsync(a => a.Id == notificacaoId);

    public async Task<IEnumerable<Notificacao>> GetUsuarioNotificacoes(int usuarioId) =>
      await _context.Notificacoes
        .Include(a => a.Usuario)
        .Where(a => a.UsuarioId == usuarioId)
        .OrderByDescending(a => a.Id)
        .ToListAsync();
  }
}
