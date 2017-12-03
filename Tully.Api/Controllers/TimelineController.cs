using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tully.Api.Repositories.Contracts;
using Tully.Api.ViewModels.FotoViewModels;

namespace Tully.Api.Controllers
{
  [Authorize]
  [Route("api/usuarios")]
  public class TimelineController : Controller
  {
    private ITimelineRepository _timelineRepository;
    private IUsuarioRepository _usuarioRepository;
    private IMapper _mapper;

    public TimelineController(
      ITimelineRepository timelineRepository, 
      IUsuarioRepository usuarioRepository,
      IMapper mapper)
    {
      _timelineRepository = timelineRepository;
      _usuarioRepository = usuarioRepository;
      _mapper = mapper;
    }

    [Route("{usuarioId}/timeline")]
    public async Task<IActionResult> GetUsuarioTimeline(int usuarioId)
    {
      var usuario = await _usuarioRepository.GetUsuario(usuarioId);

      if (usuario == null) return NotFound();

      var timeline = await _timelineRepository.GetUsuarioTimeline(usuarioId);

      var result = _mapper.Map<IEnumerable<FotoViewModel>>(timeline);

      return Ok(result);
    }
  }
}
