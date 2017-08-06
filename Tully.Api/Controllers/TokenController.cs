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
using Tully.Api.Models.User;
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
            var usuario = await _userManager.FindByNameAsync(model.Usuario);

            if (usuario == null) return Unauthorized();

            if (_hasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) != PasswordVerificationResult.Success) return Unauthorized();

            var usuarioClaims = await _userManager.GetClaimsAsync(usuario);

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(usuarioClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
        }
    }
}
