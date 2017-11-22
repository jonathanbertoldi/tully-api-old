using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Repositories.Contracts;
using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.Controllers
{
  [Authorize]
  [Route("api/ranking")]
  public class RankingController : Controller
  {
    private IUsuarioRepository _usuarioRepository;

    public RankingController(IUsuarioRepository usuarioRepository)
    {
      _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetRankingGeral()
    {
      var usuarios = await _usuarioRepository.GetRankingGeral();

      var result = Mapper.Map<IEnumerable<UsuarioViewModel>>(usuarios);

      return Ok(result);
    }
  }
}
