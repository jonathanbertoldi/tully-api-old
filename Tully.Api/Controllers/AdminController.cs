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
using Tully.Api.ViewModels.AdminViewModels;

namespace Tully.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admins")]
    public class AdminController : Controller
    {
        private TullyContext _context;
        private UserManager<Usuario> _userManager;
        private RoleManager<Perfil> _roleManager;

        public AdminController(TullyContext context, UserManager<Usuario> userManager, RoleManager<Perfil> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            var role = await _roleManager.FindByNameAsync("Admin");

            var admins = await _context.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).ToListAsync();
            var result = Mapper.Map<IEnumerable<AdminViewModel>>(admins);

            return Ok(result);
        }
    }
}
