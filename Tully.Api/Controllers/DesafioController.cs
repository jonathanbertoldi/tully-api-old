using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Filters;
using Tully.Api.Models;
using Tully.Api.ViewModels.DesafioViewModels;
using Tully.Api.Repositories.Contracts;
using Tully.Api.Utils;

namespace Tully.Api.Controllers
{
  [Authorize]
  [Route("api/desafios")]
  public class DesafioController : Controller
  {
    private TullyContext _context;
    private IDesafioRepository _desafioRepository;
    private IUsuarioRepository _usuarioRepository;
    private IFotoRepository _fotoRepository;

    public DesafioController(TullyContext context, 
                             IDesafioRepository desafioRepository, 
                             IUsuarioRepository usuarioRepository,
                             IFotoRepository fotoRepository)
    {
      _context = context;
      _desafioRepository = desafioRepository;
      _usuarioRepository = usuarioRepository;
      _fotoRepository = fotoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetDesafios()
    {
      var desafios = await _desafioRepository.GetDesafios();

      var result = Mapper.Map<ICollection<DesafioViewModel>>(desafios);

      var usuarioId = HttpContext.GetLoggedUserId();
      var usuario = await _usuarioRepository.GetUsuario(usuarioId);

      if (usuario != null)
      {
        var usuarioFotos = await _fotoRepository.GetUsuarioFotos(usuarioId);

        foreach (var foto in usuarioFotos)
          foreach(var desafio in result)
            if (desafio.Id == foto.DesafioId)
              desafio.Realizado = true;
      }

      return Ok(result);
    }

    [HttpGet("{desafioId}", Name = "GetDesafio")]
    public async Task<IActionResult> GetDesafio(int desafioId)
    {
      var desafio = await _desafioRepository.GetDesafio(desafioId);

      if (desafio == null) return NotFound();

      var result = Mapper.Map<DesafioViewModel>(desafio);

      return Ok(result);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> PostDesafio([FromBody] DesafioPostViewModel model)
    {
      var desafio = Mapper.Map<Desafio>(model);

      await _context.Desafios.AddAsync(desafio);
      await _context.SaveChangesAsync();

      var result = Mapper.Map<DesafioViewModel>(desafio);

      return CreatedAtRoute("GetDesafio", new { desafioId = desafio.Id }, result);
    }

    [ValidateModel]
    [HttpPatch("{desafioId}")]
    public async Task<IActionResult> PatchDesafio(int desafioId, [FromBody] JsonPatchDocument<DesafioUpdateViewModel> patchDocument)
    {
      if (patchDocument == null) return BadRequest();

      var desafio = await _context.Desafios.FindAsync(desafioId);

      if (desafio == null) return NotFound();

      return Ok();
    }

    [HttpDelete("{desafioId}")]
    public async Task<IActionResult> DeleteDesafio(int desafioId)
    {
      var desafio = await _context.Desafios.FindAsync(desafioId);

      if (desafio == null) return NotFound();

      desafio.RemovidoEm = DateTime.Now;

      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
