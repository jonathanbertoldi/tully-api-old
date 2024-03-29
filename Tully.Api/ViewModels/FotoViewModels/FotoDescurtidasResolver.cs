﻿using AutoMapper;
using System.Linq;
using Tully.Api.Models;
using Tully.Api.Models.Enums;

namespace Tully.Api.ViewModels.FotoViewModels
{
  public class FotoDescurtidasResolver : IValueResolver<Foto, FotoViewModel, int>
  {
    public int Resolve(Foto source, FotoViewModel destination, int destMember, ResolutionContext context) =>
      source.Avaliacoes
        .Where(a => a.Tipo == TipoAvaliacao.Negativo)
        .Where(a => !a.RemovidoEm.HasValue)
        .Count();
  }
}
