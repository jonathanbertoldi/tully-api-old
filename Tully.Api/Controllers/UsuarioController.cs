using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Filters;
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
            var role = await _roleManager.FindByNameAsync("Usuario");

            var usuarios = await _context.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).ToListAsync();
            var result = Mapper.Map<IEnumerable<UsuarioViewModel>>(usuarios);

            return Ok(result);
        }

        [HttpGet("{usuarioId}", Name = "GetUsuario")]
        public async Task<IActionResult> GetUsuario(int usuarioId)
        {
            var role = await _roleManager.FindByNameAsync("Usuario");

            var usuario = await _context
                .Users
                .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null) return NotFound();

            var result = Mapper.Map<UsuarioViewModel>(usuario);

            return Ok(result);
        }

        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioPostViewModel model)
        {
            var usuario = Mapper.Map<Usuario>(model);

            var userResult = await _userManager.CreateAsync(usuario, model.Senha);

            if (!userResult.Succeeded)
                return StatusCode(409, userResult.Errors);

            var roleResult = await _userManager.AddToRoleAsync(usuario, "Usuario");

            if (!roleResult.Succeeded)
                return StatusCode(409, roleResult.Errors);

            var result = Mapper.Map<UsuarioViewModel>(usuario);

            return CreatedAtRoute("GetUsuario", new { usuarioId = usuario.Id }, result);
        }

        [ValidateModel]
        [HttpPatch("{usuarioId}")]
        public async Task<IActionResult> PatchUsuario(int usuarioId, [FromBody] JsonPatchDocument<UsuarioUpdateViewModel> patchDocument)
        {
            if (patchDocument == null) return BadRequest();

            var usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
            var isUsuario = await _userManager.IsInRoleAsync(usuario, "Usuario");

            if (usuario == null || !isUsuario) return NotFound();

            var usuarioToPatch = Mapper.Map<UsuarioUpdateViewModel>(usuario);
            patchDocument.ApplyTo(usuarioToPatch, ModelState);

            Mapper.Map(usuarioToPatch, usuario);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [ValidateModel]
        [HttpPatch("{usuarioId}/senha")]
        public async Task<IActionResult> ChangePasswordUsuario(int usuarioId, [FromBody] ChangePasswordViewModel model)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
            var isUsuario = await _userManager.IsInRoleAsync(usuario, "Usuario");

            if (usuario == null || !isUsuario) return NotFound();

            var userResult = await _userManager.ChangePasswordAsync(usuario, model.SenhaAtual, model.SenhaNova);

            if (!userResult.Succeeded)
                return BadRequest(userResult.Errors);

            return NoContent();
        }
    }
}
