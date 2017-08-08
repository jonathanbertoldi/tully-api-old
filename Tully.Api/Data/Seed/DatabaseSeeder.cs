using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Tully.Api.Models.User;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tully.Api.Data.Seed
{
    public class DatabaseSeeder
    {
        private UserManager<Usuario> _userManager;
        private RoleManager<Perfil> _roleManager;
        private TullyContext _context;

        public DatabaseSeeder(UserManager<Usuario> userManager, RoleManager<Perfil> roleManager, TullyContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task Seed()
        {
            await SeedRoles();
            await SeedUsers();
        }

        private async Task SeedRoles()
        {
            if (!(await _roleManager.RoleExistsAsync("Admin")))
            {
                var perfil = new Perfil("Admin");
                perfil.Claims.Add(new IdentityRoleClaim<int>() { ClaimType = "IsAdmin", ClaimValue = "true" });

                await _roleManager.CreateAsync(perfil);
            }
            if (!(await _roleManager.RoleExistsAsync("Usuario")))
            {
                var perfil = new Perfil("Usuario");
                perfil.Claims.Add(new IdentityRoleClaim<int>() { ClaimType = "IsAdmin", ClaimValue = "false" });

                await _roleManager.CreateAsync(perfil);
            }
        }

        private async Task SeedUsers()
        {
            var usuario = await _userManager.FindByNameAsync("admin");

            if (usuario == null)
            {
                usuario = new Usuario() { UserName = "admin", Email = "admin@tully.com", Nome = "Usuário Administrador" };

                var userResult = await _userManager.CreateAsync(usuario, "Senha#123");
                var roleResult = await _userManager.AddToRoleAsync(usuario, "Admin");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Falha na construção do usuário solicitado.");
                }
            }
        }
    }
}
