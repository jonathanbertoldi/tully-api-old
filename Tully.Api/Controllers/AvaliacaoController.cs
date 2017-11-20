using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tully.Api.Repositories.Contracts;
using Tully.Api.ViewModels.AvaliacaoViewModels;

namespace Tully.Api.Controllers
{
  [Authorize]
  [Route("api")]
  public class AvaliacaoController : Controller
  {
    private IRepository _repository;
    private IAvaliacaoRepository _avaliacaoRepository;
    private IFotoRepository _fotoRepository;

    public AvaliacaoController(
      IRepository repository, 
      IAvaliacaoRepository avaliacaoRepository,
      IFotoRepository fotoRepository)
    {
      _repository = repository;
      _avaliacaoRepository = avaliacaoRepository;
      _fotoRepository = fotoRepository;
    }

    [HttpGet("avaliacoes/{avaliacaoId}", Name = "GetAvaliacao")]
    public async Task<IActionResult> GetAvaliacao(int avaliacaoId)
    {
      var avaliacao = await _avaliacaoRepository.GetAvaliacao(avaliacaoId);

      if (avaliacao == null) return NotFound();

      return Ok();
    }

    [HttpPost("fotos/{fotoId}/avaliacoes")]
    public async Task<IActionResult> PostAvaliacao(int fotoId, AvaliacaoPostViewModel model)
    {
      var foto = await _fotoRepository.GetFoto(fotoId);

      if (foto == null) return NotFound();

      return CreatedAtRoute("GetAvaliacao", new { avaliacaoId = 1 }, new { });
    }
  }
}
