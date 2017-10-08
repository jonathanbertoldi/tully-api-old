using Firebase.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tully.Api.Services;

namespace Tully.Api.Controllers
{
  [Route("api/files")]
  public class FileUploadController : Controller
  {
    private const string FirebaseTullyApp = "tullyteste.appspot.com";
    private const string FirebaseFotoPerfilPath = "fotos_perfil";
    private const string FirebaseFotoPath = "fotos";

    private IHostingEnvironment _env;

    public FileUploadController(IHostingEnvironment env)
    {
      _env = env;
    }

    [HttpPost("foto_perfil")]
    public async Task<IActionResult> UploadFotoPerfil(IFormFile image)
    {
      if (image != null && image.Length > 0)
      {
        var fileName = $"{Guid.NewGuid()}.jpg";

        var fileUrl = await FirebaseStorageService
          .UploadFile(FirebaseTullyApp, FirebaseFotoPerfilPath, fileName, image.OpenReadStream());

        return Ok(new { fileUrl = fileUrl });
      }

      return BadRequest();
    }

    [HttpPost("fotos")]
    public async Task<IActionResult> UploadFoto(IFormFile image)
    {
      if (image != null && image.Length > 0)
      {
        var fileName = $"{Guid.NewGuid()}.jpg";

        var fileUrl = await FirebaseStorageService
          .UploadFile(FirebaseTullyApp, FirebaseFotoPath, fileName, image.OpenReadStream());

        return Ok(new { fileUrl = fileUrl });
      }

      return BadRequest();
    }
  }
}
