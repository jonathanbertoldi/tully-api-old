using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tully.Api.Models;

namespace Tully.Api.Data.Seeders
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
            var admin = await _userManager.FindByNameAsync("admin");

            if (admin == null)
            {
                admin = new Usuario() {
                    UserName = "admin",
                    Email = "admin@tully.com",
                    Nome = "Usuário Administrador"
                };

                var adminResult = await _userManager.CreateAsync(admin, "Senha#123");
                var adminRoleResult = await _userManager.AddToRoleAsync(admin, "Admin");

                if (!adminResult.Succeeded || !adminRoleResult.Succeeded)
                {
                    throw new InvalidOperationException("Falha na construção do usuário solicitado.");
                }
            }

            var admin2 = await _userManager.FindByNameAsync("admin2");

            if (admin2 == null)
            {
                admin2 = new Usuario() {
                    UserName = "admin2",
                    Email = "admin2@tully.com",
                    Nome = "Segundo Usuário Administrador"
                };

                var admin2Result = await _userManager.CreateAsync(admin2, "Senha#123");
                var admin2RoleResult = await _userManager.AddToRoleAsync(admin2, "Admin");

                if (!admin2Result.Succeeded || !admin2RoleResult.Succeeded)
                {
                    throw new InvalidOperationException("Falha na construção do usuário solicitado.");
                }
            }

            var usuario = await _userManager.FindByNameAsync("usuario");

            if (usuario == null)
            {
                usuario = new Usuario()
                {
                    UserName = "usuario",
                    Email = "usuario@tully.com",
                    Nome = "Usuário Jogador da Silva",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Pais = "Brasil"
                };

                var userResult = await _userManager.CreateAsync(usuario, "Senha#123");
                var roleResult = await _userManager.AddToRoleAsync(usuario, "Usuario");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Falha na construção do usuário solicitado.");
                }
            }

            var matheus = await _userManager.FindByNameAsync("matheus");

            if (matheus == null)
            {
                matheus = new Usuario()
                {
                    UserName = "matheus",
                    Email = "matheus@tully.com",
                    Nome = "Matheus Vieira",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Pais = "Brasil"
                };

                var userResult = await _userManager.CreateAsync(matheus, "Senha#123");
                var roleResult = await _userManager.AddToRoleAsync(matheus, "Usuario");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Falha na construção do usuário solicitado.");
                }
            }

            var jeff = await _userManager.FindByNameAsync("jeff");

            if (jeff == null)
            {
                jeff = new Usuario()
                {
                    UserName = "jeff",
                    Email = "jeff@tully.com",
                    Nome = "Jefferson",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Pais = "Brasil"
                };

                var userResult = await _userManager.CreateAsync(jeff, "Senha#123");
                var roleResult = await _userManager.AddToRoleAsync(jeff, "Usuario");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Falha na construção do usuário solicitado.");
                }
            }
        }
    }
}
