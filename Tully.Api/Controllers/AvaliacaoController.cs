using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

      var result = Mapper.Map<AvaliacaoViewModel>(avaliacao);

      return Ok(result);
    }

    [HttpGet("fotos/{fotoId}/avaliacoes")]
    public async Task<IActionResult> GetFotoAvaliacoes(int fotoId)
    {
      var foto = await _fotoRepository.GetFoto(fotoId);

      if (foto == null) return NotFound();

      var avaliacoes = await _avaliacaoRepository.GetFotoAvaliacoes(fotoId);

      var result = Mapper.Map<IEnumerable<AvaliacaoViewModel>>(avaliacoes);

      return Ok(result);
    }

    [ValidateModel]
    [HttpPost("fotos/{fotoId}/avaliacoes")]
    public async Task<IActionResult> PostAvaliacao(int fotoId, [FromBody] AvaliacaoPostViewModel model)
    {
      if (!CheckId(HttpContext.GetLoggedUserId(), model.UsuarioId.Value) || !CheckId(fotoId, model.FotoId.Value))
        return BadRequest(new MessageViewModel("URL and Model ids do not match"));

      var foto = await _fotoRepository.GetFoto(fotoId);

      if (foto == null) return NotFound();

      var avaliacao = Mapper.Map<Avaliacao>(model);

      await _repository.Add(avaliacao);
      await _repository.SaveAllAsync();

      avaliacao = await _avaliacaoRepository.GetAvaliacao(avaliacao.Id);

      var result = Mapper.Map<AvaliacaoViewModel>(avaliacao);

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

      return NoContent();
    }

    [NonAction]
    private bool CheckId(int id, int modelId) => id == modelId;
  }
}
