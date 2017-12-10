using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Filters;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;
using Tully.Api.Utils;
using Tully.Api.ViewModels;
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
    private IMapper _mapper;

    public AvaliacaoController(
      IRepository repository, 
      IAvaliacaoRepository avaliacaoRepository,
      IFotoRepository fotoRepository,
      IMapper mapper)
    {
      _repository = repository;
      _avaliacaoRepository = avaliacaoRepository;
      _fotoRepository = fotoRepository;
      _mapper = mapper;
    }

    [HttpGet("avaliacoes/{avaliacaoId}", Name = "GetAvaliacao")]
    public async Task<IActionResult> GetAvaliacao(int avaliacaoId)
    {
      var avaliacao = await _avaliacaoRepository.GetAvaliacao(avaliacaoId);

      if (avaliacao == null) return NotFound();

      var result = _mapper.Map<AvaliacaoViewModel>(avaliacao);

      return Ok(result);
    }

    [HttpGet("fotos/{fotoId}/avaliacoes")]
    public async Task<IActionResult> GetFotoAvaliacoes(int fotoId)
    {
      var foto = await _fotoRepository.GetFoto(fotoId);

      if (foto == null) return NotFound();

      var avaliacoes = await _avaliacaoRepository.GetFotoAvaliacoes(fotoId);

      var result = _mapper.Map<IEnumerable<AvaliacaoViewModel>>(avaliacoes);

      return Ok(result);
    }

    [ValidateModel]
    [HttpPost("fotos/{fotoId}/avaliacoes")]
    public async Task<IActionResult> PostAvaliacao(int fotoId, [FromBody] AvaliacaoPostViewModel model)
    {
      var loggedUserId = HttpContext.GetLoggedUserId();

      if (!CheckId(loggedUserId, model.UsuarioId.Value) || !CheckId(fotoId, model.FotoId.Value))
        return BadRequest(new MessageViewModel("URL and Model ids do not match"));

      var foto = await _fotoRepository.GetFoto(fotoId);

      if (foto == null) return NotFound();

      var check = foto.Avaliacoes
        .Where(a => !a.RemovidoEm.HasValue)
        .FirstOrDefault(a => a.UsuarioId == loggedUserId);

      if (check != null)
        return BadRequest(new MessageViewModel("User already liked or disliked this photo"));

      var avaliacao = _mapper.Map<Avaliacao>(model);

      await _repository.Add(avaliacao);
      await _repository.SaveAllAsync();

      avaliacao = await _avaliacaoRepository.GetAvaliacao(avaliacao.Id);

      var result = _mapper.Map<AvaliacaoViewModel>(avaliacao);

      return CreatedAtRoute("GetAvaliacao", new { avaliacaoId = avaliacao.Id }, result);
    }

    [ValidateModel]
    [HttpPatch("avaliacoes/{avaliacaoId}")]
    public async Task<IActionResult> PatchAvaliacao(int avaliacaoId, [FromBody] AvaliacaoPatchViewModel model)
    {
      var avaliacao = await _avaliacaoRepository.GetAvaliacao(avaliacaoId);

      if (avaliacao == null) return NotFound();

      avaliacao.Tipo = model.Tipo;

      await _repository.SaveAllAsync();

      return NoContent();
    }

    [HttpDelete("avaliacoes/{avaliacaoId}")]
    public async Task<IActionResult> DeleteAvaliacao(int avaliacaoId)
    {
      var avaliacao = await _avaliacaoRepository.GetAvaliacao(avaliacaoId);

      if (avaliacao == null) return NotFound();

      avaliacao.RemovidoEm = DateTime.Today;

      await _repository.SaveAllAsync();

      return Ok();
    }

    [NonAction]
    private bool CheckId(int id, int modelId) => id == modelId;
  }
}
