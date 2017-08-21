using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Models;
using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.Controllers
{
    [Authorize(Roles = "Usuario")]
    [Route("api/usuarios")]
    public class UsuarioController : Controller
    {
        private TullyContext _context;
        private UserManager<Usuario> _userManager;
        private RoleManager<Perfil> _roleManager;

        public UsuarioController(TullyContext context, UserManager<Usuario> userManager, RoleManager<Perfil> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var role = _roleManager.FindByNameAsync("Usuario");

            var usuarios = await _context.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).ToListAsync();
            var result = Mapper.Map<IEnumerable<UsuarioViewModel>>(usuarios);

            return Ok(result);
        }
    }
}
