using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;

namespace Tully.Api.Repositories
{
  public class RelacionamentoRepository : IRelacionamentoRepository
  {
    private TullyContext _context;
    private IUsuarioRepository _usuarioRepository;

    public RelacionamentoRepository(TullyContext context, IUsuarioRepository usuarioRepository)
    {
      _context = context;
      _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<Usuario>> GetSeguidores(int usuarioId)
    {
      var usuario = await _usuarioRepository.GetUsuario(usuarioId);

      if (usuario == null) return null;

      var relacionamentos = await _context.Relacionamentos
        .Include(a => a.Usuario)
        .Include(a => a.Seguido)
        .Where(a => a.SeguidoId == usuarioId)
        .Where(a => a.RemovidoEm == null)
        .ToListAsync();

      List<Usuario> result = new List<Usuario>();

      relacionamentos.ForEach(r => result.Add(r.Usuario));

      return result;
    }

    public async Task<IEnumerable<Usuario>> GetSeguindo(int usuarioId)
    {
      var usuario = await _usuarioRepository.GetUsuario(usuarioId);

      if (usuario == null) return null;

      var relacionamentos = await _context.Relacionamentos
        .Include(a => a.Usuario)
        .Include(a => a.Seguido)
        .Where(a => a.UsuarioId == usuarioId)
        .Where(a => a.RemovidoEm == null)
        .ToListAsync();

      List<Usuario> result = new List<Usuario>();

      relacionamentos.ForEach(r => result.Add(r.Seguido));

      return result;
    }

    public async Task<Relacionamento> GetRelacionamento(int relacionamentoId) =>
      await _context.Relacionamentos
        .Include(a => a.Usuario)
        .Include(a => a.Seguido)
        .Where(a => a.RemovidoEm == null)
        .FirstOrDefaultAsync(a => a.Id == relacionamentoId);

    public async Task<Relacionamento> GetRelacionamentoPorUsuario(int usuarioId, int seguidoId) =>
      await _context.Relacionamentos
        .Include(a => a.Usuario)
        .Include(a => a.Seguido)
        .Where(a => a.UsuarioId == usuarioId)
        .Where(a => a.SeguidoId == seguidoId)
        .Where(a => a.RemovidoEm == null)
        .FirstOrDefaultAsync();
  }
}
