﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Filters;
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

        [HttpGet("{adminId}", Name = "GetAdmin")]
        public async Task<IActionResult> GetAdmin(int adminId)
        {
            var role = await _roleManager.FindByNameAsync("Admin");
            var admin = await _context.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).FirstOrDefaultAsync(a => a.Id == adminId);

            if (admin == null) return NotFound();

            var result = Mapper.Map<AdminViewModel>(admin);

            return Ok(result);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> PostAdmin([FromBody] AdminPostViewModel model)
        {
            var admin = Mapper.Map<Usuario>(model);

            var userResult = await _userManager.CreateAsync(admin, model.Password);
            var roleResult = await _userManager.AddToRoleAsync(admin, "Admin");

            if (!userResult.Succeeded)
                return StatusCode(409, userResult.Errors);

            if (!roleResult.Succeeded)
                return StatusCode(409, roleResult.Errors);

            var result = Mapper.Map<AdminViewModel>(admin);

            return CreatedAtRoute("GetAdmin", new { adminId = admin.Id }, result);
        }
    }
}
