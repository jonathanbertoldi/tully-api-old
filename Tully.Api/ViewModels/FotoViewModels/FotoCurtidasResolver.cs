using AutoMapper;
using System.Linq;
using Tully.Api.Models;
using Tully.Api.Models.Enums;

namespace Tully.Api.ViewModels.FotoViewModels
{
  public class FotoCurtidasResolver : IValueResolver<Foto, FotoViewModel, int>
  {
    public int Resolve(Foto source, FotoViewModel destination, int destMember, ResolutionContext context) => 
      source.Avaliacoes
        .Where(a => a.Tipo == TipoAvaliacao.Positivo)
        .Count();
  }
}
