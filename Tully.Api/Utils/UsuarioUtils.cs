using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Tully.Api.Utils
{
  public static class UsuarioUtils
  {
    public static string GetLoggedUserLogin(this HttpContext context)
    {
      return context?.User.Claims.First(a => a.Type == ClaimTypes.Name).Value;
    }

    public static int GetLoggedUserId(this HttpContext context)
    {
      var id = context?.User.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value;
      return Convert.ToInt32(id);
    }
  }
}
