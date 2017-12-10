using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Models;
using Tully.Api.Repositories.Contracts;
using Tully.Api.Utils;
using Tully.Api.ViewModels.AvaliacaoViewModels;

namespace Tully.Api.ViewModels.FotoViewModels
{
  public class FotoAvaliacaoUsuarioLogadoResolver : IValueResolver<Foto, FotoViewModel, AvaliacaoSimplesViewModel>
  {
    private IHttpContextAccessor _httpContextAccessor;
    private IUsuarioRepository _usuarioRepository;
    private IMapper _mapper;

    public FotoAvaliacaoUsuarioLogadoResolver(
      IHttpContextAccessor httpContextAccessor, 
      IUsuarioRepository usuarioRepository,
      IMapper mapper)
    {
      _httpContextAccessor = httpContextAccessor;
      _usuarioRepository = usuarioRepository;
      _mapper = mapper;
    }

    public AvaliacaoSimplesViewModel Resolve(Foto source, FotoViewModel destination, AvaliacaoSimplesViewModel destMember, ResolutionContext context)
    {
      var usuarioId = _httpContextAccessor.HttpContext.GetLoggedUserId();

      var usuario = Task.Run(async () => await _usuarioRepository.GetUsuario(usuarioId)).Result;

      if (usuario == null) return null;

      var avaliacao = source.Avaliacoes
        .Where(a => !a.RemovidoEm.HasValue)
        .FirstOrDefault(a => a.UsuarioId == usuarioId);

      if (avaliacao == null) return null;

      var result = _mapper.Map<AvaliacaoSimplesViewModel>(avaliacao);

      return result;
    }
  }
}
