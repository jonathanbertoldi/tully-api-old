using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Repositories.Contracts;
using Tully.Api.ViewModels.NotificacaoViewModels;

namespace Tully.Api.Controllers
{
  [Authorize]
  [Route("api")]
  public class NotificacaoController : Controller
  {
    private IUsuarioRepository _usuarioRepository;
    private INotificacaoRepository _notificacaoRepository;

    public NotificacaoController(IUsuarioRepository usuarioRepository, INotificacaoRepository notificacaoRepository)
    {
      _usuarioRepository = usuarioRepository;
      _notificacaoRepository = notificacaoRepository;
    }

    [HttpGet("notificacoes/{notificacaoId}")]
    public async Task<IActionResult> GetNotificacao(int notificacaoId)
    {
      var notificacao = await _notificacaoRepository.GetNotificacao(notificacaoId);

      if (notificacao == null) return NotFound();

      var result = Mapper.Map<NotificacaoViewModel>(notificacao);

      return Ok(result);
    }

    [HttpGet("usuarios/{usuarioId}/notificacoes")]
    public async Task<IActionResult> GetUsuarioNotificacoes(int usuarioId)
    {
      var usuario = await _usuarioRepository.GetUsuario(usuarioId);

      if (usuario == null) return NotFound();

      var notificacoes = await _notificacaoRepository.GetUsuarioNotificacoes(usuarioId);

      var result = Mapper.Map<IEnumerable<NotificacaoViewModel>>(notificacoes);

      return Ok(result);
    }
  }
}
