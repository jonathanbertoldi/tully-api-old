using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;
using Tully.Api.ViewModels;
using Tully.Api.ViewModels.FotoViewModels;

namespace Tully.Api.Controllers
{
  [Authorize]
  [Route("api")]
  public class FotoController : Controller
  {
    private IRepository _repository;
    private IUsuarioRepository _usuarioRepository;
    private IFotoRepository _fotoRepository;
    private IDesafioRepository _desafioRepository;

    public FotoController(
      IRepository repository,
      IUsuarioRepository usuarioRepository,
      IFotoRepository fotoRepository,
      IDesafioRepository desafioRepository)
    {
      _repository = repository;
      _usuarioRepository = usuarioRepository;
      _fotoRepository = fotoRepository;
      _desafioRepository = desafioRepository;
    }

    [HttpGet("fotos/{fotoId}", Name = "GetFoto")]
    public async Task<IActionResult> GetFoto(int fotoId)
    {
      var foto = await _fotoRepository.GetFoto(fotoId);

      if (foto == null) return NotFound();

      var result = Mapper.Map<FotoViewModel>(foto);

      return Ok(result);
    }

    [HttpGet("usuarios/{usuarioId}/fotos")]
    public async Task<IActionResult> GetUsuarioFotos(int usuarioId)
    {
      var usuario = await _usuarioRepository.GetUsuario(usuarioId);

      if (usuario == null) return NotFound();

      var fotos = await _fotoRepository.GetUsuarioFotos(usuarioId);

      var result = Mapper.Map<IEnumerable<FotoViewModel>>(fotos);

      return Ok(result);
    }

    [HttpPost("usuarios/{usuarioId}/fotos")]
    public async Task<IActionResult> PostUsuarioFoto(int usuarioId, [FromBody] FotoPostViewModel model)
    {
      if (!CheckUserId(usuarioId, model.UsuarioId.Value))
        return BadRequest(new MessageViewModel("URL and Model ids do not match"));

      var usuario = await _usuarioRepository.GetUsuario(model.UsuarioId.Value);
      if (usuario == null) return NotFound();

      var desafio = await _desafioRepository.GetDesafio(model.DesafioId.Value);
      if (desafio == null) return NotFound();

      var usuarioFotos = await _fotoRepository.GetUsuarioFotos(usuarioId);
      if (!usuarioFotos.Any(a => a.DesafioId == desafio.Id))
        usuario.Experiencia += 10;

      var foto = Mapper.Map<Foto>(model);

      await _repository.Add(foto);
      await _repository.SaveAllAsync();

      var result = Mapper.Map<FotoViewModel>(foto);

      return CreatedAtRoute("GetFoto", new { fotoId = foto.Id }, result);
    }

    [NonAction]
    private bool CheckUserId(int urlId, int modelId) => urlId == modelId;
  }
}
