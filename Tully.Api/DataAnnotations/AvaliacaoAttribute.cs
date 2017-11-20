using System.ComponentModel.DataAnnotations;
using Tully.Api.Models.Enums;

namespace Tully.Api.DataAnnotations
{
  public class AvaliacaoAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      if (value is string avaliacao)
        if (TipoAvaliacao.GetTipoAvaliacoes().Contains(avaliacao))
          return true;

      return false;
    }
  }
}
