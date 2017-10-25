using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;
using Tully.Api.ViewModels;
using Tully.Api.ViewModels.RelacionamentoViewModels;
using Tully.Api.ViewModels.UsuarioViewModels;

namespace Tully.Api.Controllers
{
  [Authorize]
  [Route("api")]
  public class RelacionamentoController : Controller
  {
    private IRepository _repository;
    private IRelacionamentoRepository _relacionamentoRepository;
    private IUsuarioRepository _usuarioRepository;

    public RelacionamentoController(
      IRepository repository,
      IRelacionamentoRepository relacionamentoRepository,
      IUsuarioRepository usuarioRepository)
    {
      _repository = repository;
      _relacionamentoRepository = relacionamentoRepository;
      _usuarioRepository = usuarioRepository;
    }

    [HttpGet("usuarios/{usuarioId}/seguidores")]
    public async Task<IActionResult> GetSeguidores(int usuarioId)
    {
      var seguidores = await _relacionamentoRepository.GetSeguidores(usuarioId);

      if (seguidores == null) return NotFound();

      var result = Mapper.Map<IEnumerable<UsuarioViewModel>>(seguidores);

      return Ok(result);
    }

    [HttpGet("usuarios/{usuarioId}/seguindo")]
    public async Task<IActionResult> GetSeguindo(int usuarioId)
    {
      var seguindo = await _relacionamentoRepository.GetSeguindo(usuarioId);

      if (seguindo == null) return NotFound();

      var result = Mapper.Map<IEnumerable<UsuarioViewModel>>(seguindo);

      return Ok(result);
    }

    [HttpGet("relacionamentos/{relacionamentoId}", Name = "GetRelacionamento")]
    public async Task<IActionResult> GetRelacionamento(int relacionamentoId)
    {
      var relacionamento = await _relacionamentoRepository.GetRelacionamento(relacionamentoId);

      if (relacionamento == null) return NotFound();

      var result = Mapper.Map<RelacionamentoViewModel>(relacionamento);

      return Ok(result);
    }

    [HttpGet("relacionamentos")]
    public async Task<IActionResult> GetRelacionamentoByUsers(int usuarioId, int segueId)
    {
      var relacionamento = await _relacionamentoRepository.GetRelacionamentoPorUsuario(usuarioId, segueId);

      if (relacionamento == null) return NotFound();

      var result = Mapper.Map<RelacionamentoViewModel>(relacionamento);

      return Ok(result);
    }

    [HttpPost("usuarios/{usuarioId}/seguindo")]
    public async Task<IActionResult> PostSeguir(int usuarioId, [FromBody] RelacionamentoPostViewModel model)
    {
      if (!CheckUserId(usuarioId, model.UsuarioId))
        return BadRequest(new MessageViewModel("URL and Model ids do not match"));

      var usuario = await _usuarioRepository.GetUsuario(usuarioId);
      if (usuario == null) return NotFound();

      var seguido = await _usuarioRepository.GetUsuario(model.SeguidoId);
      if (seguido == null) return BadRequest(new MessageViewModel("Invalid user"));

      var check = await _relacionamentoRepository.GetRelacionamentoPorUsuario(model.UsuarioId, model.SeguidoId);
      if (check != null) return BadRequest(new MessageViewModel("User already follows the target"));

      var relacionamento = Mapper.Map<Relacionamento>(model);

      await _repository.Add(relacionamento);
      await _repository.SaveAllAsync();

      var result = Mapper.Map<RelacionamentoViewModel>(relacionamento);
      
      return CreatedAtRoute("GetRelacionamento", new { relacionamentoId = relacionamento.Id }, result);
    }

    [HttpDelete("usuarios/{usuarioId}/seguindo/{seguidoId}")]
    public async Task<IActionResult> DeleteSeguir(int usuarioId, int seguidoId)
    {
      var relacionamento = await _relacionamentoRepository.GetRelacionamentoPorUsuario(usuarioId, seguidoId);

      if (relacionamento == null) return NotFound();

      relacionamento.RemovidoEm = DateTime.Now;

      await _repository.SaveAllAsync();

      return NoContent();
    }

    [NonAction]
    private bool CheckUserId(int urlId, int modelId) => urlId == modelId;
  }
}
