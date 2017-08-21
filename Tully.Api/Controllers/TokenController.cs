using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tully.Api.Filters;
using Tully.Api.Models;
using Tully.Api.ViewModels;

namespace Tully.Api.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private IConfigurationRoot _configuration;
        private IPasswordHasher<Usuario> _hasher;
        private UserManager<Usuario> _userManager;
        private RoleManager<Perfil> _roleManager;

        public TokenController(IConfigurationRoot configuration,
            IPasswordHasher<Usuario> hasher,
            UserManager<Usuario> userManager,
            RoleManager<Perfil> roleManager)
        {
            _configuration = configuration;
            _hasher = hasher;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateToken([FromBody] CredenciaisViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Usuario);

            if (user == null) return Unauthorized();

            if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Senha) != PasswordVerificationResult.Success) return Unauthorized();

            var usuarioRoles = await _userManager.GetRolesAsync(user);
            var usuarioClaims = await _userManager.GetClaimsAsync(user);

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim("roles", String.Join(", ", usuarioRoles))
            }.Union(usuarioClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMonths(1),
                signingCredentials: creds
            );

            var usuario = new
            {
                UserName = user.UserName,
                Nome = user.Nome,
                Email = user.Email,
                Perfil = usuarioRoles[0]
            };

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo, usuario = usuario });
        }

        [Authorize]
        [HttpGet("validar")]
        public async Task<IActionResult> ValidateToken()
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);

            var usuario = new
            {
                UserName = user.UserName,
                Nome = user.Nome,
                Email = user.Email,
                Perfil = roles[0]
            };

            var authorizationHeader = HttpContext.Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString().Remove(0, 7);

            var jwt = new JwtSecurityToken(authorizationHeader);

            var response = new
            {
                token = jwt.RawData,
                expiration = jwt.ValidTo,
                usuario = usuario
            };

            return Ok(response);
        }
    }
}
