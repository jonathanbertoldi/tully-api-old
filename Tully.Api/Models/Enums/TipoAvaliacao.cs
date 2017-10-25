using System.Collections.Generic;

namespace Tully.Api.Models.Enums
{
  public class TipoAvaliacao
  {
    public static readonly string Positivo = "Positivo";
    public static readonly string Negativo = "Negativo";

    public static List<string> GetTipoAvaliacoes() => new List<string> { Positivo, Negativo };
  }
}
