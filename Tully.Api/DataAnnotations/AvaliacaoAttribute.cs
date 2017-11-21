using System.ComponentModel.DataAnnotations;
using Tully.Api.Models.Enums;

namespace Tully.Api.DataAnnotations
{
  public class AvaliacaoAttribute : ValidationAttribute
  {
    public AvaliacaoAttribute()
    {
      ErrorMessage = "Enter with a valid review type";
    }

    public override bool IsValid(object value)
    {
      if (value is string avaliacao)
      {
        if (string.IsNullOrEmpty(avaliacao))
          return false;
          
        if (TipoAvaliacao.GetTipoAvaliacoes().Contains(avaliacao))
          return true;
      }

      return false;
    }
  }
}
