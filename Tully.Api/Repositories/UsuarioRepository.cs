using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;

namespace Tully.Api.Repositories
{
  public class UsuarioRepository : IUsuarioRepository
  {
    private TullyContext _context;
    private UserManager<Usuario> _userManager;
    private RoleManager<Perfil> _roleManager;

    public UsuarioRepository(TullyContext context, UserManager<Usuario> userManager, RoleManager<Perfil> roleManager)
    {
      _context = context;
      _userManager = userManager;
      _roleManager = roleManager;
    }

    public async Task<Usuario> GetAdministrador(int id)
    {
      var perfilAdmin = await _roleManager.FindByNameAsync("Admin");

      return await _context.Users
        .Where(a => a.Roles.Any(r => r.RoleId == perfilAdmin.Id))
        .Where(a => a.RemovidoEm == null)
        .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Usuario> GetUsuario(int id)
    {
      var perfilUsuario = await _roleManager.FindByNameAsync("Usuario");

      return await _context.Users
        .Where(a => a.Roles.Any(r => r.RoleId == perfilUsuario.Id))
        .Where(a => a.RemovidoEm == null)
        .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Usuario>> GetRankingGeral()
    {
      var perfilUsuario = await _roleManager.FindByNameAsync("Usuario");

      return await _context.Users
        .Where(a => a.Roles.Any(r => r.RoleId == perfilUsuario.Id))
        .Where(a => a.RemovidoEm == null)
        .OrderByDescending(a => a.Experiencia)
        .ToListAsync();
    }
  }
}
